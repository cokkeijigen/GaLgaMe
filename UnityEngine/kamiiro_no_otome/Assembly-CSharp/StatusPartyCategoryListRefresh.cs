using System;
using System.Collections;
using Arbor;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class StatusPartyCategoryListRefresh : StateBehaviour
{
	private StatusManager statusManager;

	private ItemCategoryDataBase itemCategoryDataBase;

	private ItemPartyWeaponDataBase itemPartyWeaponDataBase;

	private ItemPartyArmorDataBase itemPartyArmorDataBase;

	public StateLink stateLink;

	public StateLink noStatusLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GetComponent<StatusManager>();
		itemCategoryDataBase = GameDataManager.instance.itemCategoryDataBase;
		itemPartyWeaponDataBase = GameDataManager.instance.itemPartyWeaponDataBase;
		itemPartyArmorDataBase = GameDataManager.instance.itemPartyArmorDataBase;
	}

	public override void OnStateBegin()
	{
		int num = int.MinValue;
		int setIndexNum = 0;
		int selectCharacterNum = statusManager.selectCharacterNum;
		GameObject[] itemScrollSummaryGoArray = statusManager.itemScrollSummaryGoArray;
		for (int i = 0; i < itemScrollSummaryGoArray.Length; i++)
		{
			itemScrollSummaryGoArray[i].SetActive(value: false);
		}
		string characterDungeonFollowUnLockFlag = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[selectCharacterNum].characterDungeonFollowUnLockFlag;
		if (PlayerFlagDataManager.scenarioFlagDictionary[characterDungeonFollowUnLockFlag])
		{
			int index = selectCharacterNum - 1;
			switch (statusManager.selectItemCategoryNum)
			{
			case 7:
			{
				statusManager.factorEquipButtonGo.SetActive(value: true);
				statusManager.itemScrollSummaryGoArray[3].SetActive(value: true);
				ItemPartyWeaponData weaponData = itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData item) => item.itemID == PlayerInventoryDataManager.havePartyWeaponList[index].itemID);
				if (num == int.MinValue)
				{
					num = PlayerInventoryDataManager.havePartyWeaponList[index].itemID;
					_ = PlayerInventoryDataManager.havePartyWeaponList[index].equipCharacter;
				}
				WeaponContentSpawn(weaponData, setIndexNum, index);
				break;
			}
			case 8:
			{
				statusManager.factorEquipButtonGo.SetActive(value: true);
				statusManager.itemScrollSummaryGoArray[3].SetActive(value: true);
				ItemPartyArmorData armorData = itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData item) => item.itemID == PlayerInventoryDataManager.havePartyArmorList[index].itemID);
				if (num == int.MinValue)
				{
					num = PlayerInventoryDataManager.havePartyArmorList[index].itemID;
					_ = PlayerInventoryDataManager.havePartyArmorList[index].equipCharacter;
				}
				ArmorContentSpawn(armorData, setIndexNum, index);
				break;
			}
			case 9:
			{
				int j;
				for (j = 0; j < PlayerInventoryDataManager.haveAccessoryList.Count; j++)
				{
					ItemAccessoryData itemAccessoryData = GameDataManager.instance.itemAccessoryDataBase.itemAccessoryDataList.Find((ItemAccessoryData item) => item.itemID == PlayerInventoryDataManager.haveAccessoryList[j].itemID);
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
						if (num != int.MinValue && equipCharacter == statusManager.selectCharacterNum)
						{
							statusManager.selectItemScrollContentIndex = j;
							Debug.Log("アクセサリ／初回表示の場合、選択インデックスを代入する／仲間：" + statusManager.selectCharacterNum);
						}
					}
					else
					{
						transform.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("equipNameText").localize.Term = "empty";
						transform.GetComponent<ParameterContainer>().GetGameObject("equipIconImage").SetActive(value: false);
					}
					string key = itemAccessoryData.category.ToString();
					Sprite sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[key];
					transform.GetComponent<ParameterContainer>().GetVariable<UguiImage>("iconImage").image.sprite = sprite;
					StatusItemListClickAction component = transform.GetComponent<StatusItemListClickAction>();
					component.itemID = PlayerInventoryDataManager.haveAccessoryList[j].itemID;
					component.instanceID = 0;
				}
				statusManager.itemScrollSummaryGoArray[2].SetActive(value: true);
				statusManager.headerCategoryTextLoc.Term = "itemTypeHeader_accessory";
				statusManager.itemEquipButtonGO.SetActive(value: true);
				statusManager.itemSelectWIndowScrollView.offsetMin = new Vector2(23f, 104f);
				break;
			}
			}
		}
		if (num != int.MinValue)
		{
			if (!statusManager.isEquipedItemSummaryShow)
			{
				switch (statusManager.selectItemCategoryNum)
				{
				case 7:
					statusManager.selectItemId = num;
					break;
				case 8:
					statusManager.selectItemId = num;
					break;
				case 9:
				{
					int playerHaveAccessoryID = PlayerInventoryDataEquipAccess.GetPlayerHaveAccessoryID(statusManager.selectCharacterNum);
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

	private void WeaponContentSpawn(ItemPartyWeaponData weaponData, int setIndexNum, int i)
	{
		Transform obj = PoolManager.Pools["Status Item Pool"].Spawn(statusManager.scrollContentPrefabArray[3]);
		obj.SetParent(statusManager.itemContentGO.transform);
		obj.transform.localScale = new Vector3(1f, 1f, 1f);
		obj.transform.SetSiblingIndex(setIndexNum);
		string term = weaponData.category.ToString() + weaponData.itemID;
		obj.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term;
		int equipCharacter = PlayerInventoryDataManager.havePartyWeaponList[i].equipCharacter;
		obj.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("equipNameText").localize.Term = "character" + equipCharacter;
		obj.GetComponent<ParameterContainer>().GetGameObject("equipIconImage").SetActive(value: true);
		StatusPartyListClickAction component = obj.GetComponent<StatusPartyListClickAction>();
		component.EquipCharacterNum = equipCharacter;
		component.itemID = weaponData.itemID;
		Sprite sprite = itemCategoryDataBase.itemCategoryIconDictionary[weaponData.category.ToString()];
		obj.GetComponent<ParameterContainer>().GetVariable<UguiImage>("iconImage").image.sprite = sprite;
	}

	private void ArmorContentSpawn(ItemPartyArmorData armorData, int setIndexNum, int i)
	{
		Transform obj = PoolManager.Pools["Status Item Pool"].Spawn(statusManager.scrollContentPrefabArray[3]);
		obj.SetParent(statusManager.itemContentGO.transform);
		obj.transform.localScale = new Vector3(1f, 1f, 1f);
		obj.transform.SetSiblingIndex(setIndexNum);
		string term = "armor" + armorData.itemID;
		obj.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("nameText").localize.Term = term;
		int equipCharacter = PlayerInventoryDataManager.havePartyArmorList[i].equipCharacter;
		obj.GetComponent<ParameterContainer>().GetVariable<I2LocalizeComponent>("equipNameText").localize.Term = "character" + equipCharacter;
		obj.GetComponent<ParameterContainer>().GetGameObject("equipIconImage").SetActive(value: true);
		StatusPartyListClickAction component = obj.GetComponent<StatusPartyListClickAction>();
		component.EquipCharacterNum = equipCharacter;
		component.itemID = armorData.itemID;
		Sprite sprite = itemCategoryDataBase.itemCategoryIconDictionary["armor"];
		obj.GetComponent<ParameterContainer>().GetVariable<UguiImage>("iconImage").image.sprite = sprite;
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
					if (statusManager.itemContentGO.transform.GetChild(j).GetComponent<StatusPartyListClickAction>().itemID == itemID)
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
}
