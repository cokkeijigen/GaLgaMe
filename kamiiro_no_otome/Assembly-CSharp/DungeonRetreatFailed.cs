using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class DungeonRetreatFailed : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private ParameterContainer parameterContainer;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GetComponent<DungeonBattleManager>();
		parameterContainer = GameObject.Find("Dungeon Agility Manager").GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		dungeonBattleManager.messageWindowGO.SetActive(value: true);
		float time = 0.4f / (float)PlayerDataManager.dungeonBattleSpeed;
		Invoke("InvokeMethod", time);
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

	private void InvokeMethod()
	{
		dungeonBattleManager.messageWindowGO.SetActive(value: false);
		dungeonBattleManager.RestartAglityCoroutine();
		dungeonBattleManager.isRetreat = false;
		parameterContainer.GetStringList("AgilityQueueList").RemoveAt(0);
		parameterContainer.SetBool("isRetreatQueued", value: false);
		Transition(stateLink);
	}
}
