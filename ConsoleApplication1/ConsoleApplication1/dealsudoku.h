#pragma once
#include "xuanji.h"
#include <vector>
using matrix_t = std::vector<std::vector<int>>;
class dealsudoku
{
public:
	dealsudoku()
	{

	}

	void deal(matrix_t&);

	void solve(matrix_t & , xuanji [9][9]);


	xuanji findshortest(xuanji kexuan[9][9], matrix_t &);

	bool tianman(matrix_t&);
	
private:

	

	
	
};

