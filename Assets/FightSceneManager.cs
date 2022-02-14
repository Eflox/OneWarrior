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

	public void Start()
	{
		fightRecord = GameObject.Find("TestRecord").GetComponent<FightRecord>();
		SetupPlayers();
	}

	public void SetupPlayers()
	{
		originalPositionP1 = player1.transform.position;

		FightPlayer tmpFightPlayer = player1.GetComponent<FightPlayer>();

		tmpFightPlayer.playerData = fightRecord.player1;

		tmpFightPlayer.hairSlot.sprite = items.hair[fightRecord.player1.hairColor][fightRecord.player1.hair];
		tmpFightPlayer.facialHairSlot.sprite = items.facialHair[fightRecord.player1.facialHairColor][fightRecord.player1.facialHair];
		tmpFightPlayer.headSlot.sprite = items.head[fightRecord.player1.skinColor][fightRecord.player1.head];
		tmpFightPlayer.shieldHandSlot.sprite = items.hand[fightRecord.player1.skinColor];
		tmpFightPlayer.weaponHandSlot.sprite = items.hand[fightRecord.player1.skinColor];

		originalPositionP2 = player2.transform.position;
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
		if (turnIndex != fightRecord.allTurns.Count - 1)
		{
			currentTurn = fightRecord.allTurns[++turnIndex];
			currentTurnDone = false;
			didAttack = false;


			playerTurn = (playerTurn == 1) ? 2 : 1;
		}
	}

	private void Update()
	{

		if (Input.GetKeyDown(KeyCode.Space))
		{
			NextTurn();
		}
		//player1.transform.Translate(Vector2.right * playerSpeed * Time.deltaTime);
		
		if (currentTurnDone == false)
		{
			if (playerTurn == 1)
			{
				if (didAttack == false)
				{
					if (player1.transform.position.x < player2.transform.position.x - 0.15f)
						player1.transform.position = new Vector3(player1.transform.position.x + playerSpeed * Time.deltaTime, player1.transform.position.y, player1.transform.position.z);
						//player1.transform.position = Vector2.Lerp(player1.transform.position, player2.transform.position, playerSpeed * Time.deltaTime);
					else
					{
						didAttack = true;
					}
				}
				else
				{
					if (player1.transform.position.x > originalPositionP1.x)
						player1.transform.position = new Vector3(player1.transform.position.x - playerSpeed * Time.deltaTime, player1.transform.position.y, player1.transform.position.z);
					else
					{
						NextTurn();
					}
				}

			}
			if (playerTurn == 2)
			{
				if (didAttack == false)
				{
					if (player2.transform.position.x > player1.transform.position.x + 0.15f)
						//player2.transform.position = new Vector3(player2.transform.position.x - playerSpeed * Time.deltaTime, player2.transform.position.y, player2.transform.position.z);
						player2.transform.Translate(Vector2.left * playerSpeed * Time.deltaTime);
					else
					{
						didAttack = true;
					}
				}
				else
				{
					if (player2.transform.position.x < originalPositionP2.x)
						player2.transform.position = new Vector3(player2.transform.position.x + playerSpeed * Time.deltaTime, player2.transform.position.y, player2.transform.position.z);
					else
					{
						NextTurn();
					}
				}
			}	
		}
		
	}
}