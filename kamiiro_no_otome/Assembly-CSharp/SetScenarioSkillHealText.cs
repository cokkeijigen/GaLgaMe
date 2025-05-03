using System.Collections.Generic;
using Arbor;
using DarkTonic.MasterAudio;
using I2.Loc;
using PathologicalGames;
using TMPro;
using UnityEngine;

[AddComponentMenu("")]
public class SetScenarioSkillHealText : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	private BattleSkillData battleSkillData;

	private List<Transform> poolGoList = new List<Transform>();

	public float despawnTime;

	public StateLink healLink;

	public StateLink medicLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
	}

	public override void OnStateBegin()
	{
		int num = 0;
		bool active = true;
		battleSkillData = scenarioBattleTurnManager.useSkillData;
		string text = battleSkillData.skillType.ToString();
		poolGoList.Clear();
		if (scenarioBattleTurnManager.isAllTargetSkill)
		{
			if (scenarioBattleTurnManager.isUseSkillPlayer)
			{
				string allTargetTerm = scenarioBattleSkillManager.GetAllTargetTerm();
				utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = allTargetTerm;
			}
			else
			{
				utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "battleTextEnemyAllTarget";
			}
			for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
			{
				num += scenarioBattleTurnManager.skillAttackHitDataList[i].healValue;
			}
			num /= scenarioBattleTurnManager.skillAttackHitDataList.Count;
			utageBattleSceneManager.battleTextArray3[2].GetComponent<TextMeshProUGUI>().text = num.ToString();
		}
		else
		{
			if (scenarioBattleTurnManager.isUseSkillPlayer)
			{
				int memberID = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.playerTargetNum).memberID;
				utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "character" + memberID;
			}
			else
			{
				int num2 = PlayerStatusDataManager.enemyMember[scenarioBattleTurnManager.enemyTargetNum];
				utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "enemy" + num2;
			}
			if (battleSkillData.skillType == BattleSkillData.SkillType.heal)
			{
				utageBattleSceneManager.battleTextArray3[2].GetComponent<TextMeshProUGUI>().text = scenarioBattleTurnManager.skillAttackHitDataList[0].healValue.ToString();
			}
		}
		switch (text)
		{
		case "heal":
			active = true;
			CalcHealSkill();
			break;
		case "medic":
			active = false;
			CalcMedicSkill();
			break;
		case "revive":
			active = false;
			CalcReviveSkill();
			break;
		}
		utageBattleSceneManager.battleTextGroupArray[2].SetActive(value: true);
		utageBattleSceneManager.battleTextArray3[0].SetActive(value: true);
		utageBattleSceneManager.battleTextArray3[1].SetActive(value: true);
		utageBattleSceneManager.battleTextArray3[2].SetActive(active);
		utageBattleSceneManager.battleTextArray3[3].SetActive(active);
		string effectType = battleSkillData.effectType;
		string text2 = effectType.Substring(0, 1).ToUpper();
		effectType = "SeSkill" + text2 + effectType.Substring(1);
		MasterAudio.PlaySound(effectType, 1f, null, 0f, null, null);
		switch (text)
		{
		case "heal":
		case "revive":
			Transition(healLink);
			break;
		case "medic":
			Transition(medicLink);
			break;
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

	private void CalcHealSkill()
	{
		if (scenarioBattleTurnManager.isAllTargetSkill)
		{
			utageBattleSceneManager.battleTextArray3[1].GetComponent<Localize>().Term = "battleTextEffectHpAverage";
		}
		else
		{
			utageBattleSceneManager.battleTextArray3[1].GetComponent<Localize>().Term = "battleTextEffectHp";
		}
		utageBattleSceneManager.battleTextArray3[3].GetComponent<Localize>().Term = "battleTextEffectHpHeal";
		if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
			{
				int memberNum = scenarioBattleTurnManager.skillAttackHitDataList[i].memberNum;
				Transform transform = utageBattleSceneManager.playerFrameGoList[memberNum].transform;
				utageBattleSceneManager.damagePointRect[i].position = new Vector3(transform.position.x, -2f, 0f);
				poolGoList.Add(PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[3], utageBattleSceneManager.damagePointRect[i]));
			}
		}
		else
		{
			for (int j = 0; j < scenarioBattleTurnManager.skillAttackHitDataList.Count; j++)
			{
				Transform transform = utageBattleSceneManager.enemyImageGoList[scenarioBattleTurnManager.skillAttackHitDataList[j].memberNum].transform;
				utageBattleSceneManager.damagePointRect[j].position = new Vector3(transform.position.x, 0f, 0f);
				poolGoList.Add(PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[3], utageBattleSceneManager.damagePointRect[j]));
			}
		}
		for (int k = 0; k < poolGoList.Count; k++)
		{
			poolGoList[k].GetComponent<TextMeshProUGUI>().text = scenarioBattleTurnManager.skillAttackHitDataList[k].healValue.ToString();
			PoolManager.Pools["BattleEffect"].Despawn(poolGoList[k], despawnTime, utageBattleSceneManager.poolManagerGO);
		}
	}

	private void CalcMedicSkill()
	{
		Localize component = utageBattleSceneManager.battleTextArray3[1].GetComponent<Localize>();
		switch (battleSkillData.skillPower)
		{
		case 0:
			if (scenarioBattleTurnManager.isMedicineNoTarget)
			{
				component.Term = "battleTextEffectNoSoloTarget";
			}
			else
			{
				component.Term = "battleTextEffectMedicPoison";
			}
			break;
		case 1:
			if (scenarioBattleTurnManager.isMedicineNoTarget)
			{
				component.Term = "battleTextEffectNoSoloTarget";
			}
			else
			{
				component.Term = "battleTextEffectMedicPoison";
			}
			break;
		case 2:
			if (scenarioBattleTurnManager.isMedicineNoTarget)
			{
				component.Term = "battleTextEffectNoSoloTarget";
			}
			else
			{
				component.Term = "battleTextEffectMedicAll";
			}
			break;
		}
	}

	private void CalcReviveSkill()
	{
		utageBattleSceneManager.battleTextArray3[1].GetComponent<Localize>().Term = "battleTextEffectRevive";
		if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
			{
				int memberNum = scenarioBattleTurnManager.skillAttackHitDataList[i].memberNum;
				Transform transform = utageBattleSceneManager.playerFrameGoList[memberNum].transform;
				utageBattleSceneManager.damagePointRect[i].position = new Vector3(transform.position.x, -2f, 0f);
				poolGoList.Add(PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[3], utageBattleSceneManager.damagePointRect[i]));
			}
		}
		else
		{
			for (int j = 0; j < scenarioBattleTurnManager.skillAttackHitDataList.Count; j++)
			{
				Transform transform = utageBattleSceneManager.enemyImageGoList[scenarioBattleTurnManager.skillAttackHitDataList[j].memberNum].transform;
				utageBattleSceneManager.damagePointRect[j].position = new Vector3(transform.position.x, 0f, 0f);
				poolGoList.Add(PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[3], utageBattleSceneManager.damagePointRect[j]));
			}
		}
		for (int k = 0; k < poolGoList.Count; k++)
		{
			poolGoList[k].GetComponent<TextMeshProUGUI>().text = scenarioBattleTurnManager.skillAttackHitDataList[k].healValue.ToString();
			PoolManager.Pools["BattleEffect"].Despawn(poolGoList[k], despawnTime, utageBattleSceneManager.poolManagerGO);
		}
	}
}
