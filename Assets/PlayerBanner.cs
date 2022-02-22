/*
 * Eflox - Charles d'Ansembourg
*/

using UnityEngine;
using UnityEngine.UI;

public class PlayerBanner : MonoBehaviour
{
	[SerializeField] private Items items;

	[SerializeField] public PlayerData player;

	[SerializeField] private GameObject holder;
	[SerializeField] private Image hairSlot, facialHairSlot, headSlot;
	[SerializeField] private StatBarHandler statBars;
	[SerializeField] private Text nameTag;
	[SerializeField] private Text levelTag;
	[SerializeField] private Button challengeButton;

	private void Start()
	{
		holder.SetActive(false);
	}

	public void ReceivedPlayerData(PlayerData playerData)
	{
		holder.SetActive(true);
		player = playerData;

		nameTag.text = player.characterName;
		levelTag.text = player.level.ToString();

		statBars.SetStats(player);
		SetHeadVisuals();
	}

	private void SetHeadVisuals()
	{
		hairSlot.sprite = items.hair[player.hairColor][player.hair];
		facialHairSlot.sprite = items.facialHair[player.facialHairColor][player.facialHair];
		headSlot.sprite = items.head[player.skinColor][player.head];
	}
}