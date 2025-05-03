using DG.Tweening;
using Doozy.PlayMaker.Actions;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("DOTween")]
	[Tooltip("Rewinds and pauses all tweens (meaning tweens that were not already rewinded)")]
	[HelpUrl("http://dotween.demigiant.com/documentation.php")]
	public class DOTweenControlMethodsRewindAll : FsmStateAction
	{
		[UIHint(UIHint.FsmBool)]
		[Tooltip("If TRUE includes the eventual tween delay, otherwise skips it.")]
		public FsmBool includeDelay;

		[ActionSection("Debug Options")]
		[UIHint(UIHint.FsmBool)]
		public FsmBool debugThis;

		public override void Reset()
		{
			base.Reset();
			includeDelay = new FsmBool
			{
				UseVariable = false,
				Value = true
			};
			debugThis = new FsmBool
			{
				Value = false
			};
		}

		public override void OnEnter()
		{
			int num = DOTween.RewindAll(includeDelay.Value);
			if (debugThis.Value)
			{
				base.State.Debug("DOTween Control Methods Rewind All - Rewinded and paused " + num + " tweens");
			}
			Finish();
		}
	}
}
