using System.Collections;
using PathologicalGames;
using UnityEngine;

public class ParticleWaitTest : MonoBehaviour
{
	public float spawnInterval = 0.25f;

	public string particlesPoolName;

	public ParticleSystem particleSystemPrefab;

	private IEnumerator Start()
	{
		SpawnPool particlesPool = PoolManager.Pools[particlesPoolName];
		ParticleSystem prefab = particleSystemPrefab;
		Vector3 prefabXform = particleSystemPrefab.transform.position;
		Quaternion prefabRot = particleSystemPrefab.transform.rotation;
		while (true)
		{
			yield return new WaitForSeconds(spawnInterval);
			ParticleSystem emitter = particlesPool.Spawn(prefab, prefabXform, prefabRot);
			while (emitter.IsAlive(withChildren: true))
			{
				yield return new WaitForSeconds(3f);
			}
			ParticleSystem particleSystem = particlesPool.Spawn(prefab, prefabXform, prefabRot);
			particlesPool.Despawn(particleSystem.transform, 2f);
			yield return new WaitForSeconds(2f);
			particlesPool.Spawn(prefab, prefabXform, prefabRot);
		}
	}
}
