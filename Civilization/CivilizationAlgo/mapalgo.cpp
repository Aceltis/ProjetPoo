#include "mapalgo.h"

int Algo::computeFoo() {
	return 1;
}

Algo* Algo_new() { return new Algo(); }
void Algo_delete(Algo* algo) { delete algo; }
int Algo_computeAlgo(Algo* algo) { return algo->computeFoo(); }

/*
 * createSmallMap()
 * Create a new 25x25 map
 * return vector<vector<square>>
 */
vector<vector<square>> createSmallMap() {
	int height, width = 25;
	int random, bonus;
	vector<vector<square>> smallMap;
	

	// Initialize random seed
	srand(time(NULL));

	// Set up sizes. (height x width)
	smallMap.resize(height);
	for (int i = 0; i < height; ++i)
		smallMap[i].resize(width);

	// Free random on the first square
	random = rand() % 3 + 1;
	smallMap[0][0].type = random;
	bonus = rand() % 3 + 1;
	smallMap[0][0].bonus = bonus;

	for(int i = 0; i<25; i++)
	{
		for(int j = 0; j<25; j++)
		{
			
			random = rand() % 3 + 1;
			smallMap[i][j].type;
		}
	}
	return smallMap;
}