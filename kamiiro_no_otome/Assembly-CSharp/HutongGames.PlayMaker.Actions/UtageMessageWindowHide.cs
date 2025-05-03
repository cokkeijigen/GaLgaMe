using Utage;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Utage")]
	[Tooltip("")]
	public class UtageMessageWindowHide : FsmStateAction
	{
		[RequiredField]
		[ObjectType(typeof(AdvEngine))]
		public FsmObject engine;

		public override void Reset()
		{
			engine = null;
		}

		public override void OnEnter()
		{
			(engine.Value as AdvEngine).UiManager.HideMessageWindow();
			Finish();
		}
	}
}
