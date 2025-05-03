using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ClickDungeonScrollItemButton : StateBehaviour
{
	private DungeonItemManager dungeonItemManager;

	private ArborFSM arborFSM;

	private ParameterContainer parameterContainer;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonItemManager = GameObject.Find("Dungeon Item Manager").GetComponent<DungeonItemManager>();
		arborFSM = dungeonItemManager.GetComponent<ArborFSM>();
		parameterContainer = GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		dungeonItemManager.selectItemID = parameterContainer.GetInt("itemID");
		dungeonItemManager.selectItemSiblingIndex = base.transform.GetSiblingIndex();
		arborFSM.SendTrigger("SendItemListIndex");
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
