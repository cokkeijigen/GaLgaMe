namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Static")]
	public class SetStaticIntAction : FsmStateAction
	{
		public enum Type
		{
			playerHaveMoney = 0,
			timeZone = 1,
			heroineFollowNum = 2,
			selectSlotPageNum = 3,
			selectSlotNum = 6,
			mapPlaceStatusNum = 4,
			openUiScreenLevel = 8,
			playerLibido = 5,
			sexHeroineId = 7,
			sendHelpMarkButtonIndex = 9
		}

		public Type type;

		[RequiredField]
		public FsmInt setIntValue;

		public override void Reset()
		{
			type = Type.playerHaveMoney;
			setIntValue = 0;
		}

		public override void OnEnter()
		{
			switch (type)
			{
			case Type.playerHaveMoney:
				PlayerDataManager.playerHaveMoney = setIntValue.Value;
				break;
			case Type.timeZone:
				PlayerDataManager.currentTimeZone = setIntValue.Value;
				break;
			case Type.selectSlotPageNum:
				PlayerNonSaveDataManager.selectSlotPageNum = setIntValue.Value;
				break;
			case Type.selectSlotNum:
				PlayerNonSaveDataManager.selectSlotNum = setIntValue.Value;
				break;
			case Type.mapPlaceStatusNum:
				PlayerDataManager.mapPlaceStatusNum = setIntValue.Value;
				break;
			case Type.openUiScreenLevel:
				PlayerNonSaveDataManager.openUiScreenLevel = setIntValue.Value;
				break;
			case Type.playerLibido:
				PlayerDataManager.playerLibido = setIntValue.Value;
				break;
			case Type.heroineFollowNum:
				PlayerDataManager.DungeonHeroineFollowNum = setIntValue.Value;
				break;
			case Type.sexHeroineId:
				PlayerNonSaveDataManager.selectSexBattleHeroineId = setIntValue.Value;
				break;
			case Type.sendHelpMarkButtonIndex:
				PlayerNonSaveDataManager.sendHelpMarkButtonIndex = setIntValue.Value;
				break;
			}
			Finish();
		}
	}
}
