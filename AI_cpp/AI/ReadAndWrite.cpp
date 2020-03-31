#include <iostream>
#include <fstream>
#include <string>

using namespace std;

char oldStr[1024] = "";
char *newStr = new char[1024];

char* ReadData()
{
	delete newStr;
	newStr = new char[1024];
	while(1)
	{
		ifstream infile;
		infile.open("Input.txt");
		if (!infile.fail())
		{
			
			infile >> newStr;
			infile.close();
			if (strcmp(newStr,"")!=0 && strcmp(newStr, oldStr) != 0)
			{
				strncpy_s(oldStr, newStr, 1024);
				return newStr;
			}
		}
	}
}

void WriteData(char* data)
{
	while(1)
	{
		ofstream outfile;
		outfile.open("Output.txt");
		if (!outfile.fail())
		{
			outfile << data;
			outfile.close();
			return;
		}
	}
}



