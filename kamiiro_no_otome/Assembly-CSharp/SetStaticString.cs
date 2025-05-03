using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetStaticString : StateBehaviour
{
	public enum Type
	{
		currentAccessPointName = 0,
		currentPlaceName = 1,
		currentDungeonName = 9,
		loadSceneName = 2,
		unLoadSceneName = 3,
		currentSceneName = 4,
		selectAccessPointName = 11,
		selectDisableMapPointTerm = 15,
		selectSystemTabName = 5,
		selectScenarioName = 6,
		selectCraftCanvasName = 7,
		selectExtensionCanvasName = 12,
		selectStatusCanvasName = 8,
		selectInDoorCommandName = 13,
		playBgmCategoryName = 10,
		playInDoorBgmCategoryName = 16,
		openDialogName = 14
	}

	public Type type;

	public string setStringValue;

	private PlayMakerFSM bgmManegerFSM;

	public StateLink nextState;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		bgmManegerFSM = GameObject.Find("Bgm Play Manager").GetComponent<PlayMakerFSM>();
	}

	public override void OnStateBegin()
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
		case Type.selectAccessPointName:
			PlayerNonSaveDataManager.selectAccessPointName = setStringValue;
			break;
		case Type.selectDisableMapPointTerm:
			PlayerNonSaveDataManager.selectDisableMapPointTerm = setStringValue;
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
			bgmManegerFSM.SendEvent("ChangeMasterAudioPlaylist");
			break;
		case Type.playInDoorBgmCategoryName:
			PlayerDataManager.playBgmCategoryName = setStringValue;
			bgmManegerFSM.SendEvent("ChangeInDoorMasterAudioPlaylist");
			break;
		case Type.openDialogName:
			PlayerNonSaveDataManager.openDialogName = setStringValue;
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
