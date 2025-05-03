using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Sets the 2d Position of a Game Object. To leave any axis unchanged, set variable to 'None'.")]
	public class SetPosition2d : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to position.")]
		public FsmOwnerDefault gameObject;

		[UIHint(UIHint.Variable)]
		[Tooltip("Use a stored Vector2 position, and/or set individual axis below.")]
		public FsmVector2 vector;

		[Tooltip("Set the X position.")]
		public FsmFloat x;

		[Tooltip("Set the Y position.")]
		public FsmFloat y;

		[Tooltip("Use local or world space.")]
		public Space space;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

		[Tooltip("Perform in LateUpdate. This is useful if you want to override the position of objects that are animated or otherwise positioned in Update.")]
		public bool lateUpdate;

		public override void Reset()
		{
			gameObject = null;
			vector = null;
			x = new FsmFloat
			{
				UseVariable = true
			};
			y = new FsmFloat
			{
				UseVariable = true
			};
			space = Space.Self;
			everyFrame = false;
			lateUpdate = false;
		}

		public override void OnPreprocess()
		{
			if (lateUpdate)
			{
				base.Fsm.HandleLateUpdate = true;
			}
		}

		public override void OnEnter()
		{
			if (!everyFrame && !lateUpdate)
			{
				DoSetPosition();
				Finish();
			}
		}

		public override void OnUpdate()
		{
			if (!lateUpdate)
			{
				DoSetPosition();
			}
		}

		public override void OnLateUpdate()
		{
			if (lateUpdate)
			{
				DoSetPosition();
			}
			if (!everyFrame)
			{
				Finish();
			}
		}

		private void DoSetPosition()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (!(ownerDefaultTarget == null))
			{
				Vector2 vector = ((!this.vector.IsNone) ? this.vector.Value : ((Vector2)((space == Space.World) ? ownerDefaultTarget.transform.position : ownerDefaultTarget.transform.localPosition)));
				if (!x.IsNone)
				{
					vector.x = x.Value;
				}
				if (!y.IsNone)
				{
					vector.y = y.Value;
				}
				if (space == Space.World)
				{
					ownerDefaultTarget.transform.position = vector;
				}
				else
				{
					ownerDefaultTarget.transform.localPosition = vector;
				}
			}
		}
	}
}
