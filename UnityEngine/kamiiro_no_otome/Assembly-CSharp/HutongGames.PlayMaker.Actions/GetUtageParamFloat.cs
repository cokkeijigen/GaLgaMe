using Utage;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Utage")]
	[Tooltip("")]
	public class GetUtageParamFloat : FsmStateAction
	{
		[RequiredField]
		public FsmString paramKey;

		[RequiredField]
		public FsmFloat storeResult;

		[RequiredField]
		[ObjectType(typeof(AdvEngine))]
		public FsmObject engine;

		public override void Reset()
		{
			paramKey = null;
			storeResult = null;
			engine = null;
		}

		public override void OnEnter()
		{
			AdvEngine advEngine = engine.Value as AdvEngine;
			storeResult.Value = (float)advEngine.Param.GetParameter(paramKey.Value);
			Finish();
		}
	}
}
