using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public class SimpleSpawner : MonoBehaviour
{
	public string poolName;

	public Transform testPrefab;

	public int spawnAmount = 50;

	public float spawnInterval = 0.25f;

	public string particlesPoolName;

	public ParticleSystem particleSystemPrefab;

	private void Start()
	{
		StartCoroutine(Spawner());
		if (particlesPoolName != "")
		{
			StartCoroutine(ParticleSpawner());
		}
	}

	private IEnumerator ParticleSpawner()
	{
		SpawnPool particlesPool = PoolManager.Pools[particlesPoolName];
		ParticleSystem prefab = particleSystemPrefab;
		Vector3 prefabXform = particleSystemPrefab.transform.position;
		Quaternion prefabRot = particleSystemPrefab.transform.rotation;
		while (true)
		{
			ParticleSystem emitter = particlesPool.Spawn(prefab, prefabXform, prefabRot);
			while (emitter.IsAlive(withChildren: true))
			{
				yield return new WaitForSeconds(1f);
			}
			ParticleSystem particleSystem = particlesPool.Spawn(prefab, prefabXform, prefabRot);
			particlesPool.Despawn(particleSystem.transform, 2f);
			yield return new WaitForSeconds(2f);
		}
	}

	private IEnumerator Spawner()
	{
		int count = spawnAmount;
		SpawnPool shapesPool = PoolManager.Pools[poolName];
		while (count > 0)
		{
			shapesPool.Spawn(testPrefab).localPosition = new Vector3(spawnAmount + 2 - count, 0f, 0f);
			count--;
			yield return new WaitForSeconds(spawnInterval);
		}
		StartCoroutine(Despawner());
		yield return null;
	}

	private IEnumerator Despawner()
	{
		SpawnPool shapesPool = PoolManager.Pools[poolName];
		List<Transform> list = new List<Transform>(shapesPool);
		Debug.Log(shapesPool.ToString());
		foreach (Transform item in list)
		{
			shapesPool.Despawn(item);
			yield return new WaitForSeconds(spawnInterval);
		}
		StartCoroutine(Spawner());
		yield return null;
	}
}
