using PathologicalGames;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Path-o-logical/Pool Manager 2")]
	[Tooltip("Destroys ALL SpawnPool in PoolManager.Pools including all instances and references as well as the GameObjects")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W844")]
	public class PmtDestroyAllPools : FsmStateAction
	{
		public override void OnEnter()
		{
			PoolManager.Pools.DestroyAll();
		}
	}
}
