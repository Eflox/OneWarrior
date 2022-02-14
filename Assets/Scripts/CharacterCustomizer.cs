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

namespace OneWarriorCharacterCreation
{
	public class CharacterCustomizer : MonoBehaviour
	{
		[SerializeField] private Items items;
		[SerializeField] private Player player;

		[SerializeField] private GameObject[] vitalityBar, strengthBar, intellectBar, agilityBar, magickaBar;

		[SerializeField] private InputField nameField;
		[SerializeField] private Button createButton;

		private void Start()
		{
			nameField.onValueChanged.AddListener(delegate { WroteName(); });
			SetDefault();
			SetStats();
		}

		void WroteName()
		{
			player.characterName = nameField.text;
			createButton.interactable = true;
		}

		public void Randomise()
		{
			NewStats();

			player.hairColor = Random.Range(0, items.hair.Length - 1);
			NextHairColor();
			player.hair = Random.Range(0, items.hair[player.hairColor].Length - 1);
			NextHair();
			player.facialHairColor = Random.Range(0, items.facialHair.Length - 1);
			NextFacialHairColor();
			player.facialHair = Random.Range(0, items.facialHair[player.facialHairColor].Length - 1);
			NextFacialHair();
			player.skinColor = Random.Range(0, items.head.Length - 1);
			NextSkinColor();
			player.head = Random.Range(0, items.head[player.skinColor].Length - 1);
			NextFace();
		}

		public void NextHairColor()
		{
			player.hairColor = (player.hairColor == items.hair.Length - 1) ? 0 : player.hairColor + 1;
			player.LoadVisuals();
		}

		public void NextHair()
		{
			NewStats();

			player.hair = (player.hair == items.hair[player.hairColor].Length - 1) ? 0 : player.hair + 1;
			player.LoadVisuals();
		}

		public void NextFacialHairColor()
		{

			player.facialHairColor = (player.facialHairColor == items.facialHair.Length - 1) ? 0 : player.facialHairColor + 1;
			player.LoadVisuals();
		}

		public void NextFacialHair()
		{
			NewStats();

			player.facialHair = (player.facialHair == items.facialHair[player.facialHairColor].Length - 1) ? 0 : player.facialHair + 1;
			player.LoadVisuals();
		}

		public void NextSkinColor()
		{
			player.skinColor = (player.skinColor == items.head.Length - 1) ? 0 : player.skinColor + 1;
			player.LoadVisuals();
		}

		public void NextFace()
		{
			NewStats();

			player.head = (player.head == items.head[player.skinColor].Length - 1) ? 0 : player.head + 1;
			player.LoadVisuals();
		}

		public void SetDefault()
		{
			NewStats();


			player.hairColor = 0;
			player.hair = 30;
			player.facialHairColor = 0;
			player.facialHair = 29;
			player.skinColor = 0;
			player.head = 0;

			player.LoadVisuals();
		}

		public void SavePlayer()
		{
			List<PlayerData> players = new List<PlayerData>();
			players.Add(player.ReturnDataClass());

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

		private void ResetStats()
		{
			for (int i = 0; i < vitalityBar.Length; i++)
				vitalityBar[i].SetActive(false);
			for (int i = 0; i < vitalityBar.Length; i++)
				strengthBar[i].SetActive(false);
			for (int i = 0; i < vitalityBar.Length; i++)
				intellectBar[i].SetActive(false);
			for (int i = 0; i < vitalityBar.Length; i++)
				agilityBar[i].SetActive(false);
			for (int i = 0; i < vitalityBar.Length; i++)
				magickaBar[i].SetActive(false);
		}

		private void SetStats()
		{
			for (int i = 0; i < player.constitution; i++)
				vitalityBar[i].SetActive(true);
			for (int i = 0; i < player.strength; i++)
				strengthBar[i].SetActive(true);
			for (int i = 0; i < player.agility; i++)
				intellectBar[i].SetActive(true);
			for (int i = 0; i < player.speed; i++)
				agilityBar[i].SetActive(true);
		}

		private void NewStats()
		{
			player.level = 1;
			player.constitution = 1;
			player.strength = 1;
			player.agility = 1;
			player.speed = 1;


			for (int i = 0; i < 6; i++)
			{
				int j = Random.Range(0, 4);

				if (j == 0)
					player.constitution++;
				if (j == 1)
					player.strength++;
				if (j == 2)
					player.agility++;
				if (j == 3)
					player.speed++;
			}

			ResetStats();
			SetStats();
		}
	}
}