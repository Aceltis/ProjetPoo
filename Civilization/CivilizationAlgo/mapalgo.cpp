#include "mapalgo.h"

int Algo::computeFoo() {
return 1;
}

/*
 * createSmallMap()
 * Create a new 25x25 map
 * return square**
 */
square** Algo::createSmallMap() {
	int height = 25, width = 25;
	square** smallMap = new square*[height];
	for (int i=0; i<height; i++)
       smallMap[i] = new square[width];

	// Initialize random seed
	srand(time(NULL));
	
	//generateMap(smallMap, height, width);
	return smallMap;
}

/*
 * createMediumMap()
 * Create a new 100x100 map
 * return square**
 */
square** Algo::createMediumMap() {
	int height = 100, width = 100;
	square** mediumMap = new square*[height];
	for (int i=0; i<height; i++)
       mediumMap[i] = new square[width];

	// Initialize random seed
	srand(time(NULL));
	
	//generateMap(mediumMap, height, width);
	return mediumMap;
}

Algo* Algo_new() { return new Algo(); }
void Algo_delete(Algo* algo) { delete algo; }
square** Algo_createSmallMap(Algo* algo) { return algo->createSmallMap(); }
square** Algo_createMediumMap(Algo* algo) { return algo->createMediumMap(); }
int Algo_computeAlgo(Algo* algo) { return algo->computeFoo(); }




void generateMap(square** &map, int height, int width){
	for(int i = 0; i<height; i++)
	{
		for(int j = 0; j<width; j++)
		{
			map[i][j].type = rand() %3;
		}
	}

	while(!(mapFull(map, height, width))){
		for(int i = 0; i<height; i++)
		{
			for(int j = 0; j<width; j++)
			{
				if(map[i][j].state!=2){
					//Can the given square be included in a 4 (or more) square group ?
					if(checkFourGroups(map, i, j, 0, map[i][j].type, height, width)>=4)map[i][j].state=2;
					else map[i][j].type = rand() %3;

				}
			}
		}
	}
	while(!(threeTypesPresent(map, height, width))){
		int i = rand()%height, j =rand()%width;
		changeGroup(map, i, j, map[i][j].type, rand()%3);
	}
}

bool mapFull(square** &map, int height, int width){
	for(int i = 0; i<height; i++)
	{
		for(int j = 0; j<width; j++)
		{
			if(map[i][j].state!=2) return false;
		}
	}
	return true;
}

int checkFourGroups(square** &map, int i, int j, int compteur, int type, int height, int width){
	compteur++;
	map[i][j].state=1;
	if (i+1<height && map[i+1][j].type==type && map[i+1][j].state==2)compteur=4;
	else if (j+1<width && map[i][j+1].type==type && map[i+1][j].state==2)compteur=4;
	else if (i>0 && map[i-1][j].type==type && map[i+1][j].state==2)compteur=4;
	else if (j>0 && map[i][j-1].type==type && map[i+1][j].state==2)compteur=4;
	else{
		if (i+1<height && map[i+1][j].type==type && map[i+1][j].state!=1)checkFourGroups(map, i+1, j, compteur, type, height, width);
		if (j+1<width && map[i][j+1].type==type && map[i+1][j].state!=1)checkFourGroups(map, i, j+1, compteur, type, height, width);
		if (i>0 && map[i-1][j].type==type && map[i+1][j].state!=1)checkFourGroups(map, i-1, j, compteur, type, height, width);
		if (j>0 && map[i][j-1].type==type && map[i+1][j].state!=1)checkFourGroups(map, i, j-1, compteur, type, height, width);
	}
	return compteur;
}

bool threeTypesPresent(square** &map, int height, int width){
	int types[3] = {0,0,0};
	for(int i = 0; i<height; i++)
	{
		for(int j = 0; j<width; j++)
		{
			types[map[i][j].type]=1;
		}
	}
	if ((types[0]+types[1]+types[2])==3) return true;
	return false;
}

void changeGroup(square** &map, int i, int j, int itype, int dtype){
	map[i][j].type = dtype;
	if(map[i+1][j].type == itype) changeGroup(map, i+1, j, itype, dtype);
	if(map[i][j+1].type == itype) changeGroup(map, i, j+1, itype, dtype);
	if(map[i-1][j].type == itype) changeGroup(map, i-1, j, itype, dtype);
	if(map[i][j-1].type == itype) changeGroup(map, i, j-1, itype, dtype);
}