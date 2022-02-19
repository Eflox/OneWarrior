/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;

public class PlayerEquipper
{
	[SerializeField] Items items;

	[Header("Player Sprites")]
	[SerializeField] private SpriteRenderer hairSlot, facialHairSlot, headSlot, shieldHandSlot, weaponHandSlot, weaponSlot, shieldSlot;


	public void EquipPlayer(PlayerData data)
	{

		//hairSlot.transform.position = new Vector3(hairSlot.transform.position.x, originalYTMP, 0);

		if (data.helmet > 0)
		{
			//hairSlot.transform.position = new Vector3(hairSlot.transform.position.x, hairSlot.transform.position.y + 0.02f, 0);
			hairSlot.sprite = items.helmets[data.helmet];
		}
		else
		{
			hairSlot.sprite = items.hair[data.hairColor][data.hair];
		}

		facialHairSlot.sprite = items.facialHair[data.facialHairColor][data.facialHair];
		headSlot.sprite = items.head[data.skinColor][data.head];
		shieldHandSlot.sprite = items.hand[data.skinColor];
		weaponHandSlot.sprite = items.hand[data.skinColor];
	}
}