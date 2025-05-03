namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Static")]
	public class SetStaticBoolAction : FsmStateAction
	{
		public enum Type
		{
			systemSaveEnable = 2,
			isSaveDataLoad = 14,
			isWorldMapVisible = 3,
			isWorldMapToUtage = 4,
			isWorldMapPointDisable = 13,
			isMapMenuRightClickDisable = 17,
			isBattleMenuRightClickDisable = 18,
			isSelectDungeon = 8,
			isNewMapNotice = 5,
			isNewRecipeNotice = 11,
			worldMapInputBlock = 6,
			isCraftWorkShop = 7,
			isShopBuy = 12,
			isClockChangeEnable = 10,
			isGarellyOpenWithTitle = 9,
			isSkillMoveCursor = 15,
			isSexEnd = 16,
			isInterruptedAfterSave = 20,
			isTitleDebugToUtage = 19
		}

		public Type type;

		[RequiredField]
		public FsmBool setBoolValue;

		public override void Reset()
		{
			type = Type.systemSaveEnable;
			setBoolValue = false;
		}

		public override void OnEnter()
		{
			switch (type)
			{
			case Type.systemSaveEnable:
				PlayerNonSaveDataManager.systemSaveEnable = setBoolValue.Value;
				break;
			case Type.isSaveDataLoad:
				PlayerNonSaveDataManager.isSaveDataLoad = setBoolValue.Value;
				break;
			case Type.isWorldMapVisible:
				PlayerNonSaveDataManager.isWorldMapVisibleFlag = setBoolValue.Value;
				break;
			case Type.isWorldMapToUtage:
				PlayerNonSaveDataManager.isWorldMapToUtage = setBoolValue.Value;
				break;
			case Type.isWorldMapPointDisable:
				PlayerNonSaveDataManager.isWorldMapPointDisable = setBoolValue.Value;
				break;
			case Type.isMapMenuRightClickDisable:
				PlayerNonSaveDataManager.isMapMenuRightClickDisable = setBoolValue.Value;
				break;
			case Type.isBattleMenuRightClickDisable:
				PlayerNonSaveDataManager.isBattleMenuRightClickDisable = setBoolValue.Value;
				break;
			case Type.isSelectDungeon:
				PlayerDataManager.isSelectDungeon = setBoolValue.Value;
				break;
			case Type.isNewMapNotice:
				PlayerDataManager.isNewMapNotice = setBoolValue.Value;
				break;
			case Type.isNewRecipeNotice:
				PlayerDataManager.isNewRecipeNotice = setBoolValue.Value;
				break;
			case Type.worldMapInputBlock:
				PlayerDataManager.worldMapInputBlock = setBoolValue.Value;
				break;
			case Type.isCraftWorkShop:
				PlayerNonSaveDataManager.isCraftWorkShop = setBoolValue.Value;
				break;
			case Type.isShopBuy:
				PlayerNonSaveDataManager.isShopBuy = setBoolValue.Value;
				break;
			case Type.isClockChangeEnable:
				PlayerNonSaveDataManager.isClockChangeEnable = setBoolValue.Value;
				break;
			case Type.isGarellyOpenWithTitle:
				PlayerNonSaveDataManager.isGarellyOpenWithTitle = setBoolValue.Value;
				break;
			case Type.isSkillMoveCursor:
				PlayerDataManager.isCaseOfSkillMoveCursor = setBoolValue.Value;
				break;
			case Type.isSexEnd:
				PlayerNonSaveDataManager.isSexEnd = setBoolValue.Value;
				break;
			case Type.isInterruptedAfterSave:
				PlayerNonSaveDataManager.isInterruptedAfterSave = setBoolValue.Value;
				break;
			case Type.isTitleDebugToUtage:
				PlayerNonSaveDataManager.isTitleDebugToUtage = setBoolValue.Value;
				break;
			}
			Finish();
		}
	}
}
