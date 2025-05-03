using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class RefreshInDoorBgSprite : StateBehaviour
{
	private InDoorTalkManager inDoorTalkManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
	}

	public override void OnStateBegin()
	{
	}

	public override void OnStateEnd()
	{
		PlayerNonSaveDataManager.isAddTimeEnd = false;
	}

	public override void OnStateUpdate()
	{
		if (PlayerNonSaveDataManager.isAddTimeEnd)
		{
			Sprite sprite = inDoorTalkManager.localMapUnlockDataBase.localMapUnlockDataList.Find((LocalMapUnlockData data) => data.currentPlaceName == PlayerDataManager.currentPlaceName).inDoorBgSpriteArray[PlayerDataManager.currentTimeZone];
			inDoorTalkManager.inDoorBgImage.sprite = sprite;
			inDoorTalkManager.talkFSM.SendTrigger("RefreshInDoorAfterRest");
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
