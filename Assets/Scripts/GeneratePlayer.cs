/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;
using UnityEngine.UI;

public class GeneratePlayer : MonoBehaviour
{
	public Player mainPlayer;

	[SerializeField] Items items;

	PlayerData player = new PlayerData("Opponent", 1, 0, 0, 0, 0, 0, 0, /* limit */ 0, 0, 0, 0, 0, 0, 0, 0, 0, null, null, null, null);

	[Header("--Visuals--")]
	[SerializeField] private Image hairSlot, facialHairSlot, headSlot;
	[SerializeField] private GameObject[] vitalityBar, strengthBar, intellectBar, agilityBar, magickaBar;
	[SerializeField] private Text nameTag;
	[SerializeField] private Button challengeButton;


	private void Start()
	{
		mainPlayer = GameObject.Find("_PLAYER_").GetComponent<Player>();

		RandomiseBaseStats();
		RandomAppearanceStats();
		SetVisuals();

		challengeButton.onClick.AddListener(OnChallenged);
	}

	void OnChallenged()
	{
		mainPlayer.NewOpponent(player);
	}

	void RandomiseBaseStats()
	{
		player.level = 1;
		player.vitality = 1;
		player.strength = 1;
		player.intellec = 1;
		player.agility = 1;
		player.magicka = 1;


		for (int i = 0; i < 6; i++) // temporarily 6 normally 10
		{
			int j = Random.Range(0, 4); //temporarily 4 as magicka is not used for the moment

			if (j == 0)
				player.vitality++;
			if (j == 1)
				player.strength++;
			if (j == 2)
				player.intellec++;
			if (j == 3)
				player.agility++;
			if (j == 4)
				player.magicka++;
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

		Debug.Log(player.hairColor);
		Debug.Log(player.hair);
		Debug.Log(player.facialHairColor);
		Debug.Log(player.facialHair);
		Debug.Log(player.head);


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
		for (int i = 0; i < player.vitality; i++)
			vitalityBar[i].SetActive(true);
		for (int i = 0; i < player.strength; i++)
			strengthBar[i].SetActive(true);
		for (int i = 0; i < player.intellec; i++)
			intellectBar[i].SetActive(true);
		for (int i = 0; i < player.agility; i++)
			agilityBar[i].SetActive(true);
		for (int i = 0; i < player.magicka; i++)
			magickaBar[i].SetActive(true);
	}


}