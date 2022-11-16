// Nguyễn Trọng Thuận - 20120590 - 20CTT5C
#include<iostream>
#include<list>

using namespace std;

class GRAPH
{
private:
	int** graph;
	int size;
	bool* visited; 
public:
	int* path;
	void initGraph(int size)
	{
		this->graph = new int*[size];
		this->visited = new bool[size];
		this->path = new int[size];
		if (this->graph == NULL || this->visited == NULL) return;
		for (int i = 0; i < size; i++)
		{
			this->graph[i] = new int[size];
			if (this->graph[i] == NULL) return;
		}
		this->size = size;
		for (int i = 0; i < size; i++)
		{
			this->visited[i] = false;
			for (int j = 0; j < size; j++)
			{
				this->graph[i][j] = 0;
			}
		}
	}
	bool addEdge(int firstVertex, int secondVertex)
	{
		if (firstVertex >= this->size || secondVertex >= this->size || firstVertex < 0 || secondVertex < 0) return false;
		this->graph[firstVertex][secondVertex] = 1;
		return true;
	}
	void exportAdjacencyMatrix()
	{
		cout << "--- Adjacency Matrix ---" << endl;
		for (int i = 0; i < this->size; i++)
		{
			for (int j = 0; j < this->size; j++)
			{
				cout<< this->graph[i][j]<<"  ";
			}
			cout << endl;
		}
	}
	void DFS(int beginVertex = 0)
	{
		this->visited[beginVertex] = true;
		cout << beginVertex << " ";
		for (int i = 0; i < this->size; i++)
		{
			if (this->graph[beginVertex][i] == 1 && !this->visited[i])
			{
				this->DFS(i);
			}
		}
	}
	void BFS(int beginVertex = 0)
	{
		for (int i = 0; i < this->size; i++)
		{
			this->visited[i] = false;
		}
		list<int> queue;
		this->visited[beginVertex] = 1;
		queue.push_back(beginVertex);

		while (!queue.empty())
		{
			beginVertex = queue.front();
			cout << beginVertex << " ";
			queue.pop_front();

			for (int i = 0; i < this->size; i++)
			{
				if (this->graph[beginVertex][i] == 1 && !this->visited[i])
				{
					this->visited[i] = true;
					queue.push_back(i);
				}
			}
		}
	}

	void printAllPathsUtil(int beginVertex, int desVertex,int& path_index, bool &checking)
	{
		this->visited[beginVertex] = true;
		this->path[path_index] = beginVertex;
		path_index++;

		if (beginVertex == desVertex) 
		{
			for (int i = 0; i < path_index; i++)
			{
				cout << path[i] << " ";
			}
			cout << endl;
			checking = true;
		}
		else
		{
			for (int i = 0; i < this->size; i++)
			{
				if (this->graph[beginVertex][i] == 1 && !this->visited[i])
				{
					printAllPathsUtil(i, desVertex, path_index, checking);
				}
			}
		}
		path_index--;
		visited[beginVertex] = false;
	}
	void printAllPaths(int beginVertex, int desVertex, bool&checking)
	{
		for (int i = 0; i < this->size; i++)
		{
			this->visited[i] = 0;
		}
		int path_index = 0;

		for (int i = 0; i < this->size; i++)
		{
			visited[i] = false;
		}

		printAllPathsUtil(beginVertex, desVertex, path_index, checking);
	}
};

int main()
{
	GRAPH graph;
	int size;
	cout << "Enter the number of vertexs of the graph: ";
	//cin >> size;
	size = 6;
	graph.initGraph(size);
	graph.addEdge(0, 2);
	graph.addEdge(0, 3);
	graph.addEdge(1, 3);
	graph.addEdge(1, 5);
	graph.addEdge(2, 4);
	graph.addEdge(3, 4);
	graph.addEdge(3, 5);

	graph.exportAdjacencyMatrix();

	cout << "DFS: ";
	graph.DFS(0);
	cout << endl;

	cout << "BFS: ";
	graph.BFS(0);
	cout << endl;

	bool hasPath = false;
	graph.printAllPaths(0, 3, hasPath);
	cout << hasPath;
	return 0;
}