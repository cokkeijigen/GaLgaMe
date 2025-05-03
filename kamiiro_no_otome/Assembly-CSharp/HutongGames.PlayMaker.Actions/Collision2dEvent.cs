using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Physics2D)]
	[Tooltip("Detect collisions between the Owner of this FSM and other Game Objects that have RigidBody2D components. NOTE: The system events, COLLISION ENTER 2D, COLLISION STAY 2D, and COLLISION EXIT 2D are sent automatically on collisions with any object. Use this action instead to filter collisions by Tag.")]
	public class Collision2dEvent : FsmStateAction
	{
		[Tooltip("The GameObject to detect collisions on.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("The type of collision to detect.")]
		public Collision2DType collision;

		[Tooltip("If true, Collision that are not enabled will be ignored.")]
		public FsmBool ignoreDisabled;

		[UIHint(UIHint.TagMenu)]
		[Tooltip("Filter by Tag.")]
		public FsmString collideTag;

		[Tooltip("Event to send if a collision is detected.")]
		public FsmEvent sendEvent;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the GameObject that collided with the Owner of this FSM.")]
		public FsmGameObject storeCollider;

		[UIHint(UIHint.Variable)]
		[Tooltip("Store the force of the collision. NOTE: Use Get Collision 2D Info to get more info about the collision.")]
		public FsmFloat storeForce;

		private PlayMakerProxyBase cachedProxy;

		public override void Reset()
		{
			collision = Collision2DType.OnCollisionEnter2D;
			collideTag = "";
			sendEvent = null;
			storeCollider = null;
			storeForce = null;
			ignoreDisabled = false;
		}

		public override void OnPreprocess()
		{
			if (gameObject == null)
			{
				gameObject = new FsmOwnerDefault();
			}
			if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				switch (collision)
				{
				case Collision2DType.OnCollisionEnter2D:
					base.Fsm.HandleCollisionEnter2D = true;
					break;
				case Collision2DType.OnCollisionStay2D:
					base.Fsm.HandleCollisionStay2D = true;
					break;
				case Collision2DType.OnCollisionExit2D:
					base.Fsm.HandleCollisionExit2D = true;
					break;
				case Collision2DType.OnParticleCollision:
					base.Fsm.HandleParticleCollision = true;
					break;
				}
			}
			else
			{
				GetProxyComponent();
			}
		}

		public override void OnEnter()
		{
			if (gameObject.OwnerOption != 0)
			{
				if (cachedProxy == null)
				{
					GetProxyComponent();
				}
				AddCallback();
				gameObject.GameObject.OnChange += UpdateCallback;
			}
		}

		public override void OnExit()
		{
			if (gameObject.OwnerOption != 0)
			{
				RemoveCallback();
				gameObject.GameObject.OnChange -= UpdateCallback;
			}
		}

		private void UpdateCallback()
		{
			RemoveCallback();
			GetProxyComponent();
			AddCallback();
		}

		private void GetProxyComponent()
		{
			cachedProxy = null;
			GameObject value = gameObject.GameObject.Value;
			if (!(value == null))
			{
				switch (collision)
				{
				case Collision2DType.OnCollisionEnter2D:
					cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerCollisionEnter2D>(value);
					break;
				case Collision2DType.OnCollisionStay2D:
					cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerCollisionStay2D>(value);
					break;
				case Collision2DType.OnCollisionExit2D:
					cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerCollisionExit2D>(value);
					break;
				case Collision2DType.OnParticleCollision:
					cachedProxy = PlayMakerFSM.GetEventHandlerComponent<PlayMakerParticleCollision>(value);
					break;
				}
			}
		}

		private void AddCallback()
		{
			if (!(cachedProxy == null))
			{
				switch (collision)
				{
				case Collision2DType.OnCollisionEnter2D:
					cachedProxy.AddCollision2DEventCallback(CollisionEnter2D);
					break;
				case Collision2DType.OnCollisionStay2D:
					cachedProxy.AddCollision2DEventCallback(CollisionStay2D);
					break;
				case Collision2DType.OnCollisionExit2D:
					cachedProxy.AddCollision2DEventCallback(CollisionExit2D);
					break;
				case Collision2DType.OnParticleCollision:
					cachedProxy.AddParticleCollisionEventCallback(ParticleCollision);
					break;
				}
			}
		}

		private void RemoveCallback()
		{
			if (!(cachedProxy == null))
			{
				switch (collision)
				{
				case Collision2DType.OnCollisionEnter2D:
					cachedProxy.RemoveCollision2DEventCallback(CollisionEnter2D);
					break;
				case Collision2DType.OnCollisionStay2D:
					cachedProxy.RemoveCollision2DEventCallback(CollisionStay2D);
					break;
				case Collision2DType.OnCollisionExit2D:
					cachedProxy.RemoveCollision2DEventCallback(CollisionExit2D);
					break;
				case Collision2DType.OnParticleCollision:
					cachedProxy.RemoveParticleCollisionEventCallback(ParticleCollision);
					break;
				}
			}
		}

		private void StoreCollisionInfo(Collision2D collisionInfo)
		{
			storeCollider.Value = collisionInfo.gameObject;
			storeForce.Value = collisionInfo.relativeVelocity.magnitude;
		}

		public override void DoCollisionEnter2D(Collision2D collisionInfo)
		{
			if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				CollisionEnter2D(collisionInfo);
			}
		}

		public override void DoCollisionStay2D(Collision2D collisionInfo)
		{
			if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				CollisionStay2D(collisionInfo);
			}
		}

		public override void DoCollisionExit2D(Collision2D collisionInfo)
		{
			if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				CollisionExit2D(collisionInfo);
			}
		}

		public override void DoParticleCollision(GameObject other)
		{
			if (gameObject.OwnerOption == OwnerDefaultOption.UseOwner)
			{
				ParticleCollision(other);
			}
		}

		private void CollisionEnter2D(Collision2D collisionInfo)
		{
			if (collision == Collision2DType.OnCollisionEnter2D && (!ignoreDisabled.Value || collisionInfo.enabled) && FsmStateAction.TagMatches(collideTag, collisionInfo))
			{
				StoreCollisionInfo(collisionInfo);
				base.Fsm.Event(sendEvent);
			}
		}

		private void CollisionStay2D(Collision2D collisionInfo)
		{
			if (collision == Collision2DType.OnCollisionStay2D && (!ignoreDisabled.Value || collisionInfo.enabled) && FsmStateAction.TagMatches(collideTag, collisionInfo))
			{
				StoreCollisionInfo(collisionInfo);
				base.Fsm.Event(sendEvent);
			}
		}

		private void CollisionExit2D(Collision2D collisionInfo)
		{
			if (collision == Collision2DType.OnCollisionExit2D && (!ignoreDisabled.Value || collisionInfo.enabled) && FsmStateAction.TagMatches(collideTag, collisionInfo))
			{
				StoreCollisionInfo(collisionInfo);
				base.Fsm.Event(sendEvent);
			}
		}

		private void ParticleCollision(GameObject other)
		{
			if (collision == Collision2DType.OnParticleCollision && FsmStateAction.TagMatches(collideTag, other))
			{
				if (storeCollider != null)
				{
					storeCollider.Value = other;
				}
				storeForce.Value = 0f;
				base.Fsm.Event(sendEvent);
			}
		}

		public override string ErrorCheck()
		{
			return ActionHelpers.CheckPhysics2dSetup(base.Fsm.GetOwnerDefaultTarget(gameObject));
		}
	}
}
