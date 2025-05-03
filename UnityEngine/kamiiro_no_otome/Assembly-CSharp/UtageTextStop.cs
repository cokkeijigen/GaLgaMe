using Arbor;
using UnityEngine;
using Utage;

[AddComponentMenu("")]
public class UtageTextStop : StateBehaviour
{
	public bool isEnable;

	public AdvUguiManager advUguiManager;

	private AdvConfig advConfig;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (advUguiManager == null)
		{
			advUguiManager = GameObject.Find("AdvEngine").transform.Find("UI").GetComponent<AdvUguiManager>();
		}
		if (advConfig == null)
		{
			advConfig = GameObject.Find("AdvEngine").GetComponent<AdvConfig>();
		}
		advUguiManager.enabled = isEnable;
		Debug.Log("宴の文字送り：" + isEnable + "／オート状態：" + advConfig.IsAutoBrPage);
		Transition(stateLink);
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
	}

	public override void OnStateLateUpdate()
	{
	}
}
