/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FightManager : MonoBehaviour
{
	[SerializeField] Player player;

	PlayerData player1;
	PlayerData player2;

	public GameObject fightRecord;

	public List<Turns> turns = new List<Turns>();

	public void OrganiseFight(PlayerData _player2)
	{
		player1 = player.ReturnDataClass();
		player2 = _player2;

		player1.ResetBattleStats();
		player2.ResetBattleStats();

		DontDestroyOnLoad(this);

		SceneManager.LoadScene("FightScene", LoadSceneMode.Single);

		turns.Clear();

		Fight();
	}

	void Fight(/*PlayerData opponent*/)
	{

		while (player1.maxHealth > 0 && player2.maxHealth > 0)
		{

			PlayerAttack(player1, player2);

			if (player2.maxHealth <= 0)
				break;

			PlayerAttack(player2, player1);

		}

		if (player2.maxHealth <= 0)
			Debug.Log("P1 WON  -  HP left = " + player1.maxHealth);

		if (player1.maxHealth <= 0)
			Debug.Log("P2 WON  -  HP left = " + player2.maxHealth);

		player1.ResetBattleStats();
		player2.ResetBattleStats();

		GameObject newFightRecord = Instantiate(fightRecord);

		newFightRecord.GetComponent<FightRecord>().player1 = player1;
		newFightRecord.GetComponent<FightRecord>().player2 = player2;
		newFightRecord.GetComponent<FightRecord>().allTurns = turns;
		newFightRecord.GetComponent<FightRecord>().StartRecord();

	}

	void PlayerAttack(PlayerData attacker, PlayerData defender)
	{
		Turns currentTurn = new Turns();

		currentTurn.attacker = attacker;
		currentTurn.defender = defender;

		EquipOrDequipWeapon(attacker, currentTurn);

		if (CalculateHitOrMiss(attacker, defender))
		{
			int damage = CalculateDamage(attacker);
			defender.maxHealth -= damage;

			currentTurn.damageDone = damage;
		}
		currentTurn.explanation = attacker.characterName + " attacked " + defender.characterName + " and dealt " + currentTurn.damageDone + " damage";

		if (attacker.equippedWeapon > 0)
			currentTurn.weaponUsed = player.items.weapons[attacker.equippedWeapon];

		currentTurn.healthLeft = defender.maxHealth;

		turns.Add(currentTurn);
	}

	void EquipOrDequipWeapon(PlayerData attacker, Turns currentTurn)
	{
		if (attacker.equippedWeapon == 0)
		{
			if (Random.Range(0, 6) == 1)    //equip weapon
			{
				attacker.equippedWeapon = Random.Range(1, player.items.weapons.Length - 1);
				currentTurn.equippedWeapon = true;
			}
		}
		else if (Random.Range(0, 4) == 1)	//throw weapon
		{
			attacker.equippedWeapon = 0;
			currentTurn.threwWeapon = true;
		}


	}

	bool CalculateHitOrMiss(PlayerData attacker, PlayerData defencer)
	{
		int chanceToHit = 35;

		chanceToHit += attacker.speed;
		chanceToHit -= attacker.agility;


		if (chanceToHit >= Random.Range(0, 50))
			return true;

		return false;
	}

	int CalculateDamage(PlayerData attacker)
	{
		float damage;

		if (attacker.equippedWeapon > 0)
			damage = player.items.weapons[attacker.equippedWeapon].damage;

		else
			damage = 10;	//base damage

		damage += Random.Range(-3, 3);	//random modifier

		for (int i = 0; i < attacker.strength; i++)
		{
			damage = damage + (damage * 0.1f); //increase by 10% for amount of str
		}

		return Mathf.RoundToInt(damage);
	}

}