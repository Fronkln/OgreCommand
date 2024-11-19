#include "cfc_table.h"

cfc_table* cfc_table::parse(uintptr_t tableAddr, bool y3)
{
	cfc_table* table = new cfc_table();

	while (true)
	{
		cfc_attack attk = *((cfc_attack*)tableAddr);
		table->attacks.push_back(attk);

		tableAddr += (y3 ? 16 : 20);

		//Table end
		if (*(int*)tableAddr == 0x26)
			break;
	}

	return table;
;}