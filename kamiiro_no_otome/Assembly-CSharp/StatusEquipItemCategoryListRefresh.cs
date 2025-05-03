using System;
using System.Collections;
using Arbor;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class StatusEquipItemCategoryListRefresh : StateBehaviour
{
	private StatusManager statusManager;

	private ItemWeaponDataBase itemWeaponDataBase;

	private ItemArmorDataBase itemArmorDataBase;

	private ItemAccessoryDataBase itemAccessoryDataBase;

	public StateLink stateLink;

	public StateLink noStatusLink;

	public StateLink partyCategoryLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GetComponent<StatusManager>();
		itemWeaponDataBase = GameDataManager.instance.itemWeaponDataBase;
		itemArmorDataBase = GameDataManager.instance.itemArmorDataBase;
		itemAccessoryDataBase = GameDataManager.instance.itemAccessoryDataBase;
	}

	public override void OnStateBegin()
	{
		statusManager.ResetScrollViewContents(statusManager.itemContentGO.transform, isCustom: false);
		statusManager.factorEquipButtonGo.SetActive(value: false);
		GameObject[] itemScrollSummaryGoArray = statusManager.itemScrollSummaryGoArray;
		for (int i = 0; i < itemScrollSummaryGoArray.Length; i++)
		{
			itemScrollSummaryGoArray[i].SetActive(value: false);
		}
		int num = int.MinValue;
		if (statusManager.selectCharacterNum == 0)
		{
			switch (statusManager.selectItemCategoryNum)
			{
			case 7:
			{
				statusManager.factorEquipButtonGo.SetActive(value: true);
				int l;
				for (l = 0; l < PlayerInventoryDataManager.haveWeaponList.Count; l++)
				{
					ItemWeaponData itemWeaponData = itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData item) => item.itemID == PlayerInventoryDataManager.haveWeaponList[l].itemID);
					if (!(itemWeaponData != null))
					{
						continue;
					}
					if (num == int.MinValue)
					{
						num = PlayerInventoryDataManager.haveWeaponList[l].itemID;
						_ = PlayerInventoryDataManager.haveWeaponList[l].itemUniqueID;
					}
					Transform transform3 = PoolManager.Pools["Status Item Pool"].Spawn(statusManager.scrollContentPrefabArray[1]);
					RefreshItemList(transform3, l);
					string term3 = itemWeaponData.category.ToString() + itemWeaponData.itemID;
					transform3.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term3;
					transform3.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("powerText").text.text = itemWeaponData.attackPower.ToString();
					transform3.GetComponent<ParameterContainer>().GetGameObject("mpGroupGo").SetActive(value: true);
					transform3.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("addMpText").text.gameObject.SetActive(value: false);
					int weaponIncludeMp = PlayerInventoryDataManager.haveWeaponList[l].weaponIncludeMp;
					int weaponIncludeMaxMp = PlayerInventoryDataManager.haveWeaponList[l].weaponIncludeMaxMp;
					transform3.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("currentMpText").text.text = weaponIncludeMp.ToString();
					transform3.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("maxMpText").text.text = weaponIncludeMaxMp.ToString();
					int remainingDaysToCraft2 = PlayerInventoryDataManager.haveWeaponList[l].remainingDaysToCraft;
					transform3.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("remainingDayText").text.text = remainingDaysToCraft2.ToString();
					string category3 = itemWeaponData.category.ToString();
					SetItemIconSprite(transform3, category3);
					if (PlayerInventoryDataManager.haveWeaponList[l].equipCharacter == 0)
					{
						transform3.GetComponent<ParameterContainer>().GetGameObject("equipIconImage").SetActive(value: true);
						if (num != int.MinValue)
						{
							statusManager.selectItemScrollContentIndex = l;
						}
					}
					else
					{
						transform3.GetComponent<ParameterContainer>().GetGameObject("equipIconImage").SetActive(value: false);
					}
					if (PlayerInventoryDataManager.haveWeaponList[l].isItemStoreDisplay)
					{
						transform3.GetComponent<ParameterContainer>().GetGameObject("storeIconImage").SetActive(value: true);
					}
					else
					{
						transform3.GetComponent<ParameterContainer>().GetGameObject("storeIconImage").SetActive(value: false);
					}
					StatusItemListClickAction component3 = transform3.GetComponent<StatusItemListClickAction>();
					component3.itemID = PlayerInventoryDataManager.haveWeaponList[l].itemID;
					component3.instanceID = PlayerInventoryDataManager.haveWeaponList[l].itemUniqueID;
				}
				statusManager.itemScrollSummaryGoArray[1].SetActive(value: true);
				statusManager.powerTextLoc.Term = "statusAttack";
				statusManager.mpTextLoc.Term = "statusItemMp";
				statusManager.headerCategoryTextLoc.Term = "itemTypeHeader_weapon";
				SetScrollRectHeight(isEquipVisible: true);
				break;
			}
			case 8:
			{
				statusManager.factorEquipButtonGo.SetActive(value: true);
				int k;
				for (k = 0; k < PlayerInventoryDataManager.haveArmorList.Count; k++)
				{
					ItemArmorData itemArmorData = itemArmorDataBase.itemArmorDataList.Find((ItemArmorData item) => item.itemID == PlayerInventoryDataManager.haveArmorList[k].itemID);
					if (!(itemArmorData != null))
					{
						continue;
					}
					if (num == int.MinValue)
					{
						num = PlayerInventoryDataManager.haveArmorList[k].itemID;
						_ = PlayerInventoryDataManager.haveArmorList[k].itemUniqueID;
					}
					Transform transform2 = PoolManager.Pools["Status Item Pool"].Spawn(statusManager.scrollContentPrefabArray[1]);
					RefreshItemList(transform2, k);
					string term2 = "armor" + itemArmorData.itemID;
					transform2.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term2;
					transform2.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("powerText").text.text = itemArmorData.defensePower.ToString();
					int recoveryMp = itemArmorData.recoveryMp;
					transform2.GetComponent<ParameterContainer>().GetGameObject("mpGroupGo").SetActive(value: false);
					transform2.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("addMpText").text.gameObject.SetActive(value: true);
					transform2.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("addMpText").text.text = recoveryMp.ToString();
					int remainingDaysToCraft = PlayerInventoryDataManager.haveArmorList[k].remainingDaysToCraft;
					transform2.GetComponent<ParameterContainer>().GetVariable<UguiTextVariable>("remainingDayText").text.text = remainingDaysToCraft.ToString();
					string category2 = "armor";
					SetItemIconSprite(transform2, category2);
					if (PlayerInventoryDataManager.haveArmorList[k].equipCharacter == 0)
					{
						transform2.GetComponent<ParameterContainer>().GetGameObject("equipIconImage").SetActive(value: true);
						if (num != int.MinValue)
						{
							statusManager.selectItemScrollContentIndex = k;
						}
					}
					else
					{
						transform2.GetComponent<ParameterContainer>().GetGameObject("equipIconImage").SetActive(value: false);
					}
					if (PlayerInventoryDataManager.haveArmorList[k].isItemStoreDisplay)
					{
						transform2.GetComponent<ParameterContainer>().GetGameObject("storeIconImage").SetActive(value: true);
					}
					else
					{
						transform2.GetComponent<ParameterContainer>().GetGameObject("storeIconImage").SetActive(value: false);
					}
					StatusItemListClickAction component2 = transform2.GetComponent<StatusItemListClickAction>();
					component2.itemID = PlayerInventoryDataManager.haveArmorList[k].itemID;
					component2.instanceID = PlayerInventoryDataManager.haveArmorList[k].itemUniqueID;
				}
				statusManager.itemScrollSummaryGoArray[1].SetActive(value: true);
				statusManager.powerTextLoc.Term = "statusDefense";
				statusManager.mpTextLoc.Term = "statusRecoveryMpAddShort";
				statusManager.headerCategoryTextLoc.Term = "itemTypeHeader_armor";
				SetScrollRectHeight(isEquipVisible: true);
				break;
			}
			case 9:
			{
				int j;
				for (j = 0; j < PlayerInventoryDataManager.haveAccessoryList.Count; j++)
				{
					ItemAccessoryData itemAccessoryData = itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData item) => item.itemID == PlayerInventoryDataManager.haveAccessoryList[j].itemID);
					if (!(itemAccessoryData != null))
					{
						continue;
					}
					if (num == int.MinValue)
					{
						num = PlayerInventoryDataManager.haveAccessoryList[j].itemID;
					}
					Transform transform = PoolManager.Pools["Status Item Pool"].Spawn(statusManager.scrollContentPrefabArray[2]);
					RefreshItemList(transform, j);
					string term = itemAccessoryData.category.ToString() + itemAccessoryData.itemID;
					transform.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term;
					if (PlayerInventoryDataManager.haveAccessoryList[j].equipCharacter != 9)
					{
						int equipCharacter = PlayerInventoryDataManager.haveAccessoryList[j].equipCharacter;
						transform.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("equipNameText").localize.Term = "character" + equipCharacter;
						transform.GetComponent<ParameterContainer>().GetGameObject("equipIconImage").SetActive(value: true);
						if (num != int.MinValue && equipCharacter == 0)
						{
							statusManager.selectItemScrollContentIndex = j;
							Debug.Log("アクセサリ／初回表示の場合、選択インデックスを代入する：エデン");
						}
					}
					else
					{
						transform.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("equipNameText").localize.Term = "empty";
						transform.GetComponent<ParameterContainer>().GetGameObject("equipIconImage").SetActive(value: false);
					}
					string category = itemAccessoryData.category.ToString();
					SetItemIconSprite(transform, category);
					StatusItemListClickAction component = transform.GetComponent<StatusItemListClickAction>();
					component.itemID = PlayerInventoryDataManager.haveAccessoryList[j].itemID;
					component.instanceID = 0;
				}
				statusManager.itemScrollSummaryGoArray[2].SetActive(value: true);
				statusManager.headerCategoryTextLoc.Term = "itemTypeHeader_accessory";
				SetScrollRectHeight(isEquipVisible: true);
				break;
			}
			}
			if (num != int.MinValue)
			{
				if (!statusManager.isEquipedItemSummaryShow)
				{
					switch (statusManager.selectItemCategoryNum)
					{
					case 7:
					{
						int[] playerHaveWeaponItemID = PlayerInventoryDataEquipAccess.GetPlayerHaveWeaponItemID(0);
						statusManager.selectItemId = playerHaveWeaponItemID[0];
						statusManager.selectItemUniqueId = playerHaveWeaponItemID[1];
						break;
					}
					case 8:
					{
						int[] playerHaveArmorItemID = PlayerInventoryDataEquipAccess.GetPlayerHaveArmorItemID(0);
						statusManager.selectItemId = playerHaveArmorItemID[0];
						statusManager.selectItemUniqueId = playerHaveArmorItemID[1];
						break;
					}
					case 9:
					{
						int playerHaveAccessoryID = PlayerInventoryDataEquipAccess.GetPlayerHaveAccessoryID(0);
						if (playerHaveAccessoryID != 0)
						{
							statusManager.selectItemId = playerHaveAccessoryID;
						}
						else
						{
							statusManager.selectItemId = num;
						}
						break;
					}
					}
					ResetItemListSlider();
				}
				else
				{
					StartCoroutine(SetItemListSliderToSelected(statusManager.selectItemId));
				}
				Transition(stateLink);
			}
			else
			{
				Transition(noStatusLink);
			}
		}
		else
		{
			SetScrollRectHeight(isEquipVisible: false);
			Transition(partyCategoryLink);
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

	private void RefreshItemList(Transform transform, int i)
	{
		transform.SetParent(statusManager.itemContentGO.transform);
		transform.transform.localScale = new Vector3(1f, 1f, 1f);
		transform.transform.SetSiblingIndex(i);
	}

	private void SetItemIconSprite(Transform go, string category)
	{
		Sprite sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[category];
		go.GetComponent<ParameterContainer>().GetVariable<UguiImage>("iconImage").image.sprite = sprite;
	}

	private void ResetItemListSlider()
	{
		statusManager.itemViewArray[0].transform.Find("Scroll View/Scrollbar Vertical").GetComponent<Scrollbar>().value = 1f;
	}

	private IEnumerator SetItemListSliderToSelected(int itemID)
	{
		for (int i = 0; i < 1; i++)
		{
			yield return null;
		}
		ResetItemListSlider();
		float height = statusManager.itemContentGO.transform.parent.GetComponent<RectTransform>().rect.height;
		int childCount = statusManager.itemContentGO.transform.childCount;
		if (childCount > 0)
		{
			int num = childCount;
			int num2 = 0;
			for (int j = 0; j < childCount; j++)
			{
				if (statusManager.itemContentGO.transform.GetChild(j).gameObject.activeSelf)
				{
					num2++;
					if (statusManager.itemContentGO.transform.GetChild(j).GetComponent<StatusItemListClickAction>().itemID == itemID)
					{
						num = j;
					}
				}
			}
			if (num < childCount)
			{
				float height2 = statusManager.itemContentGO.transform.GetChild(0).GetComponent<RectTransform>().rect.height;
				float spacing = statusManager.itemContentGO.GetComponent<VerticalLayoutGroup>().spacing;
				float num3 = (float)num2 * (height2 + spacing);
				int num4 = Convert.ToInt32(Math.Floor((double)height / (double)(height2 + spacing)));
				if (num >= num4)
				{
					float num5 = ((float)(num + 1) * (height2 + spacing) - height) / (num3 - height);
					if (num5 < 0f)
					{
						num5 = 0f;
					}
					if (num5 > 1f)
					{
						num5 = 1f;
					}
					statusManager.itemViewArray[0].transform.Find("Scroll View/Scrollbar Vertical").GetComponent<Scrollbar>().value = 1f - num5;
					Debug.Log(num5.ToString());
					yield return null;
				}
			}
		}
		yield return null;
	}

	private void SetScrollRectHeight(bool isEquipVisible)
	{
		if (isEquipVisible)
		{
			statusManager.itemEquipButtonGO.SetActive(value: true);
			statusManager.itemSelectWIndowScrollView.offsetMin = new Vector2(23f, 104f);
		}
		else
		{
			statusManager.itemEquipButtonGO.SetActive(value: false);
			statusManager.itemSelectWIndowScrollView.offsetMin = new Vector2(23f, 28f);
		}
	}
}
