#include "mapalgo.h"

/*
 * createSmallMap()
 * Create a new 25x25 map
 * return square**
 */
int** Algo::createMap(int height, int width) {
	vector<vector<square>> newMap;

	newMap.resize(height);
	for(int i=0; i<height; i++)
		newMap[i].resize(width);

	// Initialize random seed
	srand(time(NULL));
	
	generateMap(newMap);
	int** map = new int*[height];
	for (int i=0; i<height; i++)
       map[i] = new int[width];

	for(int i = 0; i<height; i++)
	{
		for(int j = 0; j<width; j++)
		{
			map[i][j] = newMap[i][j].type;
		}
	}
	return map;
}

int** Algo::createBonusesMap(int height, int width, double ratio) {
	int** newBMap = new int*[height];
	for (int i=0; i<height; i++)
       newBMap[i] = new int[width];

	// Initialize random seed
	srand(time(NULL));
	
	generateBMap(newBMap, ratio, height, width);
	return newBMap;
}


Algo* Algo_new() { return new Algo(); }
void Algo_delete(Algo* algo) { delete algo; }
int** Algo_createMap(Algo* algo, int height, int width) { return algo->createMap(height, width); }
int** Algo_createBonusesMap(Algo* algo, int height, int width, double ratio) { return algo->createBonusesMap(height, width, ratio); }

void generateBMap(int** &BMap, int height, int width, double ratio){
	for(int i = 0; i<height; i++)
	{
		for(int j = 0; j<width; j++)
		{
			//ratio=0 : no bonus
			//ratio=1 : all squares have additionnal ressources
			// 0: No additionnal ressource, 1: Additionnal Iron, 2: Additionnal Food
			if(((double) rand() / (RAND_MAX))>=(1-ratio))
				BMap[i][j] = 1 + (rand() %2);
			else BMap[i][j] = 0;
		}
	}
}

void generateMap(vector<vector<square>> &map){
	do{
		for(int i = 0; i<map.size(); i++)
		{
			for(int j = 0; j<map[i].size(); j++)
			{
				map[i][j].type = rand() %3;
				map[i][j].state = 0;
			}
		}

		while(!(mapFull(map))){
			for(int i = 0; i<map.size(); i++)
			{
				for(int j = 0; j<map[i].size(); j++)
				{
					if(map[i][j].state!=2){
						//Can the given square be included in a 4 (or more) square group ?
						if(checkFourGroups(map, i, j)>=4){
							//Yes. Then all the squares in the same groupe are to be frozen
							map[i][j].state=2;
							for(int k = 0; k<map.size(); k++)
							{
								for(int l = 0; l<map[k].size(); l++)
								{
									if(map[k][l].state==1)map[k][l].state=2;
								}
							}
						}

						else{
							//No. Then all the squares in the same groupe will be randomized and analysed again
							map[i][j].type = rand()%3;
							for(int k = 0; k<map.size(); k++)
							{
								for(int l = 0; l<map[k].size(); l++)
								{
									if(map[k][l].state==1) {map[k][l].type= rand()%3; map[k][l].state = 0;}
								}
							}
						}
					}
				}
			}
		}
	}
	while(!(threeTypesPresent(map)));
}

bool mapFull(vector<vector<square>> &map){
	for(int i = 0; i<map.size(); i++)
	{
		for(int j = 0; j<map[i].size(); j++)
		{
			if(map[i][j].state!=2) return false;
		}
	}
	return true;
}

int checkFourGroups(vector<vector<square>> &map, int i, int j){
	int compteur=1, stop=0;
	map[i][j].state=1;
	//If a square standing next to current square is in a 4square group and is the same type, current square is in a 4sq groupe
	if (i+1<map.size() && stop == 0)	{if(map[i+1][j].type==map[i][j].type && map[i+1][j].state==2)	{compteur=4; stop=1;}}
	if (j+1<map[i].size() && stop == 0)	{if(map[i][j+1].type==map[i][j].type && map[i][j+1].state==2)	{compteur=4; stop=1;}}
	if (i>0 && stop == 0)				{if(map[i-1][j].type==map[i][j].type && map[i-1][j].state==2)	{compteur=4; stop=1;}}
	if (j>0 && stop == 0)				{if(map[i][j-1].type==map[i][j].type && map[i][j-1].state==2)	{compteur=4; stop=1;}}

	if (i+1<map.size() && stop == 0)	{if(map[i+1][j].type==map[i][j].type && map[i+1][j].state!=1) compteur += checkFourGroups(map, i+1, j);}
	if (j+1<map[i].size() && stop == 0)	{if(map[i][j+1].type==map[i][j].type && map[i][j+1].state!=1) compteur += checkFourGroups(map, i, j+1);}
	if (i>0 && stop == 0)				{if(map[i-1][j].type==map[i][j].type && map[i-1][j].state!=1) compteur += checkFourGroups(map, i-1, j);}
	if (j>0 && stop == 0)				{if(map[i][j-1].type==map[i][j].type && map[i][j-1].state!=1) compteur += checkFourGroups(map, i, j-1);}
	return compteur;
}

bool threeTypesPresent(vector<vector<square>> &map){
	int types[3] = {0,0,0};
	for(int i = 0; i<map.size(); i++)
	{
		for(int j = 0; j<map[i].size(); j++)
		{
			types[map[i][j].type]+=1;
		}
	}
	if (types[0]!=0 && types[1]!=0 && types[2]!=0) return true;
	if (types[0] + types[1] + types[2] >= 3) {return rearrangeGroups(map, types);}
	return false;
}

bool rearrangeGroups(vector<vector<square>> &map, int* types){
	int itype = max(types[0],max(types[1],types[2]));
	int dtype = min(types[0],min(types[1],types[2]));
	
	int end=0;
	for(int i = 0; i<map.size(); i++)
	{
		for(int j = 0; j<map[i].size(); j++)
		{
			if(map[i][j].type == itype){
				changeGroup(map, i, j, map[i][j].type, dtype);
				end = 1;
				break;
			}
		}
		if(end==1) break;
	}

	return true;
}

void changeGroup(vector<vector<square>> &map, int i, int j, int itype, int dtype){
	map[i][j].type = dtype;
	if (i+1<map.size())		if(map[i+1][j].type == itype) changeGroup(map, i+1, j, itype, dtype);
	if (j+1<map[i].size())	if(map[i][j+1].type == itype) changeGroup(map, i, j+1, itype, dtype);
	if (i>0)				if(map[i-1][j].type == itype) changeGroup(map, i-1, j, itype, dtype);
	if (j>0)				if(map[i][j-1].type == itype) changeGroup(map, i, j-1, itype, dtype);
}