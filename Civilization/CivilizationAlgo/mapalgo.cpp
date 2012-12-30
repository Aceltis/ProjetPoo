#include "mapalgo.h"

/*
 * createSmallMap()
 * Create a new 25x25 map
 * return square**
 */
int** Algo::createSmallMap() {
	int height = 25, width = 25;
	square** smallMap = new square*[height];
	for (int i=0; i<height; i++)
       smallMap[i] = new square[width];

	// Initialize random seed
	srand(time(NULL));
	
	generateMap(smallMap, height, width);
	int** map = new int*[height];
	for (int i=0; i<height; i++)
       map[i] = new int[width];

	for(int i = 0; i<height; i++)
	{
		for(int j = 0; j<width; j++)
		{
			map[i][j] = smallMap[i][j].type;
		}
	}
	return map;
}

/*
 * createMediumMap()
 * Create a new 100x100 map
 * return square**
 */
int** Algo::createMediumMap() {
	int height = 100, width = 100;
	square** mediumMap = new square*[height];
	for (int i=0; i<height; i++)
       mediumMap[i] = new square[width];

	// Initialize random seed
	srand(time(NULL));
	
	generateMap(mediumMap, height, width);
	int** map = new int*[height];
	for (int i=0; i<height; i++)
       map[i] = new int[width];

	for(int i = 0; i<height; i++)
	{
		for(int j = 0; j<width; j++)
		{
			map[i][j] = mediumMap[i][j].type;
		}
	}
	return map;
}

Algo* Algo_new() { return new Algo(); }
void Algo_delete(Algo* algo) { delete algo; }
int** Algo_createSmallMap(Algo* algo) { return algo->createSmallMap(); }
int** Algo_createMediumMap(Algo* algo) { return algo->createMediumMap(); }




void generateMap(square** &map, int height, int width){
	do{
		for(int i = 0; i<height; i++)
		{
			for(int j = 0; j<width; j++)
			{
				map[i][j].type = rand() %3;
				map[i][j].state = 0;
			}
		}

		while(!(mapFull(map, height, width))){
			for(int i = 0; i<height; i++)
			{
				for(int j = 0; j<width; j++)
				{
					if(map[i][j].state!=2){
						//Can the given square be included in a 4 (or more) square group ?
						if(checkFourGroups(map, i, j, height, width)>=4){
							//Yes. Then all the squares in the same groupe are to be frozen
							map[i][j].state=2;
							for(int k = 0; k<height; k++)
							{
								for(int l = 0; l<width; l++)
								{
									if(map[k][l].state==1)map[k][l].state=2;
								}
							}
						}

						else{
							//No. Then all the squares in the same groupe will be randomized and analysed again
							map[i][j].type = rand()%3;
							for(int k = 0; k<height; k++)
							{
								for(int l = 0; l<width; l++)
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
	while(!(threeTypesPresent(map, height, width)));
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

int checkFourGroups(square** &map, int i, int j, int height, int width){
	int compteur=1, stop=0;
	map[i][j].state=1;
	//If a square standing next to current square is in a 4square group and is the same type, current square is in a 4sq groupe
	if (i+1<height && stop == 0)	{if(map[i+1][j].type==map[i][j].type && map[i+1][j].state==2)	{compteur=4; stop=1;}}
	if (j+1<width && stop == 0)		{if(map[i][j+1].type==map[i][j].type && map[i][j+1].state==2)	{compteur=4; stop=1;}}
	if (i>0 && stop == 0)			{if(map[i-1][j].type==map[i][j].type && map[i-1][j].state==2)	{compteur=4; stop=1;}}
	if (j>0 && stop == 0)			{if(map[i][j-1].type==map[i][j].type && map[i][j-1].state==2)	{compteur=4; stop=1;}}

	if (i+1<height && stop == 0)	{if(map[i+1][j].type==map[i][j].type && map[i+1][j].state!=1) compteur += checkFourGroups(map, i+1, j, height, width);}
	if (j+1<width && stop == 0)		{if(map[i][j+1].type==map[i][j].type && map[i][j+1].state!=1) compteur += checkFourGroups(map, i, j+1, height, width);}
	if (i>0 && stop == 0)			{if(map[i-1][j].type==map[i][j].type && map[i-1][j].state!=1) compteur += checkFourGroups(map, i-1, j, height, width);}
	if (j>0 && stop == 0)			{if(map[i][j-1].type==map[i][j].type && map[i][j-1].state!=1) compteur += checkFourGroups(map, i, j-1, height, width);}
	return compteur;
}

bool threeTypesPresent(square** &map, int height, int width){
	int types[3] = {0,0,0};
	for(int i = 0; i<height; i++)
	{
		for(int j = 0; j<width; j++)
		{
			types[map[i][j].type]+=1;
		}
	}
	if (types[0]!=0 && types[1]!=0 && types[2]!=0) return true;
	if (types[0] + types[1] + types[2] >= 3) {return rearrangeGroups(map, height, width, types);}
	return false;
}

bool rearrangeGroups(square** &map, int height, int width, int* types){
	int itype = max(types[0],max(types[1],types[2]));
	int dtype = min(types[0],min(types[1],types[2]));
	
	int end=0;
	for(int i = 0; i<height; i++)
	{
		for(int j = 0; j<width; j++)
		{
			if(map[i][j].type == itype){
				changeGroup(map, i, j, height, width, map[i][j].type, dtype);
				end = 1;
				break;
			}
		}
		if(end==1) break;
	}

	return true;
}

void changeGroup(square** &map, int i, int j, int height, int width, int itype, int dtype){
	map[i][j].type = dtype;
	if (i+1<height)	if(map[i+1][j].type == itype) changeGroup(map, i+1, j, height, width, itype, dtype);
	if (j+1<width)	if(map[i][j+1].type == itype) changeGroup(map, i, j+1, height, width, itype, dtype);
	if (i>0)		if(map[i-1][j].type == itype) changeGroup(map, i-1, j, height, width, itype, dtype);
	if (j>0)		if(map[i][j-1].type == itype) changeGroup(map, i, j-1, height, width, itype, dtype);
}