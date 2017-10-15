// ConsoleApplication1.cpp: 定义控制台应用程序的入口点。
//

#include "stdafx.h"
#include "dealsudoku.h"

int main()
{
	matrix_t sudu = {
		{0,0,0,0,0,0,0,1,2},{0,0,3,6,0,0,0,0,0},{0,0,0,0,0,7,0,0,0},{4,1,0,0,2,0,0,0,0},{0,0,0,5,0,0,3,0,0},{7,0,0,0,0,0,6,0,0},{2,8,0,0,0,0,0,4,0},{0,0,0,3,0,0,5,0,0},{0,0,0,0,0,0,0,0,0}
	};
	dealsudoku* D = new dealsudoku();
	D->deal(sudu);
    return 0;
}

