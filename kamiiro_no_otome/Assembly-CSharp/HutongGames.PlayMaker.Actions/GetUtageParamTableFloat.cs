using Utage;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Utage")]
	[Tooltip("")]
	public class GetUtageParamTableFloat : FsmStateAction
	{
		[RequiredField]
		public FsmString tableName;

		[RequiredField]
		public FsmString paramKey;

		[RequiredField]
		public FsmString columnName;

		[RequiredField]
		public FsmFloat storeResult;

		[RequiredField]
		[ObjectType(typeof(AdvEngine))]
		public FsmObject engine;

		public override void Reset()
		{
			tableName = null;
			paramKey = null;
			columnName = null;
			storeResult = null;
			engine = null;
		}

		public override void OnEnter()
		{
			AdvEngine advEngine = engine.Value as AdvEngine;
			string key = tableName.Value + "[" + paramKey.Value + "]." + columnName;
			storeResult.Value = (float)advEngine.Param.GetParameter(key);
			Finish();
		}
	}
}
