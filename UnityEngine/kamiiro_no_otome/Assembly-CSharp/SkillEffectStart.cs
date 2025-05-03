using System.Collections.Generic;
using Arbor;
using DG.Tweening;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class SkillEffectStart : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private BattleSkillData battleSkillData;

	private List<Transform> poolGoList = new List<Transform>();

	private GameObject enemyButtonGo;

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
		battleSkillData = scenarioBattleTurnManager.useSkillData;
		SkillEffectData skillEffectData = GameDataManager.instance.skillEffectDataBase.skillEffectDataList.Find((SkillEffectData data) => data.effectName == battleSkillData.effectType.ToString());
		_ = scenarioBattleTurnManager.isUseSkillPlayer;
		BattleSkillData.SkillType skillType = battleSkillData.skillType;
		if ((uint)skillType <= 3u || skillType == BattleSkillData.SkillType.deBuff)
		{
			AttackEffectSpawn();
		}
		else
		{
			PositiveEffectSpawn();
		}
		for (int i = 0; i < poolGoList.Count; i++)
		{
			poolGoList[i].transform.localScale = new Vector3(skillEffectData.localScale, skillEffectData.localScale, skillEffectData.localScale);
			poolGoList[i].transform.localPosition = skillEffectData.effectLocalPosition;
			PoolManager.Pools["SkillEffect"].Despawn(poolGoList[i], battleSkillData.animationTime, utageBattleSceneManager.poolSkillManagerGO);
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

	private void AttackEffectSpawn()
	{
		SkillEffectData skillEffectData = GameDataManager.instance.skillEffectDataBase.skillEffectDataList.Find((SkillEffectData data) => data.effectName == battleSkillData.effectType.ToString());
		GameObject effectPrefabGo = skillEffectData.effectPrefabGo;
		Vector2 effectLocalPosition = skillEffectData.effectLocalPosition;
		if (battleSkillData.skillTarget == BattleSkillData.SkillTarget.all)
		{
			if (scenarioBattleTurnManager.isUseSkillPlayer)
			{
				int i;
				for (i = 0; i < PlayerStatusDataManager.enemyMember.Length; i++)
				{
					if (!PlayerBattleConditionManager.enemyIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == i).isDead)
					{
						Transform transform = utageBattleSceneManager.enemyImageGoList[i].transform;
						utageBattleSceneManager.damagePointRect[i].position = new Vector3(transform.position.x + effectLocalPosition.x, transform.position.y, 0f);
						poolGoList.Add(PoolManager.Pools["SkillEffect"].Spawn(effectPrefabGo, utageBattleSceneManager.damagePointRect[i]));
					}
				}
				return;
			}
			int j;
			for (j = 0; j < PlayerStatusDataManager.playerPartyMember.Length; j++)
			{
				if (!PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == j).isDead)
				{
					Transform transform2 = utageBattleSceneManager.playerFrameGoList[j].transform;
					utageBattleSceneManager.damagePointRect[j].position = new Vector3(transform2.position.x + effectLocalPosition.x, -2f, 0f);
					poolGoList.Add(PoolManager.Pools["SkillEffect"].Spawn(effectPrefabGo, utageBattleSceneManager.damagePointRect[j]));
				}
			}
			SetEnemySkillStartEffect();
		}
		else if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			Transform transform3 = utageBattleSceneManager.enemyImageGoList[scenarioBattleTurnManager.playerTargetNum].transform;
			utageBattleSceneManager.damagePointRect[0].position = new Vector3(transform3.position.x + effectLocalPosition.x, transform3.position.y, 0f);
			poolGoList.Add(PoolManager.Pools["SkillEffect"].Spawn(effectPrefabGo, utageBattleSceneManager.damagePointRect[0]));
		}
		else
		{
			Transform transform4 = utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.enemyTargetNum].transform;
			utageBattleSceneManager.damagePointRect[0].position = new Vector3(transform4.position.x + effectLocalPosition.x, -2f, 0f);
			poolGoList.Add(PoolManager.Pools["SkillEffect"].Spawn(effectPrefabGo, utageBattleSceneManager.damagePointRect[0]));
			SetEnemySkillStartEffect();
		}
	}

	private void PositiveEffectSpawn()
	{
		SkillEffectData skillEffectData = GameDataManager.instance.skillEffectDataBase.skillEffectDataList.Find((SkillEffectData data) => data.effectName == battleSkillData.effectType.ToString());
		GameObject effectPrefabGo = skillEffectData.effectPrefabGo;
		Vector2 effectLocalPosition = skillEffectData.effectLocalPosition;
		if (battleSkillData.skillTarget == BattleSkillData.SkillTarget.all)
		{
			if (scenarioBattleTurnManager.isUseSkillPlayer)
			{
				int i;
				for (i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
				{
					if (!PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == i).isDead)
					{
						Transform transform = utageBattleSceneManager.playerFrameGoList[i].transform;
						utageBattleSceneManager.damagePointRect[i].position = new Vector3(transform.position.x + effectLocalPosition.x, transform.position.y, 0f);
						poolGoList.Add(PoolManager.Pools["SkillEffect"].Spawn(effectPrefabGo, utageBattleSceneManager.damagePointRect[i]));
					}
				}
				return;
			}
			int j;
			for (j = 0; j < PlayerStatusDataManager.enemyMember.Length; j++)
			{
				if (!PlayerBattleConditionManager.enemyIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == j).isDead)
				{
					Transform transform2 = utageBattleSceneManager.enemyImageGoList[j].transform;
					utageBattleSceneManager.damagePointRect[j].position = new Vector3(transform2.position.x + effectLocalPosition.x, -2f, 0f);
					poolGoList.Add(PoolManager.Pools["SkillEffect"].Spawn(effectPrefabGo, utageBattleSceneManager.damagePointRect[j]));
				}
			}
		}
		else if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			int playerTargetNum = scenarioBattleTurnManager.playerTargetNum;
			Transform transform3 = utageBattleSceneManager.playerFrameGoList[playerTargetNum].transform;
			utageBattleSceneManager.damagePointRect[0].position = new Vector3(transform3.position.x + effectLocalPosition.x, transform3.position.y, 0f);
			poolGoList.Add(PoolManager.Pools["SkillEffect"].Spawn(effectPrefabGo, utageBattleSceneManager.damagePointRect[0]));
		}
		else
		{
			Transform transform4 = utageBattleSceneManager.enemyImageGoList[scenarioBattleTurnManager.enemyTargetNum].transform;
			utageBattleSceneManager.damagePointRect[0].position = new Vector3(transform4.position.x + effectLocalPosition.x, -2f, 0f);
			poolGoList.Add(PoolManager.Pools["SkillEffect"].Spawn(effectPrefabGo, utageBattleSceneManager.damagePointRect[0]));
		}
	}

	private void SetEnemySkillStartEffect()
	{
		int enemyAttackCount = scenarioBattleTurnManager.enemyAttackCount;
		int memberNum = PlayerBattleConditionManager.enemyIsDead[enemyAttackCount].memberNum;
		enemyButtonGo = utageBattleSceneManager.enemyButtonGoList[memberNum].GetComponent<ParameterContainer>().GetGameObject("enemyButtonGo");
		DOTween.Sequence().Append(enemyButtonGo.transform.DOScale(1.1f, 0.1f)).Append(enemyButtonGo.transform.DOScale(1f, 0.1f));
		GameObject effectPrefabGo = GameDataManager.instance.skillEffectDataBase.skillEffectDataList.Find((SkillEffectData data) => data.effectName == "simpleCircleGlowRed").effectPrefabGo;
		utageBattleSceneManager.damagePointRect[4].position = enemyButtonGo.GetComponent<RectTransform>().position;
		Transform transform = PoolManager.Pools["SkillEffect"].Spawn(effectPrefabGo, utageBattleSceneManager.damagePointRect[4]);
		transform.localScale = new Vector3(1f, 1f, 1f);
		transform.localPosition = new Vector2(0f, 0f);
		PoolManager.Pools["SkillEffect"].Despawn(transform, 1f, utageBattleSceneManager.effectSpawnParent.transform);
	}
}
