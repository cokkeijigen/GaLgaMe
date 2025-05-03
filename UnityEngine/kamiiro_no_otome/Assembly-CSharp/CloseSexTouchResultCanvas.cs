using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class CloseSexTouchResultCanvas : StateBehaviour
{
	private SexTouchManager sexTouchManager;

	private SexBattleManager sexBattleManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchManager = GameObject.Find("Sex Touch Manager").GetComponent<SexTouchManager>();
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
	}

	public override void OnStateBegin()
	{
		foreach (Transform touchExpPrefabSpawnGo in sexTouchManager.touchExpPrefabSpawnGoList)
		{
			PoolManager.Pools["sexBattlePool"].Despawn(touchExpPrefabSpawnGo, 0f, sexBattleManager.skillPrefabParent);
		}
		if (PoolManager.Pools["sexTouchPool"].IsSpawned(sexTouchManager.resultEffectSpawnGo))
		{
			PoolManager.Pools["sexTouchPool"].Despawn(sexTouchManager.resultEffectSpawnGo, 0f, sexTouchManager.prefabParentTransform);
		}
		sexTouchManager.uIParticleTouch.RefreshParticles();
		PlayerSexStatusDataManager.AddTotalSexCount("mouth", 0, 1);
		PlayerSexStatusDataManager.AddTotalSexCount("ecstasy", 0, 1);
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		PlayerSexStatusDataManager.AddTotalSexCount("mouth", selectSexBattleHeroineId, 1);
		sexTouchManager.touchResultWindow.SetActive(value: false);
		Debug.Log("身体観察終了");
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
