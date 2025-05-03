using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class InitializeInDoorPrivateLocation : StateBehaviour
{
	private InDoorTalkManager inDoorTalkManager;

	private ParameterContainer parameterContainer;

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
		Vector2 vector = new Vector2(0f, 0f);
		foreach (KeyValuePair<string, GameObject> item in inDoorTalkManager.commandButtonDictionary)
		{
			item.Value.SetActive(value: false);
		}
		inDoorTalkManager.commandButtonGroupGo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 20f);
		inDoorTalkManager.FullScreenTextButton.interactable = false;
		if (PlayerDataManager.currentPlaceName == "Inn" || PlayerDataManager.currentPlaceName == "InnStreet1")
		{
			GameObject gameObject = null;
			bool value = false;
			string currentPlaceName = PlayerDataManager.currentPlaceName;
			if (!(currentPlaceName == "Inn"))
			{
				if (currentPlaceName == "InnStreet1")
				{
					gameObject = GameObject.Find("Local Area/City1/InnStreet1");
				}
			}
			else
			{
				gameObject = GameObject.Find("Local Area/Kingdom1/Inn");
			}
			if (gameObject != null)
			{
				parameterContainer = gameObject.GetComponent<ParameterContainer>();
				parameterContainer.TryGetBool("isHeroineExist", out value);
			}
			if (PlayerDataManager.isDungeonHeroineFollow)
			{
				inDoorTalkManager.talkBalloonTailRightGo.SetActive(value: true);
				CharacterStatusData characterStatusData = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == PlayerDataManager.DungeonHeroineFollowNum);
				if (PlayerFlagDataManager.scenarioFlagDictionary[characterStatusData.characterSexTouchUnLockFlag])
				{
					inDoorTalkManager.commandButtonDictionary["Survey"].SetActive(value: true);
				}
				else
				{
					inDoorTalkManager.commandButtonDictionary["Survey"].SetActive(value: false);
				}
				inDoorTalkManager.commandButtonDictionary["Follow"].SetActive(value: true);
				inDoorTalkManager.commandFollowButtonLoc.Term = "buttonFollowCancel";
				inDoorTalkManager.commandFollowButtonImage.sprite = inDoorTalkManager.commandFollowButtonSpriteArray[1];
				inDoorTalkManager.isFollowRequest = false;
				CanvasGroup component = inDoorTalkManager.commandButtonDictionary["Follow"].GetComponent<CanvasGroup>();
				if (PlayerDataManager.isHeroineSpecifyFollow && PlayerDataManager.heroineSpecifyFollowPoint == PlayerDataManager.currentAccessPointName)
				{
					component.interactable = false;
					component.alpha = 0.5f;
					Debug.Log("同行ボタン使用不可");
				}
				else
				{
					component.interactable = true;
					component.alpha = 1f;
				}
				inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>().SetInt("sortID", PlayerDataManager.DungeonHeroineFollowNum);
				inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>().GetStringList("scenarioNameList")[0] = PlayerDataManager.currentAccessPointName + "_Survey";
			}
			else if (value)
			{
				if (parameterContainer.GetBool("isHeroineExist"))
				{
					inDoorTalkManager.talkBalloonTailRightGo.SetActive(value: true);
					int heroineNum = parameterContainer.GetInt("heroineNum");
					CharacterStatusData characterStatusData2 = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == heroineNum);
					if (PlayerFlagDataManager.scenarioFlagDictionary[characterStatusData2.characterSexTouchUnLockFlag])
					{
						inDoorTalkManager.commandButtonDictionary["Survey"].SetActive(value: true);
					}
					else
					{
						inDoorTalkManager.commandButtonDictionary["Survey"].SetActive(value: false);
					}
					inDoorTalkManager.commandButtonDictionary["Follow"].SetActive(value: true);
					inDoorTalkManager.commandFollowButtonLoc.Term = "buttonFollowRequest";
					inDoorTalkManager.commandFollowButtonImage.sprite = inDoorTalkManager.commandFollowButtonSpriteArray[1];
					inDoorTalkManager.isFollowRequest = true;
					CanvasGroup component2 = inDoorTalkManager.commandButtonDictionary["Follow"].GetComponent<CanvasGroup>();
					if (PlayerDataManager.isHeroineSpecifyFollow && PlayerDataManager.heroineSpecifyFollowPoint == PlayerDataManager.currentAccessPointName)
					{
						component2.interactable = false;
						component2.alpha = 0.5f;
						Debug.Log("同行ボタン使用不可");
					}
					else
					{
						component2.interactable = true;
						component2.alpha = 1f;
					}
					inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>().SetInt("sortID", heroineNum);
					inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>().GetStringList("scenarioNameList")[0] = PlayerDataManager.currentAccessPointName + "_Survey";
				}
				else
				{
					inDoorTalkManager.positionTalkImageArray[0].SetActive(value: false);
					inDoorTalkManager.talkBalloonTailRightGo.SetActive(value: false);
				}
			}
			else
			{
				inDoorTalkManager.positionTalkImageArray[0].SetActive(value: false);
				inDoorTalkManager.talkBalloonTailRightGo.SetActive(value: false);
			}
			inDoorTalkManager.commandButtonDictionary["Rest"].SetActive(value: true);
			CanvasGroup component3 = inDoorTalkManager.commandButtonDictionary["Rest"].GetComponent<CanvasGroup>();
			if (PlayerDataManager.isLocalMapActionLimit)
			{
				component3.interactable = false;
				component3.alpha = 0.5f;
			}
			else
			{
				component3.interactable = true;
				component3.alpha = 1f;
			}
			vector = inDoorTalkManager.inDoorCharacterCgDataBase.inDoorCharacterCgDataList.Find((InDoorCharacterCgData data) => data.sortID == PlayerDataManager.DungeonHeroineFollowNum).heroineInnBalloonV2;
		}
		else if (PlayerDataManager.currentPlaceName == "Carriage" || PlayerDataManager.currentPlaceName == "CityCarriage")
		{
			inDoorTalkManager.commandButtonDictionary["Blacksmith"].SetActive(value: true);
			inDoorTalkManager.commandButtonDictionary["Recovery"].SetActive(value: true);
			inDoorTalkManager.commandButtonDictionary["Extension"].SetActive(value: true);
			GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>().clockCanvasGroup.gameObject.SetActive(value: false);
			vector = inDoorTalkManager.inDoorCharacterCgDataBase.inDoorCharacterCgDataList.Find((InDoorCharacterCgData data) => data.sortID == 0).talkBalloonV2;
			inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>().SetInt("sortID", 0);
		}
		else
		{
			inDoorTalkManager.talkBalloonTailRightGo.SetActive(value: true);
			inDoorTalkManager.commandButtonDictionary["Buy"].SetActive(value: true);
			vector = inDoorTalkManager.inDoorCharacterCgDataBase.inDoorCharacterCgDataList.Find((InDoorCharacterCgData data) => data.characterName == "シャーロ").talkBalloonV2;
		}
		inDoorTalkManager.talkBalloonTailRightGo.GetComponent<RectTransform>().localPosition = vector;
		inDoorTalkManager.talkBalloonHeroineGo.SetActive(value: false);
		inDoorTalkManager.talkBalloonTailLeftGo.SetActive(value: false);
		inDoorTalkManager.commandButtonGroupGo.SetActive(value: true);
		SetTalkBalloonTerm();
		inDoorTalkManager.commandTalkOriginGo = inDoorTalkManager.positionTalkImageArray[0];
		inDoorTalkManager.clickTargetGo = inDoorTalkManager.positionTalkImageArray[0];
		inDoorTalkManager.clickSummaryGo.SetActive(value: false);
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

	private void SetTalkBalloonTerm()
	{
		string term = "";
		InDoorCharacterTalkData inDoorCharacterTalkData = null;
		if (PlayerDataManager.currentPlaceName == "Inn" || PlayerDataManager.currentPlaceName == "InnStreet1")
		{
			if (PlayerDataManager.isDungeonHeroineFollow)
			{
				List<InDoorCharacterTalkData> list = inDoorTalkManager.inDoorTalkDataBase.inDoorCharacterTalkDataList.Where((InDoorCharacterTalkData data) => data.sortID > 1000).ToList();
				ParameterContainer param = inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>();
				inDoorCharacterTalkData = list.Find((InDoorCharacterTalkData data) => data.accessPointName == PlayerDataManager.currentAccessPointName && data.placeName == PlayerDataManager.currentPlaceName && data.characterName == param.GetString("characterName"));
				term = GetTalkTerm(inDoorCharacterTalkData);
			}
			else if (PlayerDataManager.currentPlaceName == "InnStreet1")
			{
				List<InDoorCharacterTalkData> list2 = inDoorTalkManager.inDoorTalkDataBase.inDoorCharacterTalkDataList.Where((InDoorCharacterTalkData data) => data.sortID < 1000).ToList();
				if (GameObject.Find("InnStreet1").GetComponent<ParameterContainer>().GetBool("isHeroineExist"))
				{
					ParameterContainer param2 = inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>();
					inDoorCharacterTalkData = list2.Find((InDoorCharacterTalkData data) => data.accessPointName == PlayerDataManager.currentAccessPointName && data.placeName == PlayerDataManager.currentPlaceName && data.characterName == param2.GetString("characterName"));
					term = GetTalkTerm(inDoorCharacterTalkData);
				}
			}
		}
		else if (!(PlayerDataManager.currentPlaceName == "Carriage") && !(PlayerDataManager.currentPlaceName == "CityCarriage"))
		{
			List<InDoorCharacterTalkData> list3 = inDoorTalkManager.inDoorTalkDataBase.inDoorCharacterTalkDataList.Where((InDoorCharacterTalkData data) => data.sortID < 1000).ToList();
			ParameterContainer param3 = inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>();
			inDoorCharacterTalkData = list3.Find((InDoorCharacterTalkData data) => data.accessPointName == PlayerDataManager.currentAccessPointName && data.placeName == PlayerDataManager.currentPlaceName && data.characterName == param3.GetString("characterName"));
			term = GetTalkTerm(inDoorCharacterTalkData);
		}
		inDoorTalkManager.talkBalloonTailRightTerm.Term = term;
	}

	private string GetTalkTerm(InDoorCharacterTalkData inDoorCharacterTalkData)
	{
		string result = "";
		for (int num = inDoorCharacterTalkData.talkSectionFlagNameList.Count; num > 0; num--)
		{
			if (PlayerFlagDataManager.scenarioFlagDictionary[inDoorCharacterTalkData.talkSectionFlagNameList[num - 1]])
			{
				result = inDoorCharacterTalkData.talkSectionTermList[num - 1];
				break;
			}
		}
		return result;
	}
}
