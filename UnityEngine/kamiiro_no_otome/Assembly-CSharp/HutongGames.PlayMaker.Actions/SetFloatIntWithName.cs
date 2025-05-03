namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Variables")]
	public class SetFloatIntWithName : FsmStateAction
	{
		[RequiredField]
		public FsmString variablesName;

		[RequiredField]
		public FsmFloat setValue;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		public override void Reset()
		{
			variablesName = null;
			setValue = 0f;
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
				base.Fsm.Variables.GetFsmFloat(variablesName.Value).Value = setValue.Value;
			}
		}
	}
}
