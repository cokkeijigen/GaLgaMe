using DG.Tweening;
using Doozy.PlayMaker.Actions;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("DOTween")]
	[Tooltip("Pauses all tweens")]
	[HelpUrl("http://dotween.demigiant.com/documentation.php")]
	public class DOTweenControlMethodsPauseAll : FsmStateAction
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
			int num = DOTween.PauseAll();
			if (debugThis.Value)
			{
				base.State.Debug("DOTween Control Methods Pause All - Paused " + num + " tweens");
			}
			Finish();
		}
	}
}
