/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
	public string characterName;

	public int level;
	public float xp;
	public int maxHealth;
	public int constitution, strength, agility, speed;

	public int hairColor, hair, facialHairColor, facialHair, skinColor, head, shieldHand, weaponHand, helmet;

	public int equippedWeapon;

	public List<int> helmets;
	public List<int> skills;
	public List<int> weapons;
	public List<int> pets;

	public PlayerData(string _characterName, int _level, float _xp, int _constitution, int _strength, int _agility, int _speed, int _hairColor, int _hair, int _facialHairColor, int _facialHair, int _skinColor, int _head, int _shieldHand, int _weaponHand, int _helmet, List<int> _helmets, List<int> _skills, List<int> _weapons, List<int> _pets)
	{
		characterName = _characterName;
		level = _level;
		xp = _xp;

		constitution = _constitution;
		strength = _strength;
		agility = _agility;
		speed = _speed;

		hairColor = _hairColor;
		hair = _hair;
		facialHairColor = _facialHairColor;
		facialHair = _facialHair;
		skinColor = _skinColor;
		head = _head;
		shieldHand = _shieldHand;
		weaponHand = _weaponHand;
		helmet = _helmet;
		

		helmets = _helmets;
		skills = _skills;
		weapons = _weapons;
		pets = _pets;

		ResetBattleStats();
	}

	public void ResetBattleStats()
	{
		float tmpHealth = 100;

		for (int i = 0; i < constitution; i++)
		{
			tmpHealth = tmpHealth + (tmpHealth * 0.05f); //increase by 10% for amount of str
		}

		maxHealth = Mathf.RoundToInt(tmpHealth);

		equippedWeapon = 0;
	}
}