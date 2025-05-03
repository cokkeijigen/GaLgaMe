using Utage;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Utage")]
	[Tooltip("")]
	public class SetUtageParamBool : FsmStateAction
	{
		[RequiredField]
		public FsmString paramKey;

		[RequiredField]
		public FsmBool value;

		[RequiredField]
		[ObjectType(typeof(AdvEngine))]
		public FsmObject engine;

		public override void Reset()
		{
			paramKey = null;
			value = false;
			engine = null;
		}

		public override void OnEnter()
		{
			(engine.Value as AdvEngine).Param.TrySetParameter(paramKey.Value, value.Value);
			Finish();
		}
	}
}
