using System.Collections.Generic;
using System.Linq;
using Arbor;
using DarkTonic.MasterAudio;
using PathologicalGames;
using TMPro;
using UnityEngine;

[AddComponentMenu("")]
public class SpawnDamageNumText : StateBehaviour
{
	public enum Type
	{
		damage,
		poison
	}

	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public Type type;

	public float despawnTime;

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
		GameObject prefab = null;
		List<Transform> list = new List<Transform>();
		List<Transform> list2 = new List<Transform>();
		if (scenarioBattleTurnManager.skillAttackHitDataList.Any())
		{
			for (int i = 0; i < scenarioBattleTurnManager.skillAttackHitDataList.Count; i++)
			{
				Transform transform = utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.skillAttackHitDataList[i].memberNum].transform;
				utageBattleSceneManager.damagePointRect[i].position = new Vector3(transform.position.x, -2f, 0f);
				switch (type)
				{
				case Type.damage:
					prefab = utageBattleSceneManager.poolEffectArray[0];
					break;
				case Type.poison:
					prefab = utageBattleSceneManager.poolEffectArray[7];
					break;
				}
				list.Add(PoolManager.Pools["BattleEffect"].Spawn(prefab, utageBattleSceneManager.damagePointRect[i]));
			}
			for (int j = 0; j < list.Count; j++)
			{
				list[j].GetComponent<TextMeshProUGUI>().text = scenarioBattleTurnManager.skillAttackHitDataList[j].attackDamage.ToString();
				list[j].GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
				PoolManager.Pools["BattleEffect"].Despawn(list[j], despawnTime, utageBattleSceneManager.poolManagerGO);
			}
		}
		if (scenarioBattleTurnManager.skillAttackHitDataSubList.Any())
		{
			for (int k = 0; k < scenarioBattleTurnManager.skillAttackHitDataSubList.Count; k++)
			{
				Transform transform = utageBattleSceneManager.enemyImageGoList[scenarioBattleTurnManager.skillAttackHitDataSubList[k].memberNum].transform;
				utageBattleSceneManager.damagePointRect[k].position = new Vector3(transform.position.x, 0f, 0f);
				switch (type)
				{
				case Type.damage:
					prefab = utageBattleSceneManager.poolEffectArray[1];
					break;
				case Type.poison:
					prefab = utageBattleSceneManager.poolEffectArray[7];
					break;
				}
				list2.Add(PoolManager.Pools["BattleEffect"].Spawn(prefab, utageBattleSceneManager.damagePointRect[k]));
			}
			for (int l = 0; l < list2.Count; l++)
			{
				list2[l].GetComponent<TextMeshProUGUI>().text = scenarioBattleTurnManager.skillAttackHitDataSubList[l].attackDamage.ToString();
				list2[l].GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
				PoolManager.Pools["BattleEffect"].Despawn(list2[l], despawnTime, utageBattleSceneManager.poolManagerGO);
			}
		}
		MasterAudio.PlaySound("SeSlipPoisonDamage", 1f, null, 0f, null, null);
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
