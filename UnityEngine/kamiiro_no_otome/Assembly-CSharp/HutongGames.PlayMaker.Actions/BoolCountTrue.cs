namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Tests if any of the given Bool Variables are True.")]
	public class BoolCountTrue : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The Bool variables to check.")]
		public FsmBool[] boolVariables;

		[RequiredField]
		public FsmInt checkCount;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the result in a Bool variable.")]
		public FsmBool storeResult;

		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		private int count;

		public override void Reset()
		{
			boolVariables = null;
			checkCount = null;
			storeResult = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoAnyTrue();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoAnyTrue();
		}

		private void DoAnyTrue()
		{
			if (boolVariables.Length == 0)
			{
				return;
			}
			storeResult.Value = false;
			count = 0;
			for (int i = 0; i < boolVariables.Length; i++)
			{
				if (boolVariables[i].Value)
				{
					count++;
					if (count == checkCount.Value)
					{
						storeResult.Value = true;
					}
				}
			}
		}
	}
}
