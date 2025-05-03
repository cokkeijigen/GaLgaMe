namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Variables")]
	public class GetGlobalBoolWithName : FsmStateAction
	{
		[RequiredField]
		public FsmString variablesName;

		[RequiredField]
		public FsmBool storeValue;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		public override void Reset()
		{
			variablesName = null;
			storeValue = false;
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
				storeValue.Value = FsmVariables.GlobalVariables.GetFsmBool(variablesName.Value).Value;
			}
		}
	}
}
