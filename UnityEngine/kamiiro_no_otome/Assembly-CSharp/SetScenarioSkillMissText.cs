using System.Collections.Generic;
using System.Linq;
using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using I2.Loc;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class SetScenarioSkillMissText : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private Transform poolGO;

	public float waitTime;

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
		if (scenarioBattleTurnManager.isAllTargetSkill)
		{
			if (scenarioBattleTurnManager.isUseSkillPlayer)
			{
				HashSet<int> hashSet = new HashSet<int>(PlayerStatusDataManager.enemyMember);
				HashSet<int> other = new HashSet<int>(scenarioBattleTurnManager.skillAttackHitDataList.Select((SkillAttackHitData data) => data.memberNum));
				hashSet.SymmetricExceptWith(other);
				int[] array = new int[hashSet.Count];
				hashSet.CopyTo(array);
				for (int i = 0; i < array.Length; i++)
				{
					Transform transform = utageBattleSceneManager.enemyImageGoList[array[i]].transform;
					utageBattleSceneManager.damagePointRect[i].position = new Vector3(transform.position.x, 0f, 0f);
					poolGO = PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[2], utageBattleSceneManager.damagePointRect[i]);
				}
			}
			else
			{
				HashSet<int> hashSet2 = new HashSet<int>(PlayerStatusDataManager.playerPartyMember);
				HashSet<int> other2 = new HashSet<int>(scenarioBattleTurnManager.skillAttackHitDataList.Select((SkillAttackHitData data) => data.memberNum));
				hashSet2.SymmetricExceptWith(other2);
				int[] array2 = new int[hashSet2.Count];
				hashSet2.CopyTo(array2);
				for (int j = 0; j < array2.Length; j++)
				{
					Transform transform2 = utageBattleSceneManager.playerFrameGoList[array2[j]].transform;
					utageBattleSceneManager.damagePointRect[j].position = new Vector3(transform2.position.x, -2f, 0f);
					poolGO = PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[2], utageBattleSceneManager.damagePointRect[j]);
				}
			}
		}
		else if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			Transform transform3 = utageBattleSceneManager.enemyImageGoList[scenarioBattleTurnManager.playerTargetNum].transform;
			utageBattleSceneManager.damagePointRect[0].position = new Vector3(transform3.position.x, 0f, 0f);
			poolGO = PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[2], utageBattleSceneManager.damagePointRect[0]);
		}
		else
		{
			Transform transform4 = utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.enemyTargetNum].transform;
			utageBattleSceneManager.damagePointRect[0].position = new Vector3(transform4.position.x, -2f, 0f);
			poolGO = PoolManager.Pools["BattleEffect"].Spawn(utageBattleSceneManager.poolEffectArray[2], utageBattleSceneManager.damagePointRect[0]);
		}
		poolGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
		PoolManager.Pools["BattleEffect"].Despawn(poolGO, despawnTime, utageBattleSceneManager.poolManagerGO);
		utageBattleSceneManager.battleTextArray3[0].GetComponent<Localize>().Term = "battleTextAttackMiss";
		utageBattleSceneManager.battleTextGroupArray[2].SetActive(value: true);
		utageBattleSceneManager.battleTextArray3[0].SetActive(value: true);
		AttackMissTween();
		float time = waitTime / (float)utageBattleSceneManager.battleSpeed;
		Invoke("InvokeMethod", time);
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
		Transition(stateLink);
	}

	private void AttackMissTween()
	{
		MasterAudio.PlaySound("SeAttackMiss", 1f, null, 0f, null, null);
		int num = ((Random.Range(0, 1) > 0) ? 1 : (-1));
		if (scenarioBattleTurnManager.isAllTargetSkill)
		{
			if (scenarioBattleTurnManager.isUseSkillPlayer)
			{
				HashSet<int> hashSet = new HashSet<int>(PlayerStatusDataManager.enemyMember);
				HashSet<int> other = new HashSet<int>(scenarioBattleTurnManager.skillAttackHitDataList.Select((SkillAttackHitData data) => data.memberNum));
				hashSet.SymmetricExceptWith(other);
				int[] array = new int[hashSet.Count];
				hashSet.CopyTo(array);
				for (int i = 0; i < array.Length; i++)
				{
					utageBattleSceneManager.enemyImageGoList[array[i]].transform.DOPunchPosition(new Vector3(30f * (float)num, 0f, 0f), 0.3f, 1);
				}
			}
			else
			{
				HashSet<int> hashSet2 = new HashSet<int>(PlayerStatusDataManager.playerPartyMember);
				HashSet<int> other2 = new HashSet<int>(scenarioBattleTurnManager.skillAttackHitDataList.Select((SkillAttackHitData data) => data.memberNum));
				hashSet2.SymmetricExceptWith(other2);
				int[] array2 = new int[hashSet2.Count];
				hashSet2.CopyTo(array2);
				for (int j = 0; j < array2.Length; j++)
				{
					utageBattleSceneManager.playerFrameGoList[array2[j]].transform.DOPunchPosition(new Vector3(30f * (float)num, 0f, 0f), 0.3f, 1);
				}
			}
		}
		else if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			utageBattleSceneManager.enemyImageGoList[scenarioBattleTurnManager.playerTargetNum].transform.DOPunchPosition(new Vector3(30f * (float)num, 0f, 0f), 0.3f, 1);
		}
		else
		{
			utageBattleSceneManager.playerFrameGoList[scenarioBattleTurnManager.enemyTargetNum].transform.DOPunchPosition(new Vector3(30f * (float)num, 0f, 0f), 0.3f, 1);
		}
	}
}
