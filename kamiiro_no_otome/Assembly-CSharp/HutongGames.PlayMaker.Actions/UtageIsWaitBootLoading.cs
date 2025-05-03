using Utage;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Utage")]
	[Tooltip("")]
	public class UtageIsWaitBootLoading : FsmStateAction
	{
		[RequiredField]
		[ObjectType(typeof(AdvEngine))]
		public FsmObject engine;

		public FsmBool isWait;

		public override void Reset()
		{
			engine = null;
			isWait = true;
		}

		public override void OnUpdate()
		{
			AdvEngine advEngine = engine.Value as AdvEngine;
			if (!advEngine.IsWaitBootLoading)
			{
				isWait.Value = advEngine.IsWaitBootLoading;
				Finish();
			}
		}
	}
}
