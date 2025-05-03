using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class CheckHeroineIsContract : StateBehaviour
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
		if (inDoorTalkManager.isFollowRequest)
		{
			ParameterContainer component = inDoorCommandManager.inDoorDialogGroupGo.GetComponent<ParameterContainer>();
			int heroineID = inDoorTalkManager.commandTalkOriginGo.GetComponent<ParameterContainer>().GetInt("sortID");
			int num = 0;
			string characterPowerUpFlag = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == heroineID).characterPowerUpFlag;
			if (PlayerFlagDataManager.scenarioFlagDictionary[characterPowerUpFlag])
			{
				Transition(stateLink);
				return;
			}
			if (PlayerDataManager.currentTimeZone == 0)
			{
				Transition(stateLink);
				return;
			}
			component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = "character" + heroineID;
			component.GetVariable<I2LocalizeComponent>("followTextLoc").localize.Term = "alertHeroineFollowRequestDisable";
			component.GetVariable<I2LocalizeComponent>("secondLineTextLoc").localize.Term = "alertHeroineFollowOnlyOne";
			if (heroineID == 2)
			{
				int index = 0;
				if (PlayerDataManager.currentAccessPointName == "Kingdom1")
				{
					inDoorTalkManager.talkBalloonTailRightTerm.Term = "talk_FollowRequestDisable_" + heroineID + 1;
					index = 1;
				}
				else
				{
					inDoorTalkManager.talkBalloonTailRightTerm.Term = "talk_FollowRequestDisable_" + heroineID + 0;
				}
				Sprite sprite = inDoorTalkManager.inDoorHeroineFollowSpriteDataBase.inDoorHeroineFollowSpriteList.Find((InDoorHeroineFollowSpriteData data) => data.sortID == heroineID).followOnlyMorningSpriteList[index];
				inDoorTalkManager.positionTalkImageArray[0].GetComponent<Image>().sprite = sprite;
				if (PlayerDataManager.isDungeonHeroineFollow)
				{
					component.GetGameObject("secondLineTextGo").SetActive(value: true);
					num = Random.Range(0, 2);
					inDoorTalkManager.talkBalloonHeroineTerm.Term = "talk_FollowCancel_" + PlayerDataManager.DungeonHeroineFollowNum + num;
				}
				else
				{
					component.GetGameObject("secondLineTextGo").SetActive(value: false);
				}
			}
			else
			{
				inDoorTalkManager.talkBalloonTailRightTerm.Term = "talk_FollowRequestDisable_" + heroineID + 0;
				Sprite sprite2 = inDoorTalkManager.inDoorHeroineFollowSpriteDataBase.inDoorHeroineFollowSpriteList.Find((InDoorHeroineFollowSpriteData data) => data.sortID == heroineID).followOnlyMorningSpriteList[0];
				inDoorTalkManager.positionTalkImageArray[0].GetComponent<Image>().sprite = sprite2;
				if (PlayerDataManager.isDungeonHeroineFollow)
				{
					component.GetGameObject("secondLineTextGo").SetActive(value: true);
					num = Random.Range(0, 2);
					inDoorTalkManager.talkBalloonHeroineTerm.Term = "talk_FollowCancel_" + PlayerDataManager.DungeonHeroineFollowNum + num;
				}
				else
				{
					component.GetGameObject("secondLineTextGo").SetActive(value: false);
				}
			}
			inDoorCommandManager.followRequestButtonGrouopGo.SetActive(value: false);
			inDoorCommandManager.followCancelButtonGrouopGo.SetActive(value: false);
			inDoorCommandManager.followDisableButtonGrouopGo.SetActive(value: true);
			inDoorTalkManager.inDoorCanvasGo.GetComponent<CanvasGroup>().interactable = false;
			inDoorTalkManager.commandButtonGroupGo.GetComponent<CanvasGroup>().interactable = false;
			inDoorTalkManager.commandButtonGroupGo.GetComponent<CanvasGroup>().alpha = 0.5f;
			inDoorCommandManager.inDoorDialogGroupGo.SetActive(value: true);
			component.GetGameObject("cancelButtonGo").SetActive(value: true);
			inDoorCommandManager.dialogType = InDoorCommandManager.DialogType.select;
		}
		else
		{
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
}
