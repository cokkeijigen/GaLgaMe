using Utage;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Utage")]
	[Tooltip("")]
	public class GetUtageParamInt : FsmStateAction
	{
		[RequiredField]
		public FsmString paramKey;

		[RequiredField]
		public FsmInt storeResult;

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
			storeResult.Value = (int)advEngine.Param.GetParameter(paramKey.Value);
			Finish();
		}
	}
}
