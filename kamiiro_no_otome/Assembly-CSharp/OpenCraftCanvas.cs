using Arbor;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class OpenCraftCanvas : StateBehaviour
{
	private CraftManager craftManager;

	private NewCraftCanvasManager newCraftCanvasManager;

	private CraftAddOnManager craftAddOnManager;

	public StateLink stateLink;

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
		craftManager.itemSerectScrollSummaryGoArray[0].SetActive(value: true);
		switch (craftManager.selectCategoryNum)
		{
		case 0:
			craftManager.itemSerectScrollSummaryGoArray[0].GetComponent<Localize>().Term = "statusAttack";
			break;
		case 1:
			craftManager.itemSerectScrollSummaryGoArray[0].GetComponent<Localize>().Term = "statusDefense";
			break;
		}
		craftAddOnManager.selectedMagicMatrialID[0] = 0;
		craftAddOnManager.selectedMagicMatrialID[1] = 0;
		newCraftCanvasManager.craftQuantity = 1;
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
