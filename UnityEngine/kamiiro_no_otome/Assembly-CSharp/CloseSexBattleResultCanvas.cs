using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class CloseSexBattleResultCanvas : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		sexBattleTurnManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleTurnManager>();
	}

	public override void OnStateBegin()
	{
		foreach (Transform expPrefabSpawnGo in sexBattleManager.expPrefabSpawnGoList)
		{
			PoolManager.Pools["sexBattlePool"].Despawn(expPrefabSpawnGo, 0f, sexBattleManager.skillPrefabParent);
		}
		if (PoolManager.Pools["sexTouchPool"].IsSpawned(sexBattleManager.resultEffectSpawnGo))
		{
			PoolManager.Pools["sexTouchPool"].Despawn(sexBattleManager.resultEffectSpawnGo, 0f, sexBattleManager.skillPrefabParent);
		}
		sexBattleManager.uIParticleSex.RefreshParticles();
		PlayerSexStatusDataManager.AddTotalSexCount("piston", 0, sexBattleTurnManager.sexBattlePistonCount);
		PlayerSexStatusDataManager.AddTotalSexCount("outShot", 0, sexBattleTurnManager.sexBattlePlayerOutShotCount);
		PlayerSexStatusDataManager.AddTotalSexCount("inShot", 0, sexBattleTurnManager.sexBattlePlayerInShotCount);
		PlayerSexStatusDataManager.AddTotalSexCount("condomShot", 0, sexBattleTurnManager.sexBattlePlayerCondomShotCount);
		PlayerSexStatusDataManager.AddTotalSexCount("ecstasy", 0, sexBattleTurnManager.sexBattlePlayerEcstasyCount);
		PlayerSexStatusDataManager.AddTotalSexCount("sexCount", 0, 1);
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		PlayerSexStatusDataManager.AddTotalSexCount("piston", selectSexBattleHeroineId, sexBattleTurnManager.sexBattlePistonCount);
		PlayerSexStatusDataManager.AddTotalSexCount("outShot", selectSexBattleHeroineId, sexBattleTurnManager.sexBattlePlayerOutShotCount);
		PlayerSexStatusDataManager.AddTotalSexCount("inShot", selectSexBattleHeroineId, sexBattleTurnManager.sexBattlePlayerInShotCount);
		PlayerSexStatusDataManager.AddTotalSexCount("condomShot", selectSexBattleHeroineId, sexBattleTurnManager.sexBattlePlayerCondomShotCount);
		PlayerSexStatusDataManager.AddTotalSexCount("ecstasy", selectSexBattleHeroineId, sexBattleTurnManager.sexBattleHeroineEcstasyCount);
		PlayerSexStatusDataManager.AddTotalSexCount("sexCount", selectSexBattleHeroineId, 1);
		sexBattleManager.resultCanvasGo.SetActive(value: false);
		Debug.Log("えっちバトル終了");
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
