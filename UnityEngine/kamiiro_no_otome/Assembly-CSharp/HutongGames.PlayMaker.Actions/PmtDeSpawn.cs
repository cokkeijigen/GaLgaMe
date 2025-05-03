using PathologicalGames;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Path-o-logical/Pool Manager 2")]
	[Tooltip("If the passed transform is managed by the SpawnPool, it will be deactivated and made available to be spawned again.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W843")]
	public class PmtDeSpawn : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Pool name")]
		public FsmString poolName;

		[RequiredField]
		[Tooltip("GameObject or prefab to deSpawn")]
		public FsmGameObject gameObject;

		[Tooltip("Will parent the instance back under its pool's group before calling Despawn().")]
		public FsmBool reParent;

		[Tooltip("delay")]
		public FsmFloat delay;

		[ActionSection(" ")]
		[Tooltip("Trigger this event if something went wrong")]
		public FsmEvent failureEvent;

		private SpawnPool _pool;

		public override void Reset()
		{
			poolName = null;
			gameObject = null;
			reParent = false;
			delay = 0f;
		}

		public override void OnEnter()
		{
			DoDeSpawn();
			Finish();
		}

		private void DoDeSpawn()
		{
			if (poolName.Value == "" || gameObject.Value == null)
			{
				return;
			}
			if (!PoolManager.Pools.TryGetValue(poolName.Value, out _pool))
			{
				LogWarning($"Pool {poolName.Value} doesn't exists");
				if (failureEvent != null)
				{
					base.Fsm.Event(failureEvent);
				}
				return;
			}
			if (reParent.Value)
			{
				gameObject.Value.transform.parent = _pool.group;
			}
			if (delay.Value > 0f)
			{
				_pool.Despawn(gameObject.Value.transform, delay.Value);
			}
			else
			{
				_pool.Despawn(gameObject.Value.transform);
			}
		}
	}
}
