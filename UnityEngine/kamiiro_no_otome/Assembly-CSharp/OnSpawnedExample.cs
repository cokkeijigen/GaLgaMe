using PathologicalGames;
using UnityEngine;

public class OnSpawnedExample : MonoBehaviour
{
	private void OnSpawned(SpawnPool pool)
	{
		Debug.Log($"OnSpawnedExample | OnSpawned running for '{base.name}' in pool '{pool.poolName}'.");
	}

	private void OnDespawned(SpawnPool pool)
	{
		Debug.Log($"OnSpawnedExample | OnDespawned unning for '{base.name}' in pool '{pool.poolName}'.");
	}
}
