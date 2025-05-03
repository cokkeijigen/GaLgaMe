using System.Collections.Generic;
using System.Linq;
using Arbor;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DungeonGetItemManager : SerializedMonoBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private ArborFSM arborFSM;

	public DungeonCommonItemDataBase dungeonCommonItemDataBase;

	public GameObject mapOverlayCanvas;

	public ParameterContainer summaryWindowParameter;

	public List<GameObject> itemCategoryTabList;

	public List<TextMeshProUGUI> categoryCurrentNumText;

	public TextMeshProUGUI currentGetMoneyText;

	public GameObject viewWindowContentGo;

	public Scrollbar viewScrollbar;

	public Sprite[] categoryTabSpriteArray;

	public Sprite[] viewScrollSpriteArray;

	public Dictionary<string, Sprite> itemCategorySpriteDictionary;

	public GameObject getItemScrollSpawnGo;

	public GameObject getItemScrollPoolParent;

	public int selectTabNum;

	public int selectItemID;

	public int selectItemSiblingIndex;

	public List<KeyValuePair<int, int>> whereKeyValuePairsList;

	private void Start()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		arborFSM = GetComponent<ArborFSM>();
	}

	public void SetCategoryTabSprite()
	{
		if (selectTabNum == 0)
		{
			selectTabNum = 1;
		}
		foreach (GameObject itemCategoryTab in itemCategoryTabList)
		{
			itemCategoryTab.GetComponent<Image>().sprite = categoryTabSpriteArray[0];
		}
		itemCategoryTabList[selectTabNum].GetComponent<Image>().sprite = categoryTabSpriteArray[1];
	}

	public void PushCategoryTabButton(int num)
	{
		selectTabNum = num;
		arborFSM.SendTrigger("ChengeGetItemTab");
	}

	public int GetCommonItem(string categoryName)
	{
		int result = 0;
		DungeonMapData dungeonMapData = GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName);
		switch (categoryName)
		{
		case "collect":
		{
			int num2 = Random.Range(0, 100);
			if (PlayerDungeonBorderGetItemManager.separatorInMostLargeFloorList.Contains(dungeonMapManager.currentBorderNum) && num2 < 30)
			{
				result = PlayerDungeonBorderGetItemManager.GetInMostLargeFloorItemId(dungeonMapManager.currentBorderNum, categoryName);
				Debug.Log("区切りの最後の境界でアイテムを獲得／収集ID：" + result + "／現在の境界：" + dungeonMapManager.currentBorderNum);
				break;
			}
			int id4 = 0;
			if (Random.Range(0, 2) == 0)
			{
				id4 = dungeonMapData.collectCommonMaterialItemTable[dungeonMapManager.currentBorderNum];
			}
			else
			{
				id4 = dungeonMapData.collectCommonItemTable[dungeonMapManager.currentBorderNum];
			}
			List<int> itemDataIdList6 = dungeonCommonItemDataBase.dungeonCommonItemDataList.Find((DungeonCommonItemData data) => data.sortId == id4).itemDataIdList;
			if (Random.Range(0, 2) == 0)
			{
				int index6 = Random.Range(0, itemDataIdList6.Count);
				result = itemDataIdList6[index6];
			}
			else
			{
				result = GetDontHaveItemFromCommonItemList(itemDataIdList6);
			}
			break;
		}
		case "corpse":
		{
			int num = Random.Range(0, 100);
			if (PlayerDungeonBorderGetItemManager.separatorInMostLargeFloorList.Contains(dungeonMapManager.currentBorderNum) && num < 30)
			{
				result = PlayerDungeonBorderGetItemManager.GetInMostLargeFloorItemId(dungeonMapManager.currentBorderNum, categoryName);
				Debug.Log("区切りの最後の境界でアイテムを獲得／亡骸ID：" + result + "／現在の境界：" + dungeonMapManager.currentBorderNum);
				break;
			}
			int id3 = dungeonMapData.corpseCommonItemTable[dungeonMapManager.currentBorderNum];
			List<int> itemDataIdList5 = dungeonCommonItemDataBase.dungeonCommonItemDataList.Find((DungeonCommonItemData data) => data.sortId == id3).itemDataIdList;
			if (Random.Range(0, 2) == 0)
			{
				int index5 = Random.Range(0, itemDataIdList5.Count);
				result = itemDataIdList5[index5];
			}
			else
			{
				result = GetDontHaveItemFromCommonItemList(itemDataIdList5);
			}
			break;
		}
		case "treasure":
		{
			int id2 = dungeonMapData.treasureCommonItemTable[dungeonMapManager.currentBorderNum];
			List<int> itemDataIdList4 = dungeonCommonItemDataBase.dungeonCommonItemDataList.Find((DungeonCommonItemData data) => data.sortId == id2).itemDataIdList;
			int index4 = Random.Range(0, itemDataIdList4.Count);
			result = itemDataIdList4[index4];
			break;
		}
		case "rareItem":
		{
			int id = 0;
			switch (Random.Range(0, 5))
			{
			case 0:
			case 1:
			{
				id = dungeonMapData.rareAddOnCommonItemTable[dungeonMapManager.currentBorderNum];
				List<int> itemDataIdList3 = dungeonCommonItemDataBase.dungeonCommonItemDataList.Find((DungeonCommonItemData data) => data.sortId == id).itemDataIdList;
				int index3 = Random.Range(0, itemDataIdList3.Count);
				result = itemDataIdList3[index3];
				break;
			}
			case 2:
			{
				id = dungeonMapData.rareWonderCommonItemTable[dungeonMapManager.currentBorderNum];
				List<int> itemDataIdList2 = dungeonCommonItemDataBase.dungeonCommonItemDataList.Find((DungeonCommonItemData data) => data.sortId == id).itemDataIdList;
				int index2 = Random.Range(0, itemDataIdList2.Count);
				result = itemDataIdList2[index2];
				break;
			}
			case 3:
			case 4:
			{
				id = dungeonMapData.rarePowerCommonItemTable[dungeonMapManager.currentBorderNum];
				List<int> itemDataIdList = dungeonCommonItemDataBase.dungeonCommonItemDataList.Find((DungeonCommonItemData data) => data.sortId == id).itemDataIdList;
				int index = Random.Range(0, itemDataIdList.Count);
				result = itemDataIdList[index];
				break;
			}
			}
			break;
		}
		}
		return result;
	}

	public int GetDontHaveItemFromCommonItemList(List<int> commonList)
	{
		int result = 0;
		int num = 0;
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
		if (PlayerInventoryDataManager.haveItemMaterialList.Count == 0)
		{
			return 100;
		}
		for (int i = 0; i < PlayerInventoryDataManager.haveItemMaterialList.Count; i++)
		{
			dictionary2.Add(PlayerInventoryDataManager.haveItemMaterialList[i].itemID, PlayerInventoryDataManager.haveItemMaterialList[i].haveCountNum);
		}
		foreach (KeyValuePair<int, int> item in dungeonMapManager.getDropItemDictionary)
		{
			if (dictionary2.ContainsKey(item.Key))
			{
				dictionary2[item.Key] += item.Value;
			}
			else
			{
				dictionary2.Add(item.Key, item.Value);
			}
		}
		for (int j = 0; j < commonList.Count; j++)
		{
			if (!dictionary2.ContainsKey(commonList[j]))
			{
				dictionary.Add(commonList[j], 0);
			}
			else
			{
				dictionary.Add(commonList[j], dictionary2[commonList[j]]);
			}
		}
		using (IEnumerator<KeyValuePair<int, int>> enumerator2 = dictionary.OrderBy((KeyValuePair<int, int> data) => data.Value).GetEnumerator())
		{
			if (enumerator2.MoveNext())
			{
				KeyValuePair<int, int> current2 = enumerator2.Current;
				result = current2.Key;
				num = current2.Value;
			}
		}
		Debug.Log("コモンリストで一番所持数の少ないアイテムID：" + result + "／所持数：" + num);
		return result;
	}
}
