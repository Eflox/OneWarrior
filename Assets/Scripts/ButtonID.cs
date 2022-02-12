/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;
using UnityEngine.UI;

public class ButtonID : MonoBehaviour
{
	public GameUI UI;
	public int ID;

	public void Start()
	{
		GetComponent<Button>().onClick.AddListener(HelmetSelected);
	}

	void HelmetSelected()
	{
		UI.HelmetSelected(ID);
	}
}
