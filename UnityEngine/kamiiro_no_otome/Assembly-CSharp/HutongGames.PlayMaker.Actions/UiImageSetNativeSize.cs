using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.UI)]
	[Tooltip("Sets the source image sprite of a UI Image component.")]
	public class UiImageSetNativeSize : ComponentAction<Image>
	{
		[RequiredField]
		[CheckForComponent(typeof(Image))]
		[Tooltip("The GameObject with the Image UI component.")]
		public FsmOwnerDefault gameObject;

		private Image image;

		public override void Reset()
		{
			gameObject = null;
		}

		public override void OnEnter()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (UpdateCache(ownerDefaultTarget))
			{
				image = cachedComponent;
			}
			Do();
			Finish();
		}

		private void Do()
		{
			if (!(image == null))
			{
				image.SetNativeSize();
			}
		}

		public override void OnExit()
		{
			_ = image == null;
		}
	}
}
