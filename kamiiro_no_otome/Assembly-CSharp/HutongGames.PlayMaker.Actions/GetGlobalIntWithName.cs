namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Variables")]
	public class GetGlobalIntWithName : FsmStateAction
	{
		[RequiredField]
		public FsmString variablesName;

		[RequiredField]
		public FsmInt storeValue;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		public override void Reset()
		{
			variablesName = null;
			storeValue = 0;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetValue();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetValue();
		}

		public void DoGetValue()
		{
			if (!variablesName.IsNone)
			{
				storeValue.Value = FsmVariables.GlobalVariables.GetFsmInt(variablesName.Value).Value;
			}
		}
	}
}
