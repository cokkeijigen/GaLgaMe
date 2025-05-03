using Arbor;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class OpenCraftCanvasBranch : StateBehaviour
{
	private CraftManager craftManager;

	private CraftAddOnManager craftAddOnManager;

	private CraftTalkManager craftTalkManager;

	private CraftUiManager craftUiManager;

	private InDoorTalkManager inDoorTalkManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GetComponent<CraftManager>();
		craftAddOnManager = GameObject.Find("Craft AddOn Manager").GetComponent<CraftAddOnManager>();
		craftTalkManager = GameObject.Find("Craft Talk Manager").GetComponent<CraftTalkManager>();
		craftUiManager = GameObject.Find("Craft Dialog Manager").GetComponent<CraftUiManager>();
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
	}

	public override void OnStateBegin()
	{
		craftManager.selectCategoryNum = 0;
		craftManager.clickedItemID = 0;
		craftManager.selectScrollContentIndex = 0;
		craftManager.clickedUniqueID = int.MaxValue;
		craftAddOnManager.selectedMagicMatrialID[0] = 0;
		craftAddOnManager.selectedMagicMatrialID[1] = 0;
		inDoorTalkManager.exitButtonCanvasGroup.gameObject.SetActive(value: false);
		inDoorTalkManager.commandButtonGroupGo.SetActive(value: false);
		inDoorTalkManager.carriageBgGroup.SetActive(value: true);
		bool num = PlayerFlagDataManager.scenarioFlagDictionary[GameDataManager.instance.characterStatusDataBase.characterStatusDataList[2].characterPowerUpFlag];
		bool flag = PlayerFlagDataManager.scenarioFlagDictionary[GameDataManager.instance.characterStatusDataBase.characterStatusDataList[3].characterPowerUpFlag];
		if (num || flag)
		{
			craftManager.mergeButton.SetActive(value: true);
		}
		else
		{
			craftManager.mergeButton.SetActive(value: false);
		}
		Localize component = craftManager.itemSerectScrollSummaryGoArray[1].GetComponent<Localize>();
		switch (PlayerNonSaveDataManager.selectCraftCanvasName)
		{
		case "blacksmith":
			craftUiManager.uiVisibleButtonGo.SetActive(value: true);
			craftManager.blackSmithButtonGroup.SetActive(value: true);
			craftTalkManager.characterCanvasGo.SetActive(value: false);
			craftUiManager.craftBookGo.SetActive(value: false);
			craftUiManager.craftLvWindowGo.SetActive(value: false);
			craftUiManager.exitButtonTextLoc.Term = "buttonBack";
			craftManager.craftCommandTypeLoc.Term = "buttonBlacksmith";
			break;
		case "craft":
			craftUiManager.uiVisibleButtonGo.SetActive(value: false);
			craftManager.blackSmithButtonGroup.SetActive(value: false);
			craftTalkManager.characterCanvasGo.SetActive(value: true);
			craftUiManager.craftBookGo.SetActive(value: true);
			craftUiManager.craftLvWindowGo.SetActive(value: true);
			craftUiManager.exitButtonTextLoc.Term = "buttonBackCarriage";
			craftManager.craftCommandTypeLoc.Term = "buttonCraft";
			craftManager.craftAndMergeFSM.SendTrigger("OpenCraftCanvas");
			component.Term = "summaryEnhanceCount";
			break;
		case "newCraft":
			craftUiManager.uiVisibleButtonGo.SetActive(value: false);
			craftManager.blackSmithButtonGroup.SetActive(value: false);
			craftTalkManager.characterCanvasGo.SetActive(value: true);
			craftUiManager.craftBookGo.SetActive(value: true);
			craftUiManager.craftLvWindowGo.SetActive(value: true);
			if (PlayerNonSaveDataManager.isTutorialOpened)
			{
				craftUiManager.exitButtonTextLoc.Term = "buttonBack";
				Debug.Log("クラフトチュートリアルの新規作成");
			}
			else
			{
				craftUiManager.exitButtonTextLoc.Term = "buttonBackCarriage";
			}
			craftManager.craftCommandTypeLoc.Term = "buttonNewCraftMini";
			craftManager.newCraftFSM.SendTrigger("OpenNewCraftCanvas");
			component.Term = "summaryHaveCount";
			break;
		case "merge":
			craftUiManager.uiVisibleButtonGo.SetActive(value: false);
			craftManager.blackSmithButtonGroup.SetActive(value: false);
			craftTalkManager.characterCanvasGo.SetActive(value: true);
			craftUiManager.craftBookGo.SetActive(value: true);
			craftUiManager.craftLvWindowGo.SetActive(value: true);
			craftUiManager.exitButtonTextLoc.Term = "buttonBackCarriage";
			craftManager.craftCommandTypeLoc.Term = "buttonMerge";
			craftManager.craftAndMergeFSM.SendTrigger("OpenCraftCanvas");
			component.Term = "summaryCharacter";
			break;
		}
		Transition(stateLink);
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
