/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerMechanics : MonoBehaviour
{
	[Header("Main")]
	[HideInInspector] public PlayerManager playerManager;

	public void LevelUp(PlayerData playerData)
	{
		playerData.level++;

		for (int i = 0; i < 1; i++)
		{
			RecieveHelmet(playerData);
			int randomCategory = Random.Range(0, 2);	//----------------------Pick between receiving a weapon or increase in stats

			if (randomCategory == 0)	//--------------------------------------Receive a weapon
			{
				if (playerData.weapons.Count >= playerManager.items.weapons.Length)
					return;

				int ran = Random.Range(0, playerManager.items.weapons.Length);

				while (playerData.weapons.Contains(ran))
					ran = Random.Range(0, playerManager.items.weapons.Length);

				playerData.weapons.Add(ran);
				Debug.Log("Unlocked Weapon: " + playerManager.items.weapons[ran].name);
			}
			else if (randomCategory == 1)	//----------------------------------Increase stats
			{
				int skill = Random.Range(0, 4);
				int amountToAdd = Random.Range(4, 5);

				if (skill == 0)
					playerData.constitution += amountToAdd;
				if (skill == 1)
					playerData.strength += amountToAdd;
				if (skill == 2)
					playerData.agility += amountToAdd;
				if (skill == 3)
					playerData.speed += amountToAdd;
			}
			else if (randomCategory == 2)
			{

			}
		}
	}

	void RecieveHelmet(PlayerData playerData)		//--------------------------Receive a helmet
	{
		int ran = Random.Range(0, playerManager.items.helmets.Length);

		while (playerData.helmets.Contains(ran))
			ran = Random.Range(0, playerManager.items.helmets.Length);

		playerData.helmets.Add(ran);
	}
}