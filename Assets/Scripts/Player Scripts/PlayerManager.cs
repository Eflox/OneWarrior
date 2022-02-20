/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
	[Header("Main")]
	[SerializeField] public GameManager gameManager;
	[SerializeField] public Items items;
	[SerializeField] public PlayerData playerData;

	[Header("Options")]
	[SerializeField] private bool Equipper;
	[SerializeField] private bool Mechanics;


	[Header("Player Compenents")]
	private PlayerEquipper playerEquipper;
	private PlayerMechanics playerMechanics;

	private void Start()
	{
		if (Equipper == true)
		{
			if (!(playerEquipper = GetComponent<PlayerEquipper>()))
				Debug.Log("Could not find PlayerEquipper component.");
			playerEquipper.playerManager = this;
		}
		if (Mechanics == true)
		{
			if (!(playerMechanics = GetComponent<PlayerMechanics>()))
				Debug.Log("Could not find PlayerMechanics component.");
			playerMechanics.playerManager = this;
		}

	}
	

	public void LoadPlayer(PlayerData _playerData)
	{
		playerData = _playerData;

		if (Equipper == true)
			EquipPlayer();
	}

	public void EquipPlayer()
	{
		playerEquipper.Equip(playerData);
	}

	public void LevelUp()
	{
		playerMechanics.LevelUp(playerData);
		gameManager.SavePlayer(playerData);
	}

	public PlayerData ReturnDataClass()
	{
		return playerData;
	}
}