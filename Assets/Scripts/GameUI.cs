/*
 * Eflox - Charles d'Ansembourg
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	[Header("Main")]
	[SerializeField] public GameManager gameManager;


	[Header("Player")]
	[SerializeField] private Text levelText;
	[SerializeField] private Text nameText;
	[SerializeField] private StatBarHandler statBars;
	[SerializeField] GameObject[] inventoryWeapons;

	[Header("Connecting Screen")]
	[SerializeField] GameObject connectingScreen;
	[SerializeField] private InputField emailInput;
	[SerializeField] private InputField passwordInput;
	[SerializeField] private Text messageText;

	

	[Header("Options")]
	[SerializeField] private Color normalColor;
	[SerializeField] private Color shadedColor;

	[Header("Helmet")]
	[SerializeField] private GameObject helmetSelector;
	[SerializeField] private GameObject helmetButton;
	private List<GameObject> helmetbuttons = new List<GameObject>();

	[SerializeField] private Button[] allButtons;

	public void SetupUI()
	{
		if (!gameManager.IsLoggedIn())
			connectingScreen.SetActive(true);
		else
			gameManager.LoadPlayer();
		//DisableButtons();
		
	}

	public void UpdateUI(PlayerData playerData)
	{

		Debug.Log("UI Updated");
		levelText.text = playerData.level.ToString();
		nameText.text = playerData.characterName;

		SetWeapons(playerData);
		statBars.SetStats(playerData);

	}

	private void SetWeapons(PlayerData playerData)
	{
		for (int i = 0; i < playerData.weapons.Count; i++)
		{
			inventoryWeapons[playerData.weapons[i]].GetComponent<Image>().color = normalColor;
		}
	}

	public void LoadHelmets(PlayerData playerData)
	{
		helmetSelector.SetActive(true);
		for (int i = 0; i < playerData.helmets.Count; i++)
		{
			GameObject tmpButton = Instantiate(helmetButton) as GameObject;
			
			tmpButton.GetComponent<ButtonID>().ID = playerData.helmets[i];
			tmpButton.GetComponentInChildren<Image>().sprite = gameManager.items.helmets[playerData.helmets[i]];
			tmpButton.GetComponent<ButtonID>().UI = this;
			tmpButton.transform.SetParent(helmetSelector.transform);

			helmetbuttons.Add(tmpButton);
		}
	}

	public void RemoveConnectScreen()
	{
		connectingScreen.SetActive(false);
	}

	public void RegisterButton()
	{
		gameManager.Register(emailInput.text, passwordInput.text);
	}

	public void LoginButton()
	{
		gameManager.Login(emailInput.text, passwordInput.text);
	}

	public void ResetPasswordButton()
	{
		gameManager.ResetPassword(emailInput.text, passwordInput.text);
	}

	public void Message(string message)
	{
		messageText.text = message;
	}

	

	/*
	public void HelmetSelected(int helmetID)
	{
		foreach (var helmet in helmetbuttons)
		{
			Destroy(helmet);
		}
		helmetSelector.SetActive(false);
		player.helmet = helmetID;
		gameManager.SavePlayer();
	}

	public void EnableButtons()
	{
		for (int i = 0; i < allButtons.Length; i++)
		{
			allButtons[i].interactable = true;
		}
	}

	public void DisableButtons()
	{
		for (int i = 0; i < allButtons.Length; i++)
		{
			allButtons[i].interactable = false;
		}
	}
	*/
}