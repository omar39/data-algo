#include <iostream>
#include <queue>
using namespace std;
int main()
{
	int number_of_cards;
	queue<int> cards;
	cin >> number_of_cards;
	int *arr = new int[number_of_cards - 1];
	int remaining_card;
	int toBeDiscarded;
	int toBeAtTheBack;
	for (int i = 1; i <= number_of_cards; i++)
	{
		cards.push(i);
	}
	
		for (int i = 0; i < number_of_cards - 1; i++)
		{
			toBeDiscarded = cards.front();
			arr[i] = toBeDiscarded;
			cards.pop();
			toBeAtTheBack = cards.front();
			cards.push(toBeAtTheBack);
			cards.pop();

		}
		remaining_card = cards.front();
		for (int i = 0; i < number_of_cards - 1; i++)
		{
			cout << arr[i] << " ";
		}
		cout << endl;
		cout << remaining_card << endl;





		system("pause");
	return 0;
}