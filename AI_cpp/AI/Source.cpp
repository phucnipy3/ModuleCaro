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
		string data;
		data = ReadData();
		if (data._Equal(PlaySecondMessage))
			continue;
		cout << data;
		infile >> data;
		WriteData(data);
		cout << endl;
	}
}