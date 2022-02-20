/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;
using UnityEngine.UI;

public class GeneratePlayer : MonoBehaviour
{
	[SerializeField] Items items;

	PlayerData player = new PlayerData("Opponent", 1, 0, 0, 0, 0, 0, /* limit */ 0, 0, 0, 0, 0, 0, 0, 0, 0, null, null, null, null);

	[Header("--Visuals--")]
	[SerializeField] private Image hairSlot, facialHairSlot, headSlot;
	[SerializeField] private StatBarHandler statBars;
	[SerializeField] private Text nameTag;
	[SerializeField] private Button challengeButton;


	private void Start()
	{
		//mainPlayer = GameObject.Find("_PLAYER_").GetComponent<Player>();

		RandomiseBaseStats();
		RandomAppearanceStats();
		SetVisuals();
	}

	void RandomiseBaseStats()
	{
		player.level = 1;
		player.constitution = 1;
		player.strength = 1;
		player.agility = 1;
		player.speed = 1;


		for (int i = 0; i < 6; i++) // temporarily 6 normally 10
		{
			int j = Random.Range(0, 4); //temporarily 4 as magicka is not used for the moment

			if (j == 0)
				player.constitution++;
			if (j == 1)
				player.strength++;
			if (j == 2)
				player.agility++;
			if (j == 3)
				player.speed++;
		}

		//player.maxHealth = 25 * player.vitality;
	}

	void RandomAppearanceStats()
	{
		player.hairColor = Random.Range(0, items.hair.Length - 1);
		player.hair = Random.Range(0, items.hair[player.hairColor].Length - 1);

		player.facialHairColor = Random.Range(0, items.facialHair.Length - 1);
		player.facialHair = Random.Range(0, items.facialHair[player.facialHairColor].Length - 1);

		player.skinColor = Random.Range(0, items.head.Length - 1);
		player.head = Random.Range(0, items.head[player.skinColor].Length - 1);

	}

	void SetVisuals()
	{
		nameTag.text = player.characterName;
		SetStats();
		RandomiseHeadVisuals();
	}

	private void RandomiseHeadVisuals()
	{
		hairSlot.sprite = items.hair[player.hairColor][player.hair];
		facialHairSlot.sprite = items.facialHair[player.facialHairColor][player.facialHair];
		headSlot.sprite = items.head[player.skinColor][player.head];
	}

	private void SetStats()
	{
		statBars.SetStats(player);
	}
}