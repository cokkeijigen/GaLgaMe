using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ScreenSettings")]
	[Tooltip("Toggle between fullscreen and windowed mode.")]
	public class ToggleFullScreen : FsmStateAction
	{
		public override void OnEnter()
		{
			Screen.fullScreen = !Screen.fullScreen;
			Finish();
		}
	}
}
