using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Get CanvasGroup Alpha.")]
	public class UiCanvasGroupGetAlpha : ComponentAction<CanvasGroup>
	{
		[RequiredField]
		[CheckForComponent(typeof(CanvasGroup))]
		[Tooltip("The GameObject with a UI CanvasGroup component.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[UIHint(UIHint.Variable)]
		[Tooltip("The alpha of the UI CanvasGroup.")]
		public FsmFloat alpha;

		[Tooltip("Repeats every frame, useful for animation")]
		public bool everyFrame;

		public override void Reset()
		{
			gameObject = null;
			alpha = null;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetValue();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetValue();
		}

		private void DoGetValue()
		{
			if (UpdateCache(base.Fsm.GetOwnerDefaultTarget(gameObject)))
			{
				alpha.Value = cachedComponent.alpha;
			}
		}
	}
}
