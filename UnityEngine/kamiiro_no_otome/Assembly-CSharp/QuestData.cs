using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Yuusha Game Data/Quest Data")]
public class QuestData : SerializedScriptableObject
{
	public enum QuestCategory
	{
		story,
		order
	}

	public enum QuestSubCategory
	{
		story,
		sub,
		none
	}

	public enum QuestType
	{
		extermination = 0,
		contract = 14,
		heroineContract = 17,
		supply = 1,
		carriageStore = 2,
		facilityLv = 3,
		facilityToolLv = 4,
		furnaceLv = 5,
		addOnLv = 6,
		craft = 7,
		itemGet = 11,
		skillLearn = 15,
		dungeonClear = 16,
		itemShop = 8,
		scenario = 10,
		sexScenario = 12,
		fertilize = 13,
		totalSalesAmount = 19,
		gameClear = 18,
		sexScenarioClear = 20,
		scenarioClear = 21
	}

	public string accessPointName;

	public int sortID;

	public QuestCategory questCategory;

	public QuestSubCategory questSubCategory;

	public QuestType questType;

	public List<int> requirementList = new List<int>();

	public List<int[]> rewardList = new List<int[]>();

	public string questUnlockRecipeName;

	public List<string> questStartFlagNameList;

	public string questEndFlagName;

	public List<string> questClearFlagNameList;

	public bool isDeleteTheRequirementItemAtClear;

	public Sprite questRequirementItemImage;
}
