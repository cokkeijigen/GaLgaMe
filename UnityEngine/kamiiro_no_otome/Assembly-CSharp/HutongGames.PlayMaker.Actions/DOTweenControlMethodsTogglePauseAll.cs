using DG.Tweening;
using Doozy.PlayMaker.Actions;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("DOTween")]
	[Tooltip("Toggles the play state of all tweens (meaning tweens that could be played or paused, depending on the toggle state)")]
	[HelpUrl("http://dotween.demigiant.com/documentation.php")]
	public class DOTweenControlMethodsTogglePauseAll : FsmStateAction
	{
		[ActionSection("Debug Options")]
		[UIHint(UIHint.FsmBool)]
		public FsmBool debugThis;

		public override void Reset()
		{
			base.Reset();
			debugThis = new FsmBool
			{
				Value = false
			};
		}

		public override void OnEnter()
		{
			int num = DOTween.TogglePauseAll();
			if (debugThis.Value)
			{
				base.State.Debug("DOTween Control Methods TogglePause All - Toggled " + num + " tweens");
			}
			Finish();
		}
	}
}
