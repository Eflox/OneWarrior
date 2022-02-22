/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;

[System.Serializable]
class Fighters
{
	public PlayerData playerData;
	public GameObject gameObject;
	public Animator animator;

	public Fighters(PlayerData _playerData, GameObject _gameObject, Vector3 _position, Vector3 _scale)
	{
		playerData = _playerData;
		gameObject = _gameObject;
		animator = gameObject.GetComponentInChildren<Animator>();

		gameObject.transform.position = _position;
		gameObject.transform.localScale = _scale;
		gameObject.GetComponent<PlayerManager>().LoadPlayer(playerData);
	}
}

public class FightSceneManager : MonoBehaviour
{
	[SerializeField] private FightRecord fightRecord;
	//[SerializeField] private Turns currentTurn;
	[SerializeField] private int turnIndex;

	[SerializeField] private Fighters[] fighters;
	[SerializeField] private GameObject fighterPrefab;
	[SerializeField] private int currentFighter = 0;

	bool inAttack = false;
	bool fightOver = false;

	private void Start()
	{
		fightRecord = GameObject.Find("FightRecord").GetComponent<FightRecord>();
		SetupFighters();
	}

	private void SetupFighters()
	{
		fighters = new Fighters[2]
		{
			new Fighters(fightRecord.allTurns[0].attacker, Instantiate(fighterPrefab), new Vector3(-0.7f, 0, 0), new Vector3(0.8f, 0.8f, 1)),
			new Fighters(fightRecord.allTurns[0].defender, Instantiate(fighterPrefab), new Vector3(0.7f, 0, 0), new Vector3(-0.8f, 0.8f, 1))
		};
	}

	private void StartFight()
	{

	}

	private void Attack()
	{
		if (turnIndex == fightRecord.allTurns.Count - 1)	//------------------Checks if the fight is over
			return;
		else
			fightOver = true;
		//--------

		WeaponsHandler();
		


		//--------
		inAttack = true;
		currentFighter = (currentFighter == 0) ? 1 : 0;
	}

	private void WeaponsHandler()
	{
		if (fightRecord.allTurns[turnIndex].equippedWeapon == true)
			EquipWeapon();
		if (fightRecord.allTurns[turnIndex].threwWeapon == true)
			ThrowWeapon();
	}

	private void EquipWeapon()
	{

	}

	private void ThrowWeapon()
	{

	}
	/*
	private void Update()
	{
		if (fightOver == true)
			return;


		if (inAttack == false)
			Attack();

	}
	*/
}
		//tmpFightPlayer.hairSlot.sprite = items.hair[fightRecord.player1.hairColor][fightRecord.player1.hair];
		//tmpFightPlayer.facialHairSlot.sprite = items.facialHair[fightRecord.player1.facialHairColor][fightRecord.player1.facialHair];
		//tmpFightPlayer.headSlot.sprite = items.head[fightRecord.player1.skinColor][fightRecord.player1.head];
		//tmpFightPlayer.shieldHandSlot.sprite = items.hand[fightRecord.player1.skinColor];
		//tmpFightPlayer.weaponHandSlot.sprite = items.hand[fightRecord.player1.skinColor];




//	void NextTurn()
//	{
//		if (turnIndex == fightRecord.allTurns.Count - 1)
//			return;
//		else
//			fightOver = true;

//		currentTurnDone = false;
//		didAttack = false;


//		playerTurn = (playerTurn == 1) ? 2 : 1;

//		if (currentTurn.equippedWeapon == true)
//		{
//			Debug.Log("equipped");
//			if (playerTurn == 1)
//				player1.GetComponent<FightPlayer>().weaponSlot.sprite = items.weapons[currentTurn.weaponUsed.ID].image;
//			if (playerTurn == 2)
//				player2.GetComponent<FightPlayer>().weaponSlot.sprite = items.weapons[currentTurn.weaponUsed.ID].image;
//		}

//		if (currentTurn.threwWeapon == true)
//		{
//			Debug.Log("threw");
//			if (playerTurn == 1)
//				player1.GetComponent<FightPlayer>().weaponSlot.sprite = null;
//			if (playerTurn == 2)
//				player2.GetComponent<FightPlayer>().weaponSlot.sprite = null;
//		}

//		if (currentTurn.weaponUsed != null)
//			p1Animator.SetTrigger(currentTurn.weaponUsed.name);

//		currentTurn = fightRecord.allTurns[++turnIndex];
//	}

	
//	private void Update()
//	{

//		if (Input.GetKeyDown(KeyCode.Space))
//		{
//			NextTurn();
//		}

//		if (fightOver == true)
//			return;

//	}
//}