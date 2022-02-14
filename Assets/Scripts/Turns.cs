/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;

[System.Serializable]
public class Turns
{
	public PlayerData attacker;
	public PlayerData defender;

	public string explanation;


	public int damageDone;
	public int healthLeft;

	public bool equippedWeapon;
	public bool threwWeapon;

	public Weapon weaponUsed;

}