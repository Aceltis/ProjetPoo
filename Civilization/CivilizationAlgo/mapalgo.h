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
	// 0: free, 1: frozen
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
EXTERNC DLL vector<vector<square>> createSmallMap();