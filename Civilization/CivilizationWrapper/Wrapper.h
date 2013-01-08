#ifndef __WRAPPER__
#define __WRAPPER__

#include "../CivilizationAlgo/mapalgo.h"
#include "../CivilizationAlgo/battleAlgo.h"
#pragma comment(lib, "../Debug/CivilizationAlgo.lib")

using namespace System;

namespace Wrapper {
	public ref class WrapperMapAlgo {
		private:
			MapAlgo* mapAlgo;
	public:
		WrapperMapAlgo(){ mapAlgo = MapAlgo_new(); }
		~WrapperMapAlgo(){ MapAlgo_delete(mapAlgo); }
		int** createMap(int height, int width) { return mapAlgo->createMap(height, width); }
		int** createBonusesMap(int height, int width, double ratio) { return mapAlgo->createBonusesMap(height, width, ratio); }
		int** giveInitPos(int height, int width, int nbPlayers) {return mapAlgo->giveInitPos(height, width, nbPlayers);}
		int** computeSuggestions(int* &ressourcesMap, int* &knownTowns, int* &knownUnits, int size) {return mapAlgo->computeSuggestions(ressourcesMap, knownTowns, knownUnits, size);}
	};

	public ref class WrapperBattleAlgo {
		private:
			BattleAlgo* battleAlgo;
	public:
		WrapperBattleAlgo(){ battleAlgo = BattleAlgo_new(); }
		~WrapperBattleAlgo(){ BattleAlgo_delete(battleAlgo); }
		int* computeBattle(int atkPoints, int defPoints, int atkHP, int defHP) {return battleAlgo->computeBattle(atkPoints, defPoints, atkHP, defHP);}
	};
}
#endif