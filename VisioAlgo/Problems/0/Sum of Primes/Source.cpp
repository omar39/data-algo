#include <iostream>
#include<fstream>
#include<string>
using namespace std;
/*
int main()
{
	for (int i = 1; i <= 10; ++i)
	{
		ofstream stream;
		stream.open("input" + to_string(i) + ".txt");
		int x = rand() % 10000;
		stream << x << "\n";
		for (int i = 0; i < x; ++i)
		{
			int a = rand() % 10000;
			stream << a << " ";
		}
		stream.close();
		cout << "okaaay\n";
	}

	return 0;
}*/

bool checkPrime(int n) 
{
	{
		int i;
		bool isPrime = true;

		for (i = 2; i <= n / 2; ++i)
		{
			if (n % i == 0)
			{
				isPrime = false;
				break; 
			}
		}

		return isPrime;
	}
}

int main()
{
	for (int z = 1; z <= 10; ++z)
	{
		ifstream stream;
		stream.open("input" + to_string(z) + ".txt");
		ofstream output;
		output.open("output" + to_string(z) + ".txt");
		int n, i, x, count = 0;
		bool flag = false;
		stream >> x;
		for (int j = 1; j <= x; j++)
		{
			stream >> n;
			for (i = 2; i <= n / 2; ++i)
			{
				if (checkPrime(i))
				{
					if (checkPrime(n - i))
					{
						flag = true;
						count++;
					}
				}

			}
			output << count << endl;
			count = 0;
		}
		stream.close();
	}
	system("pause");
	return 0;
}
