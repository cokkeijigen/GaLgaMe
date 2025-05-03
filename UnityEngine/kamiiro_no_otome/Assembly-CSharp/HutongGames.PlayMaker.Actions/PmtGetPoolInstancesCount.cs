using PathologicalGames;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Path-o-logical/Pool Manager 2")]
	[Tooltip("Get the number of spawned instances in the pool.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W847")]
	public class PmtGetPoolInstancesCount : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The name of the pool")]
		public FsmString poolName;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The number of spawned instances in the pool.")]
		public FsmInt instancesCount;

		[Tooltip("Trigger this event if something went wrong")]
		public FsmEvent failureEvent;

		[ActionSection("")]
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		private SpawnPool _pool;

		public override void Reset()
		{
			poolName = null;
			instancesCount = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			doGetPoolInstancesCount();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			doGetPoolInstancesCount();
		}

		private void doGetPoolInstancesCount()
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
				instancesCount.Value = _pool.Count;
			}
		}
	}
}
