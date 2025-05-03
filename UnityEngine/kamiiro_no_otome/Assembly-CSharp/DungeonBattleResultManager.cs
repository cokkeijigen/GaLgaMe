using Arbor;
using Coffee.UIExtensions;
using PathologicalGames;
using UnityEngine;
using UnityEngine.Playables;

public class DungeonBattleResultManager : MonoBehaviour
{
	public DungeonBattleManager dungeonBattleManager;

	public ArborFSM resultFSM;

	public GameObject resultEffectGroupGo;

	public UIParticle uIParticle;

	public GameObject[] resultEffectPrefabGoArray;

	public Transform resultEffectSpawnGo;

	public PlayableDirector playableDirector;

	public PlayableAsset[] resultEffectAssetArray;

	public void StartBattleResultEffect(bool isVictory)
	{
		if (isVictory)
		{
			playableDirector.playableAsset = resultEffectAssetArray[0];
		}
		else
		{
			playableDirector.playableAsset = resultEffectAssetArray[1];
		}
		resultEffectGroupGo.SetActive(value: true);
		resultEffectGroupGo.GetComponent<AudioSource>().volume = PlayerOptionsDataManager.optionsSeVolume;
		playableDirector.time = 0.0;
		playableDirector.Play();
	}

	public void SpawnBattleResultEffect(bool isVictory)
	{
		if (isVictory)
		{
			resultEffectSpawnGo = PoolManager.Pools["DungeonUseItem"].Spawn(resultEffectPrefabGoArray[0], uIParticle.transform);
		}
		else
		{
			resultEffectSpawnGo = PoolManager.Pools["DungeonUseItem"].Spawn(resultEffectPrefabGoArray[1], uIParticle.transform);
		}
		resultEffectSpawnGo.localScale = new Vector3(1f, 1f, 1f);
		resultEffectSpawnGo.localPosition = new Vector3(0f, 0f, 0f);
		Debug.Log("リザルトエフェクトをスポーン");
		uIParticle.RefreshParticles();
	}

	public void DeSpawnBattleResultEffect(bool isVictory)
	{
		PoolManager.Pools["DungeonUseItem"].Despawn(resultEffectSpawnGo, dungeonBattleManager.poolManagerGO);
		resultEffectGroupGo.SetActive(value: false);
		if (isVictory)
		{
			resultFSM.SendTrigger("VictoryEffectEnd");
		}
		else
		{
			resultFSM.SendTrigger("DefeatEffectEnd");
		}
	}
}
