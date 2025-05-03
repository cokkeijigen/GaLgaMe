namespace HutongGames.PlayMaker.Actions
{
	public abstract class QuaternionBaseAction : FsmStateAction
	{
		public enum everyFrameOptions
		{
			Update,
			FixedUpdate,
			LateUpdate
		}

		[Tooltip("Repeat every frame. Useful if any of the values are changing.")]
		public bool everyFrame;

		[Tooltip("Defines how to perform the action when 'every Frame' is enabled.")]
		public everyFrameOptions everyFrameOption;

		public override void Awake()
		{
			if (everyFrame)
			{
				switch (everyFrameOption)
				{
				case everyFrameOptions.FixedUpdate:
					base.Fsm.HandleFixedUpdate = true;
					break;
				case everyFrameOptions.LateUpdate:
					base.Fsm.HandleLateUpdate = true;
					break;
				}
			}
		}
	}
}
