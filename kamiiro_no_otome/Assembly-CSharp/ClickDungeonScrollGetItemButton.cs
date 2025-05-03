using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ClickDungeonScrollGetItemButton : StateBehaviour
{
	private DungeonGetItemManager dungeonGetItemManager;

	private ArborFSM arborFSM;

	private ParameterContainer parameterContainer;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonGetItemManager = GameObject.Find("Dungeon Get Item Manager").GetComponent<DungeonGetItemManager>();
		arborFSM = dungeonGetItemManager.GetComponent<ArborFSM>();
		parameterContainer = GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		dungeonGetItemManager.selectItemID = parameterContainer.GetInt("itemID");
		dungeonGetItemManager.selectItemSiblingIndex = base.transform.GetSiblingIndex();
		arborFSM.SendTrigger("PushScrollContent");
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
