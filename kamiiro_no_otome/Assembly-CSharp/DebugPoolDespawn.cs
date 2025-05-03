using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class DebugPoolDespawn : StateBehaviour
{
	public GameObject debugPrefabGo;

	public Transform spawnTransfrom;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		spawnTransfrom = PoolManager.Pools["testPool"].Spawn(debugPrefabGo);
		spawnTransfrom.localPosition = new Vector3(0f, 0f, 0f);
		Transition(stateLink);
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (Input.GetButton("Fire2"))
		{
			if (PoolManager.Pools["testPool"].IsSpawned(spawnTransfrom))
			{
				Debug.Log("デスポーン");
				PoolManager.Pools["testPool"].Despawn(spawnTransfrom);
			}
			else
			{
				Debug.Log("スポーンしてない");
			}
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
