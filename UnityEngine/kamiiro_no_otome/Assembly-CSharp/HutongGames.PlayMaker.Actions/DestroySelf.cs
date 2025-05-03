using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GameObject)]
	[Tooltip("Destroys the Owner of the FSM! Useful for spawned Prefabs that need to kill themselves, e.g., a projectile that explodes on impact.")]
	public class DestroySelf : FsmStateAction
	{
		[Tooltip("Seconds to wait before destroying the owner. Can be useful to let other actions finish first. E.g. a fade-out effect.\nNOTE: If you use a delay and the FSM switches away from this state before it is finished, the owner will not be destroyed!")]
		public FsmFloat delay;

		[Tooltip("Ignore any time scaling.")]
		public FsmBool realTime;

		[Tooltip("Detach children before destroying the Owner. This allows children to survive, useful to allow FX to fade out etc.")]
		public FsmBool detachChildren;

		private float elapsedTime;

		public override void Reset()
		{
			delay = null;
			realTime = null;
			detachChildren = new FsmBool
			{
				Value = false
			};
		}

		public override void OnEnter()
		{
			if (delay.Value < 0.001f)
			{
				DoDestroySelf();
				Finish();
			}
			elapsedTime = 0f;
		}

		public override void OnUpdate()
		{
			elapsedTime += (realTime.Value ? Time.unscaledDeltaTime : Time.deltaTime);
			if (elapsedTime > delay.Value)
			{
				DoDestroySelf();
				Finish();
			}
		}

		private void DoDestroySelf()
		{
			if (base.Owner != null)
			{
				if (detachChildren.Value)
				{
					base.Owner.transform.DetachChildren();
				}
				Object.Destroy(base.Owner);
			}
		}
	}
}
