/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;

[System.Serializable]
public class PlayerData
{
	public int level;
	public int vitality, strength, intellec, agility, magicka;

	public int hairColor, hair, facialHairColor, facialHair, skinColor, head, shieldHand, weaponHand;

	public int[] helmets;
	public int[] skills;
	public int[] weapons;
	public int[] pets;

	public PlayerData(int _level, int _vitality, int _strength, int _intellec, int _agility, int _magicka, int _hairColor, int _hair, int _facialHairColor, int _facialHair, int _skinColor, int _head, int _shieldHand, int _weaponHand, int[] _helmets, int[] _skills, int[] _weapons, int[] _pets)
	{
		level = _level;

		vitality = _vitality;
		strength = _strength;
		intellec = _intellec;
		agility = _agility;
		magicka = _magicka;

		hairColor = _hairColor;
		hair = _hair;
		facialHairColor = _facialHairColor;
		facialHair = _facialHair;
		skinColor = _skinColor;
		head = _head;
		shieldHand = _shieldHand;
		weaponHand = _weaponHand;

		helmets = _helmets;
		skills = _skills;
		weapons = _weapons;
		pets = _pets;
	}
}