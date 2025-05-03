using DG.Tweening;
using Doozy.PlayMaker.Actions;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("DOTween")]
	[Tooltip("Toggles the play state of all tweens with the given ID (meaning tweens that could be played or paused, depending on the toggle state)")]
	[HelpUrl("http://dotween.demigiant.com/documentation.php")]
	public class DOTweenControlMethodsTogglePauseById : FsmStateAction
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
					num = DOTween.TogglePause(stringAsId.Value);
				}
				break;
			case TweenId.UseTag:
				if (!string.IsNullOrEmpty(tagAsId.Value))
				{
					num = DOTween.TogglePause(tagAsId.Value);
				}
				break;
			case TweenId.UseGameObject:
				if (gameObjectAsId.Value != null)
				{
					num = DOTween.TogglePause(gameObjectAsId.Value);
				}
				break;
			}
			if (debugThis.Value)
			{
				base.State.Debug("DOTween Control Methods TogglePause All - Toggled " + num + " tweens");
			}
			Finish();
		}
	}
}
