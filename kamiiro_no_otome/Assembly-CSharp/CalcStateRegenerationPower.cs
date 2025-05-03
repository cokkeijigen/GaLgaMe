using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcStateRegenerationPower : StateBehaviour
{
	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public StateLink noRegeneLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
	}

	public override void OnStateBegin()
	{
		new List<int>();
		scenarioBattleTurnManager.skillAttackHitDataList.Clear();
		scenarioBattleTurnManager.skillAttackHitDataSubList.Clear();
		for (int i = 0; i < PlayerBattleConditionManager.playerIsDead.Count; i++)
		{
			if ((from ano in PlayerBattleConditionManager.playerBuffCondition[i].Select((PlayerBattleConditionManager.MemberBuffCondition con, int index) => new
				{
					Content = con,
					Index = index
				})
				where ano.Content.type == "regeneration"
				select ano.Index).ToList().Any())
			{
				int id = PlayerBattleConditionManager.playerIsDead[i].memberID;
				int memberNum = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id).memberNum;
				float num = (float)PlayerStatusDataManager.characterMaxHp[id] * 0.1f;
				float num2 = Random.Range(0.9f, 1.1f);
				int healValue = Mathf.FloorToInt(num * num2);
				SkillAttackHitData item = new SkillAttackHitData
				{
					memberID = id,
					memberNum = memberNum,
					healValue = healValue
				};
				scenarioBattleTurnManager.skillAttackHitDataList.Add(item);
				Debug.Log("味方リジェネ／Num：" + memberNum + "／インデックス：" + i);
			}
		}
		int j;
		for (j = 0; j < PlayerBattleConditionManager.enemyIsDead.Count; j++)
		{
			if ((from ano in PlayerBattleConditionManager.enemyBuffCondition[j].Select((PlayerBattleConditionManager.MemberBuffCondition con, int index) => new
				{
					Content = con,
					Index = index
				})
				where ano.Content.type == "regeneration"
				select ano.Index).ToList().Any())
			{
				int memberNum2 = PlayerBattleConditionManager.enemyIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == j).memberNum;
				float num3 = (float)PlayerStatusDataManager.enemyMaxHp[j] * 0.1f;
				float num4 = Random.Range(0.9f, 1.1f);
				int healValue2 = Mathf.FloorToInt(num3 * num4);
				SkillAttackHitData item2 = new SkillAttackHitData
				{
					memberNum = memberNum2,
					healValue = healValue2
				};
				scenarioBattleTurnManager.skillAttackHitDataSubList.Add(item2);
			}
		}
		if (scenarioBattleTurnManager.skillAttackHitDataList.Any() || scenarioBattleTurnManager.skillAttackHitDataSubList.Any())
		{
			Debug.Log("リジェネがいる");
			Transition(stateLink);
		}
		else
		{
			Debug.Log("リジェネはいない");
			Transition(noRegeneLink);
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
