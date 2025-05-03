using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class OpenCarrigeSummaryWindow : StateBehaviour
{
	private CarriageManager carriageManager;

	private CarriageTalkManager carriageTalkManager;

	private ItemWeaponData itemWeaponData;

	private ItemArmorData itemArmorData;

	private HaveWeaponData haveWeaponData;

	private HaveArmorData haveArmorData;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		carriageManager = GameObject.Find("Carriage Manager").GetComponent<CarriageManager>();
		carriageTalkManager = GameObject.Find("Carriage Talk Manager").GetComponent<CarriageTalkManager>();
	}

	public override void OnStateBegin()
	{
		switch (carriageManager.selectCategoryNum)
		{
		case 0:
		{
			itemWeaponData = null;
			itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == carriageManager.clickedItemID);
			haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == carriageManager.clickedItemID && data.itemUniqueID == carriageManager.clickedUniqueID);
			ParameterContainer summaryParameterContainer2 = carriageManager.summaryParameterContainer;
			summaryParameterContainer2.GetVariable<UguiImage>("iconImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemWeaponData.category.ToString()];
			string text2 = itemWeaponData.category.ToString() + itemWeaponData.itemID;
			summaryParameterContainer2.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = text2;
			if (haveWeaponData.isItemStoreDisplay)
			{
				carriageManager.itemLockButtonImage.gameObject.GetComponent<Button>().interactable = false;
				carriageManager.itemLockImageGo.SetActive(value: false);
				carriageManager.itemLockButtonImage.sprite = carriageManager.itemLockButtonSpriteArray[2];
				carriageManager.itemLockButtonLocText.Term = "summaryStoreDisplay";
				carriageManager.isSelectItemLock = false;
			}
			else
			{
				carriageManager.itemLockButtonImage.gameObject.GetComponent<Button>().interactable = true;
				if (haveWeaponData.isItemStoreDisplayLock)
				{
					carriageManager.itemLockImageGo.SetActive(value: true);
					carriageManager.itemLockButtonImage.sprite = carriageManager.itemLockButtonSpriteArray[1];
					carriageManager.itemLockButtonLocText.Term = "buttonItemUnLock";
					carriageManager.isSelectItemLock = true;
				}
				else
				{
					carriageManager.itemLockImageGo.SetActive(value: false);
					carriageManager.itemLockButtonImage.sprite = carriageManager.itemLockButtonSpriteArray[0];
					carriageManager.itemLockButtonLocText.Term = "buttonItemLock";
					carriageManager.isSelectItemLock = false;
				}
			}
			summaryParameterContainer2.GetVariable<UguiImage>("itemImage").image.sprite = itemWeaponData.itemSprite;
			summaryParameterContainer2.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = text2 + "_summary";
			int price2 = itemWeaponData.price;
			carriageManager.defaultPriceText.text = price2.ToString();
			int equipItemFactorPrice2 = PlayerInventoryDataEquipAccess.GetEquipItemFactorPrice(carriageManager.clickedItemID, carriageManager.clickedUniqueID);
			carriageManager.factorPriceText.text = equipItemFactorPrice2.ToString();
			carriageManager.storeAnimator.SetBool("isDisplay", haveWeaponData.isItemStoreDisplay);
			if (!carriageManager.isSellChangeClick)
			{
				carriageManager.storeAnimator.SetTrigger("itemReload");
			}
			carriageManager.itemStatusParam[0] = itemWeaponData.attackPower;
			carriageManager.itemStatusParam[1] = itemWeaponData.magicAttackPower;
			carriageManager.itemStatusParam[2] = itemWeaponData.accuracy;
			carriageManager.itemStatusParam[3] = itemWeaponData.critical;
			carriageManager.itemStatusParam[4] = itemWeaponData.weaponMp;
			carriageManager.itemStatusParam[6] = itemWeaponData.factorSlot;
			carriageManager.itemStatusParam[7] = itemWeaponData.factorHaveLimit;
			carriageManager.RefreshCarriageSummaryParameter();
			break;
		}
		case 1:
		{
			itemArmorData = null;
			itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == carriageManager.clickedItemID);
			haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData data) => data.itemID == carriageManager.clickedItemID && data.itemUniqueID == carriageManager.clickedUniqueID);
			ParameterContainer summaryParameterContainer = carriageManager.summaryParameterContainer;
			summaryParameterContainer.GetVariable<UguiImage>("iconImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary["armor"];
			string text = "armor" + itemArmorData.itemID;
			summaryParameterContainer.GetVariable<I2LocalizeComponent>("itemNameTextLoc").localize.Term = text;
			if (haveArmorData.isItemStoreDisplay)
			{
				Debug.Log("選択アイテムは販売中");
				carriageManager.itemLockButtonImage.gameObject.GetComponent<Button>().interactable = false;
				carriageManager.itemLockImageGo.SetActive(value: false);
				carriageManager.itemLockButtonImage.sprite = carriageManager.itemLockButtonSpriteArray[2];
				carriageManager.itemLockButtonLocText.Term = "summaryStoreDisplay";
				carriageManager.isSelectItemLock = false;
			}
			else
			{
				carriageManager.itemLockButtonImage.gameObject.GetComponent<Button>().interactable = true;
				if (haveArmorData.isItemStoreDisplayLock)
				{
					Debug.Log("選択アイテムはロック中");
					carriageManager.itemLockImageGo.SetActive(value: true);
					carriageManager.itemLockButtonImage.sprite = carriageManager.itemLockButtonSpriteArray[1];
					carriageManager.itemLockButtonLocText.Term = "buttonItemUnLock";
					carriageManager.isSelectItemLock = true;
				}
				else
				{
					Debug.Log("選択アイテムはロックしていない");
					carriageManager.itemLockImageGo.SetActive(value: false);
					carriageManager.itemLockButtonImage.sprite = carriageManager.itemLockButtonSpriteArray[0];
					carriageManager.itemLockButtonLocText.Term = "buttonItemLock";
					carriageManager.isSelectItemLock = false;
				}
			}
			summaryParameterContainer.GetVariable<UguiImage>("itemImage").image.sprite = itemArmorData.itemSprite;
			summaryParameterContainer.GetVariable<I2LocalizeComponent>("summaryTextLoc").localize.Term = text + "_summary";
			int price = itemArmorData.price;
			carriageManager.defaultPriceText.text = price.ToString();
			int equipItemFactorPrice = PlayerInventoryDataEquipAccess.GetEquipItemFactorPrice(carriageManager.clickedItemID, carriageManager.clickedUniqueID);
			carriageManager.factorPriceText.text = equipItemFactorPrice.ToString();
			carriageManager.storeAnimator.SetBool("isDisplay", haveArmorData.isItemStoreDisplay);
			if (!carriageManager.isSellChangeClick)
			{
				carriageManager.storeAnimator.SetTrigger("itemReload");
			}
			carriageManager.itemStatusParam[0] = itemArmorData.defensePower;
			carriageManager.itemStatusParam[1] = itemArmorData.magicDefensePower;
			carriageManager.itemStatusParam[2] = itemArmorData.evasion;
			carriageManager.itemStatusParam[3] = itemArmorData.abnormalResist;
			carriageManager.itemStatusParam[4] = itemArmorData.recoveryMp;
			carriageManager.itemStatusParam[6] = itemArmorData.factorSlot;
			carriageManager.itemStatusParam[7] = itemArmorData.factorHaveLimit;
			carriageManager.RefreshCarriageSummaryParameter();
			break;
		}
		}
		carriageManager.RefreshCarriagePriceSetting(isInitialize: true);
		carriageManager.isSellChangeClick = false;
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
