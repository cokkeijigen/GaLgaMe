using DG.Tweening;
using Doozy.PlayMaker.Actions;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("DOTween")]
	[Tooltip("Plays all tweens with the given ID (meaning the tweens that were not already playing or complete)")]
	[HelpUrl("http://dotween.demigiant.com/documentation.php")]
	public class DOTweenControlMethodsPlayById : FsmStateAction
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
					num = DOTween.Play(stringAsId.Value);
				}
				break;
			case TweenId.UseTag:
				if (!string.IsNullOrEmpty(tagAsId.Value))
				{
					num = DOTween.Play(tagAsId.Value);
				}
				break;
			case TweenId.UseGameObject:
				if (gameObjectAsId.Value != null)
				{
					num = DOTween.Play(gameObjectAsId.Value);
				}
				break;
			}
			if (debugThis.Value)
			{
				base.State.Debug("DOTween Control Methods Play By Id - Played " + num + " tweens");
			}
			Finish();
		}
	}
}
