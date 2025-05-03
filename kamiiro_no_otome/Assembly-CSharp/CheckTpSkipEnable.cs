using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckTpSkipEnable : StateBehaviour
{
	private DungeonMapStatusManager dungeonMapStatusManager;

	private ParameterContainer agilityContainer;

	private ArborFSM dungeonSkillFSM;

	public StateLink disableLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapStatusManager = GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapStatusManager>();
		agilityContainer = GameObject.Find("Dungeon Agility Manager").GetComponent<ParameterContainer>();
		dungeonSkillFSM = GameObject.Find("Dungeon Skill Manager").GetComponent<ArborFSM>();
	}

	public override void OnStateBegin()
	{
		bool num = PlayerStatusDataManager.enemyMember.Contains(360);
		bool flag = PlayerStatusDataManager.enemyMember.Contains(361);
		bool flag2 = PlayerStatusDataManager.enemyMember.Contains(362);
		bool flag3 = PlayerStatusDataManager.enemyMember.Contains(363);
		if (num || flag || flag2 || flag3)
		{
			Transition(disableLink);
			return;
		}
		if (!dungeonMapStatusManager.isTpSkipEnable)
		{
			Transition(disableLink);
			return;
		}
		IList<string> stringList = agilityContainer.GetStringList("AgilityQueueList");
		stringList.Add("t0");
		agilityContainer.SetStringList("AgilityQueueList", stringList);
		dungeonSkillFSM.SendTrigger("UseBattleSkill");
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
