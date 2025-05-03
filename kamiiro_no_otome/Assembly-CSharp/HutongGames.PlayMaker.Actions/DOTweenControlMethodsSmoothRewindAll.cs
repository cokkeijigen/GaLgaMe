using DG.Tweening;
using Doozy.PlayMaker.Actions;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("DOTween")]
	[Tooltip("Smoothly rewinds all tweens (delays excluded) (meaning tweens that were not already rewinded). A 'smooth rewind' animates the tween to its start position, skipping all elapsed loops (except in case of LoopType.Incremental) while keeping the animation fluent. Note that a tween that was smoothly rewinded will have its play direction flipped")]
	[HelpUrl("http://dotween.demigiant.com/documentation.php")]
	public class DOTweenControlMethodsSmoothRewindAll : FsmStateAction
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
			int num = DOTween.SmoothRewindAll();
			if (debugThis.Value)
			{
				base.State.Debug("DOTween Control Methods Smooth Rewind All - Rewinding/Rewinded " + num + " tweens");
			}
			Finish();
		}
	}
}
