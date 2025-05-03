using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class EndDungeonBattleItem : StateBehaviour
{
	private ParameterContainer parameterContainer;

	private DungeonBattleManager dungeonBattleManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GetComponentInParent<DungeonBattleManager>();
		parameterContainer = GameObject.Find("Dungeon Agility Manager").GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		parameterContainer.GetStringList("AgilityQueueList").RemoveAt(0);
		parameterContainer.SetBool("isItemQueued", value: false);
		dungeonBattleManager.itemWaitSlider.GetComponent<ArborFSM>().SendTrigger("ResetItemWait");
		dungeonBattleManager.dungeonBattleCanvas.GetComponent<CanvasGroup>().interactable = true;
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
