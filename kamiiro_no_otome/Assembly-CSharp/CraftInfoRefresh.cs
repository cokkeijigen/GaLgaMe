using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CraftInfoRefresh : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
	}

	public override void OnStateBegin()
	{
		_ = craftCanvasManager.selectItemInfoParam;
		craftCanvasManager.remainingDaysFrameGo.SetActive(value: true);
		craftCanvasManager.remainingDaysAlertGo.SetActive(value: false);
		craftCanvasManager.isRemainingDaysZero = false;
		craftCanvasManager.infoApplyButtonCanvasGroup.interactable = true;
		craftCanvasManager.infoApplyButtonCanvasGroup.alpha = 1f;
		craftCanvasManager.craftStartButtonTextLocArray[0].Term = "buttonCraftDetail";
		craftCanvasManager.craftStartButtonTextLocArray[1].Term = "buttonInheritCraftDetail";
		switch (craftManager.selectCategoryNum)
		{
		case 0:
		{
			ItemWeaponData itemWeaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == craftManager.clickedItemID);
			HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == craftManager.clickedItemID && data.itemUniqueID == craftManager.clickedUniqueID);
			craftCanvasManager.infoEditIconImage[0].sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[itemWeaponData.category.ToString()];
			craftCanvasManager.infoEditTextLoc[0].Term = itemWeaponData.category.ToString() + craftManager.clickedItemID;
			craftCanvasManager.infoEditIconImage[1].sprite = GameDataManager.instance.itemCategoryDataBase.skillCategoryIconDictionary["attack"];
			craftCanvasManager.infoEditTextLoc[1].Term = "statusAttack";
			craftCanvasManager.infoEditNumText[1].text = itemWeaponData.attackPower.ToString();
			craftCanvasManager.infoEditIconImage[2].sprite = GameDataManager.instance.itemCategoryDataBase.skillCategoryIconDictionary["magicAttack"];
			craftCanvasManager.infoEditTextLoc[2].Term = "statusMagicAttack";
			craftCanvasManager.infoEditNumText[2].text = itemWeaponData.magicAttackPower.ToString();
			craftCanvasManager.infoEditNumText[0].text = itemWeaponData.needWorkShopLevel.ToString();
			craftCanvasManager.remainingDaysNumTextArray[1].text = haveWeaponData.remainingDaysToCraft.ToString();
			if (haveWeaponData.remainingDaysToCraft == 0)
			{
				craftCanvasManager.remainingDaysAlertGo.SetActive(value: true);
				craftCanvasManager.isRemainingDaysZero = true;
			}
			craftCanvasManager.craftInheritImageArray[0].sprite = itemWeaponData.itemInheritSprite;
			int nextID2 = craftManager.GetNextUpgradeItemID(craftManager.clickedItemID);
			Debug.Log("次の継承アイテムID：" + nextID2);
			if (nextID2 != -1)
			{
				craftManager.nextItemID = nextID2;
				ItemWeaponData itemWeaponData2 = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == nextID2);
				if (PlayerFlagDataManager.recipeFlagDictionary[itemWeaponData2.recipeFlagName])
				{
					craftCanvasManager.isInheritButtonLock = false;
					craftCanvasManager.craftInheritImageArray[1].sprite = itemWeaponData2.itemInheritSprite;
					if (haveWeaponData.remainingDaysToCraft > 0)
					{
						if (itemWeaponData.factorSlot <= haveWeaponData.itemEnhanceCount)
						{
							craftCanvasManager.isCompleteEnhanceCount = true;
							craftCanvasManager.craftInheritButtonTextLoc.Term = "buttonEvolutionCraftMini";
							break;
						}
						craftCanvasManager.isCompleteEnhanceCount = false;
						craftCanvasManager.isInheritButtonLock = true;
						craftCanvasManager.craftInheritImageArray[1].sprite = craftCanvasManager.craftInheritLockSpriteArray[0];
						craftCanvasManager.infoPowerUpLockTextLoc.Term = "buttonEvolutionCraftImpossible";
					}
					else
					{
						craftCanvasManager.isCompleteEnhanceCount = false;
						craftCanvasManager.craftInheritButtonTextLoc.Term = "buttonInheritCraftMini";
					}
				}
				else
				{
					craftCanvasManager.isInheritButtonLock = true;
					craftCanvasManager.craftInheritImageArray[1].sprite = craftCanvasManager.craftInheritLockSpriteArray[0];
					craftCanvasManager.infoPowerUpLockTextLoc.Term = "infoLockRecipe";
				}
			}
			else
			{
				craftCanvasManager.isInheritButtonLock = true;
				craftCanvasManager.craftInheritImageArray[1].sprite = craftCanvasManager.craftInheritLockSpriteArray[1];
				craftCanvasManager.infoPowerUpLockTextLoc.Term = "infoLockCraftMax";
			}
			break;
		}
		case 1:
		{
			ItemArmorData itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == craftManager.clickedItemID);
			HaveArmorData haveArmorData = PlayerInventoryDataManager.haveArmorList.Find((HaveArmorData data) => data.itemID == craftManager.clickedItemID && data.itemUniqueID == craftManager.clickedUniqueID);
			craftCanvasManager.infoEditIconImage[0].sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary["armor"];
			craftCanvasManager.infoEditTextLoc[0].Term = "armor" + craftManager.clickedItemID;
			craftCanvasManager.infoEditIconImage[1].sprite = GameDataManager.instance.itemCategoryDataBase.craftIconDictionary["defense"];
			craftCanvasManager.infoEditTextLoc[1].Term = "statusDefense";
			craftCanvasManager.infoEditNumText[1].text = itemArmorData.defensePower.ToString();
			craftCanvasManager.infoEditIconImage[2].sprite = GameDataManager.instance.itemCategoryDataBase.craftIconDictionary["magicDefense"];
			craftCanvasManager.infoEditTextLoc[2].Term = "statusMagicDefense";
			craftCanvasManager.infoEditNumText[2].text = itemArmorData.magicDefensePower.ToString();
			craftCanvasManager.infoEditNumText[0].text = itemArmorData.needWorkShopLevel.ToString();
			craftCanvasManager.remainingDaysNumTextArray[1].text = haveArmorData.remainingDaysToCraft.ToString();
			if (haveArmorData.remainingDaysToCraft == 0)
			{
				craftCanvasManager.remainingDaysAlertGo.SetActive(value: true);
				craftCanvasManager.isRemainingDaysZero = true;
			}
			craftCanvasManager.craftInheritImageArray[0].sprite = itemArmorData.itemInheritSprite;
			int nextID = craftManager.GetNextUpgradeItemID(craftManager.clickedItemID);
			if (nextID != -1)
			{
				craftManager.nextItemID = nextID;
				ItemArmorData itemArmorData2 = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == nextID);
				if (PlayerFlagDataManager.recipeFlagDictionary[itemArmorData2.recipeFlagName])
				{
					craftCanvasManager.isInheritButtonLock = false;
					craftCanvasManager.craftInheritImageArray[1].sprite = itemArmorData2.itemInheritSprite;
					if (haveArmorData.remainingDaysToCraft > 0)
					{
						if (itemArmorData.factorSlot <= haveArmorData.itemEnhanceCount)
						{
							craftCanvasManager.isCompleteEnhanceCount = true;
							craftCanvasManager.craftInheritButtonTextLoc.Term = "buttonEvolutionCraftMini";
							break;
						}
						craftCanvasManager.isCompleteEnhanceCount = false;
						craftCanvasManager.isInheritButtonLock = true;
						craftCanvasManager.craftInheritImageArray[1].sprite = craftCanvasManager.craftInheritLockSpriteArray[0];
						craftCanvasManager.infoPowerUpLockTextLoc.Term = "buttonEvolutionCraftImpossible";
					}
					else
					{
						craftCanvasManager.isCompleteEnhanceCount = false;
						craftCanvasManager.craftInheritButtonTextLoc.Term = "buttonInheritCraftMini";
					}
				}
				else
				{
					craftCanvasManager.isInheritButtonLock = true;
					craftCanvasManager.craftInheritImageArray[1].sprite = craftCanvasManager.craftInheritLockSpriteArray[0];
					craftCanvasManager.infoPowerUpLockTextLoc.Term = "infoLockRecipe";
				}
			}
			else
			{
				craftCanvasManager.isInheritButtonLock = true;
				craftCanvasManager.craftInheritImageArray[1].sprite = craftCanvasManager.craftInheritLockSpriteArray[1];
				craftCanvasManager.infoPowerUpLockTextLoc.Term = "infoLockCraftMax";
			}
			break;
		}
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
