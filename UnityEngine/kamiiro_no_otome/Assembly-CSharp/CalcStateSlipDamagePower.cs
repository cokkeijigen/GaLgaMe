using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcStateSlipDamagePower : StateBehaviour
{
	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public StateLink noSlipLink;

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
		List<int> list = new List<int>();
		scenarioBattleTurnManager.skillAttackHitDataList.Clear();
		scenarioBattleTurnManager.skillAttackHitDataSubList.Clear();
		for (int i = 0; i < PlayerBattleConditionManager.playerIsDead.Count; i++)
		{
			list = (from ano in PlayerBattleConditionManager.playerBadState[i].Select((PlayerBattleConditionManager.MemberBadState con, int index) => new
				{
					Content = con,
					Index = index
				})
				where ano.Content.type == "poison"
				select ano.Index).ToList();
			if (!list.Any())
			{
				continue;
			}
			if (PlayerBattleConditionManager.playerBadState[i][list[0]].continutyTurn != 0)
			{
				int id = PlayerBattleConditionManager.playerIsDead[i].memberID;
				int memberNum = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id).memberNum;
				float num = (float)PlayerStatusDataManager.characterMaxHp[i] * 0.08f;
				float num2 = Random.Range(0.9f, 1.1f);
				int attackDamage = Mathf.FloorToInt(num * num2);
				SkillAttackHitData item = new SkillAttackHitData
				{
					memberID = id,
					memberNum = memberNum,
					attackDamage = attackDamage
				};
				scenarioBattleTurnManager.skillAttackHitDataList.Add(item);
				Debug.Log("味方毒ダメージ／Num：" + memberNum + "／インデックス：" + i);
			}
			else
			{
				Debug.Log("味方毒ダメージは残りターン0／Num：" + PlayerBattleConditionManager.playerIsDead[i].memberNum + "／インデックス：" + i);
			}
		}
		for (int j = 0; j < PlayerBattleConditionManager.enemyIsDead.Count; j++)
		{
			list = (from ano in PlayerBattleConditionManager.enemyBadState[j].Select((PlayerBattleConditionManager.MemberBadState con, int index) => new
				{
					Content = con,
					Index = index
				})
				where ano.Content.type == "poison"
				select ano.Index).ToList();
			if (list.Any() && PlayerBattleConditionManager.enemyBadState[j][list[0]].continutyTurn != 0)
			{
				float num3 = (float)PlayerStatusDataManager.enemyMaxHp[j] * 0.08f;
				float num4 = Random.Range(0.9f, 1.1f);
				int attackDamage2 = Mathf.FloorToInt(num3 * num4);
				int memberNum2 = PlayerBattleConditionManager.enemyIsDead[j].memberNum;
				SkillAttackHitData item2 = new SkillAttackHitData
				{
					memberNum = memberNum2,
					attackDamage = attackDamage2
				};
				scenarioBattleTurnManager.skillAttackHitDataSubList.Add(item2);
			}
		}
		if (scenarioBattleTurnManager.skillAttackHitDataList.Any() || scenarioBattleTurnManager.skillAttackHitDataSubList.Any())
		{
			Debug.Log("毒メンバーがいる");
			Transition(stateLink);
		}
		else
		{
			Debug.Log("毒メンバーはいない");
			Transition(noSlipLink);
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
