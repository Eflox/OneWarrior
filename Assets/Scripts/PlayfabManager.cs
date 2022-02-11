/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;

public class PlayfabManager : MonoBehaviour
{
	[SerializeField] Player player;

	private void Awake()
	{
		DontDestroyOnLoad(this);
	}

	private void Start()
	{
		if (PlayFabClientAPI.IsClientLoggedIn())
			Debug.Log("Client already logged in.");
		else
			Login();
	}

	void Login()
	{
		var request = new LoginWithCustomIDRequest
		{
			CustomId = SystemInfo.deviceUniqueIdentifier,
			CreateAccount = true
		};
		PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
	}

	void OnSuccess(LoginResult result)
	{
		Debug.Log("Successful login/account created!");
	}

	void OnError(PlayFabError error)
	{
		Debug.Log("Error while logging in/creating account.");
		Debug.Log(error.GenerateErrorReport());
	}

	public void LoadPlayer()
	{
		PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecieved, OnError);
	}

	void OnDataRecieved(GetUserDataResult result)
	{
		player = GameObject.Find("_PLAYER_").GetComponent<Player>();
		Debug.Log("Data recieved!");
		if (result.Data != null && result.Data.ContainsKey("PlayerData"))
		{
			List <PlayerData> players = JsonConvert.DeserializeObject<List<PlayerData>>(result.Data["PlayerData"].Value);
			player.LoadPlayer(players[0]);
		}
	}

	public void SavePlayer()
	{
		List<PlayerData> players = new List<PlayerData>();
		players.Add(player.ReturnClass());

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
	}

	public void DeletePlayerData()
	{
		var request = new UpdateUserDataRequest
		{
			Data = new Dictionary<string, string>
			{
				{"PlayerData", null }
			}
		};
		PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
	}

	public void PlayerExist()
	{
		PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataCheckRecieved, OnError);
	}

	void OnDataCheckRecieved(GetUserDataResult result)
	{
		
		if (result.Data != null && result.Data.ContainsKey("PlayerData"))
		{
			SceneManager.LoadScene("GameScene");
		}
		else
		{
			SceneManager.LoadScene("CharacterCreationScene");
		}
	}
}