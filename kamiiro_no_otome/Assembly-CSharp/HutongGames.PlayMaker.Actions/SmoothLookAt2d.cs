using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Transform)]
	[Tooltip("Smoothly Rotates a 2d Game Object so its right vector points at a Target. The target can be defined as a 2d Game Object or a 2d/3d world Position. If you specify both, then the position will be used as a local offset from the object's position.")]
	public class SmoothLookAt2d : FsmStateAction
	{
		[RequiredField]
		[Tooltip("The GameObject to rotate to face a target.")]
		public FsmOwnerDefault gameObject;

		[ActionSection("Target")]
		[Tooltip("A target GameObject.")]
		public FsmGameObject targetObject;

		[Tooltip("A target position. If a Target Object is defined, this is used as a local offset.")]
		public FsmVector2 targetPosition2d;

		[Tooltip("A target position. If a Target Object is defined, this is used as a local offset.")]
		public FsmVector3 targetPosition;

		[ActionSection("Rotation")]
		[Tooltip("Set the GameObject starting offset. In degrees. 0 if your object is facing right, 180 if facing left etc...")]
		public FsmFloat rotationOffset;

		[HasFloatSlider(0.5f, 15f)]
		[Tooltip("How fast to rotate to look at the target. Higher numbers are faster. Note, you can enter numbers outside the slider range.")]
		public FsmFloat speed;

		[Tooltip("Set min/max angle limits for the look at rotation. Note, you can use a scene gizmo to set the angles.")]
		public FsmBool useLimits;

		[HideIf("HideLimits")]
		[Tooltip("Min angle limit.")]
		public FsmFloat minAngle;

		[HideIf("HideLimits")]
		[Tooltip("Max angle limit.")]
		public FsmFloat maxAngle;

		[Tooltip("Draw a line in the Scene View to the look at position.")]
		public FsmBool debug;

		[ActionSection("Finished")]
		[Tooltip("If the angle to the target is less than this, send the Finish Event below. Measured in degrees.")]
		public FsmFloat finishTolerance;

		[Tooltip("Event to send if the angle to target is less than the Finish Tolerance.")]
		public FsmEvent finishEvent;

		[Tooltip("Should the event stop running when it succeeds.")]
		public FsmBool finish;

		private GameObject previousGo;

		private Quaternion lastRotation;

		private Quaternion desiredRotation;

		private Vector3 lookAtPos;

		public override void Reset()
		{
			gameObject = null;
			targetObject = null;
			targetPosition2d = new FsmVector2
			{
				UseVariable = true
			};
			targetPosition = new FsmVector3
			{
				UseVariable = true
			};
			rotationOffset = null;
			useLimits = null;
			minAngle = null;
			maxAngle = null;
			debug = false;
			speed = 5f;
			finishTolerance = 1f;
			finishEvent = null;
			finish = null;
		}

		public bool HideLimits()
		{
			return !useLimits.Value;
		}

		public override void OnPreprocess()
		{
			base.Fsm.HandleLateUpdate = true;
		}

		public override void OnEnter()
		{
			previousGo = null;
		}

		public override void OnLateUpdate()
		{
			DoSmoothLookAt();
		}

		private void DoSmoothLookAt()
		{
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
			if (ownerDefaultTarget == null)
			{
				return;
			}
			Transform transform = ownerDefaultTarget.transform;
			GameObject value = targetObject.Value;
			if (previousGo != ownerDefaultTarget)
			{
				lastRotation = transform.rotation;
				desiredRotation = lastRotation;
				previousGo = ownerDefaultTarget;
			}
			if (value != null)
			{
				lookAtPos = value.transform.position;
				Vector3 zero = Vector3.zero;
				if (!targetPosition.IsNone)
				{
					zero += targetPosition.Value;
				}
				if (!targetPosition2d.IsNone)
				{
					zero.x += targetPosition2d.Value.x;
					zero.y += targetPosition2d.Value.y;
				}
				if (!targetPosition2d.IsNone || !targetPosition.IsNone)
				{
					lookAtPos += value.transform.TransformPoint(zero);
				}
			}
			else
			{
				lookAtPos = new Vector3(targetPosition2d.Value.x, targetPosition2d.Value.y, 0f);
				if (!targetPosition.IsNone)
				{
					lookAtPos += targetPosition.Value;
				}
			}
			Vector3 vector = lookAtPos - transform.position;
			vector.Normalize();
			float num = Mathf.Atan2(vector.y, vector.x) * 57.29578f;
			if (useLimits.Value)
			{
				float num2 = rotationOffset.Value + ((transform.parent != null) ? transform.parent.eulerAngles.z : 0f);
				num = ClampAngle(num, minAngle.Value + num2, maxAngle.Value + num2);
			}
			desiredRotation = Quaternion.Euler(0f, 0f, num - rotationOffset.Value);
			lastRotation = Quaternion.Slerp(lastRotation, desiredRotation, speed.Value * Time.deltaTime);
			transform.rotation = lastRotation;
			if (debug.Value)
			{
				Debug.DrawLine(transform.position, lookAtPos, Color.grey);
			}
			if (finishEvent != null || finish.Value)
			{
				if (Mathf.Abs(Vector3.Angle(lookAtPos - transform.position, transform.right) - rotationOffset.Value) <= finishTolerance.Value)
				{
					base.Fsm.Event(finishEvent);
				}
				if (finish.Value)
				{
					Finish();
				}
			}
		}

		private float ClampAngle(float angle, float min, float max)
		{
			if (angle < 90f || angle > 270f)
			{
				if (angle > 180f)
				{
					angle -= 360f;
				}
				if (max > 180f)
				{
					max -= 360f;
				}
				if (min > 180f)
				{
					min -= 360f;
				}
			}
			angle = Mathf.Clamp(angle, min, max);
			if (angle < 0f)
			{
				angle += 360f;
			}
			return angle;
		}
	}
}
