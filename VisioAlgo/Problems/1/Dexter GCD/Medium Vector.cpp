#include <iostream>
#include <vector>
using namespace std;
int main()
{
	int num1, num2;
	cin >> num1 >> num2;
	vector<int> v1;
	vector<int> v2;
	vector<int> v3;
	for (int i = 1; i <= num1; i++)
	{
		if (num1%i == 0)
		{
			v1.push_back(i);
		}
	}
	
	for (int i = 1; i <= num2; i++)
	{ 
		if (num2%i == 0)
		{
			v2.push_back(i);
		}
	}
	for (int i = 0; i < v1.size(); i++)
	{
		for (int j = 0; j < v2.size(); j++)
		{
			if (v1[i] == v2[j])
			{
				v3.push_back(v1[i]);
			}
		}
	}
	if (v3.empty())
	{
		cout << 1 << endl;
	}
	else
		cout << v3[v3.size() - 1] << endl;
	
		system("pause");
	return 0;
}