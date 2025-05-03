using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckAgilityQueue : StateBehaviour
{
	private ArborFSM dungeonItemFSM;

	private PlayMakerFSM battleDialogFSM;

	private DungeonBattleManager dungeonBattleManager;

	private ParameterContainer parameterContainer;

	private IList<string> queueList;

	public StateLink stateLink;

	public StateLink commandLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		parameterContainer = GetComponent<ParameterContainer>();
		dungeonBattleManager = GameObject.Find("Dungeon Battle Manager").GetComponent<DungeonBattleManager>();
		dungeonItemFSM = GameObject.Find("Dungeon Item Manager").GetComponent<ArborFSM>();
		battleDialogFSM = GameObject.Find("Battle Dialog Manager").GetComponent<PlayMakerFSM>();
	}

	public override void OnStateBegin()
	{
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		queueList = parameterContainer.GetStringList("AgilityQueueList");
		if (queueList != null && queueList.Count > 0 && !dungeonBattleManager.isRetreat)
		{
			switch (parameterContainer.GetStringList("AgilityQueueList")[0].Substring(0, 1))
			{
			case "p":
			case "e":
			case "c":
				Transition(stateLink);
				break;
			case "i":
				parameterContainer.SetBool("isItemQueued", value: true);
				dungeonItemFSM.SendTrigger("ItemQueueStart");
				Debug.Log("アイテム使用を送信");
				Transition(commandLink);
				break;
			case "r":
				parameterContainer.SetBool("isRetreatQueued", value: true);
				battleDialogFSM.SendEvent("RetreatQueueStart");
				Debug.Log("撤退開始を送信");
				Transition(commandLink);
				break;
			}
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
