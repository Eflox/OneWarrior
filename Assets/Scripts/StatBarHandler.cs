/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;

public class StatBarHandler : MonoBehaviour
{
	[SerializeField] private GameObject[] constitutionBar, strengthBar, agilityBar, speedBar;

	public void SetStats(PlayerData player)
	{
		for (int i = 0; i < player.constitution; i++)
			constitutionBar[i].SetActive(true);
		for (int i = 0; i < player.strength; i++)
			strengthBar[i].SetActive(true);
		for (int i = 0; i < player.agility; i++)
			agilityBar[i].SetActive(true);
		for (int i = 0; i < player.speed; i++)
			speedBar[i].SetActive(true);
	}

	private void NewStats(PlayerData player)
	{
		player.level = 1;
		player.constitution = 1;
		player.strength = 1;
		player.agility= 1;
		player.speed = 1;


		for (int i = 0; i < 6; i++) 
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
	}
}