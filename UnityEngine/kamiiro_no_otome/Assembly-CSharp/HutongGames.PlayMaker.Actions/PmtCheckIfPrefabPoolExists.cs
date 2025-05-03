using PathologicalGames;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Path-o-logical/Pool Manager 2")]
	[Tooltip("Check if a PrefabPool in this Pool exists already.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W840")]
	public class PmtCheckIfPrefabPoolExists : FsmStateAction
	{
		[ActionSection("Set up")]
		[RequiredField]
		[Tooltip("Pool Name.")]
		public FsmString poolName;

		[RequiredField]
		[Tooltip("Prefab")]
		public FsmGameObject prefab;

		[ActionSection("result")]
		[Tooltip("Trigger this event is the pool is already set with the 'prefab'.")]
		public FsmEvent alreadyExistsEvent;

		[Tooltip("Trigger this event if the pool doesn't contains prefabPool with 'prefab'")]
		public FsmEvent doesntExistsEvent;

		[Tooltip("Trigger this event if something went wrong")]
		public FsmEvent failureEvent;

		private SpawnPool _pool;

		public override void Reset()
		{
			poolName = null;
			prefab = null;
			alreadyExistsEvent = null;
			doesntExistsEvent = null;
			failureEvent = null;
		}

		public override void OnEnter()
		{
			CheckPrefabPool();
			Finish();
		}

		private void CheckPrefabPool()
		{
			if (poolName.Value == "")
			{
				LogWarning("Pool name not set");
				if (failureEvent != null)
				{
					base.Fsm.Event(failureEvent);
				}
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
			bool flag = !(_pool.GetPrefab(prefab.Value) == null);
			if (alreadyExistsEvent != null && flag)
			{
				base.Fsm.Event(alreadyExistsEvent);
			}
			else if (doesntExistsEvent != null)
			{
				base.Fsm.Event(doesntExistsEvent);
			}
		}
	}
}
