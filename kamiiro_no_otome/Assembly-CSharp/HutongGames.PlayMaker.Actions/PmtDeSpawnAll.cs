using PathologicalGames;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Path-o-logical/Pool Manager 2")]
	[Tooltip("Despawns all active instances in this SpawnPool")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W1452")]
	public class PmtDeSpawnAll : FsmStateAction
	{
		[RequiredField]
		[Tooltip("Pool name")]
		public FsmString poolName;

		[ActionSection(" ")]
		[Tooltip("Trigger this event if something went wrong")]
		public FsmEvent failureEvent;

		private SpawnPool _pool;

		public override void Reset()
		{
			poolName = null;
		}

		public override void OnEnter()
		{
			DoDeSpawnAll();
			Finish();
		}

		private void DoDeSpawnAll()
		{
			if (poolName.Value == "")
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
			}
			else
			{
				_pool.DespawnAll();
			}
		}
	}
}
