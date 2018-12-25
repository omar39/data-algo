#include<iostream>
#include<vector>
#include<fstream>
#include<string>
#include<algorithm>
using namespace std;

void DFS(int s,vector<vector<int>>Graph,vector<bool> &Visited) {
	Visited[s] = true;
	for (int i = 0; i < Graph[s].size(); i++) {
		if (Visited[Graph[s][i]-1] == false)
			DFS(Graph[s][i]-1,Graph,Visited);
	}
}


int main()
{
	int Cases, Nodes, Edges, Node1, Node2,Components = 0;
	vector<int>Total;
	for (int a = 1; a <= 10; a++)
	{
		ifstream InputFile("input" + to_string(a) + ".txt");
		ofstream OutputFile("output" + to_string(a) + ".txt");
		InputFile >> Cases;
		for (int s = 0; s < Cases; s++)
		{
			InputFile >> Nodes >> Edges;
			vector<vector<int>>Graph(Nodes);
			vector<bool>Visited(Nodes);
			fill(Visited.begin(), Visited.end(), 0);
			for (int i = 0; i < Edges; i++) {
				InputFile >> Node1 >> Node2;
				Graph[Node1 - 1].push_back(Node2);
				Graph[Node2 - 1].push_back(Node1);
			}

			for (int i = 0; i < Nodes; i++) {
				if (Visited[i] == false) {
					DFS(i, Graph, Visited);
					Components++;
				}
			}
			Total.push_back(Components);
			Components = 0;
		}
		sort(Total.begin(), Total.end());
		OutputFile << Total.back();
		Total.clear();
	}
	return 0;
}