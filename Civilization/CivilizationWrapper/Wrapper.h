#ifndef __WRAPPER__
#define __WRAPPER__

#include "../CivilizationAlgo/mapalgo.h"
#pragma comment(lib, "../Debug/CivilizationAlgo.lib")

using namespace System;

namespace Wrapper {
	public ref class WrapperAlgo {
		private:
			Algo* algo;
	public:
		WrapperAlgo(){ algo = Algo_new(); }
		~WrapperAlgo(){ Algo_delete(algo); }
		int** createMap(int height, int width) { return algo->createMap(height, width); }
		int** createBonusesMap(int height, int width, double ratio) { return algo->createBonusesMap(height, width, ratio); }
	};
}
#endif