using DG.Tweening;
using Doozy.PlayMaker.Actions;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("DOTween")]
	[Tooltip("Clears all cached tween pools.")]
	[HelpUrl("http://dotween.demigiant.com/documentation.php")]
	public class DOTweenAdditionalMethodsClearCachedTweens : FsmStateAction
	{
		[ActionSection("Debug Options")]
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
			DOTween.ClearCachedTweens();
			if (debugThis.Value)
			{
				base.State.Debug("DOTween Additional Methods Clear Cached Tweens");
			}
			Finish();
		}
	}
}
