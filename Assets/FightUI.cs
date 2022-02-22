/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;
using UnityEngine.UI;

public class FightUI : MonoBehaviour
{
	[SerializeField] private Items items;

	[SerializeField] private Button[] weaponButtons;
	[SerializeField] private int[] buttonIDs;

	private void Start()
	{
		for (int i = 0; i < weaponButtons.Length; i++)
		{
			weaponButtons[i].onClick.AddListener(delegate { WeaponSelected(buttonIDs[i]); });
			//i = buttonIDs[i];
		}
	}



	void WeaponSelected(int i)
	{
		Debug.Log(i);
	}

}