using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class StartInDoorFollowRequest : StateBehaviour
{
	private InDoorTalkManager inDoorTalkManager;

	private InDoorClickManager inDoorClickManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
		inDoorClickManager = GameObject.Find("InDoor Click Manager").GetComponent<InDoorClickManager>();
	}

	public override void OnStateBegin()
	{
		int @int = inDoorTalkManager.commandTalkOriginGo.GetComponent<ParameterContainer>().GetInt("sortID");
		PlayerNonSaveDataManager.isHeroineUnFollowRightClickBlock = true;
		PlayerDataManager.isDungeonHeroineFollow = true;
		PlayerDataManager.DungeonHeroineFollowNum = @int;
		MasterAudio.PlaySound("SeApplyButton", 1f, null, 0f, null, null);
		inDoorTalkManager.isFollowRequestApply = true;
		if (PlayerDataManager.currentPlaceName == "Inn" || PlayerDataManager.currentPlaceName == "InnStreet1")
		{
			Debug.Log("フォロー依頼／宿屋にいる");
			inDoorTalkManager.commandFollowButtonLoc.Term = "buttonFollowCancel";
			inDoorTalkManager.commandFollowButtonImage.sprite = inDoorTalkManager.commandFollowButtonSpriteArray[1];
			inDoorTalkManager.isFollowRequest = false;
		}
		else
		{
			Debug.Log("フォロー依頼／宿屋ではない");
			ParameterContainer component = inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>();
			component.GetGameObject("clickOriginGo").SetActive(value: false);
			component.GetGameObject("alertGroupGo").SetActive(value: false);
			switch (component.GetString("positionName"))
			{
			case "近_左":
			case "近_右":
				inDoorTalkManager.nearAlertGroupGo.SetActive(value: false);
				break;
			case "中_左":
			case "中_右":
				inDoorTalkManager.middleAlertGroupGo.SetActive(value: false);
				break;
			case "奥_左":
			case "奥_右":
				inDoorTalkManager.farAlertGroupGo.SetActive(value: false);
				break;
			}
			inDoorTalkManager.positionTalkImageArray[0].SetActive(value: false);
			inDoorTalkManager.talkAlertGroupGo.SetActive(value: false);
			inDoorTalkManager.talkBalloonTailRightGo.SetActive(value: false);
			inDoorClickManager.CloseInDoorCommandTalk();
			Sprite characterFollowSprite = inDoorTalkManager.inDoorCharacterCgDataBase.inDoorCharacterCgDataList.Find((InDoorCharacterCgData data) => data.sortID == PlayerDataManager.DungeonHeroineFollowNum).characterFollowSprite;
			inDoorTalkManager.positionTalkImageArray[1].GetComponent<Image>().sprite = characterFollowSprite;
			inDoorTalkManager.positionTalkImageArray[1].SetActive(value: true);
			InDoorCharacterCgData inDoorCharacterCgData = inDoorTalkManager.inDoorCharacterCgDataBase.inDoorCharacterCgDataList.Find((InDoorCharacterCgData data) => data.sortID == PlayerDataManager.DungeonHeroineFollowNum);
			inDoorTalkManager.positionTalkImageArray[1].GetComponent<RectTransform>().localPosition = inDoorCharacterCgData.heroinePositionV2;
			ParameterContainer component2 = inDoorTalkManager.positionTalkImageArray[1].GetComponent<ParameterContainer>();
			component2.SetString("characterName", inDoorCharacterCgData.characterName);
			component2.SetInt("sortID", @int);
			component2.SetVector2("balloonPosition", inDoorCharacterCgData.heroineBalloonV2);
			MapHeroineUnFollowManager component3 = GameObject.Find("Map Rest Manager").GetComponent<MapHeroineUnFollowManager>();
			if (component3 != null)
			{
				component3.RefreshUnFollowButtonVisible();
			}
		}
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
