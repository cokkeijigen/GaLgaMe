using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckInDoorCancelProcess : StateBehaviour
{
	private InDoorTalkManager inDoorTalkManager;

	public StateLink scenarioLink;

	public StateLink commandCloseLink;

	public StateLink talkBalloonCloseLink;

	public StateLink inDoorCloseLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
	}

	public override void OnStateBegin()
	{
		if (PlayerNonSaveDataManager.isRequiedUtageResume)
		{
			Transition(scenarioLink);
		}
		else if (inDoorTalkManager.commandButtonGroupGo.activeInHierarchy)
		{
			if (PlayerDataManager.currentPlaceName == "Inn" || PlayerDataManager.currentPlaceName == "Carriage" || PlayerDataManager.currentPlaceName == "ItemShop" || PlayerDataManager.currentPlaceName == "InnStreet1" || PlayerDataManager.currentPlaceName == "CityCarriage")
			{
				Transition(inDoorCloseLink);
			}
			else
			{
				Transition(commandCloseLink);
			}
		}
		else if (inDoorTalkManager.talkBalloonTailLeftGo.activeInHierarchy || inDoorTalkManager.talkBalloonTailRightGo.activeInHierarchy)
		{
			Transition(talkBalloonCloseLink);
		}
		else
		{
			Transition(inDoorCloseLink);
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
