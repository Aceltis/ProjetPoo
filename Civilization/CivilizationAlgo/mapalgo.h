#ifdef WANTDLLEXP
#define DLL _declspec(dllexport)
#define EXTERNC extern "C"
#else
#define DLL
#define EXTERNC
#endif

#include <stdio.h>
#include <stdlib.h>
#include <vector>

struct square {
	// 1: Mountain, 2: Plain, 3: Desert
	int type;
	// 1: No additionnal ressource, 2: Additionnal Iron, 3: Additionnal Food
	int bonus;
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