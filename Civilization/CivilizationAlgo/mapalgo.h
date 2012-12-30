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
	// 0: Mountain, 1: Plain, 2: Desert
	int type;
	// 0: No additionnal ressource, 1: Additionnal Iron, 2: Additionnal Food
	int bonus;
	// 0: free, 1: checked, 2: frozen
	int state;
};

class DLL Algo {
	public:
		Algo() {}
		~Algo() {}
		int** createSmallMap();
		int** createMediumMap();
		int computeFoo();
};

// A ne pas implémenter dans le .h !
EXTERNC DLL Algo* Algo_new();
EXTERNC DLL void Algo_delete(Algo* algo);
EXTERNC DLL int** Algo_createSmallMap(Algo* algo);
EXTERNC DLL int** Algo_createMediumMap(Algo* algo);

//fonctions internes
void generateMap(square** &m, int h, int w);
bool mapFull(square** &m, int h, int w);
int checkFourGroups(square** &m, int i, int j, int compteur, int type, int h, int w);
bool threeTypesPresent(square** &m, int h, int w);
void changeGroup(square** &m, int i, int j, int itype, int dtype);