using UnityEngine;
using UnityEngine.EventSystems;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Device)]
	[Tooltip("Sends an event if a touch input does not hit a gui element. It also work with the left mouse")]
	public class TouchEventNotOnUGui : FsmStateAction
	{
		[Tooltip("Event to send when the touch is over an uGui object.")]
		public FsmEvent pointerOverUI;

		[Tooltip("Event to send when the touch is NOT over an uGui object.")]
		public FsmEvent pointerNotOverUI;

		[UIHint(UIHint.Variable)]
		public FsmBool isPointerOverUI;

		public override void Reset()
		{
			pointerOverUI = null;
			pointerNotOverUI = null;
			isPointerOverUI = null;
		}

		public override void OnPreprocess()
		{
			base.Fsm.HandleLateUpdate = true;
		}

		public override void OnLateUpdate()
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (!EventSystem.current.IsPointerOverGameObject())
				{
					isPointerOverUI.Value = false;
					base.Fsm.Event(pointerNotOverUI);
				}
				else
				{
					isPointerOverUI.Value = true;
					base.Fsm.Event(pointerOverUI);
				}
			}
			else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
			{
				if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
				{
					isPointerOverUI.Value = false;
					base.Fsm.Event(pointerNotOverUI);
				}
				else
				{
					isPointerOverUI.Value = true;
					base.Fsm.Event(pointerOverUI);
				}
			}
		}
	}
}
