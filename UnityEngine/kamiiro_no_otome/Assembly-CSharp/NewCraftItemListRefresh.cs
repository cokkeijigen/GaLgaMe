using System.Collections.Generic;
using System.Linq;
using Arbor;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class NewCraftItemListRefresh : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private CraftCheckManager craftCheckManager;

	private CraftTalkManager craftTalkManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
		craftCheckManager = GameObject.Find("Craft Check Manager").GetComponent<CraftCheckManager>();
		craftTalkManager = GameObject.Find("Craft Talk Manager").GetComponent<CraftTalkManager>();
	}

	public override void OnStateBegin()
	{
		craftManager.CraftScrollItemDesapwnAll();
		string text = "";
		ItemWeaponData itemweaponData = null;
		ItemArmorData itemArmorData = null;
		ItemCanMakeMaterialData itemCanMakeMaterialData = null;
		ItemEventItemData itemEventItemData = null;
		ItemCampItemData itemCampItemData = null;
		int num = 0;
		craftCanvasManager.isUniqueItem = false;
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, bool> item in PlayerFlagDataManager.recipeFlagDictionary)
		{
			list.Add(item.Key);
		}
		list.Sort();
		for (int i = 0; i < PlayerFlagDataManager.recipeFlagDictionary.Count; i++)
		{
			text = list[i];
			Debug.Log("確認するレシピ名：" + text);
			if (!PlayerFlagDataManager.recipeFlagDictionary[text])
			{
				continue;
			}
			int num2 = int.Parse(text.Substring(6));
			if (num2 >= 9000)
			{
				if (num2 != 9020)
				{
					Debug.Log("レシピ番号9000以上はスキップ");
					continue;
				}
				Debug.Log("インゴットのレシピなのでスキップしない");
			}
			List<int> idList = new List<int>();
			if (PlayerCraftStatusManager.craftRecipeItemIdDictionary.ContainsKey(text))
			{
				idList = PlayerCraftStatusManager.craftRecipeItemIdDictionary[text];
				switch (craftManager.selectCategoryNum)
				{
				case 0:
				{
					int j;
					for (j = 0; j < idList.Count; j++)
					{
						itemweaponData = null;
						itemweaponData = GameDataManager.instance.itemWeaponDataBase.itemWeaponDataList.Find((ItemWeaponData data) => data.itemID == idList[j]);
						if (itemweaponData != null && itemweaponData.needQuestId != 9999 && !PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == itemweaponData.needQuestId).isClear)
						{
							Debug.Log("必要クエストを未クリア");
						}
						else if (itemweaponData != null && itemweaponData.itemID < 1300)
						{
							Transform transform2 = PoolManager.Pools["Craft Item Pool"].Spawn(craftManager.scrollItemGoArray[0]);
							ParameterContainer component3 = transform2.transform.GetComponent<ParameterContainer>();
							RefreshItemList(transform2, num++);
							string text5 = itemweaponData.category.ToString() + itemweaponData.itemID;
							Debug.Log(text5);
							component3.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = text5;
							if (PlayerFlagDataManager.enableNewCraftFlagDictionary[itemweaponData.itemID])
							{
								craftManager.SetIconAndTextColor(component3, isDefault: true);
								component3.GetVariable<UguiTextVariable>("powerText").text.text = itemweaponData.attackPower.ToString();
							}
							else
							{
								craftManager.SetIconAndTextColor(component3, isDefault: false);
								component3.GetVariable<UguiTextVariable>("powerText").text.text = itemweaponData.attackPower.ToString();
							}
							component3.GetVariable<UguiImage>("itemImage").image.sprite = craftManager.selectScrollContentSpriteArray[0];
							int num4 = PlayerInventoryDataManager.haveWeaponList.Where((HaveWeaponData data) => data.itemID == itemweaponData.itemID).Count();
							component3.GetVariable<UguiTextVariable>("numText").text.text = num4.ToString();
							SetItemIconSprite(component3.GetVariable<UguiImage>("iconImage").image, "greatSword");
							CraftItemListClickAction component4 = transform2.GetComponent<CraftItemListClickAction>();
							component4.itemID = itemweaponData.itemID;
							component4.instanceID = 0;
							component4.modeType = CraftItemListClickAction.ModeType.craftRecipe;
							if (PlayerEquipDataManager.playerEquipWeaponID[0] == itemweaponData.itemID)
							{
								component3.GetGameObject("equipIconGo").SetActive(value: true);
							}
							else
							{
								component3.GetGameObject("equipIconGo").SetActive(value: false);
							}
							SetFirstItemData(j, itemweaponData.itemID, transform2.gameObject);
							Debug.Log("武器データ設定完了");
						}
					}
					break;
				}
				case 1:
				{
					int l;
					for (l = 0; l < idList.Count; l++)
					{
						itemArmorData = null;
						itemArmorData = GameDataManager.instance.itemArmorDataBase.itemArmorDataList.Find((ItemArmorData data) => data.itemID == idList[l]);
						Debug.Log("防具データ取得完了");
						if (itemArmorData != null && itemArmorData.needQuestId != 9999 && !PlayerFlagDataManager.questClearFlagList.Find((QuestClearData data) => data.sortID == itemArmorData.needQuestId).isClear)
						{
							Debug.Log("必要クエストを未クリア");
						}
						else if (itemArmorData != null && itemArmorData.itemID < 2300)
						{
							Transform transform4 = PoolManager.Pools["Craft Item Pool"].Spawn(craftManager.scrollItemGoArray[0]);
							ParameterContainer component7 = transform4.transform.GetComponent<ParameterContainer>();
							RefreshItemList(transform4, num++);
							string text7 = "armor" + itemArmorData.itemID;
							Debug.Log(text7);
							component7.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = text7;
							if (PlayerFlagDataManager.enableNewCraftFlagDictionary[itemArmorData.itemID])
							{
								craftManager.SetIconAndTextColor(component7, isDefault: true);
								component7.GetVariable<UguiTextVariable>("powerText").text.text = itemArmorData.defensePower.ToString();
							}
							else
							{
								craftManager.SetIconAndTextColor(component7, isDefault: false);
								component7.GetVariable<UguiTextVariable>("powerText").text.text = itemArmorData.defensePower.ToString();
							}
							component7.GetVariable<UguiImage>("itemImage").image.sprite = craftManager.selectScrollContentSpriteArray[0];
							int num6 = PlayerInventoryDataManager.haveArmorList.Where((HaveArmorData data) => data.itemID == itemArmorData.itemID).Count();
							component7.GetVariable<UguiTextVariable>("numText").text.text = num6.ToString();
							SetItemIconSprite(component7.GetVariable<UguiImage>("iconImage").image, "armor");
							CraftItemListClickAction component8 = transform4.GetComponent<CraftItemListClickAction>();
							component8.itemID = itemArmorData.itemID;
							component8.instanceID = 0;
							component8.modeType = CraftItemListClickAction.ModeType.craftRecipe;
							if (PlayerEquipDataManager.playerEquipArmorID[0] == itemArmorData.itemID)
							{
								component7.GetGameObject("equipIconGo").SetActive(value: true);
							}
							else
							{
								component7.GetGameObject("equipIconGo").SetActive(value: false);
							}
							SetFirstItemData(l, itemArmorData.itemID, transform4.gameObject);
							Debug.Log("防具データ設定完了");
						}
					}
					break;
				}
				case 2:
				{
					int m;
					for (m = 0; m < idList.Count; m++)
					{
						itemCanMakeMaterialData = null;
						itemCanMakeMaterialData = GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Find((ItemCanMakeMaterialData data) => data.itemID == idList[m]);
						if (itemCanMakeMaterialData != null)
						{
							Transform transform5 = PoolManager.Pools["Craft Item Pool"].Spawn(craftManager.scrollItemGoArray[1]);
							ParameterContainer component9 = transform5.transform.GetComponent<ParameterContainer>();
							RefreshItemList(transform5, num++);
							string text8 = itemCanMakeMaterialData.category.ToString() + itemCanMakeMaterialData.itemID;
							Debug.Log(text8);
							int num7 = ((PlayerInventoryDataManager.haveItemCanMakeMaterialList.Find((HaveItemData data) => data.itemID == itemCanMakeMaterialData.itemID) != null) ? PlayerInventoryDataManager.haveItemCanMakeMaterialList.Find((HaveItemData data) => data.itemID == itemCanMakeMaterialData.itemID).haveCountNum : 0);
							component9.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = text8;
							component9.GetVariable<UguiTextVariable>("numText").text.text = num7.ToString();
							component9.GetGameObject("checkImageGo").SetActive(value: false);
							craftManager.SetCraftItemIconAndTextColor(component9, isDefault: true);
							component9.GetVariable<UguiImage>("itemImage").image.sprite = craftManager.selectScrollContentSpriteArray[0];
							string category3 = itemCanMakeMaterialData.category.ToString();
							SetItemIconSprite(component9.GetVariable<UguiImage>("iconImage").image, category3);
							CraftItemListClickAction component10 = transform5.GetComponent<CraftItemListClickAction>();
							component10.itemID = itemCanMakeMaterialData.itemID;
							component10.instanceID = 0;
							component10.modeType = CraftItemListClickAction.ModeType.craftRecipe;
							SetFirstItemData(m, itemCanMakeMaterialData.itemID, transform5.gameObject);
							Debug.Log("開発素材データ設定完了");
						}
					}
					break;
				}
				case 3:
				{
					int k;
					for (k = 0; k < idList.Count; k++)
					{
						itemEventItemData = null;
						itemEventItemData = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData data) => data.itemID == idList[k]);
						if (itemEventItemData != null)
						{
							Transform transform3 = PoolManager.Pools["Craft Item Pool"].Spawn(craftManager.scrollItemGoArray[1]);
							ParameterContainer component5 = transform3.transform.GetComponent<ParameterContainer>();
							RefreshItemList(transform3, num++);
							string text6 = itemEventItemData.category.ToString() + itemEventItemData.itemID;
							Debug.Log(text6);
							int num5 = ((PlayerInventoryDataManager.haveEventItemList.Find((HaveEventItemData data) => data.itemID == itemEventItemData.itemID) != null) ? 1 : 0);
							component5.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = text6;
							component5.GetVariable<UguiTextVariable>("numText").text.text = num5.ToString();
							string key2 = "eventItem" + itemEventItemData.itemID;
							if (PlayerFlagDataManager.keyItemFlagDictionary[key2])
							{
								component5.GetGameObject("checkImageGo").SetActive(value: true);
								craftManager.SetCraftItemIconAndTextColor(component5, isDefault: false);
							}
							else
							{
								component5.GetGameObject("checkImageGo").SetActive(value: false);
								craftManager.SetCraftItemIconAndTextColor(component5, isDefault: true);
							}
							component5.GetVariable<UguiImage>("itemImage").image.sprite = craftManager.selectScrollContentSpriteArray[0];
							string category2 = itemEventItemData.category.ToString();
							SetItemIconSprite(component5.GetVariable<UguiImage>("iconImage").image, category2);
							CraftItemListClickAction component6 = transform3.GetComponent<CraftItemListClickAction>();
							component6.itemID = itemEventItemData.itemID;
							component6.instanceID = 0;
							component6.modeType = CraftItemListClickAction.ModeType.craftRecipe;
							SetFirstItemData(k, itemEventItemData.itemID, transform3.gameObject);
							Debug.Log("貴重品データ設定完了");
						}
					}
					break;
				}
				case 4:
				{
					int n;
					for (n = 0; n < idList.Count; n++)
					{
						itemCampItemData = null;
						bool flag = false;
						itemCampItemData = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData data) => data.itemID == idList[n]);
						if (!(itemCampItemData != null))
						{
							continue;
						}
						string text2 = itemCampItemData.sortID.ToString();
						string text3 = text2.Substring(text2.Length - 1);
						Debug.Log("ソートID：" + text2 + "／IDの末尾は：" + text3);
						if (text3 == "0")
						{
							flag = true;
						}
						else
						{
							int beforeItemSortId = itemCampItemData.sortID - 1;
							flag = ((PlayerInventoryDataManager.haveItemCampItemList.Find((HaveCampItemData data) => data.itemSortID == beforeItemSortId) != null) ? true : false);
						}
						if (flag)
						{
							Transform transform = PoolManager.Pools["Craft Item Pool"].Spawn(craftManager.scrollItemGoArray[1]);
							ParameterContainer component = transform.transform.GetComponent<ParameterContainer>();
							RefreshItemList(transform, num++);
							string text4 = "campItem" + itemCampItemData.itemID;
							Debug.Log(text4);
							int num3 = ((PlayerInventoryDataManager.haveItemCampItemList.Find((HaveCampItemData data) => data.itemID == itemCampItemData.itemID) != null) ? PlayerInventoryDataManager.haveItemCampItemList.Find((HaveCampItemData data) => data.itemID == itemCampItemData.itemID).haveCountNum : 0);
							component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = text4;
							component.GetVariable<UguiTextVariable>("numText").text.text = num3.ToString();
							string key = "campItem" + itemCampItemData.itemID;
							if (PlayerFlagDataManager.keyItemFlagDictionary[key])
							{
								component.GetGameObject("checkImageGo").SetActive(value: true);
							}
							else
							{
								component.GetGameObject("checkImageGo").SetActive(value: false);
							}
							component.GetVariable<UguiImage>("itemImage").image.sprite = craftManager.selectScrollContentSpriteArray[0];
							string category = itemCampItemData.category.ToString();
							SetItemIconSprite(component.GetVariable<UguiImage>("iconImage").image, category);
							CraftItemListClickAction component2 = transform.GetComponent<CraftItemListClickAction>();
							component2.itemID = itemCampItemData.itemID;
							component2.instanceID = 0;
							component2.modeType = CraftItemListClickAction.ModeType.craftRecipe;
							SetFirstItemData(n, itemCampItemData.itemID, transform.gameObject);
							Debug.Log("キャンプキットデータ設定完了");
						}
						else
						{
							Debug.Log("一つ前のアイテムを作成していない");
						}
					}
					break;
				}
				}
			}
			else
			{
				Debug.Log("レシピの作成アイテムはない");
			}
		}
		if (craftCanvasManager.isCraftComplete)
		{
			int scrollContentIndexFromItemId = craftManager.GetScrollContentIndexFromItemId(craftCheckManager.craftedItemID, 0);
			craftManager.itemSelectScrollContentGO.transform.GetChild(scrollContentIndexFromItemId).GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage")
				.image.sprite = craftManager.selectScrollContentSpriteArray[1];
			Debug.Log("選択中の項目は：" + scrollContentIndexFromItemId + "番目");
			craftCanvasManager.isCraftComplete = false;
		}
		craftTalkManager.TalkBalloonItemSelect();
		Transition(stateLink);
	}

	public override void OnStateEnd()
	{
		craftManager.itemSelectScrollBar.value = 1f;
	}

	public override void OnStateUpdate()
	{
	}

	public override void OnStateLateUpdate()
	{
	}

	private void RefreshItemList(Transform transform, int i)
	{
		transform.SetParent(craftManager.itemSelectScrollContentGO.transform);
		transform.transform.localScale = new Vector3(1f, 1f, 1f);
		transform.transform.SetSiblingIndex(i);
	}

	private void SetItemIconSprite(Image imageComponent, string category)
	{
		Sprite sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[category];
		imageComponent.sprite = sprite;
	}

	private void SetFirstItemData(int loopNum, int itemID, GameObject go)
	{
		if (loopNum == 0 && craftManager.clickedItemID == 0)
		{
			craftManager.clickedItemID = itemID;
			go.GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage").image.sprite = craftManager.selectScrollContentSpriteArray[1];
			Debug.Log("ループ番号：" + loopNum + "／選択アイテムID：" + craftManager.clickedItemID);
		}
	}
}
