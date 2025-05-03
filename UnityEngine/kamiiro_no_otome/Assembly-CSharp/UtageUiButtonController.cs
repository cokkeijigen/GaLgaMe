using UnityEngine;
using Utage;

public class UtageUiButtonController : MonoBehaviour
{
	private AdvConfig advConfig;

	private void Start()
	{
		advConfig = GameObject.Find("AdvEngine").GetComponent<AdvConfig>();
	}

	public bool CheckIsUtageAutoPlaying()
	{
		if (advConfig == null)
		{
			advConfig = GameObject.Find("AdvEngine").GetComponent<AdvConfig>();
		}
		return advConfig.IsAutoBrPage;
	}
}
