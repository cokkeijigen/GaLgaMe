using PathologicalGames;
using UnityEngine;

public class DungeonMapEffectManager : MonoBehaviour
{
	public DungeonMapManager dungeonMapManager;

	public Transform dungeonLibidoSpawnPointGo;

	public GameObject dungeonLibidoEffectPrefabGo;

	public Transform libidoEffectGo;

	public void SpawnDungeonLibidoEffect(Vector2 vector2)
	{
		if (dungeonLibidoSpawnPointGo.childCount == 0)
		{
			dungeonLibidoSpawnPointGo.localPosition = vector2;
			libidoEffectGo = PoolManager.Pools["DungeonObject"].Spawn(dungeonLibidoEffectPrefabGo, dungeonLibidoSpawnPointGo);
			libidoEffectGo.localPosition = new Vector2(0f, 0f);
			libidoEffectGo.localScale = new Vector2(1f, 1f);
		}
	}

	public void DespawnDungeonLibidoEffect()
	{
		if (dungeonLibidoSpawnPointGo.childCount > 0)
		{
			PoolManager.Pools["DungeonObject"].Despawn(libidoEffectGo, dungeonMapManager.poolManagerGO);
		}
	}
}
