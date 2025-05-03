using Utage;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Utage")]
	[Tooltip("")]
	public class GetUtageParamTableString : FsmStateAction
	{
		[RequiredField]
		public FsmString tableName;

		[RequiredField]
		public FsmString paramKey;

		[RequiredField]
		public FsmString columnName;

		[RequiredField]
		public FsmString storeResult;

		[RequiredField]
		[ObjectType(typeof(AdvEngine))]
		public FsmObject engine;

		private object isGet;

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
			if (advEngine.Param.TryGetParameter(key, out isGet))
			{
				storeResult.Value = advEngine.Param.GetParameter(key).ToString();
			}
			else
			{
				LogError("指定パラメータが存在しません");
			}
			Finish();
		}
	}
}
