﻿/*
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

	[SerializeField] private Text levelText;
	[SerializeField] private Text nameText;

	[SerializeField] private GameObject[] vitalityBar, strengthBar, intellectBar, agilityBar, magickaBar;

	[SerializeField] GameObject[] inventoryWeapons;
	[SerializeField] private Color normalColor;
	[SerializeField] private Color shadedColor;

	[SerializeField] private GameObject helmetSelector;
	[SerializeField] private GameObject helmetButton;
	private List<GameObject> helmetbuttons = new List<GameObject>();

	public void UpdateUI()
	{
		levelText.text = player.level.ToString();
		nameText.text = player.characterName;

		SetWeapons();
		SetStats();

	}

	private void SetWeapons()
	{
		for (int i = 0; i < player.weapons.Count; i++)
		{
			inventoryWeapons[player.weapons[i]].GetComponent<Image>().color = normalColor;
		}
	}

	private void SetStats()
	{
		for (int i = 0; i < player.vitality; i++)
			vitalityBar[i].SetActive(true);
		for (int i = 0; i < player.strength; i++)
			strengthBar[i].SetActive(true);
		for (int i = 0; i < player.intellec; i++)
			intellectBar[i].SetActive(true);
		for (int i = 0; i < player.agility; i++)
			agilityBar[i].SetActive(true);
		for (int i = 0; i < player.magicka; i++)
			magickaBar[i].SetActive(true);
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
}