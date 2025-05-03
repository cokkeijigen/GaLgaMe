using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class DisplayNewCraftRecipeContent : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private CraftAddOnManager craftAddOnManager;

	private CraftTalkManager craftTalkManager;

	private NewCraftCanvasManager newCraftCanvasManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
		craftAddOnManager = GameObject.Find("Craft AddOn Manager").GetComponent<CraftAddOnManager>();
		craftTalkManager = GameObject.Find("Craft Talk Manager").GetComponent<CraftTalkManager>();
		newCraftCanvasManager = GameObject.Find("New Craft Manager").GetComponent<NewCraftCanvasManager>();
	}

	public override void OnStateBegin()
	{
		newCraftCanvasManager.craftQuantity = 1;
		newCraftCanvasManager.multiNumText.text = "1";
		newCraftCanvasManager.newCraftNeedItemGoArray[3].SetActive(value: true);
		int num = 0;
		int num2 = 0;
		int quantity = 1;
		switch (craftManager.selectCategoryNum)
		{
		case 0:
		{
			num = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.clickedItemID).needMaterialNewerList.Count;
			num2 = 0;
			for (int m = 0; m < num; m++)
			{
				craftCanvasManager.SetRecipeContent(m, quantity);
				num2++;
			}
			for (int n = num2; n < 4; n++)
			{
				newCraftCanvasManager.newCraftNeedItemGoArray[n].transform.Find("Group").gameObject.SetActive(value: false);
				craftManager.requiredItemENOUGH[n] = true;
			}
			newCraftCanvasManager.ItemSelectAdjustApplyButtonActive_NEW();
			SetInfoWindowType(0);
			if (!PlayerFlagDataManager.enableNewCraftFlagDictionary[craftManager.clickedItemID])
			{
				SetSelectItemInfoImpossible();
			}
			break;
		}
		case 1:
		{
			num = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.clickedItemID).needMaterialNewerList.Count;
			num2 = 0;
			for (int num3 = 0; num3 < num; num3++)
			{
				craftCanvasManager.SetRecipeContent(num3, quantity);
				num2++;
			}
			for (int num4 = num2; num4 < 4; num4++)
			{
				newCraftCanvasManager.newCraftNeedItemGoArray[num4].transform.Find("Group").gameObject.SetActive(value: false);
				craftManager.requiredItemENOUGH[num4] = true;
			}
			newCraftCanvasManager.ItemSelectAdjustApplyButtonActive_NEW();
			SetInfoWindowType(0);
			if (!PlayerFlagDataManager.enableNewCraftFlagDictionary[craftManager.clickedItemID])
			{
				SetSelectItemInfoImpossible();
			}
			break;
		}
		case 2:
		{
			num = GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Find((ItemCanMakeMaterialData data) => data.itemID == craftManager.clickedItemID).needMaterialList.Count;
			num2 = 0;
			for (int num5 = 0; num5 < num; num5++)
			{
				craftCanvasManager.SetRecipeContent(num5);
				num2++;
			}
			for (int num6 = num2; num6 < 4; num6++)
			{
				newCraftCanvasManager.newCraftNeedItemGoArray[num6].transform.Find("Group").gameObject.SetActive(value: false);
				craftManager.requiredItemENOUGH[num6] = true;
			}
			newCraftCanvasManager.ItemSelectAdjustApplyButtonActive_NEW();
			SetInfoWindowType(1);
			break;
		}
		case 3:
		{
			ItemEventItemData itemEventItemData = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData data) => data.itemID == craftManager.clickedItemID);
			if (itemEventItemData != null)
			{
				num = itemEventItemData.needMaterialList.Count;
				num2 = 0;
				for (int k = 0; k < num; k++)
				{
					craftCanvasManager.SetRecipeContent(k);
					num2++;
				}
				for (int l = num2; l < 4; l++)
				{
					newCraftCanvasManager.newCraftNeedItemGoArray[l].transform.Find("Group").gameObject.SetActive(value: false);
					craftManager.requiredItemENOUGH[l] = true;
				}
				string key2 = itemEventItemData.category.ToString() + itemEventItemData.itemID;
				bool isUniqueItem2 = PlayerFlagDataManager.keyItemFlagDictionary[key2];
				craftCanvasManager.isUniqueItem = isUniqueItem2;
				newCraftCanvasManager.ItemSelectAdjustApplyButtonActive_NEW();
			}
			else
			{
				newCraftCanvasManager.newCraftNeedItemFrameGo.SetActive(value: false);
				newCraftCanvasManager.ItemSelectApplyButtonDisable_NEW();
			}
			SetInfoWindowType(1);
			break;
		}
		case 4:
		{
			ItemCampItemData itemCampItemData = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData data) => data.itemID == craftManager.clickedItemID);
			num = itemCampItemData.needMaterialList.Count;
			num2 = 0;
			for (int i = 0; i < num; i++)
			{
				craftCanvasManager.SetRecipeContent(i);
				num2++;
			}
			for (int j = num2; j < 4; j++)
			{
				newCraftCanvasManager.newCraftNeedItemGoArray[j].transform.Find("Group").gameObject.SetActive(value: false);
				craftManager.requiredItemENOUGH[j] = true;
				Debug.Log("充足状態はtrue：" + itemCampItemData.itemID);
			}
			string key = "campItem" + itemCampItemData.itemID;
			bool isUniqueItem = PlayerFlagDataManager.keyItemFlagDictionary[key];
			craftCanvasManager.isUniqueItem = isUniqueItem;
			newCraftCanvasManager.ItemSelectAdjustApplyButtonActive_NEW();
			SetInfoWindowType(1);
			break;
		}
		}
		craftAddOnManager.MagicMaterialListRefresh();
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

	private void SetInfoWindowType(int typeNum)
	{
		int enableAddOnLv = PlayerCraftStatusManager.craftFacilityDataDictionary["Kingdom1"].enableAddOnLv;
		switch (typeNum)
		{
		case 0:
			if (!craftCanvasManager.isPowerUpCraft && enableAddOnLv > 0)
			{
				craftManager.craftAddOnGoArray[0].SetActive(value: true);
				craftManager.craftAddOnGoArray[1].SetActive(value: false);
			}
			else
			{
				craftManager.craftAddOnGoArray[0].SetActive(value: false);
				craftManager.craftAddOnGoArray[1].SetActive(value: true);
				if (craftCanvasManager.isPowerUpCraft)
				{
					craftManager.addOnLockTextLoc.Term = "craftAddOnLock_Inherit";
				}
				else
				{
					craftManager.addOnLockTextLoc.Term = "craftAddOnLock_Craft";
				}
			}
			craftCanvasManager.craftWindowGoArray[1].SetActive(value: true);
			craftCanvasManager.craftWindowGoArray[2].SetActive(value: true);
			craftCanvasManager.ResetGetFactorInfo();
			craftCanvasManager.craftWindowGoArray[3].SetActive(value: false);
			craftManager.addOnHeaderTextLoc.Term = "itemTypeSummary_addOnMaterial";
			break;
		case 1:
			craftCanvasManager.craftWindowGoArray[1].SetActive(value: false);
			craftCanvasManager.craftWindowGoArray[2].SetActive(value: false);
			switch (craftManager.selectCategoryNum)
			{
			case 2:
				craftCanvasManager.craftWindowGoArray[3].SetActive(value: true);
				break;
			case 3:
			case 4:
				craftCanvasManager.craftWindowGoArray[3].SetActive(value: false);
				break;
			}
			break;
		}
	}

	private void SetSelectItemInfoImpossible()
	{
		newCraftCanvasManager.isNewCraftImpossible = true;
		newCraftCanvasManager.newCraftApplyButton.alpha = 0.5f;
		newCraftCanvasManager.newCraftApplyButton.interactable = false;
		craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonNewCraftDetailLocked";
	}
}
