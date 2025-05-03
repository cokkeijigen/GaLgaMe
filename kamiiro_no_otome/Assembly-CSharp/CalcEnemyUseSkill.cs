using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcEnemyUseSkill : StateBehaviour
{
	private ScenarioBattleTurnManager scenarioBattleTurnManager;

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
		Dictionary<int, float> dictionary = new Dictionary<int, float>();
		if (scenarioBattleTurnManager.battleUseSkillID == 0)
		{
			int enemyID = PlayerBattleConditionManager.enemyIsDead[scenarioBattleTurnManager.enemyAttackCount].memberID;
			dictionary = GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData data) => data.enemyID == enemyID).useSkillDictionary;
			scenarioBattleTurnManager.battleUseSkillID = RandomManager.GetRandomValue(dictionary);
			Debug.Log("使用スキル：" + scenarioBattleTurnManager.battleUseSkillID);
		}
		CalcEnemySkillData();
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

	private void CalcEnemySkillData()
	{
		int num = 0;
		BattleSkillData battleSkillData = null;
		if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			num = GameDataManager.instance.playerSkillDataBase.skillDataList.FindIndex((BattleSkillData item) => item.skillID == scenarioBattleTurnManager.battleUseSkillID);
			battleSkillData = GameDataManager.instance.playerSkillDataBase.skillDataList[num];
			return;
		}
		num = GameDataManager.instance.enemySkillDataBase.skillDataList.FindIndex((BattleSkillData item) => item.skillID == scenarioBattleTurnManager.battleUseSkillID);
		if (num != -1)
		{
			battleSkillData = GameDataManager.instance.enemySkillDataBase.skillDataList[num];
			Debug.Log("スキルデータ取得：" + battleSkillData.skillName);
		}
		scenarioBattleTurnManager.enemySkillData = battleSkillData;
	}
}
