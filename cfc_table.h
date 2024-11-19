#pragma once
#include <algorithm>
#include <vector>
#include "cfc_attack.h"

class cfc_table
{
public:
	std::vector<cfc_attack> attacks;

	static cfc_table* parse(uintptr_t address, bool y3);
};