using UnityEngine;

public class DebugTotalMap : MonoBehaviour
{
	public CanvasGroup localMapCanvasGroup;

	private bool isLog;

	private void Start()
	{
	}

	private void Update()
	{
		if (localMapCanvasGroup.alpha < 1f && !isLog)
		{
			isLog = true;
			Debug.LogWarning("ローカルマップのアルファ0.5");
		}
		else if (localMapCanvasGroup.alpha == 1f)
		{
			isLog = false;
		}
	}
}
