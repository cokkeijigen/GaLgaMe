namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Static")]
	public class SetScenarioFlagBoolAction : FsmStateAction
	{
		public FsmString flagName;

		public FsmBool setValue;

		public override void Reset()
		{
			flagName = "";
			setValue = false;
		}

		public override void OnEnter()
		{
			PlayerFlagDataManager.scenarioFlagDictionary[flagName.Value] = setValue.Value;
			Finish();
		}
	}
}
