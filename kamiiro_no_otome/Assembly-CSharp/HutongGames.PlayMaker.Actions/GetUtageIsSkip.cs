using Utage;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Utage")]
	[Tooltip("")]
	public class GetUtageIsSkip : FsmStateAction
	{
		[RequiredField]
		public FsmBool storeResult;

		[RequiredField]
		[ObjectType(typeof(AdvEngine))]
		public FsmObject engine;

		public bool everyFrame;

		public override void Reset()
		{
			storeResult = false;
			engine = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			Do();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			Do();
		}

		private void Do()
		{
			AdvEngine advEngine = engine.Value as AdvEngine;
			storeResult.Value = advEngine.Config.IsSkip;
		}
	}
}
