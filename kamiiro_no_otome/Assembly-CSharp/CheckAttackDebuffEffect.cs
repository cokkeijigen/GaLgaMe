using Arbor;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class CheckAttackDebuffEffect : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private BattleSkillData battleSkillData;

	private int skillSuccessRate;

	private int getResistPower;

	private bool isTargetEden;

	public StateLink effectHitLink;

	public StateLink badStateLink;

	public StateLink noEffectLink;

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
		battleSkillData = scenarioBattleTurnManager.useSkillData;
		scenarioBattleTurnManager.skillAttackHitDataSubList.Clear();
		getResistPower = 0;
		isTargetEden = false;
		if (battleSkillData.subType == BattleSkillData.SubType.none && battleSkillData.skillType != BattleSkillData.SkillType.deBuff)
		{
			Debug.Log("スキルタイプ：" + battleSkillData.subType.ToString() + "／付与効果なし");
			Transition(noEffectLink);
			return;
		}
		if (scenarioBattleTurnManager.isAllTargetSkill)
		{
			CalcAccuracy();
			return;
		}
		if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			int num = PlayerStatusDataManager.enemyResist[scenarioBattleTurnManager.playerTargetNum];
			skillSuccessRate = battleSkillData.successRate - num;
			skillSuccessRate = Mathf.Clamp(skillSuccessRate, 0, 100);
		}
		else
		{
			switch (battleSkillData.subType)
			{
			case BattleSkillData.SubType.debuff:
				getResistPower = PlayerEquipDataManager.accessoryResistDebuff[scenarioBattleTurnManager.enemyTargetNum];
				break;
			case BattleSkillData.SubType.poison:
				getResistPower = PlayerEquipDataManager.accessoryResistPoison[scenarioBattleTurnManager.enemyTargetNum];
				getResistPower += PlayerEquipDataManager.accessoryResistUp[scenarioBattleTurnManager.enemyTargetNum];
				getResistPower += PlayerEquipDataManager.accessoryResistAll[scenarioBattleTurnManager.enemyTargetNum];
				break;
			case BattleSkillData.SubType.paralyze:
				getResistPower = PlayerEquipDataManager.accessoryResistParalyze[scenarioBattleTurnManager.enemyTargetNum];
				getResistPower += PlayerEquipDataManager.accessoryResistUp[scenarioBattleTurnManager.enemyTargetNum];
				getResistPower += PlayerEquipDataManager.accessoryResistAll[scenarioBattleTurnManager.enemyTargetNum];
				break;
			case BattleSkillData.SubType.stagger:
				if (scenarioBattleTurnManager.enemyTargetNum == 0)
				{
					isTargetEden = true;
					break;
				}
				getResistPower = PlayerEquipDataManager.accessoryResistUp[scenarioBattleTurnManager.enemyTargetNum];
				getResistPower += PlayerEquipDataManager.accessoryResistAll[scenarioBattleTurnManager.enemyTargetNum];
				break;
			}
			ScenarioBattleData scenarioBattleData = GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData data) => data.scenarioName == PlayerNonSaveDataManager.resultScenarioName);
			if (PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.enemyTargetNum).memberID == scenarioBattleData.supportBattleCharacterID)
			{
				getResistPower += 100;
			}
			int num2 = PlayerStatusDataManager.characterResist[scenarioBattleTurnManager.enemyTargetNum] + getResistPower;
			skillSuccessRate = battleSkillData.successRate - num2;
			skillSuccessRate = Mathf.Clamp(skillSuccessRate, 0, 100);
		}
		int num3 = Random.Range(1, 100);
		Debug.Log($"サブ命中率：{skillSuccessRate}／スキル命中ランダム：{num3}");
		if (skillSuccessRate >= num3 && !isTargetEden)
		{
			scenarioBattleTurnManager.skillAttackHitDataSubList.Add(scenarioBattleTurnManager.skillAttackHitDataList[0]);
			CheckSubType();
		}
		else
		{
			utageBattleSceneManager.battleTextArray4[0].GetComponent<Localize>().Term = "battleTextNoEffect";
			utageBattleSceneManager.battleTextArray4[0].SetActive(value: true);
			Invoke("InvokeMethod", 0.3f);
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

	private void InvokeMethod()
	{
		Transition(noEffectLink);
	}

	private void CalcAccuracy()
	{
		skillSuccessRate = battleSkillData.successRate;
		for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
		{
			if (!scenarioBattleTurnManager.isUseSkillPlayer)
			{
				int num = PlayerStatusDataManager.playerPartyMember[i];
				switch (battleSkillData.subType)
				{
				case BattleSkillData.SubType.debuff:
					getResistPower = PlayerEquipDataManager.accessoryResistDebuff[num];
					break;
				case BattleSkillData.SubType.poison:
					getResistPower = PlayerEquipDataManager.accessoryResistPoison[num];
					getResistPower += PlayerEquipDataManager.accessoryResistUp[num];
					getResistPower += PlayerEquipDataManager.accessoryResistAll[num];
					break;
				case BattleSkillData.SubType.paralyze:
					getResistPower = PlayerEquipDataManager.accessoryResistParalyze[num];
					getResistPower += PlayerEquipDataManager.accessoryResistUp[num];
					getResistPower += PlayerEquipDataManager.accessoryResistAll[num];
					break;
				case BattleSkillData.SubType.stagger:
					if (scenarioBattleTurnManager.enemyTargetNum == 0)
					{
						isTargetEden = true;
						break;
					}
					getResistPower = PlayerEquipDataManager.accessoryResistUp[num];
					getResistPower += PlayerEquipDataManager.accessoryResistAll[num];
					break;
				}
				int num2 = PlayerStatusDataManager.characterResist[num] + getResistPower;
				skillSuccessRate = battleSkillData.successRate - num2;
				skillSuccessRate = Mathf.Clamp(skillSuccessRate, 0, 100);
			}
			int num3 = Random.Range(0, 100);
			Debug.Log("デバフ成功率：" + battleSkillData.successRate + "／ランダム：" + num3 + "／インデックス：" + i);
			if (skillSuccessRate >= num3 && battleSkillData.successRate > 0)
			{
				scenarioBattleTurnManager.skillAttackHitDataSubList.Add(scenarioBattleTurnManager.skillAttackHitDataList[i]);
				Debug.Log("全体デバフスキル／命中HitNum：" + scenarioBattleTurnManager.skillAttackHitDataList[i].memberNum);
			}
		}
		if (scenarioBattleTurnManager.skillAttackHitDataSubList.Count > 0)
		{
			CheckSubType();
			return;
		}
		utageBattleSceneManager.battleTextArray4[0].GetComponent<Localize>().Term = "battleTextNoEffect";
		utageBattleSceneManager.battleTextArray4[0].SetActive(value: true);
		Invoke("InvokeMethod", 0.3f);
		Debug.Log("デバフ発動なし");
	}

	private void CheckSubType()
	{
		if (battleSkillData.subType == BattleSkillData.SubType.buff || battleSkillData.subType == BattleSkillData.SubType.debuff || (battleSkillData.skillType == BattleSkillData.SkillType.deBuff && battleSkillData.skillPower != 0))
		{
			Transition(effectHitLink);
		}
		else if (scenarioBattleTurnManager.skillAttackHitDataSubList.Count == 1)
		{
			if (battleSkillData.subType == BattleSkillData.SubType.stagger && scenarioBattleTurnManager.skillAttackHitDataSubList[0].memberID == 0)
			{
				utageBattleSceneManager.battleTextArray4[0].GetComponent<Localize>().Term = "battleTextNoEffect";
				utageBattleSceneManager.battleTextArray4[0].SetActive(value: true);
				Invoke("InvokeMethod", 0.3f);
			}
			else
			{
				Debug.Log("対象はエデンではない");
				Transition(badStateLink);
			}
		}
		else
		{
			Debug.Log("対象は一人ではない");
			Transition(badStateLink);
		}
	}
}
