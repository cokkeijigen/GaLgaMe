using System.Collections.Generic;
using System.Linq;
using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class DungeonOpenGetItemWindow : StateBehaviour
{
	private DungeonGetItemManager dungeonGetItemManager;

	private DungeonMapManager dungeonMapManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonGetItemManager = GetComponent<DungeonGetItemManager>();
		dungeonMapManager = GetComponentInParent<DungeonMapManager>();
		dungeonGetItemManager.selectTabNum = 0;
	}

	public override void OnStateBegin()
	{
		dungeonGetItemManager.mapOverlayCanvas.SetActive(value: true);
		dungeonGetItemManager.viewScrollbar.value = 1f;
		dungeonGetItemManager.SetCategoryTabSprite();
		GameObject[] array = new GameObject[dungeonGetItemManager.viewWindowContentGo.transform.childCount];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = dungeonGetItemManager.viewWindowContentGo.transform.GetChild(i).gameObject;
		}
		Debug.Log("ダンジョン獲得アイテムをいったんデスポーンする");
		if (array.Length != 0 && array != null)
		{
			for (int j = 0; j < array.Length; j++)
			{
				if (PoolManager.Pools["DungeonObject"].IsSpawned(array[j].transform))
				{
					PoolManager.Pools["DungeonObject"].Despawn(array[j].transform);
					array[j].transform.SetParent(dungeonGetItemManager.getItemScrollPoolParent.transform);
				}
			}
		}
		int num = 0;
		num = GetCategoryCurrentNum(100, 599);
		dungeonGetItemManager.categoryCurrentNumText[1].text = num.ToString();
		num = GetCategoryCurrentNum(680, 899);
		dungeonGetItemManager.categoryCurrentNumText[3].text = num.ToString();
		num = GetCategoryCurrentNum(950, 999);
		dungeonGetItemManager.categoryCurrentNumText[4].text = num.ToString();
		string text = $"{dungeonMapManager.getDropMoney:#,0}";
		dungeonGetItemManager.currentGetMoneyText.text = text;
		switch (dungeonGetItemManager.selectTabNum)
		{
		case 0:
			SpawnScrollContents(0, 99);
			break;
		case 1:
			SpawnScrollContents(100, 599);
			break;
		case 2:
			SpawnScrollContents(600, 649);
			break;
		case 3:
			SpawnScrollContents(680, 899);
			break;
		case 4:
			SpawnScrollContents(950, 999);
			break;
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

	private void SpawnScrollContents(int minID, int maxID)
	{
		int num = 0;
		string text = "";
		List<KeyValuePair<int, int>> source = dungeonMapManager.getDropItemDictionary.Where((KeyValuePair<int, int> item) => item.Key >= minID).ToList();
		dungeonGetItemManager.whereKeyValuePairsList = source.Where((KeyValuePair<int, int> item) => item.Key <= maxID).ToList();
		if (dungeonGetItemManager.whereKeyValuePairsList.Count() == 0)
		{
			Debug.Log("獲得アイテムで表示するリストはゼロ");
			return;
		}
		foreach (KeyValuePair<int, int> dictionary in dungeonGetItemManager.whereKeyValuePairsList)
		{
			Transform transform = PoolManager.Pools["DungeonObject"].Spawn(dungeonGetItemManager.getItemScrollSpawnGo);
			ParameterContainer component = transform.GetComponent<ParameterContainer>();
			RefreshItemList(transform, num++);
			text = ((dictionary.Key >= 100) ? ((dictionary.Key >= 600) ? ((dictionary.Key >= 680) ? ((dictionary.Key >= 899) ? GameDataManager.instance.itemCashableItemDataBase.itemCashableItemDataList.Find((ItemCashableItemData item) => item.itemID == dictionary.Key).category.ToString() : GameDataManager.instance.itemMagicMaterialDataBase.itemMagicMaterialDataList.Find((ItemMagicMaterialData item) => item.itemID == dictionary.Key).category.ToString()) : GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Find((ItemCanMakeMaterialData item) => item.itemID == dictionary.Key).category.ToString()) : GameDataManager.instance.itemMaterialDataBase.itemMaterialDataList.Find((ItemMaterialData item) => item.itemID == dictionary.Key).category.ToString()) : GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData item) => item.itemID == dictionary.Key).category.ToString());
			Sprite sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[text];
			component.GetVariable<UguiImage>("iconImage").image.sprite = sprite;
			component.SetInt("itemID", dictionary.Key);
			component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = text + dictionary.Key;
			component.GetVariable<UguiTextVariable>("numText").text.text = dictionary.Value.ToString();
		}
		dungeonGetItemManager.selectItemID = source.FirstOrDefault().Key;
		dungeonGetItemManager.selectItemSiblingIndex = 0;
	}

	private void RefreshItemList(Transform transform, int index)
	{
		transform.SetParent(dungeonGetItemManager.viewWindowContentGo.transform);
		transform.transform.localScale = new Vector3(1f, 1f, 1f);
		transform.transform.SetSiblingIndex(index);
	}

	private int GetCategoryCurrentNum(int minID, int maxID)
	{
		int num = 0;
		List<KeyValuePair<int, int>> list = dungeonMapManager.getDropItemDictionary.Where((KeyValuePair<int, int> item) => item.Key >= minID && item.Key <= maxID).ToList();
		if (list.Count == 0)
		{
			return 0;
		}
		foreach (KeyValuePair<int, int> item in list)
		{
			num += item.Value;
		}
		return num;
	}
}
