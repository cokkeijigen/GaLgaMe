using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckInDoorLocationCheckFinish : StateBehaviour
{
	private InDoorTalkManager inDoorTalkManager;

	public StateLink finishLink;

	public StateLink inCompleteLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
	}

	public override void OnStateBegin()
	{
		int currentInDoorLocationCheckCount = inDoorTalkManager.currentInDoorLocationCheckCount;
		int count = inDoorTalkManager.currentInDoorLocationDataList.Count;
		if (currentInDoorLocationCheckCount >= count)
		{
			Debug.Log("立ち絵表示を完了");
			Transition(finishLink);
		}
		else
		{
			Debug.Log("立ち絵表示は未完了");
			Transition(inCompleteLink);
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
