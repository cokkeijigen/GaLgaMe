using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class InitializeCraftCanvas : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private NewCraftCanvasManager newCraftCanvasManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
		newCraftCanvasManager = GameObject.Find("New Craft Manager").GetComponent<NewCraftCanvasManager>();
	}

	public override void OnStateBegin()
	{
		craftManager.itemSelectHeaderLocalize.Term = "headerSelectEditItem";
		newCraftCanvasManager.newCraftNeedItemFrameGo.SetActive(value: false);
		craftManager.infoWindowGoArray[0].SetActive(value: true);
		craftManager.infoWindowGoArray[1].SetActive(value: true);
		craftManager.itemCategoryTabGoArray[2].SetActive(value: false);
		craftManager.itemCategoryTabGoArray[3].SetActive(value: false);
		craftManager.itemCategoryTabGoArray[4].SetActive(value: false);
		newCraftCanvasManager.newCraftApplyButton.alpha = 1f;
		newCraftCanvasManager.newCraftApplyButton.interactable = true;
		craftCanvasManager.isPowerUpCraft = false;
		int num = 0;
		string selectCraftCanvasName = PlayerNonSaveDataManager.selectCraftCanvasName;
		if (!(selectCraftCanvasName == "craft"))
		{
			if (selectCraftCanvasName == "merge")
			{
				craftCanvasManager.infoWindowSummaryLocArray[0].Term = "headerOverView";
				craftCanvasManager.infoWindowSummaryLocArray[1].Term = "buttonCraftDetail";
				num = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].enableAddOnLv;
				craftCanvasManager.remainingDaysFrameGo.SetActive(value: false);
				craftCanvasManager.mergeInheritButtonGo.SetActive(value: true);
				craftCanvasManager.craftStartButtonGo.GetComponent<RectTransform>().sizeDelta = new Vector2(334f, 60f);
			}
		}
		else
		{
			craftCanvasManager.infoWindowSummaryLocArray[0].Term = "headerOverView";
			craftCanvasManager.infoWindowSummaryLocArray[1].Term = "buttonCraftDetail";
			num = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].enableAddOnLv;
			craftCanvasManager.remainingDaysFrameGo.SetActive(value: true);
			craftCanvasManager.mergeInheritButtonGo.SetActive(value: false);
			craftCanvasManager.craftStartButtonGo.GetComponent<RectTransform>().sizeDelta = new Vector2(468f, 60f);
		}
		if (num > 0)
		{
			craftManager.craftAddOnGoArray[0].SetActive(value: true);
			craftManager.craftAddOnGoArray[1].SetActive(value: false);
		}
		else
		{
			craftManager.craftAddOnGoArray[0].SetActive(value: false);
			craftManager.craftAddOnGoArray[1].SetActive(value: true);
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
