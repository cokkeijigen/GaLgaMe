using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class DungeonUseItemAfterListRefresh : StateBehaviour
{
	private DungeonItemManager dungeonItemManager;

	public StateLink stateLink;

	public StateLink noStatusLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonItemManager = GetComponent<DungeonItemManager>();
	}

	public override void OnStateBegin()
	{
		Transform child = dungeonItemManager.useItemScrollContentGo.transform.GetChild(dungeonItemManager.selectItemSiblingIndex);
		ParameterContainer component = child.GetComponent<ParameterContainer>();
		HaveItemData haveItemData = PlayerInventoryDataManager.haveItemList.Find((HaveItemData data) => data.itemID == dungeonItemManager.selectItemID);
		int haveCountNum = haveItemData.haveCountNum;
		if (haveCountNum >= 1)
		{
			Debug.Log("所持数を減らす");
			component.GetVariable<UguiTextVariable>("haveNumText").text.text = haveCountNum.ToString();
			Transition(stateLink);
			return;
		}
		Debug.Log("項目を削除する");
		PlayerInventoryDataManager.haveItemList.Remove(haveItemData);
		PoolManager.Pools["DungeonUseItem"].Despawn(child);
		child.transform.SetParent(dungeonItemManager.useItemPoolParentGo.transform);
		if (dungeonItemManager.useItemScrollContentGo.transform.childCount > 0)
		{
			Transform child2 = dungeonItemManager.useItemScrollContentGo.transform.GetChild(0);
			dungeonItemManager.selectItemID = child2.GetComponent<ParameterContainer>().GetInt("itemID");
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
}
