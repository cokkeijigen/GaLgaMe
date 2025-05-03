namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Static")]
	public class GetStaticStringAction : FsmStateAction
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
			resultScenarioName = 15,
			selectAccessPointName = 11,
			selectSystemTabName = 5,
			selectScenarioName = 6,
			selectCraftCanvasName = 7,
			selectExtensionCanvasName = 12,
			selectStatusCanvasName = 8,
			selectInDoorCommandName = 13,
			playBgmCategoryName = 10,
			startSexSceneTypeName = 16
		}

		public Type type;

		[RequiredField]
		public FsmString storeStringValue;

		public override void Reset()
		{
			type = Type.loadSceneName;
			storeStringValue = "";
		}

		public override void OnEnter()
		{
			switch (type)
			{
			case Type.currentAccessPointName:
				storeStringValue.Value = PlayerDataManager.currentAccessPointName;
				break;
			case Type.currentPlaceName:
				storeStringValue.Value = PlayerDataManager.currentPlaceName;
				break;
			case Type.currentDungeonName:
				storeStringValue.Value = PlayerDataManager.currentDungeonName;
				break;
			case Type.loadSceneName:
				storeStringValue.Value = PlayerNonSaveDataManager.loadSceneName;
				break;
			case Type.unLoadSceneName:
				storeStringValue.Value = PlayerNonSaveDataManager.unLoadSceneName;
				break;
			case Type.currentSceneName:
				storeStringValue.Value = PlayerNonSaveDataManager.currentSceneName;
				break;
			case Type.openDialogName:
				storeStringValue.Value = PlayerNonSaveDataManager.openDialogName;
				break;
			case Type.resultScenarioName:
				storeStringValue.Value = PlayerNonSaveDataManager.resultScenarioName;
				break;
			case Type.selectAccessPointName:
				storeStringValue.Value = PlayerNonSaveDataManager.selectAccessPointName;
				break;
			case Type.selectSystemTabName:
				storeStringValue.Value = PlayerNonSaveDataManager.selectSystemTabName;
				break;
			case Type.selectScenarioName:
				storeStringValue.Value = PlayerNonSaveDataManager.selectScenarioName;
				break;
			case Type.selectCraftCanvasName:
				storeStringValue.Value = PlayerNonSaveDataManager.selectCraftCanvasName;
				break;
			case Type.selectExtensionCanvasName:
				storeStringValue.Value = PlayerNonSaveDataManager.selectExtensionCanvasName;
				break;
			case Type.selectStatusCanvasName:
				storeStringValue.Value = PlayerNonSaveDataManager.selectStatusCanvasName;
				break;
			case Type.selectInDoorCommandName:
				storeStringValue.Value = PlayerNonSaveDataManager.inDoorCommandName;
				break;
			case Type.playBgmCategoryName:
				storeStringValue.Value = PlayerDataManager.playBgmCategoryName;
				break;
			case Type.startSexSceneTypeName:
				storeStringValue.Value = PlayerNonSaveDataManager.startSexSceneTypeName;
				break;
			}
			Finish();
		}
	}
}
