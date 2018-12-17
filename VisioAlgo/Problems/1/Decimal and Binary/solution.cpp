#include <iostream>
#include <stack>
using namespace std;
int main()
{
	int number;
	stack<long long> BinaryNumber;
	cin >> number;
	while (number > 0)
	{
		if (number % 2 == 0)
		{
			BinaryNumber.push(0);
		}
		else
		{
			BinaryNumber.push(1);
			number = number - 1;
		}
			number = number / 2;
	}
	while (!BinaryNumber.empty())
	{
		cout << BinaryNumber.top();
		BinaryNumber.pop();
	}
	cout << endl;
	system("pause");
	return 0;
}