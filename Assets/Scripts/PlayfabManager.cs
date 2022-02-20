/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;

public class PlayfabManager : MonoBehaviour
{
	[SerializeField] public GameManager gameManager;

	public void Login()
	{
		var request = new LoginWithCustomIDRequest
		{
			CustomId = SystemInfo.deviceUniqueIdentifier,
			CreateAccount = true
		};
		PlayFabClientAPI.LoginWithCustomID(request, LoginOnSuccess, OnError);
	}

	void LoginOnSuccess(LoginResult result)
	{
		Debug.Log("Successful login/account created!");
		gameManager.LoggedIn();
	}


	public void LoadPlayer()
	{
		PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecieved, OnError);
	}

	void OnDataRecieved(GetUserDataResult result)
	{
		Debug.Log("Data recieved!");
		if (result.Data != null && result.Data.ContainsKey("PlayerData"))
		{
			List <PlayerData> playerData = JsonConvert.DeserializeObject<List<PlayerData>>(result.Data["PlayerData"].Value);
			gameManager.PlayerHasBeenLoaded(playerData[0]);
		}
		
	}

	public void SavePlayer(PlayerData playerData)	//Saves the player data to playfab servers
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

	void OnDataSend(UpdateUserDataResult result)		//Receives confirmation player data has been saved
	{
		Debug.Log("Successfully sent data!");
		gameManager.PlayerHasBeenSaved();
	}

	public void DeletePlayerData()		//Deletes player data on the playfab server
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

	public void DoesPlayerExist()		//Pings data
	{
		PlayFabClientAPI.GetUserData(new GetUserDataRequest(), ReceivePlayerData, OnError);
	}

	void ReceivePlayerData(GetUserDataResult result)		//Results of the data lets us know whether player exists or not
	{
		if (result.Data != null && result.Data.ContainsKey("PlayerData"))
			gameManager.PlayerHasBeenCreated();
		else
			gameManager.PlayerHasNotBeenCreated();
	}

	void OnError(PlayFabError error)
	{
		Debug.Log("Error while logging in/creating account.");
		Debug.Log(error.GenerateErrorReport());
	}

	void GetOtherPlayerNames()
	{
		PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OtherUserDataReceived, OnError);
	}

	void OtherUserDataReceived(GetUserDataResult result)
	{

	}
}