using Utage;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Utage")]
	[Tooltip("")]
	public class SetUtageParamTableInt : FsmStateAction
	{
		[RequiredField]
		public FsmString tableName;

		[RequiredField]
		public FsmString paramKey;

		[RequiredField]
		public FsmString columnName;

		[RequiredField]
		public FsmInt value;

		[RequiredField]
		[ObjectType(typeof(AdvEngine))]
		public FsmObject engine;

		public override void Reset()
		{
			tableName = null;
			paramKey = null;
			columnName = null;
			value = null;
			engine = null;
		}

		public override void OnEnter()
		{
			AdvEngine advEngine = engine.Value as AdvEngine;
			string key = tableName.Value + "[" + paramKey.Value + "]." + columnName;
			advEngine.Param.TrySetParameter(key, value.Value);
			Finish();
		}
	}
}
