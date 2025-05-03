namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Static")]
	public class GetStaticIntAction : FsmStateAction
	{
		public enum Type
		{
			playerHaveMoney = 0,
			timeZone = 1,
			localMapHeroineFollowNum = 2,
			selectSlotPageNum = 3,
			selectSlotNum = 6,
			mapPlaceStatusNum = 4,
			openUiScreenLevel = 7,
			playerLibido = 5
		}

		public Type type;

		[RequiredField]
		public FsmInt storeIntValue;

		public override void Reset()
		{
			type = Type.playerHaveMoney;
			storeIntValue = 0;
		}

		public override void OnEnter()
		{
			switch (type)
			{
			case Type.playerHaveMoney:
				storeIntValue.Value = PlayerDataManager.playerHaveMoney;
				break;
			case Type.timeZone:
				storeIntValue.Value = PlayerDataManager.currentTimeZone;
				break;
			case Type.selectSlotPageNum:
				storeIntValue.Value = PlayerNonSaveDataManager.selectSlotPageNum;
				break;
			case Type.selectSlotNum:
				storeIntValue.Value = PlayerNonSaveDataManager.selectSlotNum;
				break;
			case Type.mapPlaceStatusNum:
				storeIntValue.Value = PlayerDataManager.mapPlaceStatusNum;
				break;
			case Type.openUiScreenLevel:
				storeIntValue.Value = PlayerNonSaveDataManager.openUiScreenLevel;
				break;
			case Type.playerLibido:
				storeIntValue.Value = PlayerDataManager.playerLibido;
				break;
			}
			Finish();
		}
	}
}
