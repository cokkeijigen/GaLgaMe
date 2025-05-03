using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class DungeonItemWaitReset : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GameObject.Find("Dungeon Battle Manager").GetComponent<DungeonBattleManager>();
	}

	public override void OnStateBegin()
	{
		dungeonBattleManager.itemWaitSlider.value = 0f;
		dungeonBattleManager.itemButton.GetComponent<CanvasGroup>().interactable = false;
		dungeonBattleManager.itemButton.GetComponent<CanvasGroup>().alpha = 0.5f;
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
