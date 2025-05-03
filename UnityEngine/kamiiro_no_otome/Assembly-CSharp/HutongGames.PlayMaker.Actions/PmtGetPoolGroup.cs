using PathologicalGames;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Path-o-logical/Pool Manager 2")]
	[Tooltip("The SpawnPool's 'group' is the GameObject with the SpawnPool component and is the parent of all instances in the pool by default. ")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W846")]
	public class PmtGetPoolGroup : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The name of the pool")]
		public FsmString poolName;

		[RequiredField]
		[Tooltip("The group of this pool")]
		public FsmGameObject poolGroup;

		[ActionSection(" ")]
		[Tooltip("Trigger this event if something went wrong")]
		public FsmEvent failureEvent;

		private SpawnPool _pool;

		public override void Reset()
		{
			poolName = null;
			poolGroup = null;
			failureEvent = null;
		}

		public override void OnEnter()
		{
			doGetPoolGroup();
		}

		private void doGetPoolGroup()
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
				poolGroup.Value = _pool.group.gameObject;
			}
		}
	}
}
