using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Input)]
	[Tooltip("Gets the Vector3 Position of the mouse and stores it in a Variable.")]
	public class GetMousePosition : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmVector3 mousePosition;

		public bool normalize;

		public bool everyFrame;

		public override void Reset()
		{
			mousePosition = null;
			normalize = true;
			everyFrame = false;
		}

		public override void OnEnter()
		{
			DoGetMousePosition();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			DoGetMousePosition();
		}

		private void DoGetMousePosition()
		{
			if (mousePosition != null)
			{
				Vector3 value = Input.mousePosition;
				if (normalize)
				{
					value.x /= Screen.width;
					value.y /= Screen.height;
				}
				mousePosition.Value = value;
			}
		}
	}
}
