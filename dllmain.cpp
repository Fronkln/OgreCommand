// dllmain.cpp : Defines the entry point for the DLL application.
#include <stdint.h>
#include <iostream>
#include <fstream>
#include "pch.h"
#include "cfc_table.h"
#include "buffer.h"
#include "utils.h"

intptr_t moduleAddress;

intptr_t* cfcBuffer;
std::vector<char> bufferTest;

typedef const char* (*t_YP_GET_FILE_PATH)(char* path);

bool isY3 = false;

inline std::uint8_t* PatternScan(void* module, const char* signature)
{
	static auto pattern_to_byte = [](const char* pattern) {
		auto bytes = std::vector<int>{};
		auto start = const_cast<char*>(pattern);
		auto end = const_cast<char*>(pattern) + strlen(pattern);

		for (auto current = start; current < end; ++current) {
			if (*current == '?') {
				++current;
				if (*current == '?')
					++current;
				bytes.push_back(-1);
			}
			else {
				bytes.push_back(strtoul(current, &current, 16));
			}
		}
		return bytes;
		};

	auto dosHeader = (PIMAGE_DOS_HEADER)module;
	auto ntHeaders = (PIMAGE_NT_HEADERS)((std::uint8_t*)module + dosHeader->e_lfanew);

	auto sizeOfImage = ntHeaders->OptionalHeader.SizeOfImage;
	auto patternBytes = pattern_to_byte(signature);
	auto scanBytes = reinterpret_cast<std::uint8_t*>(module);

	auto s = patternBytes.size();
	auto d = patternBytes.data();

	for (auto i = 0ul; i < sizeOfImage - s; ++i) {
		bool found = true;
		for (auto j = 0ul; j < s; ++j) {
			if (scanBytes[i + j] != d[j] && d[j] != -1) {
				found = false;
				break;
			}
		}
		if (found) {
			return &scanBytes[i];
		}
	}
	return nullptr;
}

inline intptr_t	ReadOffsetValue2(intptr_t address)
{
	intptr_t srcAddr = (intptr_t)address;
	intptr_t dstAddr = srcAddr + 4 + *(int32_t*)srcAddr;
	return dstAddr;
}

template<typename AT>
inline intptr_t	ReadCall2(AT address)
{
	return ReadOffsetValue2((intptr_t)address + 1);
}

void write_int(uintptr_t addr, int val)
{
	unsigned long OldProtection;
	VirtualProtect((LPVOID)(addr), 4, PAGE_EXECUTE_READWRITE, &OldProtection);

	int* ptr = (int*)addr;
	*ptr = val;

	VirtualProtect((LPVOID)(addr), 4, OldProtection, NULL);
}

inline void write_relative_addr(void* instruction_start, intptr_t target, int instruction_length = 7)
{
	intptr_t instruction_end = (intptr_t)((unsigned long long)instruction_start + instruction_length);
	unsigned int* offset = (unsigned int*)((unsigned long long)instruction_start + (instruction_length - 4));

	int calcOffset = target - instruction_end;
	write_int((intptr_t)offset, calcOffset);
}

inline void* resolve_relative_addr(void* instruction_start, int instruction_length = 7)
{
	void* instruction_end = (void*)((unsigned long long)instruction_start + instruction_length);
	unsigned int* offset = (unsigned int*)((unsigned long long)instruction_start + (instruction_length - 4));

	void* addr = (void*)(((unsigned long long)instruction_start + instruction_length) + *offset);

	return addr;
}


inline void determine_game()
{
	wchar_t exePath[MAX_PATH + 1];
	GetModuleFileNameW(NULL, exePath, MAX_PATH);

	std::wstring wstr(exePath);
	std::string game = basenameBackslashNoExt(string(wstr.begin(), wstr.end()));

	isY3 = game == "Yakuza3";
}

inline intptr_t* get_cfc_table_addr()
{
	void* tableReadCode = nullptr;

	if (isY3)
	{
		tableReadCode = PatternScan(GetModuleHandle(NULL), "48 8B 84 C3 10 40 FB 00 48 39 87 60 1A 00 00");

		if (tableReadCode == nullptr)
			return nullptr;
		else
			return (intptr_t*)(ReadCall2(tableReadCode));
	}
	else
	{
		//GOG pattern. Hope it works for MS Store too!
		tableReadCode = PatternScan(GetModuleHandle(NULL), "48 8B 84 C3 80 FD 66 01 48 39 87 70 19 00 00");

		if (tableReadCode == nullptr)
		{
			//Steam accesses data differently
			return (intptr_t*)(GetModuleHandle(NULL) + 0x016F4D80);
		}
		else
			return (intptr_t*)(ReadCall2(tableReadCode));
	}
}

