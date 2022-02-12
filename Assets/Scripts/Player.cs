/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;

public class Player : MonoBehaviour
{
	
	Items items;
	PlayfabManager playfabManager;

	public int level;
	public int vitality, strength, intellec, agility, magicka;

	public int hairColor, hair, facialHairColor, facialHair, skinColor, head, shieldHand, weaponHand;

	public int[] helmets;
	public int[] skills;
	public int[] weapons;
	public int[] pets;

	[SerializeField] private SpriteRenderer hairSlot, facialHairSlot, headSlot, shieldHandSlot, weaponHandSlot, weaponSlot, shieldSlot;

	private void Awake()
	{
		items = GameObject.Find("_ITEMS_").GetComponent<Items>();
		playfabManager = GameObject.Find("_PLAYFABMANAGER_").GetComponent<PlayfabManager>();
		playfabManager.LoadPlayer();
	}

	public void SavePlayer()
	{
		playfabManager.SavePlayer();
	}

	public void LoadPlayer(PlayerData data)
	{
		level = data.level;

		vitality = data.vitality;
		strength = data.strength;
		intellec = data.intellec;
		agility = data.agility;
		magicka = data.magicka;

		hairColor = data.hairColor;
		hair = data.hair;
		facialHairColor = data.facialHairColor;
		facialHair = data.facialHair;
		skinColor = data.skinColor;
		head = data.head;
		shieldHand = data.shieldHand;
		weaponHand = data.weaponHand;

		helmets = data.helmets;
		skills = data.skills;
		weapons = data.weapons;
		pets = data.pets;

		LoadVisuals();
	}

	public void LoadVisuals()
	{
		hairSlot.sprite = items.hair[hairColor][hair];
		facialHairSlot.sprite = items.facialHair[facialHairColor][facialHair];
		headSlot.sprite = items.head[skinColor][head];
		shieldHandSlot.sprite = items.hand[skinColor];
		weaponHandSlot.sprite = items.hand[skinColor];
	}

	public PlayerData ReturnClass()
	{
		return new PlayerData(level, vitality, strength, intellec, agility, magicka, hairColor, hair, facialHairColor, facialHair, skinColor, head, shieldHand, weaponHand, helmets, skills, weapons, pets);
	}
}