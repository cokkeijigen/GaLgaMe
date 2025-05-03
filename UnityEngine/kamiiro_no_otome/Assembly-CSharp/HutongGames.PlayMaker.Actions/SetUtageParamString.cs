using Utage;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Utage")]
	[Tooltip("")]
	public class SetUtageParamString : FsmStateAction
	{
		[RequiredField]
		public FsmString paramKey;

		[RequiredField]
		public FsmString value;

		[RequiredField]
		[ObjectType(typeof(AdvEngine))]
		public FsmObject engine;

		public override void Reset()
		{
			paramKey = null;
			value = "";
			engine = null;
		}

		public override void OnEnter()
		{
			(engine.Value as AdvEngine).Param.TrySetParameter(paramKey.Value, value.Value);
			Finish();
		}
	}
}
