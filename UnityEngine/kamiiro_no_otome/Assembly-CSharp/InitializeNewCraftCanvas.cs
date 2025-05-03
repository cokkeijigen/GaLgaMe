using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class InitializeNewCraftCanvas : StateBehaviour
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
		craftManager.itemSelectHeaderLocalize.Term = "headerSelectRecipe";
		craftCanvasManager.mergeInheritButtonGo.SetActive(value: false);
		craftCanvasManager.craftStartButtonGo.GetComponent<RectTransform>().sizeDelta = new Vector2(680f, 60f);
		craftCanvasManager.remainingDaysFrameGo.SetActive(value: false);
		craftManager.infoWindowGoArray[0].SetActive(value: false);
		craftManager.infoWindowGoArray[1].SetActive(value: false);
		craftManager.itemCategoryTabGoArray[2].SetActive(value: true);
		craftManager.itemCategoryTabGoArray[3].SetActive(value: true);
		craftManager.itemCategoryTabGoArray[4].SetActive(value: true);
		craftCanvasManager.isPowerUpCraft = false;
		craftCanvasManager.isAutoEvolutionCraft = false;
		craftCanvasManager.isCompleteEnhanceCount = false;
		craftCanvasManager.isRemainingDaysZero = false;
		if (PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].enableAddOnLv > 0)
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
