using Arbor;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class SetDungeonBattleItemStartText : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	public InputSlotAny inputItemData;

	public float waitTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GetComponentInParent<DungeonBattleManager>();
	}

	public override void OnStateBegin()
	{
		ItemData value = null;
		inputItemData.TryGetValue<ItemData>(out value);
		dungeonBattleManager.messageWindowGO.SetActive(value: true);
		dungeonBattleManager.messageTextArray[0].GetComponent<Localize>().Term = value.category.ToString() + value.itemID;
		dungeonBattleManager.messageTextArray[1].GetComponent<Localize>().Term = "battleTextUseItem";
		Invoke("InvokeMethod", waitTime);
	}

	public override void OnStateEnd()
	{
		dungeonBattleManager.messageWindowGO.SetActive(value: false);
	}

	public override void OnStateUpdate()
	{
	}

	public override void OnStateLateUpdate()
	{
	}

	private void InvokeMethod()
	{
		Transition(stateLink);
	}
}
