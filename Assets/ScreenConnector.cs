/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;
using UnityEngine.UI;

public class ScreenConnector : MonoBehaviour
{
	[SerializeField] private int time = 10;
	[SerializeField] public Text text;

	private void Start()
	{
		Invoke("CantConnect", time);
	}

	public void CantConnect()
	{
		text.text = "Error: Can't connect to servers...";
	}
}