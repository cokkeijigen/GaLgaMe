using System.Collections;
using PathologicalGames;
using UnityEngine;

public class ReallySimpleParticleSpawner : MonoBehaviour
{
	public string poolName;

	public ParticleSystem prefab;

	public float spawnInterval = 1f;

	private void Start()
	{
		StartCoroutine(ParticleSpawner());
	}

	private IEnumerator ParticleSpawner()
	{
		while (true)
		{
			PoolManager.Pools[poolName].Spawn(prefab, base.transform.position, base.transform.rotation);
			yield return new WaitForSeconds(spawnInterval);
		}
	}
}
