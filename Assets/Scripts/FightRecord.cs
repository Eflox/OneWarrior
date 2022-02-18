/*
 * Eflox - Charles d'Ansembourg
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightRecord : MonoBehaviour
{
	public PlayerData player1;
	public PlayerData player2;

	public List<Turns> allTurns = new List<Turns>();

	public void StartRecord()
	{
		DontDestroyOnLoad(this);
		SceneManager.LoadScene("FightScene");
	}
}