using DG.Tweening;
using Doozy.PlayMaker.Actions;
using HutongGames.PlayMaker;
using UnityEngine;

namespace Doozy.PlayMaker
{
	public static class DOTweenExtensions
	{
		public static void SetTweenId(this Tween tween, TweenId tweenId, FsmString stringAsId, FsmString tagAsId, GameObject gameObject)
		{
			switch (tweenId)
			{
			case TweenId.UseString:
				if (!string.IsNullOrEmpty(stringAsId.Value))
				{
					tween.SetId(stringAsId.Value);
				}
				break;
			case TweenId.UseTag:
				if (!string.IsNullOrEmpty(tagAsId.Value))
				{
					tween.SetId(tagAsId.Value);
				}
				break;
			case TweenId.UseGameObject:
				tween.SetId(gameObject);
				break;
			}
		}

		public static void SetTweenId(this Sequence sequence, TweenId tweenId, FsmString stringAsId, FsmString tagAsId, GameObject gameObject)
		{
			switch (tweenId)
			{
			case TweenId.UseString:
				if (!string.IsNullOrEmpty(stringAsId.Value))
				{
					sequence.SetId(stringAsId.Value);
				}
				break;
			case TweenId.UseTag:
				if (!string.IsNullOrEmpty(tagAsId.Value))
				{
					sequence.SetId(tagAsId.Value);
				}
				break;
			case TweenId.UseGameObject:
				sequence.SetId(gameObject);
				break;
			}
		}

		public static void SetSelectedEase(this Tween tween, SelectedEase selectedEase, Ease easeType, FsmAnimationCurve animationCurve)
		{
			switch (selectedEase)
			{
			case SelectedEase.EaseType:
				tween.SetEase(easeType);
				break;
			case SelectedEase.AnimationCurve:
				tween.SetEase(animationCurve.curve);
				break;
			}
		}

		public static void SetSelectedEase(this Sequence sequence, SelectedEase selectedEase, Ease easeType, FsmAnimationCurve animationCurve)
		{
			switch (selectedEase)
			{
			case SelectedEase.EaseType:
				sequence.SetEase(easeType);
				break;
			case SelectedEase.AnimationCurve:
				sequence.SetEase(animationCurve.curve);
				break;
			}
		}
	}
}
