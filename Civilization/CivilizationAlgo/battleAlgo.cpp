#include "battleAlgo.h"

int* BattleAlgo::computeBattle(int atkPoints, int defPoints, int atkHP, int defHP)
{
	if(defPoints == 0)
	{
		int* result = new int[2];
		result[0] = atkHP;
		result[1] = 0;
		return result;
	}
	else
	{
		// Initialize random seed
		srand(time(NULL));

		//Calcul aléatoire du nombre d'engagements
		//Compris entre 3 et le nombre max d'HP + 2
		int nbCombats;
		if(max(atkHP, defHP) <=3)
			nbCombats = max(atkHP, defHP);
		else nbCombats = (rand() % (max(atkHP, defHP) - 3 )) + 3;

		while(nbCombats!=0 && min(atkHP, defHP)!=0)
		{
			double rapport;double atkChances;
			if(atkPoints<=defPoints)
			{
				rapport = 1-((double)atkPoints/(double)defPoints);
				atkChances = 1-(rapport*0.5 + 0.5);
			}
			else
			{
				rapport = ((double)defPoints/(double)atkPoints)-1;
				atkChances = rapport*0.5 + 0.5;
			}

			if(((double) rand() / (RAND_MAX))>=(1-atkChances))
				defHP--;
			else
				atkHP--;
			nbCombats--;
		}

		int* tab = new int[2];
		tab[0] = atkHP;
		tab[1] = defHP;
		return tab;
	}
}
BattleAlgo* BattleAlgo_new() { return new BattleAlgo(); }
void BattleAlgo_delete(BattleAlgo* battleAlgo) { delete battleAlgo; }

int* BattleAlgo_computeBattle(BattleAlgo* battleAlgo, int atkPoints, int defPoints, int atkHP, int defHP) { return battleAlgo->computeBattle(atkPoints, defPoints, atkHP, defHP); }