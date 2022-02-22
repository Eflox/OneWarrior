/*
 * Eflox - Charles d'Ansembourg
*/


using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public PlayerManager playerManager;
	public PlayfabManager playfabManager;
	public GameUI UI;
	public Items items;


	public bool IsLoggedIn()
	{
		return playfabManager.IsLoggedIn();
	}

	private void SetupScripts()
	{
		playfabManager.gameManager = this;

		playerManager.gameManager = this;
		playerManager.items = items;

		UI.gameManager = this;
	}

	private void Start()
	{
		GameSetup();
	}

	public void GameSetup()
	{
		Debug.Log("Setting up game.");

		SetupScripts();
		UI.SetupUI();
		
	}

	public void LoggedIn()		
	{
		playfabManager.DoesPlayerExist();       //Once logged in it will check whether player exist
		
	}

	public void PlayerHasNotBeenCreated()		//If player does not exist it will open character creation scene
	{
		SceneManager.LoadScene("CharacterCreationScene");
	}

	public void PlayerHasBeenCreated()		//If player does exist load the player data from playfab servers and player script
	{
		Debug.Log("been here done that");
		playfabManager.LoadPlayer();
		playfabManager.GetLeaderboard();
	}

	public void PlayerHasBeenLoaded(PlayerData _playerData)		//Only updates the UI once all of the above is done eg. logged in and player loaded
	{
		playerManager.LoadPlayer(_playerData);
		UI.RemoveConnectScreen();
		//UI.EnableButtons();
		UI.UpdateUI(_playerData);
	}

	public void SavePlayer(PlayerData playerData)
	{
		playfabManager.SavePlayer(playerData);
	}

	public void LoadPlayer()
	{
		playfabManager.LoadPlayer();
	}

	public void PlayerHasBeenSaved()		//Updates the UI once the save confirmation is received
	{
		LoadPlayer();
		UI.UpdateUI(playerManager.ReturnDataClass());
	}

	public void DeletePlayerData()
	{
		playfabManager.DeletePlayerData();
	}

	public void UpdateLevelLeaderboard(int level)
	{
		playfabManager.SendLeaderboard(level);
	}

	public void MessageUI(string message)
	{
		UI.Message(message);
	}

	public void Register(string email, string password)
	{
		playfabManager.Register(email, password);
	}

	public void Login(string email, string password)
	{
		playfabManager.Login(email, password);
	}

	public void ResetPassword(string email, string password)
	{
		playfabManager.ResetPassword(email, password);
	}

	public void LoginAfterRegister()
	{
		UI.LoginButton();
	}

}
