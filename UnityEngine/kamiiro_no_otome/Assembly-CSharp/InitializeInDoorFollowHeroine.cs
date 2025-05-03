using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class InitializeInDoorFollowHeroine : StateBehaviour
{
	private InDoorTalkManager inDoorTalkManager;

	private bool isPrivatePlace;

	public StateLink privateLink;

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
		if (GameObject.Find("TotalMap Access Manager") != null)
		{
			GameObject.Find("Map Rest Manager").GetComponent<MapHeroineUnFollowManager>().RefreshUnFollowButtonVisible();
		}
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			InDoorCharacterCgData inDoorCharacterCgData = inDoorTalkManager.inDoorCharacterCgDataBase.inDoorCharacterCgDataList.Find((InDoorCharacterCgData data) => data.sortID == PlayerDataManager.DungeonHeroineFollowNum);
			if (PlayerDataManager.currentPlaceName == "Inn" || PlayerDataManager.currentPlaceName == "InnStreet1")
			{
				isPrivatePlace = true;
				int index = PlayerDataManager.DungeonHeroineFollowNum - 2;
				Debug.Log("ヒロインIndex：" + index + "／プレース名：" + PlayerDataManager.currentPlaceName);
				Sprite sprite = inDoorTalkManager.inDoorHeroineFollowSpriteDataBase.inDoorHeroineFollowSpriteList[index].followingSpriteDictionary[PlayerDataManager.currentPlaceName];
				inDoorTalkManager.positionTalkImageArray[0].GetComponent<Image>().sprite = sprite;
				inDoorTalkManager.positionTalkImageArray[0].SetActive(value: true);
				inDoorTalkManager.positionTalkImageArray[0].GetComponent<RectTransform>().localPosition = inDoorCharacterCgData.talkPositionV2;
				inDoorTalkManager.positionTalkImageArray[0].GetComponent<RectTransform>().localScale = new Vector2(inDoorCharacterCgData.talkImageDisplayMagnification, inDoorCharacterCgData.talkImageDisplayMagnification);
				ParameterContainer component = inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>();
				component.SetString("characterName", inDoorCharacterCgData.characterName);
				component.SetInt("sortID", inDoorCharacterCgData.sortID);
				component.SetVector2("balloonPosition", inDoorCharacterCgData.talkBalloonV2);
				inDoorTalkManager.heroineAlertGroupGo.GetComponent<RectTransform>().anchoredPosition = inDoorCharacterCgData.talkAlertV2;
			}
			else if (PlayerDataManager.currentPlaceName == "Carriage" || PlayerDataManager.currentPlaceName == "CityCarriage")
			{
				isPrivatePlace = true;
				Sprite characterSprite = inDoorTalkManager.inDoorCharacterCgDataBase.inDoorCharacterCgDataList.Find((InDoorCharacterCgData data) => data.sortID == 0).characterSprite;
				inDoorTalkManager.positionTalkImageArray[0].GetComponent<Image>().sprite = characterSprite;
				inDoorTalkManager.positionTalkImageArray[0].SetActive(value: false);
				InDoorCharacterCgData inDoorCharacterCgData2 = inDoorTalkManager.inDoorCharacterCgDataBase.inDoorCharacterCgDataList.Find((InDoorCharacterCgData data) => data.sortID == 0);
				inDoorTalkManager.positionTalkImageArray[0].GetComponent<RectTransform>().localPosition = inDoorCharacterCgData2.talkPositionV2;
				inDoorTalkManager.positionTalkImageArray[0].GetComponent<RectTransform>().localScale = new Vector2(inDoorCharacterCgData2.talkImageDisplayMagnification, inDoorCharacterCgData2.talkImageDisplayMagnification);
				ParameterContainer component2 = inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>();
				component2.SetString("characterName", inDoorCharacterCgData2.characterName);
				component2.SetInt("sortID", inDoorCharacterCgData2.sortID);
				component2.SetVector2("balloonPosition", inDoorCharacterCgData2.talkBalloonV2);
				inDoorTalkManager.heroineAlertGroupGo.GetComponent<RectTransform>().anchoredPosition = inDoorCharacterCgData2.talkAlertV2;
			}
			else
			{
				isPrivatePlace = false;
				int index2 = PlayerDataManager.DungeonHeroineFollowNum - 2;
				Sprite sprite2 = inDoorTalkManager.inDoorHeroineFollowSpriteDataBase.inDoorHeroineFollowSpriteList[index2].followingSpriteDictionary[PlayerDataManager.currentPlaceName];
				inDoorTalkManager.positionTalkImageArray[1].GetComponent<Image>().sprite = sprite2;
				inDoorTalkManager.positionTalkImageArray[1].SetActive(value: true);
				inDoorTalkManager.positionTalkImageArray[1].GetComponent<RectTransform>().localPosition = inDoorCharacterCgData.heroinePositionV2;
				inDoorTalkManager.positionTalkImageArray[1].GetComponent<Button>().interactable = true;
				ParameterContainer component3 = inDoorTalkManager.positionTalkImageArray[1].GetComponent<ParameterContainer>();
				component3.SetString("characterName", inDoorCharacterCgData.characterName);
				component3.SetInt("sortID", inDoorCharacterCgData.sortID);
				component3.SetVector2("balloonPosition", inDoorCharacterCgData.heroineBalloonV2);
				inDoorTalkManager.heroineAlertGroupGo.GetComponent<RectTransform>().anchoredPosition = inDoorCharacterCgData.talkAlertV2;
			}
		}
		else if (PlayerDataManager.currentPlaceName == "ItemShop")
		{
			InDoorCharacterCgData inDoorCharacterCgData3 = inDoorTalkManager.inDoorCharacterCgDataBase.inDoorCharacterCgDataList.Find((InDoorCharacterCgData data) => data.characterName == "シャーロ");
			Debug.Log("雑貨店を表示");
			isPrivatePlace = true;
			inDoorTalkManager.positionTalkImageArray[0].GetComponent<Image>().sprite = inDoorCharacterCgData3.characterSprite;
			inDoorTalkManager.positionTalkImageArray[0].SetActive(value: true);
			inDoorTalkManager.positionTalkImageArray[0].GetComponent<RectTransform>().localPosition = inDoorCharacterCgData3.talkPositionV2;
			inDoorTalkManager.positionTalkImageArray[0].GetComponent<RectTransform>().localScale = new Vector2(inDoorCharacterCgData3.talkImageDisplayMagnification, inDoorCharacterCgData3.talkImageDisplayMagnification);
			inDoorTalkManager.positionTalkImageArray[0].GetComponent<Button>().interactable = true;
			ParameterContainer component4 = inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>();
			component4.SetString("characterName", inDoorCharacterCgData3.characterName);
			component4.SetInt("sortID", inDoorCharacterCgData3.sortID);
			component4.SetVector2("balloonPosition", inDoorCharacterCgData3.talkBalloonV2);
			inDoorTalkManager.talkAlertGroupGo.GetComponent<RectTransform>().anchoredPosition = inDoorCharacterCgData3.talkAlertV2;
		}
		else if (PlayerDataManager.currentPlaceName == "Inn" || PlayerDataManager.currentPlaceName == "Carriage" || PlayerDataManager.currentPlaceName == "InnStreet1" || PlayerDataManager.currentPlaceName == "CityCarriage")
		{
			isPrivatePlace = true;
			GameObject gameObject = null;
			ParameterContainer parameterContainer = null;
			bool value = false;
			if (PlayerDataManager.currentPlaceName == "Inn" || PlayerDataManager.currentPlaceName == "InnStreet1")
			{
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
				else
				{
					Debug.Log("現在地のGoがnull");
				}
				if (value && parameterContainer.GetBool("isHeroineExist"))
				{
					int heroineNum = parameterContainer.GetInt("heroineNum");
					int index3 = PlayerDataManager.DungeonHeroineFollowNum - 2;
					Debug.Log("ヒロインIndex：" + index3 + "／プレース名：" + PlayerDataManager.currentPlaceName);
					Sprite sprite3 = inDoorTalkManager.inDoorHeroineFollowSpriteDataBase.inDoorHeroineFollowSpriteList[index3].followingSpriteDictionary[PlayerDataManager.currentPlaceName];
					InDoorCharacterCgData inDoorCharacterCgData4 = inDoorTalkManager.inDoorCharacterCgDataBase.inDoorCharacterCgDataList.Find((InDoorCharacterCgData data) => data.sortID == heroineNum);
					inDoorTalkManager.positionTalkImageArray[0].GetComponent<Image>().sprite = sprite3;
					inDoorTalkManager.positionTalkImageArray[0].SetActive(value: true);
					inDoorTalkManager.positionTalkImageArray[0].GetComponent<RectTransform>().localPosition = inDoorCharacterCgData4.talkPositionV2;
					inDoorTalkManager.positionTalkImageArray[0].GetComponent<RectTransform>().localScale = new Vector2(inDoorCharacterCgData4.talkImageDisplayMagnification, inDoorCharacterCgData4.talkImageDisplayMagnification);
					ParameterContainer component5 = inDoorTalkManager.positionTalkImageArray[0].GetComponent<ParameterContainer>();
					component5.SetString("characterName", inDoorCharacterCgData4.characterName);
					component5.SetInt("sortID", inDoorCharacterCgData4.sortID);
					component5.SetVector2("balloonPosition", inDoorCharacterCgData4.talkBalloonV2);
					inDoorTalkManager.heroineAlertGroupGo.GetComponent<RectTransform>().anchoredPosition = inDoorCharacterCgData4.talkAlertV2;
					Debug.Log("宿屋にいるヒロインを表示");
				}
			}
		}
		else
		{
			isPrivatePlace = false;
		}
		inDoorTalkManager.inDoorCanvasGo.SetActive(value: true);
		if (isPrivatePlace)
		{
			Transition(privateLink);
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
