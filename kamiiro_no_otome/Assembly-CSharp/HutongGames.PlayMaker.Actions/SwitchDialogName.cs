namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Static")]
	public class SwitchDialogName : FsmStateAction
	{
		[CompoundArray("String Switches", "Compare String", "Send Event")]
		public FsmString[] compareTo;

		public FsmEvent[] sendEvent;

		public override void Reset()
		{
			compareTo = new FsmString[1];
			sendEvent = new FsmEvent[1];
		}

		public override void OnEnter()
		{
			for (int i = 0; i < compareTo.Length; i++)
			{
				if (PlayerNonSaveDataManager.openDialogName == compareTo[i].Value)
				{
					base.Fsm.Event(sendEvent[i]);
					return;
				}
			}
			Finish();
		}
	}
}
