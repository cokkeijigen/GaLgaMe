namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Static")]
	public class GetStaticBoolAction : FsmStateAction
	{
		public enum Type
		{
			systemSaveEnable = 2,
			isWorldMapVisible = 3,
			isWorldMapToUtage = 4,
			isWorldMapPointDisable = 13,
			isMapMenuRightClickDisable = 18,
			isBattleMenuRightClickDisable = 19,
			isHeroineUnFollowRightClickBlock = 20,
			isSelectDungeon = 8,
			isDungeonMapAuto = 16,
			isScenarioBattle = 21,
			isNewMapNotice = 5,
			isNewRecipeNotice = 11,
			worldMapInputBlock = 6,
			isCraftWorkShop = 7,
			isShopBuy = 12,
			isClockChangeEnable = 10,
			isGarellyOpenWithTitle = 9,
			isUtageToJumpFromGarelly = 14,
			isSkillMoveCursor = 15,
			isSexEnd = 17,
			isInterruptedAfterSave = 22
		}

		public Type type;

		[RequiredField]
		public FsmBool storeBoolValue;

		public override void Reset()
		{
			type = Type.systemSaveEnable;
			storeBoolValue = false;
		}

		public override void OnEnter()
		{
			switch (type)
			{
			case Type.systemSaveEnable:
				storeBoolValue.Value = PlayerNonSaveDataManager.systemSaveEnable;
				break;
			case Type.isWorldMapVisible:
				storeBoolValue.Value = PlayerNonSaveDataManager.isWorldMapVisibleFlag;
				break;
			case Type.isWorldMapToUtage:
				storeBoolValue.Value = PlayerNonSaveDataManager.isWorldMapToUtage;
				break;
			case Type.isWorldMapPointDisable:
				storeBoolValue.Value = PlayerNonSaveDataManager.isWorldMapPointDisable;
				break;
			case Type.isMapMenuRightClickDisable:
				storeBoolValue.Value = PlayerNonSaveDataManager.isMapMenuRightClickDisable;
				break;
			case Type.isBattleMenuRightClickDisable:
				storeBoolValue.Value = PlayerNonSaveDataManager.isBattleMenuRightClickDisable;
				break;
			case Type.isHeroineUnFollowRightClickBlock:
				storeBoolValue.Value = PlayerNonSaveDataManager.isHeroineUnFollowRightClickBlock;
				break;
			case Type.isSelectDungeon:
				storeBoolValue.Value = PlayerDataManager.isSelectDungeon;
				break;
			case Type.isDungeonMapAuto:
				storeBoolValue.Value = PlayerDataManager.isDungeonMapAuto;
				break;
			case Type.isScenarioBattle:
				storeBoolValue.Value = PlayerNonSaveDataManager.isScenarioBattle;
				break;
			case Type.isNewMapNotice:
				storeBoolValue.Value = PlayerDataManager.isNewMapNotice;
				break;
			case Type.isNewRecipeNotice:
				storeBoolValue.Value = PlayerDataManager.isNewRecipeNotice;
				break;
			case Type.worldMapInputBlock:
				storeBoolValue.Value = PlayerDataManager.worldMapInputBlock;
				break;
			case Type.isCraftWorkShop:
				storeBoolValue.Value = PlayerNonSaveDataManager.isCraftWorkShop;
				break;
			case Type.isShopBuy:
				storeBoolValue.Value = PlayerNonSaveDataManager.isShopBuy;
				break;
			case Type.isClockChangeEnable:
				storeBoolValue.Value = PlayerNonSaveDataManager.isClockChangeEnable;
				break;
			case Type.isGarellyOpenWithTitle:
				storeBoolValue.Value = PlayerNonSaveDataManager.isGarellyOpenWithTitle;
				break;
			case Type.isUtageToJumpFromGarelly:
				storeBoolValue.Value = PlayerNonSaveDataManager.isUtageToJumpFromGarelly;
				break;
			case Type.isSkillMoveCursor:
				storeBoolValue.Value = PlayerDataManager.isCaseOfSkillMoveCursor;
				break;
			case Type.isSexEnd:
				storeBoolValue.Value = PlayerNonSaveDataManager.isSexEnd;
				break;
			case Type.isInterruptedAfterSave:
				storeBoolValue.Value = PlayerNonSaveDataManager.isInterruptedAfterSave;
				break;
			}
			Finish();
		}
	}
}
