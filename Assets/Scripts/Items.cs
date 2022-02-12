/*
* Eflox - Charles d'Ansembourg
*/

using UnityEngine;

public class Items : MonoBehaviour
{
	[Header("--Hair--")]
	[SerializeField] private string DescriptionHair = "Character hair & color.";
	public Sprite[][] hair = new Sprite[8][];
	public Sprite[] darkHair, blondeHair, blueHair, brownHair, greenHair, pinkHair, redHair, greyHair;

	[Header("--Facial Hair--")]
	[SerializeField] private string DescriptionFacialHair = "Character facial hair & color.";
	public Sprite[][] facialHair = new Sprite[8][];
	public Sprite[] darkFacialHair, blondeFacialHair, blueFacialHair, brownFacialHair, greenFacialHair, pinkFacialHair, redFacialHair, greyFacialHair;

	[Header("--Face--")]
	[SerializeField] private string DescriptionFace = "Character face emotion.";
	public Sprite[] skin1, skin2, skin3, skin4, skin5, skin6, skin7;
	public Sprite[][] head = new Sprite[7][];

	[Header("--Hand--")]
	[SerializeField] private string DescriptionHand = "Character hand.";
	public Sprite[] hand;

	/*
	[Header("--Body--")]
	[SerializeField] private string DescriptionBody = "Character body color.";
	public string[] bodyAnimatorPaths;
	*/

	[Header("--Helmets--")]
	[SerializeField] private string DescriptionHelmet = "Helmet cosmetics.";
	public Sprite[] helmets;

	[Header("--Weapons--")]
	[SerializeField] private string DescriptionWeapon = "Weapon classes.";
	public Weapon[] weapons;



	private void SetHairArray()
	{
		hair[0] = darkHair;
		hair[1] = blondeHair;
		hair[2] = blueHair;
		hair[3] = brownHair;
		hair[4] = greenHair;
		hair[5] = pinkHair;
		hair[6] = redHair;
		hair[7] = greyHair;
	}

	private void SetFacialHairArray()
	{
		facialHair[0] = darkFacialHair;
		facialHair[1] = blondeFacialHair;
		facialHair[2] = blueFacialHair;
		facialHair[3] = brownFacialHair;
		facialHair[4] = greenFacialHair;
		facialHair[5] = pinkFacialHair;
		facialHair[6] = redFacialHair;
		facialHair[7] = greyFacialHair;
	}

	private void SetHeadArray()
	{
		head[0] = skin1;
		head[1] = skin2;
		head[2] = skin3;
		head[3] = skin4;
		head[4] = skin5;
		head[5] = skin6;
		head[6] = skin7;
	}

	private void Awake()
	{
		SetHairArray();
		SetFacialHairArray();
		SetHeadArray();
	}
}