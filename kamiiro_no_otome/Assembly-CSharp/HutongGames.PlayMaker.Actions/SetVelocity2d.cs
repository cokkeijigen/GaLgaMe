using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Sets the 2d Velocity of a Game Object. To leave any axis unchanged, set variable to 'None'. NOTE: Game object must have a rigidbody 2D.")]
	public class SetVelocity2d : ComponentAction<Rigidbody2D>
	{
		[RequiredField]
		[CheckForComponent(typeof(Rigidbody2D))]
		[Tooltip("The GameObject with the Rigidbody2D attached")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Use a Vector2 value for the velocity and/or set individual axis below. If set to None, keeps current velocity.")]
		public FsmVector2 vector;

		[Tooltip("Set the x value of the velocity. If None keep current x velocity.")]
		public FsmFloat x;

		[Tooltip("Set the y value of the velocity. If None keep current y velocity.")]
		public FsmFloat y;

		[Tooltip("Set velocity in local or word space.")]
		public Space space;

		[Tooltip("Repeat every frame.")]
		public bool everyFrame;

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
			space = Space.World;
			everyFrame = false;
		}

		public override void Awake()
		{
			base.Fsm.HandleFixedUpdate = true;
		}

		public override void OnEnter()
		{
			DoSetVelocity();
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnFixedUpdate()
		{
			DoSetVelocity();
			if (!everyFrame)
			{
				Finish();
			}
		}

		private void DoSetVelocity()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (!UpdateCacheAndTransform(ownerDefaultTarget))
			{
				return;
			}
			Vector2 vector = default(Vector2);
			if (this.vector.IsNone)
			{
				if (space == Space.World)
				{
					vector = base.rigidbody2d.velocity;
				}
				else
				{
					Vector3 vector2 = base.cachedTransform.InverseTransformDirection(base.rigidbody2d.velocity);
					vector.x = vector2.x;
					vector.y = vector2.y;
				}
			}
			else
			{
				vector = this.vector.Value;
			}
			if (!x.IsNone)
			{
				vector.x = x.Value;
			}
			if (!y.IsNone)
			{
				vector.y = y.Value;
			}
			if (space == Space.Self)
			{
				Vector3 vector3 = base.cachedTransform.TransformDirection(vector);
				vector.Set(vector3.x, vector3.y);
			}
			base.rigidbody2d.velocity = vector;
		}
	}
}
