#include "battleAlgo.h"

int* BattleAlgo::computeBattle(int atkPoints, int defPoints, int atkHP, int defHP)
{
	if(defPoints == 0)
	{
		int result[2] = {atkHP, 0};
		return result;
	}
	else
	{
		// Initialize random seed
		srand(time(NULL));

		//Calcul aléatoire du nombre d'engagements
		//Compris entre 3 et le nombre max d'HP + 2
		int nbCombats = (rand() % (max(atkHP, defHP) - 3 )) + 3;

		while(nbCombats!=0 && min(atkHP, defHP)!=0)
		{
			double rapport = (double)atkPoints/(double)defPoints;
			double atkChances = rapport*0.5 + 0.5;

			if(((double) rand() / (RAND_MAX))>=(1-atkChances))
				defHP--;
			else
				atkHP--;
			nbCombats--;
		}

		int tab[2] = {atkHP, defHP};
		return tab;
	}
}
BattleAlgo* BattleAlgo_new() { return new BattleAlgo(); }
void BattleAlgo_delete(BattleAlgo* battleAlgo) { delete battleAlgo; }

int* BattleAlgo_computeBattle(BattleAlgo* battleAlgo, int atkPoints, int defPoints, int atkHP, int defHP) { return battleAlgo->computeBattle(atkPoints, defPoints, atkHP, defHP); }