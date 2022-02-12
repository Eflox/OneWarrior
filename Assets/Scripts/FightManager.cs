/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;

public class FightManager : MonoBehaviour
{
	[SerializeField] Player player;

	PlayerData player1;
	PlayerData player2;
	//PlayerData player2 = new PlayerData("Traitor", 1, 0, 5, 1, 1, 5, 3, /* limit */ 0, 0, 0, 0, 0, 0, 0, 0, 0, null, null, null, null);

	public void OrganiseFight(PlayerData _player2)
	{
		player1 = player.ReturnDataClass();
		player2 = _player2;

		player1.ResetBattleStats();
		player2.ResetBattleStats();

		Fight();
	}

	//vitality = more health
	//strengh = more damage
	//intellec = higher chances to attack
	//agility = consisteny

	void Fight(/*PlayerData opponent*/)
	{
		float averageDmgP1 = 0;
		float averageDmgP2 = 0;


		int hitCountP1 = 0;
		int hitCountP2 = 0;


		while (player1.maxHealth > 0 && player2.maxHealth > 0)
		{
			float attackerChance = Random.Range(0, 52);
			attackerChance -= player1.intellec * 2;
			attackerChance += player2.intellec * 2;


			//who goes first
			if (attackerChance <= 25)
			{
				//player 1 attack
				averageDmgP1 += playerAttack(player1, player2);
				hitCountP1++;

				//Debug.Log("Player1 current health = " + player1.maxHealth + " - his turn to hit");

			}
			else
			{
				//player 2 attack
				averageDmgP2 += playerAttack(player2, player1);
				hitCountP2++;

				//Debug.Log("Player2 current health = " + player2.maxHealth + " - his turn to hit");
			}
		}

		averageDmgP1 /= hitCountP1;
		averageDmgP2 /= hitCountP2;

		if (player1.maxHealth <= 0)
		{
			Debug.Log("player2 WON!!!");
			Debug.Log("P2 health left = " + player2.maxHealth);

		}
		else if (player2.maxHealth <= 0)
		{
			Debug.Log("player1 WON!!!");
			Debug.Log("P1 health left = " + player1.maxHealth);
		}


		Debug.Log("P1 hit count = " + hitCountP1);
		Debug.Log("P1 average damage = " + averageDmgP1);


		Debug.Log("P2 hit count = " + hitCountP2);
		Debug.Log("P2 average damage = " + averageDmgP2);
		

		player1.ResetBattleStats();
		player2.ResetBattleStats();

	}

	int playerAttack(PlayerData offencer, PlayerData defencer)
	{
		int damage = offencer.strength * Random.Range(1, offencer.agility);
		defencer.maxHealth -= damage;

		Debug.Log("Damage done = " + damage);
		return damage;
	}
}