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
	int start, row, col;

	cin >> start;
	if (start == -1) {
		infile >> row >> col;
		cout << row << endl;
		cout << col << endl;
	}
	while (1)
	{
		cin >> row >> col;
		infile >> row >> col;
		cout << row << endl;
		cout << col << endl;
	}
}