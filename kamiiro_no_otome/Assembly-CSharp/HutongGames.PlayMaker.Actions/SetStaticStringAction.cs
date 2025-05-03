namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Static")]
	public class SetStaticStringAction : FsmStateAction
	{
		public enum Type
		{
			currentAccessPointName = 0,
			currentPlaceName = 1,
			currentDungeonName = 9,
			loadSceneName = 2,
			unLoadSceneName = 3,
			currentSceneName = 4,
			openDialogName = 14,
			selectAccessPointName = 11,
			selectSystemTabName = 5,
			selectScenarioName = 6,
			selectCraftCanvasName = 7,
			selectExtensionCanvasName = 12,
			selectStatusCanvasName = 8,
			selectInDoorCommandName = 13,
			playBgmCategoryName = 10
		}

		public Type type;

		public string setStringValue;

		public override void Reset()
		{
			type = Type.loadSceneName;
			setStringValue = "";
		}

		public override void OnEnter()
		{
			switch (type)
			{
			case Type.currentAccessPointName:
				PlayerDataManager.currentAccessPointName = setStringValue;
				break;
			case Type.currentPlaceName:
				PlayerDataManager.currentPlaceName = setStringValue;
				break;
			case Type.currentDungeonName:
				PlayerDataManager.currentDungeonName = setStringValue;
				break;
			case Type.loadSceneName:
				PlayerNonSaveDataManager.loadSceneName = setStringValue;
				break;
			case Type.unLoadSceneName:
				PlayerNonSaveDataManager.unLoadSceneName = setStringValue;
				break;
			case Type.currentSceneName:
				PlayerNonSaveDataManager.currentSceneName = setStringValue;
				break;
			case Type.openDialogName:
				PlayerNonSaveDataManager.openDialogName = setStringValue;
				break;
			case Type.selectAccessPointName:
				PlayerNonSaveDataManager.selectAccessPointName = setStringValue;
				break;
			case Type.selectSystemTabName:
				PlayerNonSaveDataManager.selectSystemTabName = setStringValue;
				break;
			case Type.selectScenarioName:
				PlayerNonSaveDataManager.selectScenarioName = setStringValue;
				break;
			case Type.selectCraftCanvasName:
				PlayerNonSaveDataManager.selectCraftCanvasName = setStringValue;
				break;
			case Type.selectExtensionCanvasName:
				PlayerNonSaveDataManager.selectExtensionCanvasName = setStringValue;
				break;
			case Type.selectStatusCanvasName:
				PlayerNonSaveDataManager.selectStatusCanvasName = setStringValue;
				break;
			case Type.selectInDoorCommandName:
				PlayerNonSaveDataManager.inDoorCommandName = setStringValue;
				break;
			case Type.playBgmCategoryName:
				PlayerDataManager.playBgmCategoryName = setStringValue;
				break;
			}
			Finish();
		}
	}
}
