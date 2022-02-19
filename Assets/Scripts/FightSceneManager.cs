/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;

public class FightSceneManager : MonoBehaviour
{
	[SerializeField] Items items;
	[SerializeField] FightRecord fightRecord;

	[SerializeField] GameObject player1, player2;
	public int playerTurn;

	[SerializeField] float playerSpeed;
	public Vector3 originalPositionP1, originalPositionP2;

	public int turnIndex = 0;
	public Turns currentTurn;
	public bool currentTurnDone = false;
	public bool didAttack = false;
	public bool fightOver = false;

	public Animator p1Animator;


	public void Start()
	{
		p1Animator = player1.GetComponentInChildren<Animator>();
		fightRecord = GameObject.Find("TestRecord").GetComponent<FightRecord>();
		SetupPlayers();
	}

	public void SetupPlayers()
	{
		FightPlayer tmpFightPlayer = player1.GetComponent<FightPlayer>();

		tmpFightPlayer.playerData = fightRecord.player1;

		tmpFightPlayer.hairSlot.sprite = items.hair[fightRecord.player1.hairColor][fightRecord.player1.hair];
		tmpFightPlayer.facialHairSlot.sprite = items.facialHair[fightRecord.player1.facialHairColor][fightRecord.player1.facialHair];
		tmpFightPlayer.headSlot.sprite = items.head[fightRecord.player1.skinColor][fightRecord.player1.head];
		tmpFightPlayer.shieldHandSlot.sprite = items.hand[fightRecord.player1.skinColor];
		tmpFightPlayer.weaponHandSlot.sprite = items.hand[fightRecord.player1.skinColor];

		tmpFightPlayer = player2.GetComponent<FightPlayer>();

		tmpFightPlayer.playerData = fightRecord.player2;

		tmpFightPlayer.hairSlot.sprite = items.hair[fightRecord.player2.hairColor][fightRecord.player2.hair];
		tmpFightPlayer.facialHairSlot.sprite = items.facialHair[fightRecord.player2.facialHairColor][fightRecord.player2.facialHair];
		tmpFightPlayer.headSlot.sprite = items.head[fightRecord.player2.skinColor][fightRecord.player2.head];
		tmpFightPlayer.shieldHandSlot.sprite = items.hand[fightRecord.player2.skinColor];
		tmpFightPlayer.weaponHandSlot.sprite = items.hand[fightRecord.player2.skinColor];

		currentTurn = fightRecord.allTurns[turnIndex];
	}

	void NextTurn()
	{
		if (turnIndex == fightRecord.allTurns.Count - 1)
			return;
		else
			fightOver = true;

		currentTurnDone = false;
		didAttack = false;


		playerTurn = (playerTurn == 1) ? 2 : 1;

		if (currentTurn.equippedWeapon == true)
		{
			Debug.Log("equipped");
			if (playerTurn == 1)
				player1.GetComponent<FightPlayer>().weaponSlot.sprite = items.weapons[currentTurn.weaponUsed.ID].image;
			if (playerTurn == 2)
				player2.GetComponent<FightPlayer>().weaponSlot.sprite = items.weapons[currentTurn.weaponUsed.ID].image;
		}

		if (currentTurn.threwWeapon == true)
		{
			Debug.Log("threw");
			if (playerTurn == 1)
				player1.GetComponent<FightPlayer>().weaponSlot.sprite = null;
			if (playerTurn == 2)
				player2.GetComponent<FightPlayer>().weaponSlot.sprite = null;
		}

		if (currentTurn.weaponUsed != null)
			p1Animator.SetTrigger(currentTurn.weaponUsed.name);

		currentTurn = fightRecord.allTurns[++turnIndex];
	}

	
	private void Update()
	{

		if (Input.GetKeyDown(KeyCode.Space))
		{
			NextTurn();
		}

		if (fightOver == true)
			return;

	}
}