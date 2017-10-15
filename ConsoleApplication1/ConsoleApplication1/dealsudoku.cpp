#include "stdafx.h"
#include "dealsudoku.h"
#include "xuanji.h"
#include <vector>
bool M = false;




void dealsudoku::deal(matrix_t & sudu)
{
	//init
	xuanji kexuan[9][9];
	for (int i = 0; i < 9; i++)
	{
		for (int j = 0; j < 9; j++)
		{
			xuanji* xuan = new xuanji();
			xuan->set(i, j);
			kexuan[i][j] = *xuan; //?
			
		}
	}

	for (int i=0;i<9;i++)
	{
		for (int j=0;j<9;j++)
		{
			if (sudu[i][j] != 0)
			{
				int xmin, xmax;
				int ymin, ymax;
				if (i < 3)
				{
					xmin = 0;
					xmax = 3;
				}
				else if (i<6)
				{
					xmin = 3;
					xmax = 6;
				}
				else
				{
					xmin = 6;
					xmax = 9;
				}

				if (j < 3)
				{
					ymin = 0;
					ymax = 3;
				}
				else if (j<6)
				{
					ymin = 3;
					ymax = 6;
				}
				else
				{
					ymin = 6;
					ymax = 9;
				}
				//remove its hang,lie
				for (int temp = 0; temp < 9; temp++)
				{
					kexuan[temp][j].remove(sudu[i][j]);
					kexuan[i][temp].remove(sudu[i][j]);
					
				}
				//remove its 9gongge
				for (int tempx = xmin; tempx < xmax; tempx++)
				{
					for (int tempy = ymin; tempy<ymax; tempy++)
					{
						kexuan[tempx][tempy].remove(sudu[i][j]);
					}
				}
			}
		}
	}
	//slove
	solve(sudu, kexuan);
	//print
	printf("123");
}


void dealsudoku::solve(matrix_t & sudu, xuanji kexuan[9][9])
{
	if (M == true) return;
	M = tianman(sudu);
	xuanji jihe = findshortest(kexuan,sudu);
	int len = jihe.length();
	for (int i = 0; i < len; i++)
	{
		int number = jihe.choose(i+1);
		sudu[jihe.x][jihe.y] = number;
		int list[22];
		for (int p = 0; p < 22; p++)
		{
			list[p] = -1;
		}
		int z=0;
		int xmin, xmax;
		int ymin, ymax;
		if (jihe.x < 3)
		{
			xmin = 0;
			xmax = 3;
		}
		else if (jihe.x<6)
		{
			xmin = 3;
			xmax = 6;
		}
		else
		{
			xmin = 6;
			xmax = 9;
		}

		if (jihe.y < 3)
		{
			ymin = 0;
			ymax = 3;
		}
		else if (jihe.y<6)
		{
			ymin = 3;
			ymax = 6;
		}
		else
		{
			ymin = 6;
			ymax = 9;
		}
		//remove its hang,lie
		for (int temp=0; temp < 9; temp++)
		{
			if (kexuan[temp][jihe.y].remove(number) == true) 
			{
				list[z] = temp * 9 + jihe.y;
				z++;
			}
			if (kexuan[jihe.x][temp].remove(number) == true)
			{
				list[z] = jihe.x * 9 + temp;
				z++;
			}
		}
		//remove its 9gongge
		for (int tempx = xmin; tempx < xmax; tempx++)
		{
			for (int tempy=ymin;tempy<ymax;tempy++)
			{
				if (kexuan[tempx][tempy].remove(number) == true)
				{
					list[z] = tempx * 9 + tempy;
					z++;
				}
			}
		}
		//next solve
		solve(sudu, kexuan);
		//add back
		if (M) return;
		int tempi = 0;
		while (list[tempi]!=-1)
		{
			int xx = list[tempi] / 9;
			int yy = list[tempi] % 9;
			kexuan[xx][yy].add(number);
			tempi++;
		}
	}
	if (M == false) sudu[jihe.x][jihe.y] = 0;
}

xuanji dealsudoku::findshortest(xuanji kexuan[9][9], matrix_t & sudu )
{
	int shortest = 9;
	xuanji p;
	for (int i = 0; i < 9; i++)
	{
		for (int j = 0; j < 9; j++)
		{
			if (sudu[i][j] == 0)
			{
				int temp = kexuan[i][j].length();
				if (temp < shortest)
				{
					shortest = temp;
					p = kexuan[i][j];
				}
				xuanji J[9];
			}
		}
	}
	return p;
}


bool dealsudoku::tianman(matrix_t & sudu)
{
	bool m = true;
	for (int x = 0; x < 9; x++)
	{
		for(int y=0;y<9;y++)
		{
			if (sudu[x][y] == 0) 
			{
				m = false;
				return m;
			}
		}
	}
	return m;
}
