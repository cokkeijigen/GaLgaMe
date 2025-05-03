using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ScreenSettings")]
	[Tooltip("Sets the screen resolution.\nOptionally sets the Refresh Rate.")]
	public class GameFullScreenMode : FsmStateAction
	{
		[RequiredField]
		public bool fullscreen;

		public override void Reset()
		{
			fullscreen = false;
		}

		public override void OnEnter()
		{
			if (fullscreen)
			{
				if (!Screen.fullScreen)
				{
					Screen.fullScreen = !Screen.fullScreen;
				}
				Debug.Log("フルスクリーン表示開始");
			}
			else
			{
				if (Screen.fullScreen)
				{
					Screen.fullScreen = !Screen.fullScreen;
				}
				Debug.Log("ウィンドウ表示開始");
			}
			Finish();
		}
	}
}
