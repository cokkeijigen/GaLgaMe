using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class RestartCharacterAgility : StateBehaviour
{
	private ParameterContainer parameterContainer;

	private DungeonBattleManager dungeonBattleManager;

	private IList<string> list;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		parameterContainer = GetComponent<ParameterContainer>();
		dungeonBattleManager = GameObject.Find("Dungeon Battle Manager").GetComponent<DungeonBattleManager>();
	}

	public override void OnStateBegin()
	{
		list = parameterContainer.GetStringList("AgilityQueueList");
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (parameterContainer.GetBool("isItemQueued") || parameterContainer.GetBool("isRetreatQueued"))
		{
			return;
		}
		string text = list[0].Substring(0, 1);
		int index = int.Parse(list[0].Substring(1));
		if (!(text == "p"))
		{
			if (text == "e")
			{
				dungeonBattleManager.enemyAgilityGoList[index].GetComponent<ArborFSM>().SendTrigger("ResetEnemyAgility");
			}
		}
		else
		{
			dungeonBattleManager.playerAgilityGoList[index].GetComponent<ArborFSM>().SendTrigger("ResetAgility");
		}
		list.RemoveAt(0);
		Transition(stateLink);
	}

	public override void OnStateLateUpdate()
	{
	}
}
