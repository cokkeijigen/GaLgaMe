using DG.Tweening;
using Doozy.PlayMaker.Actions;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("DOTween")]
	[Tooltip("Smoothly rewinds all tweens (delays excluded) (meaning tweens that were not already rewinded). A 'smooth rewind' animates the tween to its start position, skipping all elapsed loops (except in case of LoopType.Incremental) while keeping the animation fluent. Note that a tween that was smoothly rewinded will have its play direction flipped")]
	[HelpUrl("http://dotween.demigiant.com/documentation.php")]
	public class DOTweenControlMethodsSmoothRewindById : FsmStateAction
	{
		[Tooltip("Select the tween ID type to use")]
		public TweenId tweenIdType;

		[UIHint(UIHint.FsmString)]
		[Tooltip("Use a String as the tween ID")]
		public FsmString stringAsId;

		[UIHint(UIHint.Tag)]
		[Tooltip("Use a Tag as the tween ID")]
		public FsmString tagAsId;

		[UIHint(UIHint.FsmGameObject)]
		[Tooltip("Use a GameObject as the tween ID")]
		public FsmGameObject gameObjectAsId;

		[ActionSection("Debug Options")]
		[UIHint(UIHint.FsmBool)]
		public FsmBool debugThis;

		public override void Reset()
		{
			base.Reset();
			stringAsId = new FsmString
			{
				UseVariable = false
			};
			tagAsId = new FsmString
			{
				UseVariable = false
			};
			gameObjectAsId = new FsmGameObject
			{
				UseVariable = false,
				Value = null
			};
			debugThis = new FsmBool
			{
				Value = false
			};
		}

		public override void OnEnter()
		{
			int num = 0;
			switch (tweenIdType)
			{
			case TweenId.UseString:
				if (!string.IsNullOrEmpty(stringAsId.Value))
				{
					num = DOTween.SmoothRewind(stringAsId.Value);
				}
				break;
			case TweenId.UseTag:
				if (!string.IsNullOrEmpty(tagAsId.Value))
				{
					num = DOTween.SmoothRewind(tagAsId.Value);
				}
				break;
			case TweenId.UseGameObject:
				if (gameObjectAsId.Value != null)
				{
					num = DOTween.SmoothRewind(gameObjectAsId.Value);
				}
				break;
			}
			if (debugThis.Value)
			{
				base.State.Debug("DOTween Control Methods Smooth Rewind All - Rewinding/Rewinded " + num + " tweens");
			}
			Finish();
		}
	}
}
