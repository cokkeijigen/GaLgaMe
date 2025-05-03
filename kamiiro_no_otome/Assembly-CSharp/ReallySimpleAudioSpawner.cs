using System.Collections;
using PathologicalGames;
using UnityEngine;

public class ReallySimpleAudioSpawner : MonoBehaviour
{
	public AudioSource prefab;

	public AudioSource musicPrefab;

	public float spawnInterval = 2f;

	private SpawnPool pool;

	private void Start()
	{
		pool = GetComponent<SpawnPool>();
		StartCoroutine(Spawner());
		if (musicPrefab != null)
		{
			StartCoroutine(MusicSpawner());
		}
	}

	private IEnumerator MusicSpawner()
	{
		AudioSource music = pool.Spawn(musicPrefab);
		yield return new WaitForSeconds(2f);
		pool.Despawn(music.transform);
		yield return new WaitForSeconds(1f);
		music = pool.Spawn(musicPrefab);
		yield return new WaitForSeconds(2f);
		music.Stop();
		yield return new WaitForSeconds(1f);
		music = pool.Spawn(musicPrefab);
		yield return new WaitForSeconds(2f);
		music.Stop();
	}

	private IEnumerator Spawner()
	{
		while (true)
		{
			pool.Spawn(prefab, base.transform.position, base.transform.rotation).pitch = Random.Range(0.7f, 1.4f);
			yield return new WaitForSeconds(spawnInterval);
		}
	}
}
