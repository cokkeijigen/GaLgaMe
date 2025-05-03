using PathologicalGames;
using UnityEngine;

public class OnCreatedDelegateExample : MonoBehaviour
{
	public string poolName = "Shapes";

	private void Awake()
	{
		PoolManager.Pools.AddOnCreatedDelegate(poolName, RunMe);
	}

	public void RunMe(SpawnPool pool)
	{
		Debug.Log("OnCreatedDelegateExample RunMe ran for pool " + pool.poolName);
	}
}
