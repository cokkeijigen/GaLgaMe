using System.Collections.Generic;
using Arbor;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class NewCraftCampItemListRefresh : StateBehaviour
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
		ItemCampItemData itemCampItemData = null;
		int num = 0;
		int num2 = 0;
		craftCanvasManager.isUniqueItem = false;
		List<ItemCampItemData> campKitList = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList;
		int i;
		for (i = 0; i < campKitList.Count; i++)
		{
			string recipeFlagName = campKitList[i].recipeFlagName;
			Debug.Log(recipeFlagName);
			if (!PlayerFlagDataManager.recipeFlagDictionary[recipeFlagName])
			{
				continue;
			}
			itemCampItemData = null;
			bool flag = false;
			itemCampItemData = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData data) => data.itemID == campKitList[i].itemID);
			Debug.Log("キャンプキットデータ取得完了");
			if (!(itemCampItemData != null))
			{
				continue;
			}
			string text = itemCampItemData.sortID.ToString();
			string text2 = text.Substring(text.Length - 1);
			Debug.Log("ソートID：" + text + "／IDの末尾は：" + text2);
			if (text2 == "0")
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
				string text3 = "campItem" + itemCampItemData.itemID;
				Debug.Log(text3);
				int num3 = ((PlayerInventoryDataManager.haveItemCampItemList.Find((HaveCampItemData data) => data.itemID == itemCampItemData.itemID) != null) ? PlayerInventoryDataManager.haveItemCampItemList.Find((HaveCampItemData data) => data.itemID == itemCampItemData.itemID).haveCountNum : 0);
				component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = text3;
				component.GetVariable<UguiTextVariable>("numText").text.text = num3.ToString();
				string key = "campItem" + itemCampItemData.itemID;
				if (PlayerFlagDataManager.keyItemFlagDictionary[key])
				{
					component.GetGameObject("checkImageGo").SetActive(value: true);
					craftManager.SetCraftItemIconAndTextColor(component, isDefault: false);
				}
				else
				{
					component.GetGameObject("checkImageGo").SetActive(value: false);
					craftManager.SetCraftItemIconAndTextColor(component, isDefault: true);
				}
				component.GetVariable<UguiImage>("itemImage").image.sprite = craftManager.selectScrollContentSpriteArray[0];
				string category = itemCampItemData.category.ToString();
				SetItemIconSprite(component.GetVariable<UguiImage>("iconImage").image, category);
				CraftItemListClickAction component2 = transform.GetComponent<CraftItemListClickAction>();
				component2.itemID = itemCampItemData.itemID;
				component2.instanceID = 0;
				component2.modeType = CraftItemListClickAction.ModeType.craftRecipe;
				SetFirstItemData(num2, itemCampItemData.itemID, transform.gameObject);
				Debug.Log("スクロール表示回数：" + num2 + "／キャンプキットデータ設定完了");
				num2++;
			}
			else
			{
				Debug.Log("一つ前のアイテムを作成していない");
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

	private void SetFirstItemData(int scrollInitialize, int itemID, GameObject go)
	{
		if (scrollInitialize == 0 && craftManager.clickedItemID == 0)
		{
			craftManager.clickedItemID = itemID;
			go.GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage").image.sprite = craftManager.selectScrollContentSpriteArray[1];
			Debug.Log("ループ番号：" + scrollInitialize + "／選択アイテムID：" + craftManager.clickedItemID);
		}
	}
}
