/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MenuUI : MonoBehaviour
{
	[SerializeField] PlayfabManager playfabManager;

	private void Start()
	{
		
	}

	public void PlayGame()
	{
		//playfabManager.PlayerExist();
	}

	public void DeleteCharacter()
	{
		playfabManager.DeletePlayerData();
	}

	public void Options()
	{
		SceneManager.LoadScene("SettingsScene");
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}