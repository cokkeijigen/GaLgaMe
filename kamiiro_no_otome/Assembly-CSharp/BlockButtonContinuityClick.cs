using System.Collections;
using Arbor;
using Sirenix.OdinInspector;
using UnityEngine;

public class BlockButtonContinuityClick : SerializedMonoBehaviour
{
	[SerializeField]
	private float blockTime;

	[SerializeField]
	private string sendArborTriggerName;

	[SerializeField]
	private ArborFSM targetArborFSM;

	[SerializeField]
	private string sendPlayMakerEventName;

	[SerializeField]
	private PlayMakerFSM targetPlayMakerFSM;

	[SerializeField]
	private bool buttonDisabled;

	private void OnEnable()
	{
		buttonDisabled = false;
	}

	public void ClickButton()
	{
		if (!buttonDisabled)
		{
			if (!string.IsNullOrEmpty(sendArborTriggerName))
			{
				targetArborFSM.SendTrigger(sendArborTriggerName);
			}
			else if (!string.IsNullOrEmpty(sendPlayMakerEventName))
			{
				targetPlayMakerFSM.SendEvent(sendPlayMakerEventName);
			}
			buttonDisabled = true;
			StartCoroutine(EnableButton());
		}
	}

	private IEnumerator EnableButton()
	{
		yield return new WaitForSeconds(blockTime);
		buttonDisabled = false;
	}
}
