/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;

public class StatBarHandler : MonoBehaviour
{
	[SerializeField] private GameObject[] constitutionBar, strengthBar, agilityBar, speedBar;

	private void ResetStats()
	{
		for (int i = 0; i < constitutionBar.Length; i++)
			constitutionBar[i].SetActive(false);
		for (int i = 0; i < strengthBar.Length; i++)
			strengthBar[i].SetActive(false);
		for (int i = 0; i < agilityBar.Length; i++)
			agilityBar[i].SetActive(false);
		for (int i = 0; i < speedBar.Length; i++)
			speedBar[i].SetActive(false);
	}

	public void SetStats(PlayerData player)
	{
		ResetStats();

		for (int i = 0; i < player.constitution; i++)
			constitutionBar[i].SetActive(true);
		for (int i = 0; i < player.strength; i++)
			strengthBar[i].SetActive(true);
		for (int i = 0; i < player.agility; i++)
			agilityBar[i].SetActive(true);
		for (int i = 0; i < player.speed; i++)
			speedBar[i].SetActive(true);
	}

	public void NewStats(PlayerData player)
	{
		player.level = 1;
		player.constitution = 1;
		player.strength = 1;
		player.agility= 1;
		player.speed = 1;


		for (int i = 0; i < 15; i++) 
		{
			int j = Random.Range(0, 4);

			if (j == 0)
				player.constitution++;
			if (j == 1)
				player.strength++;
			if (j == 2)
				player.agility++;
			if (j == 3)
				player.speed++;
		}

		SetStats(player);
	}
}