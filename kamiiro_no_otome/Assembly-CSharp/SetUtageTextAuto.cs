using Arbor;
using UnityEngine;
using UnityEngine.UI;
using Utage;

[AddComponentMenu("")]
public class SetUtageTextAuto : StateBehaviour
{
	public AdvConfig advConfig;

	public Sprite sprite;

	public bool isEnable;

	public StateLink stateLink;

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
		if (isEnable)
		{
			PlayerNonSaveDataManager.isUtageAutoPlayWait = true;
			GetComponent<Image>().sprite = sprite;
			GetComponent<Image>().SetNativeSize();
			float time = 5f * (1f - PlayerOptionsDataManager.optionsAutoTextSpeed);
			Debug.Log("オート有効／待機時間：" + time);
			base.gameObject.GetComponent<Button>().interactable = true;
			Invoke("StartUtageAuto", time);
		}
		else
		{
			PlayerNonSaveDataManager.isUtageAutoPlayWait = false;
			GetComponent<Image>().sprite = sprite;
			GetComponent<Image>().SetNativeSize();
			advConfig.IsAutoBrPage = isEnable;
			Debug.Log("オート無効：" + advConfig.IsAutoBrPage);
			base.gameObject.GetComponent<Button>().interactable = true;
			Transition(stateLink);
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

	private void StartUtageAuto()
	{
		PlayerNonSaveDataManager.isUtageAutoPlayWait = false;
		advConfig.IsAutoBrPage = isEnable;
		Transition(stateLink);
	}
}
