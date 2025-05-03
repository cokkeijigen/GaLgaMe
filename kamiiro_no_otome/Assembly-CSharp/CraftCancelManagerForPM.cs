using DG.Tweening;
using UnityEngine;
using Utage;

public class CraftCancelManagerForPM : MonoBehaviour
{
	private CraftManager craftManager;

	private CraftTalkManager craftTalkManager;

	private CraftAddOnManager craftAddOnManager;

	private HeaderStatusManager headerStatusManager;

	private InDoorTalkManager inDoorTalkManager;

	public void Awake()
	{
		craftManager = GetComponentInParent<CraftManager>();
		craftTalkManager = GameObject.Find("Craft Talk Manager").GetComponent<CraftTalkManager>();
		craftAddOnManager = GameObject.Find("Craft AddOn Manager").GetComponent<CraftAddOnManager>();
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
		if (!PlayerNonSaveDataManager.isRequiedUtageResume)
		{
			headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		}
	}

	public bool CheckHeroineUnFollow()
	{
		return PlayerNonSaveDataManager.isHeroineUnFollowRightClickBlock;
	}

	public bool CheckRequiedUtageResume()
	{
		return PlayerNonSaveDataManager.isRequiedUtageResume;
	}

	public void CloseOverlayCanvas()
	{
		craftTalkManager.TalkBalloonItemSelectAfter();
		craftAddOnManager.overlayCanvas.SetActive(value: false);
		Debug.Log("オーバーレイを閉じる");
	}

	public void ReOpenCraftCanvas()
	{
		PlayerNonSaveDataManager.selectCraftCanvasName = "blacksmith";
		craftManager.arborFSM.SendTrigger("ReOpenCraftCanvas");
	}

	public void CloseNeedMaterialCanvas()
	{
		craftTalkManager.TalkBalloonItemSelect();
		craftManager.canvasGroupArray[0].gameObject.SetActive(value: true);
		craftManager.canvasGroupArray[1].gameObject.SetActive(value: false);
		string selectCraftCanvasName = PlayerNonSaveDataManager.selectCraftCanvasName;
		if (!(selectCraftCanvasName == "craft"))
		{
			if (selectCraftCanvasName == "merge")
			{
				craftManager.craftCommandTypeLoc.Term = "buttonMerge";
			}
		}
		else
		{
			craftManager.craftCommandTypeLoc.Term = "buttonCraft";
		}
		craftAddOnManager.selectedMagicMatrialID[0] = 0;
		craftAddOnManager.selectedMagicMatrialID[1] = 0;
		craftAddOnManager.MagicMaterialListRefresh();
		Debug.Log("必要素材を閉じる");
	}

	public void PreCloseCraftUI()
	{
		if (!PlayerNonSaveDataManager.isRequiedUtageResume)
		{
			DOTweenModuleUI.DOSizeDelta(endValue: new Vector2(490f, 80f), target: headerStatusManager.placePanelGo.GetComponent<RectTransform>(), duration: 0.1f);
			headerStatusManager.placePanelGo.SetActive(value: true);
			headerStatusManager.shopRankGroupGo.SetActive(value: true);
			headerStatusManager.clockGroupGo.SetActive(value: true);
			headerStatusManager.moneyPanelGo.gameObject.SetActive(value: true);
			headerStatusManager.headerFSM.SendTrigger("HeaderStatusRefresh");
			inDoorTalkManager.exitButtonCanvasGroup.gameObject.SetActive(value: true);
			inDoorTalkManager.commandButtonGroupGo.SetActive(value: true);
			inDoorTalkManager.talkFSM.SendTrigger("RefreshInDoorAfterRest");
			inDoorTalkManager.carriageBgGroup.SetActive(value: false);
			inDoorTalkManager.SetExitButtonFsmEnable(value: true);
			inDoorTalkManager.commandButtonGroupGo.GetComponent<CanvasGroup>().interactable = true;
		}
		else
		{
			PlayerNonSaveDataManager.isRequiedUtageResume = false;
			GameObject.Find("AdvEngine").GetComponent<AdvEngine>().ResumeScenario();
			Debug.Log("宴リジューム");
		}
	}
}
