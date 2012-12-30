#include "mapalgo.h"

int Algo::computeFoo() {
	return 1;
}

Algo* Algo_new() { return new Algo(); }
void Algo_delete(Algo* algo) { delete algo; }
int Algo_computeAlgo(Algo* algo) { return algo->computeFoo(); }

vector<vector<square>> createMap(int height,int width) {

	vector<vector<square>> map;

	// Initialize random seed.
	srand(time(NULL));

	// Set up sizes. (height x width)
	map.resize(height);
	for (int i = 0; i < height; ++i)
		map[i].resize(width);

	while(!(map_full(map))){
		for(int i = 0; i<height; i++)
		{
			for(int j = 0; j<width; j++)
			{
				if(!(map[i][j].frozen)) map[i][j].type = rand() %3;
			}
		}

		for(int i = 0; i<height; i++)
		{
			for(int j = 0; j<width; j++)
			{
				// Can the given square be included in a 4 (or more) square group ?
				if(check_four_grps(map, i, j, 0, map[i][j].type)>=4)map[i][j].frozen=1;
			}
		}
	}

	return map;
}

bool map_full(vector<vector<square>> &map){
	for(int i = 0; i<25; i++)
	{
		for(int j = 0; j<25; j++)
		{
			if(!(map[i][j].frozen)) return false;
		}
	}

	if(!(three_types_present(map))) return false;
	return true;
}

int check_four_grps(vector<vector<square>> &map, int i, int j, int compteur, int type){
	compteur++;
	map[i][j].frozen=1;
	if (map[i+1][j].type==type && !(map[i+1][j].frozen))check_four_grps(map, i+1, j, compteur, type);
	if (map[i][j+1].type==type && !(map[i+1][j].frozen))check_four_grps(map, i, j+1, compteur, type);
	if (map[i-1][j].type==type && !(map[i+1][j].frozen))check_four_grps(map, i-1, j, compteur, type);
	if (map[i][j-1].type==type && !(map[i+1][j].frozen))check_four_grps(map, i, j-1, compteur, type);
	
	return compteur;
}

bool three_types_present(vector<vector<square>> &map){
	int types[3] = {0,0,0};
	for(int i = 0; i<25; i++)
	{
		for(int j = 0; j<25; j++)
		{
			types[map[i][j].type]=1;
		}
	}
	if ((types[0]+types[1]+types[2])==3) return true;
	return false;
}