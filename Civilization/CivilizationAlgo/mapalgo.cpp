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
	vector<vector<square>> smallMap;
	

	// Initialize random seed
	srand(time(NULL));

	// Set up sizes. (height x width)
	smallMap.resize(height);
	for (int i = 0; i < height; ++i)
		smallMap[i].resize(width);

	for(int i = 0; i<25; i++)
	{
		for(int j = 0; j<25; j++)
		{
			smallMap[i][j].type = rand() %3;
		}
	}

	while(!(map_full(smallMap))){
		for(int i = 0; i<25; i++)
		{
			for(int j = 0; j<25; j++)
			{
				if(smallMap[i][j].state!=2){
					//Can the given square be included in a 4 (or more) square group ?
					if(check_four_grps(smallMap, i, j, 0, smallMap[i][j].type)>=4)smallMap[i][j].state=2;
					else smallMap[i][j].type = rand() %3;

				}
			}
		}
	}

	return smallMap;
}

bool map_full(vector<vector<square>> &map){
	for(int i = 0; i<25; i++)
	{
		for(int j = 0; j<25; j++)
		{
			if(map[i][j].state!=2) return false;
		}
	}
	//Vérifier que les 3 types de terrain soient bien présents sur la carte
	if(!(three_types_present(map))) return false;
	return true;
}

int check_four_grps(vector<vector<square>> &map, int i, int j, int compteur, int type){
	compteur++;
	map[i][j].state=1;
	if (map[i+1][j].type==type && map[i+1][j].state==2)compteur=4;
	else if (map[i][j+1].type==type && map[i+1][j].state==2)compteur=4;
	else if (map[i-1][j].type==type && map[i+1][j].state==2)compteur=4;
	else if (map[i][j-1].type==type && map[i+1][j].state==2)compteur=4;
	else{
		if (map[i+1][j].type==type && map[i+1][j].state!=1)check_four_grps(map, i+1, j, compteur, type);
		if (map[i][j+1].type==type && map[i+1][j].state!=1)check_four_grps(map, i, j+1, compteur, type);
		if (map[i-1][j].type==type && map[i+1][j].state!=1)check_four_grps(map, i-1, j, compteur, type);
		if (map[i][j-1].type==type && map[i+1][j].state!=1)check_four_grps(map, i, j-1, compteur, type);
	}
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