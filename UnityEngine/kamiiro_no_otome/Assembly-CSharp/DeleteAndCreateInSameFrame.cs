using System.Collections;
using PathologicalGames;
using UnityEngine;

public class DeleteAndCreateInSameFrame : MonoBehaviour
{
	public SpawnPool poolPrefab;

	private void Start()
	{
		StartCoroutine(DoIt());
	}

	private IEnumerator DoIt()
	{
		while (true)
		{
			SpawnPool pool = Object.Instantiate(poolPrefab);
			yield return new WaitForSeconds(2f);
			PoolManager.Pools.Destroy(pool.poolName);
			Object.Instantiate(poolPrefab);
			yield return new WaitForSeconds(2f);
			PoolManager.Pools.DestroyAll();
		}
	}
}
