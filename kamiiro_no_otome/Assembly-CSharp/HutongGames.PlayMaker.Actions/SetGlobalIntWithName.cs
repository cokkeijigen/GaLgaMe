namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Variables")]
	public class SetGlobalIntWithName : FsmStateAction
	{
		[RequiredField]
		public FsmString variablesName;

		[RequiredField]
		public FsmInt setValue;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		public override void Reset()
		{
			variablesName = null;
			setValue = 0;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoSetValue();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoSetValue();
		}

		public void DoSetValue()
		{
			if (!variablesName.IsNone)
			{
				FsmVariables.GlobalVariables.GetFsmInt(variablesName.Value).Value = setValue.Value;
			}
		}
	}
}
