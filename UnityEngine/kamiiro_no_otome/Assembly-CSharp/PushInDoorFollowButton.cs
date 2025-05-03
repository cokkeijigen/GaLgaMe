using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class PushInDoorFollowButton : StateBehaviour
{
	private InDoorTalkManager inDoorTalkManager;

	private InDoorCommandManager inDoorCommandManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
		inDoorCommandManager = GameObject.Find("InDoor Command Manager").GetComponent<InDoorCommandManager>();
	}

	public override void OnStateBegin()
	{
		ParameterContainer component = inDoorCommandManager.inDoorDialogGroupGo.GetComponent<ParameterContainer>();
		int heroineID = inDoorTalkManager.commandTalkOriginGo.GetComponent<ParameterContainer>().GetInt("sortID");
		int index = 0;
		if (inDoorTalkManager.isFollowRequest)
		{
			Debug.Log("フォローの依頼");
			component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = "character" + heroineID;
			component.GetVariable<I2LocalizeComponent>("followTextLoc").localize.Term = "alertHeroineFollowRequest";
			component.GetVariable<I2LocalizeComponent>("secondLineTextLoc").localize.Term = "alertHeroineFollowOnlyOne";
			switch (heroineID)
			{
			case 2:
				index = (PlayerFlagDataManager.scenarioFlagDictionary["MH_Rina_014"] ? Random.Range(1, 3) : 0);
				break;
			case 3:
				index = (PlayerFlagDataManager.scenarioFlagDictionary["MH_Shia_011"] ? Random.Range(1, 3) : 0);
				break;
			case 4:
				index = Random.Range(0, 2);
				break;
			}
			Sprite sprite = inDoorTalkManager.inDoorHeroineFollowSpriteDataBase.inDoorHeroineFollowSpriteList.Find((InDoorHeroineFollowSpriteData data) => data.sortID == heroineID).followRequestSpriteList[index];
			inDoorTalkManager.positionTalkImageArray[0].GetComponent<Image>().sprite = sprite;
			inDoorTalkManager.talkBalloonTailRightTerm.Term = "talk_FollowRequest_" + heroineID + index;
			if (PlayerDataManager.isDungeonHeroineFollow)
			{
				component.GetGameObject("secondLineTextGo").SetActive(value: true);
				index = Random.Range(0, 2);
				inDoorTalkManager.talkBalloonHeroineTerm.Term = "talk_FollowCancel_" + PlayerDataManager.DungeonHeroineFollowNum + index;
			}
			else
			{
				component.GetGameObject("secondLineTextGo").SetActive(value: false);
			}
			inDoorCommandManager.followRequestButtonGrouopGo.SetActive(value: true);
			inDoorCommandManager.followCancelButtonGrouopGo.SetActive(value: false);
			inDoorCommandManager.followDisableButtonGrouopGo.SetActive(value: false);
		}
		else
		{
			Debug.Log("フォローの解除");
			component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = "character" + PlayerDataManager.DungeonHeroineFollowNum;
			component.GetVariable<I2LocalizeComponent>("followTextLoc").localize.Term = "alertHeroineFollowCancel";
			component.GetGameObject("secondLineTextGo").SetActive(value: false);
			index = Random.Range(0, 2);
			Sprite sprite2 = inDoorTalkManager.inDoorHeroineFollowSpriteDataBase.inDoorHeroineFollowSpriteList.Find((InDoorHeroineFollowSpriteData data) => data.sortID == heroineID).followRemoveSpriteList[index];
			if (PlayerDataManager.currentPlaceName == "Inn" || PlayerDataManager.currentPlaceName == "InnStreet1")
			{
				inDoorTalkManager.talkBalloonTailRightTerm.Term = "talk_FollowCancel_" + PlayerDataManager.DungeonHeroineFollowNum + index;
				inDoorTalkManager.positionTalkImageArray[0].GetComponent<Image>().sprite = sprite2;
			}
			else
			{
				inDoorTalkManager.talkBalloonHeroineTerm.Term = "talk_FollowCancel_" + PlayerDataManager.DungeonHeroineFollowNum + index;
				inDoorTalkManager.positionTalkImageArray[1].GetComponent<Image>().sprite = sprite2;
			}
			inDoorCommandManager.followRequestButtonGrouopGo.SetActive(value: false);
			inDoorCommandManager.followCancelButtonGrouopGo.SetActive(value: true);
			inDoorCommandManager.followDisableButtonGrouopGo.SetActive(value: false);
		}
		inDoorTalkManager.inDoorCanvasGo.GetComponent<CanvasGroup>().interactable = false;
		inDoorTalkManager.commandButtonGroupGo.GetComponent<CanvasGroup>().interactable = false;
		inDoorTalkManager.commandButtonGroupGo.GetComponent<CanvasGroup>().alpha = 0.5f;
		inDoorCommandManager.inDoorDialogGroupGo.SetActive(value: true);
		component.GetGameObject("cancelButtonGo").SetActive(value: true);
		inDoorCommandManager.dialogType = InDoorCommandManager.DialogType.select;
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
