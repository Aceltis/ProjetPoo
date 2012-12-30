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
	// 0: No additionnal ressource, 1: Additionnal Iron, 2: Additionnal Food.
	int bonus;
	// 0: free, 1: frozen.
	int frozen;
};

class DLL Algo {
	public:
		Algo() {}
		~Algo() {}
		int computeFoo();
};

// A ne pas implémenter dans le .h !
EXTERNC DLL Algo* Algo_new();
EXTERNC DLL void Algo_delete(Algo* algo);
EXTERNC DLL int Algo_computeAlgo(Algo* algo);

// Create a height x width map.
EXTERNC DLL vector<vector<square>> createMap(int height,int width);

// Return True if the map has all its squares.
EXTERNC DLL bool map_full(vector<vector<square>> &map);

// Check if a group of four same lands exists.
// Return a counter of the number of lands in this group.
EXTERNC DLL int check_four_grps(vector<vector<square>> &map, int i, int j, int compteur, int type);

// Check all 3 types are contained in the new map.
EXTERNC DLL bool three_types_present(vector<vector<square>> &map);