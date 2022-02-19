/*
 * Eflox - Charles d'Ansembourg
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	public Player player;
	public GameManager gameManager;
	public Items items;


	[SerializeField] GameObject connectingScreen;
	[SerializeField] private Text levelText;
	[SerializeField] private Text nameText;

	[SerializeField] private StatBarHandler statBars;

	[SerializeField] GameObject[] inventoryWeapons;
	[SerializeField] private Color normalColor;
	[SerializeField] private Color shadedColor;

	[SerializeField] private GameObject helmetSelector;
	[SerializeField] private GameObject helmetButton;
	private List<GameObject> helmetbuttons = new List<GameObject>();

	[SerializeField] private Button[] allButtons;

	private void Start()
	{
		connectingScreen.SetActive(true);
		DisableButtons();
		
	}

	public void UpdateUI()
	{

		Debug.Log("UI Updated");
		levelText.text = player.level.ToString();
		nameText.text = player.characterName;

		SetWeapons();
		statBars.SetStats(player.ReturnDataClass());

	}

	private void SetWeapons()
	{
		for (int i = 0; i < player.weapons.Count; i++)
		{
			inventoryWeapons[player.weapons[i]].GetComponent<Image>().color = normalColor;
		}
	}

	public void LoadHelmets()
	{
		helmetSelector.SetActive(true);
		for (int i = 0; i < player.helmets.Count; i++)
		{
			GameObject tmpButton = Instantiate(helmetButton) as GameObject;
			
			tmpButton.GetComponent<ButtonID>().ID = player.helmets[i];
			tmpButton.GetComponentInChildren<Image>().sprite = player.items.helmets[player.helmets[i]];
			tmpButton.GetComponent<ButtonID>().UI = this;
			tmpButton.transform.SetParent(helmetSelector.transform);

			helmetbuttons.Add(tmpButton);
		}
	}

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

	public void RemoveConnectScreen()
	{
		connectingScreen.SetActive(false);
	}
}