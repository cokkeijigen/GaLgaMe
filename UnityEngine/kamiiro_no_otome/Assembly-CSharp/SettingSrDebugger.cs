using UnityEngine;

public class SettingSrDebugger : MonoBehaviour
{
	private Canvas srDebuggerCanvas;

	private Canvas consoleCanvas;

	private Canvas optionCanvas;

	public void SetSrDebugCanvasSortOrder(int num)
	{
		if (srDebuggerCanvas == null)
		{
			GameObject gameObject = GameObject.Find("SR_Canvas");
			srDebuggerCanvas = gameObject.GetComponent<Canvas>();
			optionCanvas = gameObject.transform.Find("SR_Content/SR_Main/SR_Tab/Options(Clone)").GetComponent<Canvas>();
			consoleCanvas = gameObject.transform.Find("SR_Content/SR_Main/SR_Tab/Console(Clone)").GetComponent<Canvas>();
		}
		srDebuggerCanvas.sortingOrder = num;
		consoleCanvas.sortingOrder = num + 1;
		optionCanvas.sortingOrder = num + 1;
	}
}
