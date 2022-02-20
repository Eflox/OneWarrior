/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
public class PlayerEquipper : MonoBehaviour
{
	[Header("Main")]
	[HideInInspector] public PlayerManager playerManager;

	[Header("Player Sprites")]
	[SerializeField] private string description = "Sprite slots for each body part.";
	[SerializeField] private SpriteRenderer hairSlot, facialHairSlot, headSlot, shieldHandSlot, weaponHandSlot, weaponSlot, shieldSlot;


	public void Equip(PlayerData _playerData)
	{
		if (_playerData.helmet > 0)
			hairSlot.sprite = playerManager.items.helmets[_playerData.helmet];
		else
			hairSlot.sprite = playerManager.items.hair[_playerData.hairColor][_playerData.hair];

		facialHairSlot.sprite = playerManager.items.facialHair[_playerData.facialHairColor][_playerData.facialHair];
		headSlot.sprite = playerManager.items.head[_playerData.skinColor][_playerData.head];
		shieldHandSlot.sprite = playerManager.items.hand[_playerData.skinColor];
		weaponHandSlot.sprite = playerManager.items.hand[_playerData.skinColor];
	}
}