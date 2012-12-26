#include <time.h>
#include "mapalgo.h"

using namespace std;

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
	int rand, bonus;
	vector<vector<square>> smallMap;
	
	// Initialize random seed
	srand(time(NULL));

	// Set up sizes. (height x width)
	smallMap.resize(height);
	for (int i = 0; i < height; ++i)
		smallMap[i].resize(width);

	// Free random on the first square
	rand = rand() % 3 + 1;
	smallMap[0][0].type = rand;
	bonus = rand() % 3 + 1;
	smallMap[0][0].bonus = bonus;

	for(int i = 0; i<25; i++)
	{
		for(int j = 0; j<25; j++)
		{
			
			rand = rand() % 3 + 1;
			smallMap[i][j].type;
		}
	}

}