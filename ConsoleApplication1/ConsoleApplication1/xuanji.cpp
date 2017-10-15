#include "stdafx.h"
#include "xuanji.h"



int x;
int y;
int l[9];

xuanji::xuanji()
{

}

void xuanji::set(int a,int b)
{
	this->x = a;
	this->y = b;
	for (int i = 0; i < 9; i++)
	{
		l[i] = 1;
	}
}

bool xuanji::remove(int x)
{
	if (l[x - 1] == 1)
	{
		l[x - 1] = 0;
		return true;
	}
	else return false;
}

void xuanji::add(int x)
{
	l[x - 1] = 1;
}

int xuanji::length()
{
	int len = 0;
	for (int k = 0; k < 9; k++)
	{
		if (l[k] == 1) len++;
	}
	return len;
}

int xuanji::choose(int b)
{
	for (int i = 0; i<9; i++)
	{
		if (l[i] == 1) b--;
		else continue;
		if (b == 0) return i + 1;
	}
}




