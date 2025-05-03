using Arbor;
using UnityEngine;
using UnityEngine.UI;
using Utage;

[AddComponentMenu("")]
public class CheckUtageTextAuto : StateBehaviour
{
	public AdvConfig advConfig;

	public StateLink trueLink;

	public StateLink falseLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		if (advConfig == null)
		{
			advConfig = GameObject.Find("AdvEngine").GetComponent<AdvConfig>();
		}
	}

	public override void OnStateBegin()
	{
		base.gameObject.GetComponent<Button>().interactable = false;
		if (advConfig.IsAutoBrPage)
		{
			Debug.Log("オートを停止するに遷移：コンフィグ／" + advConfig.IsAutoBrPage);
			Transition(trueLink);
		}
		else if (PlayerNonSaveDataManager.isUtageAutoPlayWait)
		{
			Debug.Log("オートを停止するに遷移：Wait／" + advConfig.IsAutoBrPage);
			Transition(trueLink);
		}
		else
		{
			Transition(falseLink);
		}
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
