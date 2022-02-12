/*
 * Eflox - Charles d'Ansembourg
*/


using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public Player player;
	public PlayfabManager playfabManager;
	public GameUI UI;
	public Items items;

	public bool isLoggedIn = false;

	private void SetupScripts()
	{
		playfabManager.gameManager = this;
		playfabManager.player = player;

		player.gameManager = this;
		player.items = items;

		UI.gameManager = this;
		UI.player = player;
		UI.items = items;
	}

	private void Start()
	{
		GameSetup();
	}

	public void GameSetup()
	{
		SetupScripts();

		//Login player
		playfabManager.Login();


		//if player does not have character
		//open character creation page

		//load player

		//update UI
	}

	public void LoggedIn()		
	{
		isLoggedIn = true;
		playfabManager.DoesPlayerExist();		//Once logged in it will check whether player exist
	}

	public void PlayerHasNotBeenCreated()		//If player does not exist it will open character creation scene
	{
		SceneManager.LoadScene("CharacterCreationScene");
	}

	public void PlayerHasBeenCreated()		//If player does exist load the player data from playfab servers and player script
	{
		playfabManager.LoadPlayer();
	}

	public void PlayerHasBeenLoaded()		//Only updates the UI once all of the above is done eg. logged in and player loaded
	{
		UI.UpdateUI();
	}

	public void SavePlayer()
	{
		playfabManager.SavePlayer();
	}

	public void LoadPlayer()
	{
		playfabManager.LoadPlayer();
	}

	public void PlayerHasBeenSaved()		//Updates the UI once the save confirmation is received
	{
		LoadPlayer();
		UI.UpdateUI();
	}

	public void DeletePlayerData()
	{
		playfabManager.DeletePlayerData();
	}
}
