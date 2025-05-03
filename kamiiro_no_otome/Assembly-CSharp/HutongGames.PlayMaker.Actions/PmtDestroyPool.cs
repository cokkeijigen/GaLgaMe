using PathologicalGames;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Path-o-logical/Pool Manager 2")]
	[Tooltip("Destroys a SpawnPool in PoolManager.Pools including all instances and references as well as the GameObject.\nIMPORTANT: This is going to destroy the instances, NOT despawn. Make sure you only do this when performance is not an issue, such as when a level ends or a new level is loaded.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W845")]
	public class PmtDestroyPool : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The name of the pool to destroy")]
		public FsmString poolName;

		public override void Reset()
		{
			poolName = null;
		}

		public override void OnEnter()
		{
			PoolManager.Pools.Destroy(poolName.Value);
		}
	}
}
