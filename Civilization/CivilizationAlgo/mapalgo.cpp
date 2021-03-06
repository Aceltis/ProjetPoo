#include "mapalgo.h"

/*
 * createMap()
 * Create a new heightxwidth map
 * return int**
 */
int** MapAlgo::createMap(int height, int width) {
	square** newMap = new square*[height];
	for (int i=0; i<height; i++)
       newMap[i] = new square[width];

	// Initialize random seed
	srand(time(NULL));
	
	generateMap(newMap, height, width);
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

/*
 * createBonusesMap()
 * Create a new heightxwidth map with bonus ressources
 * return int**
 */
int** MapAlgo::createBonusesMap(int height, int width, double ratio) {
	int** newBMap = new int*[height];
	for (int i=0; i<height; i++)
       newBMap[i] = new int[width];

	// Initialize random seed.
	srand(time(NULL));
	
	generateBMap(newBMap, height, width, ratio);
	return newBMap;
}

/*
 * giveInitPos()
 * Computes each player's units' initial position
 * return int**
 */
int** MapAlgo::giveInitPos(int height, int width, int nbPlayers)
{
	// Initialize random seed.
	srand(time(NULL));

	int** newIPos = new int*[nbPlayers];
	for (int i=0; i<nbPlayers; i++)
	{
		do
		{
			newIPos[i] = new int[2];
			//dans le rand, on enl�ve les cases trop proches du bord pour �viter un sentiment de "dos au mur" au joueur
			newIPos[i][0] = rand()%(width - (int)(width/4))+ (int)(width/8);;
			newIPos[i][1] = rand()%(height - (int)(height/4))+ (int)(height/8);;
		}
		while(tooClose(newIPos, i, height, width));
	}
	return newIPos;
}

/*
 * giveInitPos()
 * Computes each player's units' initial position
 * return int**
 */
int** MapAlgo::computeSuggestions(int* &ressourcesMap, int* &knownTowns, int* &knownUnits, int size)
{	
	int** newBMap = new int*[size];
	for (int i=0; i<size; i++)
       newBMap[i] = new int[2];

	return newBMap;
}

MapAlgo* MapAlgo_new() { return new MapAlgo(); }
void MapAlgo_delete(MapAlgo* mapAlgo) { delete mapAlgo; }
int** MapAlgo_createMap(MapAlgo* mapAlgo, int height, int width) { return mapAlgo->createMap(height, width); }
int** MapAlgo_createBonusesMap(MapAlgo* mapAlgo, int height, int width, double ratio) { return mapAlgo->createBonusesMap(height, width, ratio); }
int** MapAlgo_giveCitiesPos(MapAlgo* mapAlgo, int* &ress, int* &Towns, int* &Units, int size) {return mapAlgo->computeSuggestions(ress, Towns, Units, size);}

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
						if(groupSize(map, height, width, i, j)>=8){
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

/*
 * mapFull()
 * Checks if all squares on the map are frozen
 * return bool
 */
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

/*
 * groupSize()
 * Returns the size of a given square's group
 * Marks all squares of the group "checked"
 * return int
 */
int groupSize(square** &map, int height, int width, int i, int j){
	int counter=1, stop=0;
	map[i][j].state=1;
	//If a square standing next to current square is in a 4square group and is the same type, current square is in a 4sq groupe
	if (i+1<height && stop == 0){if(map[i+1][j].type==map[i][j].type && map[i+1][j].state==2)	{counter=8; stop=1;}}
	if (j+1<width && stop == 0)	{if(map[i][j+1].type==map[i][j].type && map[i][j+1].state==2)	{counter=8; stop=1;}}
	if (i>0 && stop == 0)		{if(map[i-1][j].type==map[i][j].type && map[i-1][j].state==2)	{counter=8; stop=1;}}
	if (j>0 && stop == 0)		{if(map[i][j-1].type==map[i][j].type && map[i][j-1].state==2)	{counter=8; stop=1;}}

	if (i+1<height && stop == 0){if(map[i+1][j].type==map[i][j].type && map[i+1][j].state!=1) counter += groupSize(map, height, width, i+1, j);}
	if (j+1<width && stop == 0)	{if(map[i][j+1].type==map[i][j].type && map[i][j+1].state!=1) counter += groupSize(map, height, width, i, j+1);}
	if (i>0 && stop == 0)		{if(map[i-1][j].type==map[i][j].type && map[i-1][j].state!=1) counter += groupSize(map, height, width, i-1, j);}
	if (j>0 && stop == 0)		{if(map[i][j-1].type==map[i][j].type && map[i][j-1].state!=1) counter += groupSize(map, height, width, i, j-1);}
	return counter;
}

/*
 * threeTypesPresent()
 * Checks if map contains all types of lands
 * return bool
 */
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

/*
 * rearrangeGroups()
 * Count square groups. Tries to change groups so that map contains all types of squares
 * return bool
 */
bool rearrangeGroups(square** &map, int height, int width, int* types){
	int itype = max(types[0],max(types[1],types[2]));
	int dtype = min(types[0],min(types[1],types[2]));
	
	int end=0;
	for(int i = 0; i<height; i++)
	{
		for(int j = 0; j<width; j++)
		{
			if(map[i][j].type == itype){
				changeGroup(map, height, width, i, j, map[i][j].type, dtype);
				end = 1;
				break;
			}
		}
		if(end==1) break;
	}

	return true;
}

/*
 * rearrangeGroups()
 * Change a square group's type to another
 * return void
 */
void changeGroup(square** &map, int height, int width, int i, int j, int itype, int dtype){
	map[i][j].type = dtype;
	if (i+1<height)		if(map[i+1][j].type == itype) changeGroup(map, height, width, i+1, j, itype, dtype);
	if (j+1<width)	if(map[i][j+1].type == itype) changeGroup(map, height, width, i, j+1, itype, dtype);
	if (i>0)				if(map[i-1][j].type == itype) changeGroup(map, height, width, i-1, j, itype, dtype);
	if (j>0)				if(map[i][j-1].type == itype) changeGroup(map, height, width, i, j-1, itype, dtype);
}

bool tooClose(int** &newIPos, int i, int height, int width)
{
	for(int j=0; j < i; j++)
	{
		//(x-x0)�+(y-y0)�<R�
		if((newIPos[j][0]-newIPos[i][0])*(newIPos[j][0]-newIPos[i][0])+(newIPos[j][1]-newIPos[i][1])*(newIPos[j][1]-newIPos[i][1])<2*min(height, width))
			return true;
	}
	return false;
}