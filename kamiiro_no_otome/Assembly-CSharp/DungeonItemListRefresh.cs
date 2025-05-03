using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class DungeonItemListRefresh : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonItemManager dungeonItemManager;

	public StateLink stateLink;

	public StateLink noStatusLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		dungeonItemManager = GetComponent<DungeonItemManager>();
	}

	public override void OnStateBegin()
	{
		dungeonItemManager.useItemWindowGo.SetActive(value: true);
		GameObject[] array = GameObject.FindGameObjectsWithTag("StatusScrollItem");
		PoolManager.Pools["DungeonUseItem"].DespawnAll();
		if (array.Length != 0 && array != null)
		{
			GameObject[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].transform.SetParent(dungeonItemManager.useItemPoolParentGo.transform);
			}
		}
		int num = int.MinValue;
		int j;
		for (j = 0; j < PlayerInventoryDataManager.haveItemList.Count; j++)
		{
			ItemData itemData = GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData item) => item.itemID == PlayerInventoryDataManager.haveItemList[j].itemID);
			if (itemData != null)
			{
				if (num == int.MinValue)
				{
					num = PlayerInventoryDataManager.haveItemList[j].itemID;
				}
				Transform transform = PoolManager.Pools["DungeonUseItem"].Spawn(dungeonItemManager.useItemScrollPrefabGo);
				RefreshItemList(transform, j);
				ParameterContainer component = transform.GetComponent<ParameterContainer>();
				string term = itemData.category.ToString() + itemData.itemID;
				component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = term;
				component.GetVariable<UguiTextVariable>("haveNumText").text.text = PlayerInventoryDataManager.haveItemList[j].haveCountNum.ToString();
				string text = itemData.category.ToString();
				if (text != "")
				{
					SetItemIconSprite(transform, component, text);
				}
				component.SetInt("itemID", PlayerInventoryDataManager.haveItemList[j].itemID);
			}
		}
		if (dungeonItemManager.useItemScrollContentGo.transform.childCount > 0)
		{
			dungeonItemManager.selectItemID = num;
			dungeonItemManager.selectItemSiblingIndex = 0;
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
		transform.SetParent(dungeonItemManager.useItemScrollContentGo.transform);
		transform.transform.localScale = new Vector3(1f, 1f, 1f);
		transform.transform.SetSiblingIndex(i);
	}

	private void SetItemIconSprite(Transform go, ParameterContainer param, string category)
	{
		Sprite sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[category];
		param.GetVariable<UguiImage>("iconImage").image.sprite = sprite;
	}
}
