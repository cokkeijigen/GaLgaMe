using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class TestStaticBool : StateBehaviour
{
	public enum Type
	{
		isWorldMapToUtage = 0,
		isWorldMapToInDoor = 7,
		isWorldMapPointDisable = 4,
		isMapMenuRightClickDisable = 10,
		isBattleMenuRightClickDisable = 11,
		isHeroineUnFollowRightClickBlock = 12,
		totalMapUtageIsPlaying = 6,
		isRequiedUtageResume = 8,
		isBackBeforeWorldMap = 9,
		isUtageToWorldMapInDoor = 13,
		isNewMapNotice = 1,
		isNewRecipeNotice = 2,
		isShopBuy = 3,
		isSexEnd = 5
	}

	public Type type;

	private bool result;

	public StateLink trueLink;

	public StateLink falseLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		switch (type)
		{
		case Type.isWorldMapToUtage:
			result = PlayerNonSaveDataManager.isWorldMapToUtage;
			break;
		case Type.isWorldMapToInDoor:
			result = PlayerNonSaveDataManager.isWorldMapToInDoor;
			break;
		case Type.isWorldMapPointDisable:
			result = PlayerNonSaveDataManager.isWorldMapPointDisable;
			break;
		case Type.isMapMenuRightClickDisable:
			result = PlayerNonSaveDataManager.isMapMenuRightClickDisable;
			break;
		case Type.isBattleMenuRightClickDisable:
			result = PlayerNonSaveDataManager.isBattleMenuRightClickDisable;
			break;
		case Type.isHeroineUnFollowRightClickBlock:
			result = PlayerNonSaveDataManager.isHeroineUnFollowRightClickBlock;
			break;
		case Type.totalMapUtageIsPlaying:
			result = PlayerNonSaveDataManager.totalMapUtageIsPlaying;
			break;
		case Type.isRequiedUtageResume:
			result = PlayerNonSaveDataManager.isRequiedUtageResume;
			break;
		case Type.isBackBeforeWorldMap:
			result = PlayerNonSaveDataManager.isBackBeforeWorldMap;
			break;
		case Type.isUtageToWorldMapInDoor:
			result = PlayerNonSaveDataManager.isUtageToWorldMapInDoor;
			break;
		case Type.isNewMapNotice:
			result = PlayerDataManager.isNewMapNotice;
			break;
		case Type.isNewRecipeNotice:
			result = PlayerDataManager.isNewRecipeNotice;
			break;
		case Type.isShopBuy:
			result = PlayerNonSaveDataManager.isShopBuy;
			break;
		case Type.isSexEnd:
			result = PlayerNonSaveDataManager.isSexEnd;
			break;
		}
		if (result)
		{
			Transition(trueLink);
		}
		else
		{
			Transition(falseLink);
		}
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
	}

	public override void OnStateLateUpdate()
	{
	}
}
