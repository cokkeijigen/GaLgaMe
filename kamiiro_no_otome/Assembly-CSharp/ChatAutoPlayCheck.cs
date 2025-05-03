using System.Collections;
using Arbor;
using UnityEngine;
using Utage;

[AddComponentMenu("")]
public class ChatAutoPlayCheck : StateBehaviour
{
	private ChatWindowControl chatWindowControl;

	public AdvUguiManager advUguiManager;

	public float waitTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		chatWindowControl = GetComponent<ChatWindowControl>();
	}

	public override void OnStateBegin()
	{
		int enableTextCharCount = chatWindowControl.enableTextCharCount;
		float num = 5f * (1f - PlayerOptionsDataManager.optionsAutoTextSpeed * 1.8f);
		waitTime = (float)enableTextCharCount * 0.1f * (1f - PlayerOptionsDataManager.optionsAutoTextSpeed * 2f);
		num *= (float)chatWindowControl.enableListCount;
		waitTime = Mathf.Clamp(waitTime, num, 5f * (1f - PlayerOptionsDataManager.optionsAutoTextSpeed * 1.8f));
		Debug.Log("オート待ち時間：" + waitTime + "／待ち時間最小：" + num + "／オートテキスト速度：" + PlayerOptionsDataManager.optionsAutoTextSpeed * 1.8f);
		chatWindowControl.fullBacklogButton.interactable = true;
		StartCoroutine("AutoTransition");
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

	private IEnumerator AutoTransition()
	{
		while (waitTime > 0f)
		{
			if (chatWindowControl.isBackLogVisible)
			{
				yield return new WaitForSeconds(0.1f);
			}
			else if (chatWindowControl.advEngine.Config.IsAutoBrPage && advUguiManager.enabled)
			{
				waitTime -= 0.1f;
				yield return new WaitForSeconds(0.1f);
			}
			else
			{
				yield return null;
			}
		}
		Transition(stateLink);
		Debug.Log("オートトランジション");
	}
}
