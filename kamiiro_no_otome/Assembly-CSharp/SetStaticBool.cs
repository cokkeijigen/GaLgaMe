using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetStaticBool : StateBehaviour
{
	public enum Type
	{
		isLocalMapHeroineFollow = 1,
		systemSaveEnable = 2,
		isWorldMapVisible = 3,
		isWorldMapToUtage = 4,
		isWorldMapToInDoor = 16,
		isWorldMapPointDisable = 13,
		isMapMenuRightClickDisable = 20,
		isInDoorRest = 22,
		isRefreshLocalMap = 24,
		isBattleMenuRightClickDisable = 21,
		isScenarioBattle = 23,
		isRequiedUtageResume = 17,
		isSelectDungeon = 8,
		isNewMapNotice = 5,
		isNewRecipeNotice = 11,
		worldMapInputBlock = 6,
		totalMapUtageIsPlaying = 15,
		isCraftWorkShop = 7,
		isShopBuy = 12,
		isClockChangeEnable = 10,
		isGarellyOpenWithTitle = 9,
		isSexEnd = 14,
		isSaveDataLoad = 18,
		isInitializeMapData = 19
	}

	public Type type;

	public bool setBoolValue;

	public StateLink nextState;

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
		case Type.systemSaveEnable:
			PlayerNonSaveDataManager.systemSaveEnable = setBoolValue;
			break;
		case Type.isWorldMapVisible:
			PlayerNonSaveDataManager.isWorldMapVisibleFlag = setBoolValue;
			break;
		case Type.isWorldMapToUtage:
			PlayerNonSaveDataManager.isWorldMapToUtage = setBoolValue;
			break;
		case Type.isWorldMapToInDoor:
			PlayerNonSaveDataManager.isWorldMapToInDoor = setBoolValue;
			break;
		case Type.isWorldMapPointDisable:
			PlayerNonSaveDataManager.isWorldMapPointDisable = setBoolValue;
			break;
		case Type.isMapMenuRightClickDisable:
			PlayerNonSaveDataManager.isMapMenuRightClickDisable = setBoolValue;
			break;
		case Type.isScenarioBattle:
			PlayerNonSaveDataManager.isScenarioBattle = setBoolValue;
			break;
		case Type.isInDoorRest:
			PlayerNonSaveDataManager.isInDoorExitLock = setBoolValue;
			break;
		case Type.isRefreshLocalMap:
			PlayerNonSaveDataManager.isRefreshLocalMap = setBoolValue;
			break;
		case Type.isBattleMenuRightClickDisable:
			PlayerNonSaveDataManager.isBattleMenuRightClickDisable = setBoolValue;
			break;
		case Type.totalMapUtageIsPlaying:
			PlayerNonSaveDataManager.totalMapUtageIsPlaying = setBoolValue;
			break;
		case Type.isRequiedUtageResume:
			PlayerNonSaveDataManager.isRequiedUtageResume = setBoolValue;
			break;
		case Type.isSelectDungeon:
			PlayerDataManager.isSelectDungeon = setBoolValue;
			break;
		case Type.isNewMapNotice:
			PlayerDataManager.isNewMapNotice = setBoolValue;
			break;
		case Type.isNewRecipeNotice:
			PlayerDataManager.isNewRecipeNotice = setBoolValue;
			break;
		case Type.worldMapInputBlock:
			PlayerDataManager.worldMapInputBlock = setBoolValue;
			break;
		case Type.isCraftWorkShop:
			PlayerNonSaveDataManager.isCraftWorkShop = setBoolValue;
			break;
		case Type.isShopBuy:
			PlayerNonSaveDataManager.isShopBuy = setBoolValue;
			break;
		case Type.isClockChangeEnable:
			PlayerNonSaveDataManager.isClockChangeEnable = setBoolValue;
			break;
		case Type.isGarellyOpenWithTitle:
			PlayerNonSaveDataManager.isGarellyOpenWithTitle = setBoolValue;
			break;
		case Type.isSexEnd:
			PlayerNonSaveDataManager.isSexEnd = setBoolValue;
			break;
		case Type.isSaveDataLoad:
			PlayerNonSaveDataManager.isSaveDataLoad = setBoolValue;
			break;
		case Type.isInitializeMapData:
			PlayerNonSaveDataManager.isInitializeMapData = setBoolValue;
			break;
		}
		Transition(nextState);
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
