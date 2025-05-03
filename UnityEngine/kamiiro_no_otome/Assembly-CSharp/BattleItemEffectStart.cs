using System.Collections.Generic;
using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class BattleItemEffectStart : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ItemData itemData;

	private List<Transform> poolGoList = new List<Transform>();

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
		itemData = scenarioBattleTurnManager.useItemData;
		PositiveEffectSpawn();
		for (int i = 0; i < poolGoList.Count; i++)
		{
			poolGoList[i].transform.localScale = new Vector3(2f, 2f, 1f);
			poolGoList[i].transform.localPosition = new Vector2(0f, -20f);
			PoolManager.Pools["SkillEffect"].Despawn(poolGoList[i], itemData.animationTime, utageBattleSceneManager.poolSkillManagerGO);
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

	private void PositiveEffectSpawn()
	{
		GameObject effectPrefabGo = GameDataManager.instance.skillEffectDataBase.skillEffectDataList.Find((SkillEffectData data) => data.effectName == itemData.effectType.ToString()).effectPrefabGo;
		Vector2 spawnTransformV = itemData.spawnTransformV2;
		if (itemData.target == ItemData.Target.all)
		{
			if (itemData.category == ItemData.Category.revive)
			{
				for (int i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
				{
					Transform transform = utageBattleSceneManager.playerFrameGoList[i].transform;
					utageBattleSceneManager.damagePointRect[i].position = new Vector3(transform.position.x + spawnTransformV.x, spawnTransformV.y, 0f);
					poolGoList.Add(PoolManager.Pools["SkillEffect"].Spawn(effectPrefabGo, utageBattleSceneManager.damagePointRect[i]));
				}
				return;
			}
			int j;
			for (j = 0; j < PlayerStatusDataManager.playerPartyMember.Length; j++)
			{
				if (!PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == j).isDead)
				{
					Transform transform2 = utageBattleSceneManager.playerFrameGoList[j].transform;
					utageBattleSceneManager.damagePointRect[j].position = new Vector3(transform2.position.x + spawnTransformV.x, spawnTransformV.y, 0f);
					poolGoList.Add(PoolManager.Pools["SkillEffect"].Spawn(effectPrefabGo, utageBattleSceneManager.damagePointRect[j]));
				}
			}
		}
		else
		{
			int playerTargetNum = scenarioBattleTurnManager.playerTargetNum;
			Transform transform3 = utageBattleSceneManager.playerFrameGoList[playerTargetNum].transform;
			utageBattleSceneManager.damagePointRect[0].position = new Vector3(transform3.position.x + spawnTransformV.x, spawnTransformV.y, 0f);
			poolGoList.Add(PoolManager.Pools["SkillEffect"].Spawn(effectPrefabGo, utageBattleSceneManager.damagePointRect[0]));
		}
	}
}
