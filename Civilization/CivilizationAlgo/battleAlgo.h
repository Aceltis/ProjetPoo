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

class DLL BattleAlgo {
	public:
		BattleAlgo() {}
		~BattleAlgo() {}
		int* computeBattle(int atkPoints, int defPoints, int atkHP, int defHP);
};

EXTERNC DLL BattleAlgo* BattleAlgo_new();
EXTERNC DLL void BattleAlgo_delete(BattleAlgo* battleAlgo);

EXTERNC DLL int* BattleAlgo_computeBattle(int atkPoints, int defPoints, int atkHP, int defHP);