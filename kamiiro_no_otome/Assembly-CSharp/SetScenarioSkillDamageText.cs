using System.Collections.Generic;
using Arbor;
using DarkTonic.MasterAudio;
using I2.Loc;
using PathologicalGames;
using TMPro;
using UnityEngine;

[AddComponentMenu("")]
public class SetScenarioSkillDamageText : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	private BattleSkillData battleSkillData;

	private List<Transform> poolGoList = new List<Transform>();

	public float despawnTime;

	public StateLink stateLink;

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
		battleSkillData = scenarioBattleTurnManager.useSkillData;
		poolGoList.Clear();
		if (scenarioBattleTurnManager.isAllTargetSkill)
		{
			int num = 0;
			if (scenarioBattleTurnManager.isUseSkillPlayer)
			{
				if (PlayerStatusDataManager.enemyMember.Length > 1)
				{
					scenarioBattleSkillManager.isSkillAverageDamage = true;
					utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "battleTextEnemyAllTarget";
				}
				else
				{
					scenarioBattleSkillManager.isSkillAverageDamage = false;
					utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "enemy" + PlayerStatusDataManager.enemyMember[0];
				}
			}
			else
			{
				string allTargetTerm = scenarioBattleSkillManager.GetAllTargetTerm();
				utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = allTargetTerm;
			}
			for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
			{
				num += scenarioBattleTurnManager.skillAttackHitDataList[i].attackDamage;
			}
			if (scenarioBattleSkillManager.isSkillAverageDamage)
			{
				num /= scenarioBattleTurnManager.skillAttackHitDataList.Count;
				string translation = LocalizationManager.GetTranslation("battleTextAverage");
				utageBattleSceneManager.battleTextArray3[2].GetComponent<TextMeshProUGUI>().text = translation + num;
			}
			else
			{
				utageBattleSceneManager.battleTextArray3[2].GetComponent<TextMeshProUGUI>().text = scenarioBattleTurnManager.skillAttackHitDataList[0].attackDamage.ToString();
			}
		}
		else
		{
			if (scenarioBattleTurnManager.isUseSkillPlayer)
			{
				int num2 = PlayerStatusDataManager.enemyMember[scenarioBattleTurnManager.playerTargetNum];
				utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "enemy" + num2;
			}
			else
			{
				int num2 = PlayerStatusDataManager.playerPartyMember[scenarioBattleTurnManager.enemyTargetNum];
				utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "character" + num2;
			}
			utageBattleSceneManager.battleTextArray3[2].GetComponent<TextMeshProUGUI>().text = scenarioBattleTurnManager.skillAttackHitDataList[0].attackDamage.ToString();
		}
		utageBattleSceneManager.battleTextArray3[1].GetComponent<Localize>().Term = "textConjunctionTo";
		utageBattleSceneManager.battleTextArray3[3].GetComponent<Localize>().Term = "battleTextAttackDamage";
		utageBattleSceneManager.battleTextGroupArray[2].SetActive(value: true);
		utageBattleSceneManager.battleTextArray3[0].SetActive(value: true);
		utageBattleSceneManager.battleTextArray3[1].SetActive(value: true);
		utageBattleSceneManager.battleTextArray3[2].SetActive(value: true);
		utageBattleSceneManager.battleTextArray3[3].SetActive(value: true);
		if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			for (int j = 0; j < scenarioBattleTurnManager.skillAttackHitDataList.Count; j++)
			{
				Transform transform = utageBattleSceneManager.enemyImageGoList[scenarioBattleTurnManager.skillAttackHitDataList[j].memberNum].transform;
				utageBattleSceneManager.damagePointRect[j].position = new Vector3(transform.position.x, 0f, 0f);
				if (scenarioBattleTurnManager.skillAttackHitDataList[j].isWeakHit)
				{
					poolGoList.Add(PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[8], utageBattleSceneManager.damagePointRect[j]));
				}
				else if (scenarioBattleTurnManager.skillAttackHitDataList[j].isResistHit)
				{
					poolGoList.Add(PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[9], utageBattleSceneManager.damagePointRect[j]));
				}
				else
				{
					poolGoList.Add(PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[0], utageBattleSceneManager.damagePointRect[j]));
				}
			}
		}
		else
		{
			for (int k = 0; k < scenarioBattleTurnManager.skillAttackHitDataList.Count; k++)
			{
				Transform transform = utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.skillAttackHitDataList[k].memberNum].transform;
				utageBattleSceneManager.damagePointRect[k].position = new Vector3(transform.position.x, -2f, 0f);
				if (scenarioBattleTurnManager.skillAttackHitDataList[k].isWeakHit)
				{
					poolGoList.Add(PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[8], utageBattleSceneManager.damagePointRect[k]));
				}
				else if (scenarioBattleTurnManager.skillAttackHitDataList[k].isResistHit)
				{
					poolGoList.Add(PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[9], utageBattleSceneManager.damagePointRect[k]));
				}
				else
				{
					poolGoList.Add(PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[1], utageBattleSceneManager.damagePointRect[k]));
				}
			}
		}
		for (int l = 0; l < poolGoList.Count; l++)
		{
			poolGoList[l].GetComponent<TextMeshProUGUI>().text = scenarioBattleTurnManager.skillAttackHitDataList[l].attackDamage.ToString();
			poolGoList[l].GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
			PoolManager.Pools["BattleEffect"].Despawn(poolGoList[l], despawnTime, utageBattleSceneManager.poolManagerGO);
		}
		string effectType = battleSkillData.effectType;
		string text = effectType.Substring(0, 1).ToUpper();
		effectType = "SeSkill" + text + effectType.Substring(1);
		MasterAudio.PlaySound(effectType, 1f, null, 0f, null, null);
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
