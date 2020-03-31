#include <iostream>
#include<string>
#include <fstream>
#include "ReadAndWrite.h"


using namespace std;

void main()
{
	ifstream infile;
	infile.open("Matrix.txt");
	if (infile.fail())
		cout << "Khong mo dc file";
	while (1)
	{
		char* data;
		data = ReadData();
		if (strcmp(data, "-2,-2,") == 0)
			continue;
		puts(data);
		infile >> data;
		//cin.getline(data, 1024);
		WriteData(data);
		cout << endl;
	}
}