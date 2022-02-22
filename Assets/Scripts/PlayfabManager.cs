/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;
using System;

public class PlayfabManager : MonoBehaviour
{
	[Header("Main")]
	[SerializeField] public GameManager gameManager;

	[Header("Player Banners")]
	[SerializeField] string[] BannerPlayerIDs = new string[4];
	[SerializeField] PlayerBanner[] playerBanner;
	[SerializeField] private int index = 0;

	public bool IsLoggedIn()
	{
		return PlayFabClientAPI.IsClientLoggedIn();
	}

	public void Register(string email, string password)
	{
		var request = new RegisterPlayFabUserRequest
		{
			Email = email,
			Password = password,
			RequireBothUsernameAndEmail = false
		};

		PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
	}

	void OnRegisterSuccess(RegisterPlayFabUserResult result)
	{
		gameManager.MessageUI("Registered!");

		gameManager.LoginAfterRegister();
		
	}

	public void Login(string email, string password)
	{
		var request = new LoginWithEmailAddressRequest
		{
			Email = email,
			Password = password
		};
		PlayFabClientAPI.LoginWithEmailAddress(request, LoginOnSuccess, OnError);
	}

	public void ResetPassword(string email, string password)
	{
		var request = new SendAccountRecoveryEmailRequest
		{
			Email = email,
			TitleId = "9F104"
		};
		PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
	}

	void OnPasswordReset(SendAccountRecoveryEmailResult result)
	{
		gameManager.MessageUI("Password reset mail sent!");
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
		//GetOtherPlayerNames();
	}

	public void SavePlayer(PlayerData playerData)	//Saves the player data to playfab servers
	{
		List<PlayerData> players = new List<PlayerData>();
		players.Add(playerData);

		var request = new UpdateUserDataRequest
		{
			Data = new Dictionary<string, string>
			{
				{ "PlayerData", JsonConvert.SerializeObject(players) }
			},
			Permission = UserDataPermission.Public
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


	void GetOtherPlayers(string playerID)
	{
		
		GetUserDataRequest request = new GetUserDataRequest()
		{
			PlayFabId = playerID
		};

		PlayFabClientAPI.GetUserData(request, OtherUserDataReceived, OnError);
	}

	void OtherUserDataReceived(GetUserDataResult result)
	{
		Debug.Log("Banner user below");

		if (result.Data != null && result.Data.ContainsKey("PlayerData"))
		{
			List<PlayerData> playerData = JsonConvert.DeserializeObject<List<PlayerData>>(result.Data["PlayerData"].Value);

			Debug.Log(playerData[0].characterName);

			
			playerBanner[index].ReceivedPlayerData(playerData[0]);
			index++;
		}
	}

	public void SendLeaderboard(int level)
	{
		var request = new UpdatePlayerStatisticsRequest
		{
			Statistics = new List<StatisticUpdate>
			{
				new StatisticUpdate
				{
					StatisticName = "Levels",
					Value = level
				}
				
			}
		};
		PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardUpdate, OnError);
	}

	void OnLeaderBoardUpdate(UpdatePlayerStatisticsResult result)
	{
		Debug.Log("Successfully sent level to leaderboard.");
	}

	public void GetLeaderboard()
	{
		var request = new GetLeaderboardAroundPlayerRequest
		{
			StatisticName = "Levels",
			MaxResultsCount = 4
		};
		PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderboardGet, OnError);
	}

	void OnLeaderboardGet(GetLeaderboardAroundPlayerResult results)
	{
		int i = -1;
		foreach (var item in results.Leaderboard)
		{
			Debug.Log(item.PlayFabId + " is level " + item.StatValue);
			
			BannerPlayerIDs[++i] = item.PlayFabId;
		}
		index = 0;
		GetBannerPlayers();
	}

	void OnError(PlayFabError error)
	{
		gameManager.MessageUI(error.ErrorMessage);
		Debug.Log("Error while logging in/creating account.");
		Debug.Log(error.GenerateErrorReport());
	}
	//--------------------------------------------------------------------------

	void GetBannerPlayers()
	{
		if (index == 4)
			return;

		GetUserDataRequest request = new GetUserDataRequest()
		{
			PlayFabId = BannerPlayerIDs[index]
		};

		PlayFabClientAPI.GetUserData(request, BannerPlayerDataReceived, OnError);
	}

	void BannerPlayerDataReceived(GetUserDataResult result)
	{
		if (result.Data != null && result.Data.ContainsKey("PlayerData"))
		{
			List<PlayerData> playerData = JsonConvert.DeserializeObject<List<PlayerData>>(result.Data["PlayerData"].Value);

			Debug.Log(playerData[0].characterName);


			playerBanner[index].ReceivedPlayerData(playerData[0]);
			index++;
			GetBannerPlayers();
		}
	}
}
