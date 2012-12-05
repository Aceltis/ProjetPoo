#include "mapalgo.h"

int Algo::computeFoo() {
	return 1;
}

Algo* Algo_new() { return new Algo(); }
void Algo_delete(Algo* algo) { delete algo; }
int Algo_computeAlgo(Algo* algo) { return algo->computeFoo(); } 
