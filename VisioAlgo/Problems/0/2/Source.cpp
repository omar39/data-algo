#include<vector>
#include<iostream>
#include<fstream>
#include<string>
using namespace std;

int main()
{
	int Cases, Nodes, Edges, Node1, Node2;
	for (int i = 1; i <= 10; i++)
	{
		ifstream MyInputFile("Input" + to_string(i) + ".txt");
		ofstream MyOutputFile("Output" + to_string(i) + ".txt");
		MyInputFile >> Cases;
		for (int i = 0; i < Cases; i++)
		{
			MyInputFile >> Nodes >> Edges;
			vector<vector<int>>List(Nodes);
			for (int j = 0; j < Edges; j++)
			{
				MyInputFile >> Node1 >> Node2;
				List[Node1-1].push_back(Node2);
				List[Node2-1].push_back(Node1);
			}
			for (int s = 1; s <= List.size(); s++)
			{
				MyOutputFile << s;
				for (int a = 0; a < List[s-1].size(); a++)
				{
					MyOutputFile << "-> " << List[s-1][a];
				}
				MyOutputFile << "\n";
			}
			MyOutputFile << "\n";
			List.clear();
		}
	}

	return 0;
}