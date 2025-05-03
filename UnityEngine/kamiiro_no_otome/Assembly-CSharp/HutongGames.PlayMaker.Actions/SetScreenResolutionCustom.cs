using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ScreenSettings")]
	[Tooltip("Sets the screen resolution.\nOptionally sets the Refresh Rate.")]
	public class SetScreenResolutionCustom : FsmStateAction
	{
		[RequiredField]
		public FsmFloat screenWidth;

		[RequiredField]
		public FsmFloat screenHeight;

		public bool fullscreen;

		public FsmInt refreshRate;

		public override void Reset()
		{
			screenWidth = 800f;
			screenHeight = 600f;
			fullscreen = false;
			refreshRate = 0;
		}

		public override void OnEnter()
		{
			int num = 0;
			num = Convert.ToInt32(screenWidth.Value);
			int num2 = 0;
			num2 = Convert.ToInt32(screenHeight.Value);
			Screen.SetResolution(num, num2, fullscreen, refreshRate.Value);
			Finish();
			Debug.Log("横" + num);
			Debug.Log("縦" + num2);
		}
	}
}
