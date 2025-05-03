using PathologicalGames;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Path-o-logical/Pool Manager 2")]
	[Tooltip("Get the number of SpawnPools in Pools.")]
	[HelpUrl("https://hutonggames.fogbugz.com/default.asp?W848")]
	public class PmtGetPoolsCount : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The number of SpawnPools in Pools")]
		public FsmInt poolsCount;

		[ActionSection("")]
		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		public override void Reset()
		{
			poolsCount = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			doGetPoolsCount();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			doGetPoolsCount();
		}

		private void doGetPoolsCount()
		{
			poolsCount.Value = PoolManager.Pools.Count;
		}
	}
}
