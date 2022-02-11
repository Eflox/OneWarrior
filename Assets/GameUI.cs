/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	[SerializeField] private Player player;
	[SerializeField] private Text levelText;

	[SerializeField] private GameObject[] vitalityBar, strengthBar, intellectBar, agilityBar, magickaBar;

	private void Start()
	{
		Invoke("UpdateUI", 1.0f);
	}

	void UpdateUI()
	{
		levelText.text = player.level.ToString();
		SetStats();
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
}