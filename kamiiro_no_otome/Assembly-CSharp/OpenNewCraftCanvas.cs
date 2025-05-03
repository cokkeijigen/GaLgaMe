using Arbor;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class OpenNewCraftCanvas : StateBehaviour
{
	private CraftManager craftManager;

	private NewCraftCanvasManager newCraftCanvasManager;

	private CraftAddOnManager craftAddOnManager;

	public StateLink stateLink;

	public StateLink campKitLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		newCraftCanvasManager = GameObject.Find("New Craft Manager").GetComponent<NewCraftCanvasManager>();
		craftAddOnManager = GameObject.Find("Craft AddOn Manager").GetComponent<CraftAddOnManager>();
	}

	public override void OnStateBegin()
	{
		GameObject[] itemCategoryTabGoArray = craftManager.itemCategoryTabGoArray;
		for (int i = 0; i < itemCategoryTabGoArray.Length; i++)
		{
			itemCategoryTabGoArray[i].GetComponent<Image>().sprite = craftManager.selectTabSpriteArray[0];
		}
		craftManager.itemCategoryTabGoArray[craftManager.selectCategoryNum].GetComponent<Image>().sprite = craftManager.selectTabSpriteArray[1];
		newCraftCanvasManager.newCraftNeedItemFrameGo.SetActive(value: true);
		craftAddOnManager.selectedMagicMatrialID[0] = 0;
		craftAddOnManager.selectedMagicMatrialID[1] = 0;
		newCraftCanvasManager.craftQuantity = 1;
		newCraftCanvasManager.multiNumText.text = "1";
		switch (craftManager.selectCategoryNum)
		{
		case 0:
		case 1:
			craftManager.itemSerectScrollSummaryGoArray[0].SetActive(value: true);
			craftManager.itemSerectScrollSummaryGoArray[1].SetActive(value: true);
			break;
		case 2:
		case 3:
		case 4:
			craftManager.itemSerectScrollSummaryGoArray[0].SetActive(value: false);
			craftManager.itemSerectScrollSummaryGoArray[1].SetActive(value: true);
			break;
		}
		switch (craftManager.selectCategoryNum)
		{
		case 0:
			craftManager.itemSerectScrollSummaryGoArray[0].GetComponent<Localize>().Term = "statusAttack";
			break;
		case 1:
			craftManager.itemSerectScrollSummaryGoArray[0].GetComponent<Localize>().Term = "statusDefense";
			break;
		}
		if (craftManager.selectCategoryNum == 4)
		{
			Transition(campKitLink);
		}
		else
		{
			Transition(stateLink);
		}
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
