using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class UseDungeonBattleItem : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private DungeonItemManager dungeonItemManager;

	public OutputSlotAny outputSlotItemData;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GetComponentInParent<DungeonBattleManager>();
		dungeonItemManager = GetComponent<DungeonItemManager>();
	}

	public override void OnStateBegin()
	{
		PlayerInventoryDataAccess.ConsumePlayerHaveItems_COUNT(dungeonItemManager.selectItemID, 1);
		ItemData value = GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData data) => data.itemID == dungeonItemManager.selectItemID);
		outputSlotItemData.SetValue(value);
		dungeonBattleManager.dungeonBattleCanvas.GetComponent<CanvasGroup>().interactable = false;
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
