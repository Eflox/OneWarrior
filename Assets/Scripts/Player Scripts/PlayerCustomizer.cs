/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using System.Collections.Generic;

public class PlayerCustomizer : MonoBehaviour
{
	[Header("Main")]
	[SerializeField] private Items items;
	[SerializeField] private PlayerManager playerManager;
	[SerializeField] private PlayerData playerData;


	[Header("Inputs")]
	[SerializeField] private InputField nameField;
	[SerializeField] private Button createButton;
	[SerializeField] private StatBarHandler statBars;

	private void Start()
	{
		nameField.onValueChanged.AddListener(delegate { WroteName(); });
		SetDefault();
		NewStats();
	}

	void WroteName()
	{
		playerData.characterName = nameField.text;
		createButton.interactable = true;
	}

	public void Randomise()
	{
		NewStats();

		playerData.hairColor = Random.Range(0, items.hair.Length - 1);
		NextHairColor();
		playerData.hair = Random.Range(0, items.hair[playerData.hairColor].Length - 1);
		NextHair();
		playerData.facialHairColor = Random.Range(0, items.facialHair.Length - 1);
		NextFacialHairColor();
		playerData.facialHair = Random.Range(0, items.facialHair[playerData.facialHairColor].Length - 1);
		NextFacialHair();
		playerData.skinColor = Random.Range(0, items.head.Length - 1);
		NextSkinColor();
		playerData.head = Random.Range(0, items.head[playerData.skinColor].Length - 1);
		NextFace();
	}

	public void NextHairColor()
	{
		playerData.hairColor = (playerData.hairColor == items.hair.Length - 1) ? 0 : playerData.hairColor + 1;
		playerManager.LoadPlayer(playerData);
	}

	public void NextHair()
	{
		NewStats();

		playerData.hair = (playerData.hair == items.hair[playerData.hairColor].Length - 1) ? 0 : playerData.hair + 1;
		playerManager.LoadPlayer(playerData);
	}

	public void NextFacialHairColor()
	{

		playerData.facialHairColor = (playerData.facialHairColor == items.facialHair.Length - 1) ? 0 : playerData.facialHairColor + 1;
		playerManager.LoadPlayer(playerData);
	}

	public void NextFacialHair()
	{
		NewStats();

		playerData.facialHair = (playerData.facialHair == items.facialHair[playerData.facialHairColor].Length - 1) ? 0 : playerData.facialHair + 1;
		playerManager.LoadPlayer(playerData);
	}

	public void NextSkinColor()
	{
		playerData.skinColor = (playerData.skinColor == items.head.Length - 1) ? 0 : playerData.skinColor + 1;
		playerManager.LoadPlayer(playerData);
	}

	public void NextFace()
	{
		NewStats();

		playerData.head = (playerData.head == items.head[playerData.skinColor].Length - 1) ? 0 : playerData.head + 1;
		playerManager.LoadPlayer(playerData);
	}

	public void SetDefault()
	{
		NewStats();


		playerData.hairColor = 0;
		playerData.hair = 30;
		playerData.facialHairColor = 0;
		playerData.facialHair = 29;
		playerData.skinColor = 0;
		playerData.head = 0;

		playerManager.LoadPlayer(playerData);
	}

	public void SavePlayer()
	{
		List<PlayerData> players = new List<PlayerData>();
		players.Add(playerData);

		var request = new UpdateUserDataRequest
		{
			Data = new Dictionary<string, string>
		{
			{"PlayerData", JsonConvert.SerializeObject(players) }
		}
		};
		PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
	}

	void OnDataSend(UpdateUserDataResult result)
	{
		Debug.Log("Successfully sent data!");
		SceneManager.LoadScene("GameScene");
	}

	void OnError(PlayFabError error)
	{
		Debug.Log("Error while logging in/creating account.");
		Debug.Log(error.GenerateErrorReport());
	}

	private void NewStats()
	{
		playerData.level = 1;
		playerData.constitution = 1;
		playerData.strength = 1;
		playerData.agility = 1;
		playerData.speed = 1;


		for (int i = 0; i < 6; i++)
		{
			int j = Random.Range(0, 4);

			if (j == 0)
				playerData.constitution++;
			if (j == 1)
				playerData.strength++;
			if (j == 2)
				playerData.agility++;
			if (j == 3)
				playerData.speed++;
		}

		statBars.SetStats(playerData);
	}
}