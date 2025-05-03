using System.Collections.Generic;
using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class DespawnCraftAnimationEffect : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCheckManager craftCheckManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCheckManager = GameObject.Find("Craft Check Manager").GetComponent<CraftCheckManager>();
	}

	public override void OnStateBegin()
	{
		List<Transform> animationEffectSpawnGoList = craftCheckManager.animationEffectSpawnGoList;
		if (animationEffectSpawnGoList != null && animationEffectSpawnGoList.Count > 0)
		{
			for (int num = craftCheckManager.animationEffectSpawnGoList.Count - 1; num >= 0; num--)
			{
				Transform instance = craftCheckManager.animationEffectSpawnGoList[num];
				if (PoolManager.Pools["Craft Item Pool"].IsSpawned(instance))
				{
					PoolManager.Pools["Craft Item Pool"].Despawn(instance, 0f, craftManager.poolParentGO.transform);
					craftCheckManager.animationEffectSpawnGoList.RemoveAt(num);
					Debug.Log("クラフトエフェクトをデスポーン：" + num);
				}
			}
		}
		craftCheckManager.animationUIParticle.RefreshParticles();
		Transition(stateLink);
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
	}

	public override void OnStateLateUpdate()
	{
	}
}
