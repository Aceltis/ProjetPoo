#ifdef WANTDLLEXP
#define DLL _declspec(dllexport)
#define EXTERNC extern "C"
#else
#define DLL
#define EXTERNC
#endif

#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <vector>

using namespace std;

struct square {
	// 0: Mountain, 1: Plain, 2: Desert.
	int type;
	// 0: free, 1: checked, 2: frozen
	int state;
	// 0: No additionnal ressource, 1: Additionnal Iron, 2: Additionnal Food.
	int bonus;
	// 0: free, 1: frozen.
	int frozen;
};

class DLL MapAlgo {
	public:
		MapAlgo() {}
		~MapAlgo() {}
		int** createMap(int h, int w);
		int** createBonusesMap(int h, int w, double r);
		int** giveInitPos(int h, int w, int nb);
		int** computeSuggestions(int* &ress, int* &Towns, int* &Units, int s);
};

// A ne pas implémenter dans le .h !
EXTERNC DLL MapAlgo* MapAlgo_new();
EXTERNC DLL void MapAlgo_delete(MapAlgo* mapAlgo);

// Creates a height x width map.
EXTERNC DLL int** MapAlgo_createMap(MapAlgo* mapAlgo, int h, int w);

// Creates a height x width map of additional ressources.
EXTERNC DLL int** MapAlgo_createBonusesMap(MapAlgo* mapAlgo, int h, int w, double r);

// Returns each player's initial units' position
EXTERNC DLL int** MapAlgo_giveInitPos(MapAlgo* mapAlgo, int h, int w, int nbPlayers);

// Returns each player's initial units' position
EXTERNC DLL int** MapAlgo_giveCitiesPos(MapAlgo* mapAlgo, int* &ress, int* &Towns, int* &Units, int size);

//fonctions internes
//Generates the bonus map
void generateBMap(int** &bm, int h, int w, double r);

//Generates the types map
void generateMap(square** &m, int height, int width);

// Returns True if the map has all its squares.
bool mapFull(square** &m, int height, int width);

// Returns size of the current square's group.
int groupSize(square** &m, int height, int width, int i, int j);

//Returns true if all types are present in the map
bool threeTypesPresent(square** &m, int height, int width);

//Returns true if able to change a group's type in order to make all types present on the map
bool rearrangeGroups(square** &m, int height, int width, int* t);

//Changes a group's type
void changeGroup(square** &m, int height, int width, int i, int j, int it, int dt);

//returns true if initial units' positions are too close
bool tooClose(int** &newIPos, int i, int height, int width);