void load()
{
	auto parless = GetModuleHandle(L"YakuzaParless.asi");

	if (parless == nullptr)
	{
		std::cout << "OgreCommand: Couldn't get module handle to YakuzaParless.asi. Aborting\n";
		return;
	}

	auto funcCall = (t_YP_GET_FILE_PATH)GetProcAddress(parless, "YP_GET_FILE_PATH");

	if (funcCall == nullptr)
	{
		std::cout << "OgreCommand: YP_GET_FILE_PATH export function does not exist. Aborting\n";
		return;
	}
	std::cout << "OgreCommand: Start loading\n";


	std::string str("data/ogre_command.ofc");
	str.reserve(MAX_PATH);

	const char* ofcPath = funcCall((char*)str.c_str());

	std::cout << "OgreCommand: Finding CFC table address\n";
	intptr_t* tableList = get_cfc_table_addr();

	if (tableList == nullptr)
	{
		std::cout << "OgreCommand: Couldn't find the CFC table address. Aborting\n";
		return;
	}

	std::ifstream file(ofcPath, std::ios::binary | std::ios::ate);
	std::streamsize size = file.tellg();

	if (size <= 0)
	{
		std::cout << "OgreCommand: ogre_command.ofc does not exist in any mods. Aborting\n";
		return;
	}

	file.seekg(0, std::ios::beg);

	if (cfcBuffer == nullptr)
	{
		cfcBuffer = (intptr_t*)AllocateBuffer(tableList);
	}

	std::cout << "OgreCommand: Applying OFC file\n";

	uint32_t diff = (int64_t)cfcBuffer - moduleAddress;

	if (isY3)
	{
		write_relative_addr((void*)(moduleAddress + 0x427C44), (intptr_t)cfcBuffer, 7);
		write_int(moduleAddress + 0x420983 + 4, diff);
		write_int(moduleAddress + 0x4209A0 + 4, diff);
	}
	else
	{
		write_relative_addr((void*)(moduleAddress + 0x5FBEF2), (intptr_t)cfcBuffer, 7);
		write_int(moduleAddress + 0x5FAFD7 + 4, diff);
		write_int(moduleAddress + 0x5FB21C + 4, diff);
		write_int(moduleAddress + 0x5FB245 + 4, diff);
	}

	bufferTest = std::vector<char>(size);

	if (file.read(bufferTest.data(), size))
	{
		uintptr_t ofcDat = (uintptr_t)bufferTest.data();
		uintptr_t currentOffset = ofcDat;

		int ver = *(int*)currentOffset;
		int numTables = *(int*)(currentOffset + 4);
		currentOffset += 8;

		cfcBuffer[0] = 0;

		for (int i = 1; i < numTables; i++)
		{
			int size = *(int*)currentOffset;
			currentOffset += 36;

			cfcBuffer[i] = currentOffset;

			currentOffset += size;
		}

		//Dummy
		for (int i = numTables; i < numTables + 32; i++)
		{
			cfcBuffer[i] = cfcBuffer[7];
		}
	}

	std::cout << "OgreCommand: Initialization complete\n";

}

DWORD WINAPI CommandThread(HMODULE hModule)
{
	while (true)
	{
		if ((GetAsyncKeyState(VK_LSHIFT) & 0x8000) == 0x8000 && GetAsyncKeyState('U') == -32767)
		{
			Sleep(100);
			load();
		}
	}

	FreeLibraryAndExitThread(hModule, 0);

	return 0;
}


BOOL APIENTRY DllMain(HMODULE hModule,
	DWORD  ul_reason_for_call,
	LPVOID lpReserved
)
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
	{
		moduleAddress = (intptr_t)GetModuleHandle(NULL);

		//FILE* f;
		//freopen_s(&f, "CONOUT$", "w", stdout);

		std::cout << "OgreCommand: Start\n";

		Sleep(5000);
		determine_game();
		load();

		CloseHandle(CreateThread(nullptr, 0, (LPTHREAD_START_ROUTINE)CommandThread, hModule, 0, nullptr));

		return TRUE;
	}

	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}

