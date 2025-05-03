using System.Collections.Generic;
using System.Linq;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class NewCraftCanvasManager : MonoBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	public GameObject newCraftNeedItemFrameGo;

	public GameObject[] newCraftNeedItemGoArray;

	public GameObject[] infoSummaryFrameGoArray;

	public GameObject[] infoSummaryIconGoArray;

	public Localize[] infoSummaryTextLocArray;

	public Text[] infoSummaryNumTextArray;

	public CanvasGroup newCraftApplyButton;

	public Sprite noSelectItemImageSprite;

	public GameObject newCraftImpossibleFrameGo;

	public int craftQuantity;

	public Text multiNumText;

	public bool isNewCraftImpossible;

	private void Awake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
	}

	public void PushPlusButton(int addNum)
	{
		craftQuantity += addNum;
		craftQuantity = Mathf.Clamp(craftQuantity, 1, 999);
		multiNumText.text = craftQuantity.ToString();
		int count = craftCanvasManager.itemCanMakeMaterialData.needMaterialList.Count;
		for (int i = 0; i < count; i++)
		{
			craftCanvasManager.SetRecipeContent(i, craftQuantity);
		}
		craftCanvasManager.AdjustApplyButtonActive_NEW();
	}

	public void PushMinusButton(int subtractNum)
	{
		craftQuantity -= subtractNum;
		craftQuantity = Mathf.Clamp(craftQuantity, 1, 999);
		multiNumText.text = craftQuantity.ToString();
		int count = craftCanvasManager.itemCanMakeMaterialData.needMaterialList.Count;
		for (int i = 0; i < count; i++)
		{
			craftCanvasManager.SetRecipeContent(i, craftQuantity);
		}
		craftCanvasManager.AdjustApplyButtonActive_NEW();
	}

	public void PushLimitCraftButton()
	{
		List<NeedMaterialData> needMaterialList = craftCanvasManager.itemCanMakeMaterialData.needMaterialList;
		int count = needMaterialList.Count;
		int[] array = new int[count];
		for (int i = 0; i < count; i++)
		{
			array[i] = GetCraftEnableCount(needMaterialList[i].itemID, needMaterialList[i].needNum);
		}
		craftQuantity = array.Min();
		multiNumText.text = craftQuantity.ToString();
		for (int j = 0; j < count; j++)
		{
			craftCanvasManager.SetRecipeContent(j, craftQuantity);
		}
		craftCanvasManager.AdjustApplyButtonActive_NEW();
	}

	private int GetCraftEnableCount(int itemID, int needNum)
	{
		return PlayerInventoryDataManager.haveItemMaterialList.Find((HaveItemData data) => data.itemID == itemID).haveCountNum / needNum;
	}

	public void PushResetButton()
	{
		craftQuantity = 1;
		multiNumText.text = craftQuantity.ToString();
		int count = craftCanvasManager.itemCanMakeMaterialData.needMaterialList.Count;
		for (int i = 0; i < count; i++)
		{
			craftCanvasManager.SetRecipeContent(i, craftQuantity);
		}
		craftCanvasManager.AdjustApplyButtonActive_NEW();
	}

	public void EnterItemSelectGroupApplyButton()
	{
		switch (PlayerNonSaveDataManager.selectCraftCanvasName)
		{
		case "newCraft":
			if (isNewCraftImpossible)
			{
				newCraftImpossibleFrameGo.SetActive(value: true);
			}
			break;
		case "merge":
			break;
		}
	}

	public void ItemSelectAdjustApplyButtonActive_NEW()
	{
		bool[] requiredItemENOUGH = craftManager.requiredItemENOUGH;
		bool num = requiredItemENOUGH[0] && requiredItemENOUGH[1] && requiredItemENOUGH[2] && requiredItemENOUGH[3];
		CanvasGroup component = craftCanvasManager.craftStartButtonGo.GetComponent<CanvasGroup>();
		CanvasGroup component2 = craftCanvasManager.applyButtonGroup[0].GetComponent<CanvasGroup>();
		if (num && !craftCanvasManager.isUniqueItem && craftCanvasManager.isWorkShopLvEnough && craftCanvasManager.isFurnaceLvEnough)
		{
			component.alpha = 1f;
			component.interactable = true;
			component2.alpha = 1f;
			component2.interactable = true;
		}
		else
		{
			component.alpha = 0.5f;
			component.interactable = false;
			component2.alpha = 0.5f;
			component2.interactable = false;
		}
		if (craftCanvasManager.isUniqueItem)
		{
			switch (craftManager.selectCategoryNum)
			{
			case 3:
				craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonNewCraftCreated";
				break;
			case 4:
				craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonNewCraftHaved";
				break;
			}
		}
		else if (craftCanvasManager.isWorkShopLvEnough && craftCanvasManager.isFurnaceLvEnough)
		{
			switch (craftManager.selectCategoryNum)
			{
			case 0:
			case 1:
			case 2:
				if (component.interactable)
				{
					craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonNewCraftDetail";
				}
				else
				{
					craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonNewCraftMaterialShotage";
				}
				break;
			case 3:
			case 4:
				if (component.interactable)
				{
					craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonNewCraftStart";
				}
				else
				{
					craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonNewCraftMaterialShotage";
				}
				break;
			}
		}
		else
		{
			craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonNewCraftDetailImpossible";
		}
	}

	public void ItemSelectApplyButtonDisable_NEW()
	{
		CanvasGroup component = craftCanvasManager.craftStartButtonGo.GetComponent<CanvasGroup>();
		CanvasGroup component2 = craftCanvasManager.applyButtonGroup[0].GetComponent<CanvasGroup>();
		component.alpha = 0.5f;
		component.interactable = false;
		component2.alpha = 0.5f;
		component2.interactable = false;
		craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonNewCraftDetailImpossible";
	}
}
