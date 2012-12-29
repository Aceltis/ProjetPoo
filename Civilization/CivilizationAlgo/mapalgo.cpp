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
	
	generateMap(smallMap);
	return smallMap;
}

/*
 * createMediumMap()
 * Create a new 25x25 map
 * return vector<vector<square>>
 */
vector<vector<square>> createMediumMap() {
	int height, width = 100;
	vector<vector<square>> mediumMap;
	

	// Initialize random seed
	srand(time(NULL));

	// Set up sizes. (height x width)
	mediumMap.resize(height);
	for (int i = 0; i < height; ++i)
		mediumMap[i].resize(width);
	
	generateMap(mediumMap);
	return mediumMap;
}

void generateMap(vector<vector<square>> &map){
	for(int i = 0; i<map.size(); i++)
	{
		for(int j = 0; j<map.size(); j++)
		{
			map[i][j].type = rand() %3;
		}
	}

	while(!(mapFull(map))){
		for(int i = 0; i<map.size(); i++)
		{
			for(int j = 0; j<map.size(); j++)
			{
				if(map[i][j].state!=2){
					//Can the given square be included in a 4 (or more) square group ?
					if(checkFourGroups(map, i, j, 0, map[i][j].type)>=4)map[i][j].state=2;
					else map[i][j].type = rand() %3;

				}
			}
		}
	}
	while(!(threeTypesPresent(map))){
		int i = rand()%map.size(), j =rand()%map.size();
		changeGroup(map, i, j, map[i][j].type, rand()%3);
	}
}

bool mapFull(vector<vector<square>> &map){
	for(int i = 0; i<map.size(); i++)
	{
		for(int j = 0; j<map.size(); j++)
		{
			if(map[i][j].state!=2) return false;
		}
	}
	return true;
}

int checkFourGroups(vector<vector<square>> &map, int i, int j, int compteur, int type){
	compteur++;
	map[i][j].state=1;
	if (map[i+1][j].type==type && map[i+1][j].state==2)compteur=4;
	else if (map[i][j+1].type==type && map[i+1][j].state==2)compteur=4;
	else if (map[i-1][j].type==type && map[i+1][j].state==2)compteur=4;
	else if (map[i][j-1].type==type && map[i+1][j].state==2)compteur=4;
	else{
		if (map[i+1][j].type==type && map[i+1][j].state!=1)checkFourGroups(map, i+1, j, compteur, type);
		if (map[i][j+1].type==type && map[i+1][j].state!=1)checkFourGroups(map, i, j+1, compteur, type);
		if (map[i-1][j].type==type && map[i+1][j].state!=1)checkFourGroups(map, i-1, j, compteur, type);
		if (map[i][j-1].type==type && map[i+1][j].state!=1)checkFourGroups(map, i, j-1, compteur, type);
	}
	return compteur;
}

bool threeTypesPresent(vector<vector<square>> &map){
	int types[3] = {0,0,0};
	for(int i = 0; i<map.size(); i++)
	{
		for(int j = 0; j<map.size(); j++)
		{
			types[map[i][j].type]=1;
		}
	}
	if ((types[0]+types[1]+types[2])==3) return true;
	return false;
}

void changeGroup(vector<vector<square>> &map, int i, int j, int itype, int dtype){
	map[i][j].type = dtype;
	if(map[i+1][j].type == itype) changeGroup(map, i+1, j, itype, dtype);
	if(map[i][j+1].type == itype) changeGroup(map, i, j+1, itype, dtype);
	if(map[i-1][j].type == itype) changeGroup(map, i-1, j, itype, dtype);
	if(map[i][j-1].type == itype) changeGroup(map, i, j-1, itype, dtype);
}