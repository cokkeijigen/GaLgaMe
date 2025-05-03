using System.Collections.Generic;
using System.Linq;
using Arbor;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class MergeHaveItemListRefresh : StateBehaviour
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
		ItemPartyWeaponData itemPartyWeaponData = null;
		ItemPartyArmorData itemPartyArmorData = null;
		int num = 0;
		craftCanvasManager.isUniqueItem = false;
		switch (craftManager.selectCategoryNum)
		{
		case 0:
		{
			List<int> idList2 = PlayerInventoryDataManager.havePartyWeaponList.Select((HavePartyWeaponData data) => data.itemID).ToList();
			int i;
			for (i = 0; i < idList2.Count; i++)
			{
				itemPartyWeaponData = null;
				itemPartyWeaponData = GameDataManager.instance.itemPartyWeaponDataBase.itemPartyWeaponDataList.Find((ItemPartyWeaponData data) => data.itemID == idList2[i]);
				if (itemPartyWeaponData != null)
				{
					int partyCharacterNumFromItemID2 = PlayerInventoryDataEquipAccess.GetPartyCharacterNumFromItemID(idList2[i]);
					string characterPowerUpFlag2 = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[partyCharacterNumFromItemID2].characterPowerUpFlag;
					if (!string.IsNullOrEmpty(characterPowerUpFlag2) && PlayerFlagDataManager.scenarioFlagDictionary[characterPowerUpFlag2])
					{
						Transform transform2 = PoolManager.Pools["Craft Item Pool"].Spawn(craftManager.scrollItemGoArray[3]);
						ParameterContainer component2 = transform2.transform.GetComponent<ParameterContainer>();
						RefreshItemList(transform2, num++);
						string text2 = itemPartyWeaponData.category.ToString() + itemPartyWeaponData.itemID;
						Debug.Log(text2);
						component2.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = text2;
						component2.GetVariable<UguiTextVariable>("powerText").text.text = itemPartyWeaponData.attackPower.ToString();
						int characterIdForInventoryItem2 = PlayerInventoryDataAccess.GetCharacterIdForInventoryItem(itemPartyWeaponData.itemID);
						component2.GetVariable<I2LocalizeComponent>("characterNameLoc").localize.Term = "character" + characterIdForInventoryItem2;
						component2.GetVariable<UguiImage>("itemImage").image.sprite = craftManager.selectScrollContentSpriteArray[0];
						SetItemIconSprite(component2.GetVariable<UguiImage>("iconImage").image, itemPartyWeaponData.category.ToString());
						transform2.GetComponent<MergeItemListClickAction>().itemID = itemPartyWeaponData.itemID;
						component2.GetGameObject("equipIconGo").SetActive(value: true);
						if (!craftCanvasManager.isCraftComplete)
						{
							SetFirstItemData(i, itemPartyWeaponData.itemID, transform2.gameObject);
							Debug.Log("武器データ設定完了");
						}
					}
				}
				else
				{
					Debug.Log("武器データなし");
				}
			}
			break;
		}
		case 1:
		{
			List<int> idList = PlayerInventoryDataManager.havePartyArmorList.Select((HavePartyArmorData data) => data.itemID).ToList();
			int n;
			for (n = 0; n < idList.Count; n++)
			{
				itemPartyArmorData = null;
				itemPartyArmorData = GameDataManager.instance.itemPartyArmorDataBase.itemPartyArmorDataList.Find((ItemPartyArmorData data) => data.itemID == idList[n]);
				Debug.Log("防具データ取得完了");
				if (itemPartyArmorData != null)
				{
					int partyCharacterNumFromItemID = PlayerInventoryDataEquipAccess.GetPartyCharacterNumFromItemID(idList[n]);
					string characterPowerUpFlag = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[partyCharacterNumFromItemID].characterPowerUpFlag;
					if (!string.IsNullOrEmpty(characterPowerUpFlag) && PlayerFlagDataManager.scenarioFlagDictionary[characterPowerUpFlag])
					{
						Transform transform = PoolManager.Pools["Craft Item Pool"].Spawn(craftManager.scrollItemGoArray[3]);
						ParameterContainer component = transform.transform.GetComponent<ParameterContainer>();
						RefreshItemList(transform, num++);
						string text = "armor" + itemPartyArmorData.itemID;
						Debug.Log(text);
						component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = text;
						component.GetVariable<UguiTextVariable>("powerText").text.text = itemPartyArmorData.defensePower.ToString();
						int characterIdForInventoryItem = PlayerInventoryDataAccess.GetCharacterIdForInventoryItem(itemPartyArmorData.itemID);
						component.GetVariable<I2LocalizeComponent>("characterNameLoc").localize.Term = "character" + characterIdForInventoryItem;
						component.GetVariable<UguiImage>("itemImage").image.sprite = craftManager.selectScrollContentSpriteArray[0];
						SetItemIconSprite(component.GetVariable<UguiImage>("iconImage").image, "armor");
						transform.GetComponent<MergeItemListClickAction>().itemID = itemPartyArmorData.itemID;
						component.GetGameObject("equipIconGo").SetActive(value: true);
						if (!craftCanvasManager.isCraftComplete)
						{
							SetFirstItemData(n, itemPartyArmorData.itemID, transform.gameObject);
							Debug.Log("防具データ設定完了");
						}
					}
				}
				else
				{
					Debug.Log("防具データなし");
				}
			}
			break;
		}
		}
		if (craftCanvasManager.isCraftComplete)
		{
			int scrollContentIndexFromItemId = craftManager.GetScrollContentIndexFromItemId(craftCheckManager.craftedItemID, 0);
			craftManager.itemSelectScrollContentGO.transform.GetChild(scrollContentIndexFromItemId).GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage")
				.image.sprite = craftManager.selectScrollContentSpriteArray[1];
			Debug.Log("選択中の項目は：" + scrollContentIndexFromItemId + "番目");
			craftManager.clickedItemID = craftCheckManager.craftedItemID;
		}
		craftTalkManager.TalkBalloonItemSelect();
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

	private void RefreshItemList(Transform transform, int i)
	{
		transform.SetParent(craftManager.itemSelectScrollContentGO.transform);
		transform.transform.localScale = new Vector3(1f, 1f, 1f);
		transform.transform.SetSiblingIndex(i);
		Debug.Log("GO名" + transform.gameObject.name);
	}

	private void SetItemIconSprite(Image imageComponent, string category)
	{
		Sprite sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[category];
		imageComponent.sprite = sprite;
	}

	private void SetFirstItemData(int loopNum, int itemID, GameObject go)
	{
		if (craftManager.clickedItemID == 0)
		{
			craftManager.clickedItemID = itemID;
			go.GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage").image.sprite = craftManager.selectScrollContentSpriteArray[1];
			Debug.Log("ループ番号：" + loopNum + "／選択アイテムID：" + craftManager.clickedItemID + "／GO名" + go.name);
		}
	}
}
