using Arbor;
using Coffee.UIExtensions;
using DarkTonic.MasterAudio;
using PathologicalGames;
using UnityEngine;

public class DungeonRoundManager : MonoBehaviour
{
	public DungeonBattleManager dungeonBattleManager;

	public ArborFSM dungeonBattleFSM;

	public Transform roundEffectPrefabGo;

	private Transform roundEffectSpawnGo;

	public UIParticle uIParticle;

	public void SpawnDungeonRoundEffect()
	{
		roundEffectSpawnGo = PoolManager.Pools["DungeonObject"].Spawn(roundEffectPrefabGo, uIParticle.transform);
		MasterAudio.PlaySound("SeDungeonRound", 1f, null, 0f, null, null);
		roundEffectSpawnGo.localScale = new Vector3(1f, 1f, 1f);
		roundEffectSpawnGo.localPosition = new Vector3(0f, 0f, 0f);
		Debug.Log("ラウンド開始エフェクトをスポーン");
		uIParticle.RefreshParticles();
	}

	public void DeSpawnDungeonRoundEffect()
	{
		PoolManager.Pools["DungeonObject"].Despawn(roundEffectSpawnGo, 1f);
		dungeonBattleFSM.SendTrigger("DungeonRoundEnd");
	}
}
