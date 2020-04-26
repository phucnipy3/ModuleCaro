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
		cin >> data;
		if (data._Equal(PlaySecondMessage))
			continue;
		infile >> data;
		cout << data << endl;
	}
}