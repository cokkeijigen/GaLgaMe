using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcAttackTarget : StateBehaviour
{
	public enum Type
	{
		player,
		enemy,
		support
	}

	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public Type type;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
	}

	public override void OnStateBegin()
	{
		int num = 0;
		int num2 = 0;
		List<int> list = new List<int>();
		switch (type)
		{
		case Type.player:
		{
			while (PlayerBattleConditionManager.playerIsDead[scenarioBattleTurnManager.playerAttackCount].isDead || PlayerBattleConditionManager.playerIsDead[scenarioBattleTurnManager.playerAttackCount].memberID == utageBattleSceneManager.supportAttackMemberId)
			{
				scenarioBattleTurnManager.playerAttackCount++;
			}
			if (scenarioBattleTurnManager.playerFocusTargetNum != 9)
			{
				scenarioBattleTurnManager.playerTargetNum = scenarioBattleTurnManager.playerFocusTargetNum;
				break;
			}
			List<PlayerBattleConditionManager.MemberIsDead> list4 = PlayerBattleConditionManager.enemyIsDead.Where((PlayerBattleConditionManager.MemberIsDead value) => !value.isDead).ToList();
			foreach (PlayerBattleConditionManager.MemberIsDead item in list4)
			{
				Debug.Log("残っている敵ID：" + item.memberID);
			}
			num2 = Random.Range(0, list4.Count);
			Debug.Log("ランダム：" + num2 + "／リスト数：" + list4.Count);
			num = list4[num2].memberNum;
			Debug.Log("狙う敵のNum：" + num);
			scenarioBattleTurnManager.playerTargetNum = num;
			break;
		}
		case Type.enemy:
		{
			list.Clear();
			while (PlayerBattleConditionManager.enemyIsDead[scenarioBattleTurnManager.enemyAttackCount].isDead)
			{
				scenarioBattleTurnManager.enemyAttackCount++;
			}
			List<PlayerBattleConditionManager.MemberIsDead> list3 = PlayerBattleConditionManager.playerIsDead.Where((PlayerBattleConditionManager.MemberIsDead value) => !value.isDead).ToList();
			for (int i = 0; i < list3.Count; i++)
			{
				list.Add(list3[i].memberNum);
				if (list3[i].memberID != 3)
				{
					list.Add(list3[i].memberNum);
				}
			}
			num2 = Random.Range(0, list.Count);
			num = list[num2];
			int num3 = 9;
			for (int j = 0; j < PlayerStatusDataManager.playerPartyMember.Length; j++)
			{
				if (PlayerBattleConditionManager.playerBuffCondition[j].Find((PlayerBattleConditionManager.MemberBuffCondition data) => data.type == "hateUp") != null)
				{
					num3 = PlayerBattleConditionManager.playerIsDead[j].memberNum;
				}
			}
			if (num3 != 9 && Random.Range(0, 100) > 30)
			{
				num = num3;
				Debug.Log("攻撃対象をヘイト上昇キャラに変更／num：" + num3);
			}
			scenarioBattleTurnManager.enemyTargetNum = num;
			break;
		}
		case Type.support:
		{
			if (scenarioBattleTurnManager.playerFocusTargetNum != 9)
			{
				scenarioBattleTurnManager.playerTargetNum = scenarioBattleTurnManager.playerFocusTargetNum;
				break;
			}
			List<PlayerBattleConditionManager.MemberIsDead> list2 = PlayerBattleConditionManager.enemyIsDead.Where((PlayerBattleConditionManager.MemberIsDead value) => !value.isDead).ToList();
			foreach (PlayerBattleConditionManager.MemberIsDead item2 in list2)
			{
				Debug.Log("残っている敵ID：" + item2.memberID);
			}
			num2 = Random.Range(0, list2.Count);
			Debug.Log("ランダム：" + num2 + "／リスト数：" + list2.Count);
			num = list2[num2].memberNum;
			Debug.Log("狙う敵のNum：" + num);
			scenarioBattleTurnManager.playerTargetNum = num;
			break;
		}
		}
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
