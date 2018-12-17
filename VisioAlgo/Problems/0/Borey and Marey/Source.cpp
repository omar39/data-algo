#include<iostream>
#include<math.h>
#include<fstream>
#include<string>
#include<algorithm>
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
			int a , b ,c;
			a = rand() % 100000;
			b = rand() % 100000;
			c = rand() % 6;
			if(c == 0)
			{
				c+=1;
			}
			stream << min(a, b) << " " << max(a, b) << " ";
			stream<<c<<" ";
		}
		stream.close();
		cout << "okaaay\n";
	}
	
	return 0;
}*/
int main()
{
	for (int i = 1; i <= 10; i++)
	{
		ifstream stream;
		stream.open("input" + to_string(i) + ".txt");
		ofstream output;
		output.open("output" + to_string(i) + ".txt");
		int j, x, p1, d;
		int p[100001] = { 0 };
		int far[100001] = { 0 };
		p[0] = 1; p[1] = 1;
		for (p1 = 2; p1 <= 100001; p1++)
		{
			if (p[p1] == 0)
			{
				far[p1]++;
				for (x = p1 * 2; x <= 100001; x += p1)
				{
					p[x] = 1;
					far[x]++;
				}
			}
		}
		int t, a, b, k;
		stream >> t;
		while (t--)
		{
			d = 0;
			stream >> a >> b >> k;
			for (x = a; x <= b; x++)
			{
				if (far[x] == k)
					d++;
			}
			output << d << "\n";
		}
		stream.close();
	}
	return 0;
}
