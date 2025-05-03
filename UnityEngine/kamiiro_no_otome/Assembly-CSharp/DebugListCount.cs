using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class DebugListCount : StateBehaviour
{
	private DebugDummyManager debugDummyManager;

	public List<int> numList;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		debugDummyManager = GetComponent<DebugDummyManager>();
	}

	public override void OnStateBegin()
	{
		PlayerBattleConditionManager.playerBuffCondition.Add(new List<PlayerBattleConditionManager.MemberBuffCondition>());
		for (int i = 0; i < PlayerBattleConditionManager.playerBuffCondition.Count; i++)
		{
			numList = (from ano in PlayerBattleConditionManager.playerBuffCondition[i].Select((PlayerBattleConditionManager.MemberBuffCondition con, int index) => new
				{
					Content = con,
					Index = index
				})
				where ano.Content.type == "attack"
				select ano.Index).ToList();
		}
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
