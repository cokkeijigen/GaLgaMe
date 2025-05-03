using Utage;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Utage")]
	[Tooltip("")]
	public class SetUtageParamInt : FsmStateAction
	{
		[RequiredField]
		public FsmString paramKey;

		[RequiredField]
		public FsmInt value;

		[RequiredField]
		[ObjectType(typeof(AdvEngine))]
		public FsmObject engine;

		public override void Reset()
		{
			paramKey = null;
			value = 0;
			engine = null;
		}

		public override void OnEnter()
		{
			(engine.Value as AdvEngine).Param.TrySetParameter(paramKey.Value, value.Value);
			Finish();
		}
	}
}
