/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	public GameManager gameManager;
	public Items items;
	public FightManager fightManager;

	public string characterName;
	public int level;
	public float xp;
	public int maxHealth;

	public int constitution, strength, agility, speed;

	public int hairColor, hair, facialHairColor, facialHair, skinColor, head, shieldHand, weaponHand, helmet;

	public List<int> helmets;
	public List<int> skills;
	public List<int> weapons;
	public List<int> pets;

	private float originalYTMP;
	[SerializeField] private SpriteRenderer hairSlot, facialHairSlot, headSlot, shieldHandSlot, weaponHandSlot, weaponSlot, shieldSlot;

	private void Start()
	{
		originalYTMP = transform.position.y;
	}

	public void LoadPlayer(PlayerData data)
	{
		characterName = data.characterName;
		level = data.level;
		xp = data.xp;
		maxHealth = data.maxHealth;

		constitution = data.constitution;
		strength = data.strength;
		agility = data.agility;
		speed = data.speed;

		hairColor = data.hairColor;
		hair = data.hair;
		facialHairColor = data.facialHairColor;
		facialHair = data.facialHair;
		skinColor = data.skinColor;
		head = data.head;
		shieldHand = data.shieldHand;
		weaponHand = data.weaponHand;
		helmet = data.helmet;

		helmets = data.helmets;
		skills = data.skills;
		weapons = data.weapons;
		pets = data.pets;

		LoadVisuals();
	}

	public void LoadVisuals()
	{
		//hairSlot.transform.position = new Vector3(hairSlot.transform.position.x, originalYTMP, 0);

		if (helmet > 0)
		{
			hairSlot.sprite = items.helmets[helmet];
			//facialHairSlot.sprite = null;
			//hairSlot.transform.position = new Vector3(hairSlot.transform.position.x, hairSlot.transform.position.y + 0.02f, 0);
		}
		else
		{
			//hairSlot.transform.position = new Vector3(hairSlot.transform.position.x, hairSlot.transform.position.y - 0.02f, 0);
			hairSlot.sprite = items.hair[hairColor][hair];
		}

		facialHairSlot.sprite = items.facialHair[facialHairColor][facialHair];
		headSlot.sprite = items.head[skinColor][head];
		shieldHandSlot.sprite = items.hand[skinColor];
		weaponHandSlot.sprite = items.hand[skinColor];
	}

	public void LevelUp()
	{
		level++;

		for (int i = 0; i < 1; i++)
		{
			RecieveHelmet();
			int randomCategory = Random.Range(0, 2);
			//int randomCategory = 0;

			if (randomCategory == 0)
			{
				if (weapons.Count >= items.weapons.Length)
					return;

				int ran = Random.Range(0, items.weapons.Length);

				while (weapons.Contains(ran))
					ran = Random.Range(0, items.weapons.Length);

				weapons.Add(ran);
				Debug.Log("Unlocked Weapon: " + items.weapons[ran].name);
			}
			else if (randomCategory == 1)
			{
				int skill = Random.Range(0, 4);
				int amountToAdd = Random.Range(4, 5);

				if (skill == 0)
					constitution += amountToAdd;
				if (skill == 1)
					strength += amountToAdd;
				if (skill == 2)
					agility += amountToAdd;
				if (skill == 3)
					speed += amountToAdd;
			}
			else if (randomCategory == 2)
			{

			}
		}
		gameManager.SavePlayer();
	}

	void RecieveHelmet()
	{
		int ran = Random.Range(0, items.helmets.Length);

		while (helmets.Contains(ran))
			ran = Random.Range(0, items.helmets.Length);

		helmets.Add(ran);
	}

	public PlayerData ReturnDataClass()
	{
		return new PlayerData(characterName, level, xp, constitution, strength, agility, speed, hairColor, hair, facialHairColor, facialHair, skinColor, head, shieldHand, weaponHand, helmet, helmets, skills, weapons, pets);
	}

	public void NewOpponent(PlayerData oppponent)
	{
		fightManager.OrganiseFight(oppponent);
	}
}