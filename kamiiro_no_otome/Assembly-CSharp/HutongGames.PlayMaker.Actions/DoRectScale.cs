using DG.Tweening;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Tween")]
	[Tooltip("")]
	public class DoRectScale : FsmStateAction
	{
		[RequiredField]
		[CheckForComponent(typeof(RectTransform))]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		public FsmVector3 vector3;

		[RequiredField]
		public FsmFloat duration;

		private Tweener tween;

		public override void Reset()
		{
			gameObject = null;
			vector3 = null;
			duration = 0f;
		}

		public override void OnEnter()
		{
			tween = base.Fsm.GetOwnerDefaultTarget(gameObject).GetComponent<RectTransform>().DOScale(vector3.Value, duration.Value);
			tween.OnComplete(base.Finish);
		}
	}
}
