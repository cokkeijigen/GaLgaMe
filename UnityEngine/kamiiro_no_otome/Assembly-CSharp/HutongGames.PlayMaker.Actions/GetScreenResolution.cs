using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("ScreenSettings")]
	[Tooltip("Gets the current screen resolution.")]
	public class GetScreenResolution : FsmStateAction
	{
		[RequiredField]
		public FsmFloat storeScreenWidth;

		[RequiredField]
		public FsmFloat storeScreenHeight;

		public override void Reset()
		{
			storeScreenWidth = null;
			storeScreenHeight = null;
		}

		public override void OnEnter()
		{
			storeScreenWidth.Value = Screen.currentResolution.width;
			storeScreenHeight.Value = Screen.currentResolution.height;
			Debug.Log("横" + storeScreenWidth.Value);
			Debug.Log("縦" + storeScreenHeight.Value);
			Finish();
		}
	}
}
