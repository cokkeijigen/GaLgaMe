using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class RefreshQuestTypeTab : StateBehaviour
{
	private QuestManager questManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		questManager = GameObject.Find("Quest Manager").GetComponent<QuestManager>();
	}

	public override void OnStateBegin()
	{
		GameObject[] questTypeTabGoArray = questManager.questTypeTabGoArray;
		for (int i = 0; i < questTypeTabGoArray.Length; i++)
		{
			questTypeTabGoArray[i].GetComponent<Image>().sprite = questManager.questTypeTabSpriteArray[0];
		}
		questManager.questTypeTabGoArray[questManager.selectTabTypeNum].GetComponent<Image>().sprite = questManager.questTypeTabSpriteArray[1];
		switch (questManager.selectTabTypeNum)
		{
		case 0:
			questManager.questCharacterTextLoc.Term = "questRequestBalloon_HunterGuild";
			questManager.questCharacterImage.sprite = questManager.questCharacterImageDictionary["HunterGuild"];
			break;
		case 1:
			if ((PlayerDataManager.currentPlaceName == "HunterGuild" && PlayerDataManager.mapPlaceStatusNum == 2) || (PlayerDataManager.currentPlaceName == "Bar" && PlayerDataManager.mapPlaceStatusNum == 2))
			{
				questManager.questCharacterTextLoc.Term = "questSelectBalloon_HunterGuild";
				questManager.questCharacterImage.sprite = questManager.questCharacterImageDictionary["HunterGuild"];
			}
			else
			{
				questManager.questCharacterTextLoc.Term = "questSelectBalloon_HunterGuild";
				questManager.questCharacterImage.sprite = questManager.questCharacterImageDictionary["HunterGuild"];
			}
			break;
		case 2:
			PlayerDataManager.isNoCheckNewQuest = false;
			questManager.newStoryQuestBalloonGo.SetActive(value: false);
			if ((PlayerDataManager.currentPlaceName == "HunterGuild" && PlayerDataManager.mapPlaceStatusNum == 2) || (PlayerDataManager.currentPlaceName == "Bar" && PlayerDataManager.mapPlaceStatusNum == 2))
			{
				questManager.questCharacterTextLoc.Term = "questSelectBalloon_Story";
				questManager.questCharacterImage.sprite = questManager.questCharacterImageDictionary["Eden"];
			}
			else
			{
				questManager.questCharacterTextLoc.Term = "questSelectBalloon_Story";
				questManager.questCharacterImage.sprite = questManager.questCharacterImageDictionary["Eden"];
			}
			break;
		case 3:
			PlayerDataManager.isNoCheckNewSubQuest = false;
			questManager.newStorySubQuestBalloonGo.SetActive(value: false);
			if ((PlayerDataManager.currentPlaceName == "HunterGuild" && PlayerDataManager.mapPlaceStatusNum == 2) || (PlayerDataManager.currentPlaceName == "Bar" && PlayerDataManager.mapPlaceStatusNum == 2))
			{
				questManager.questCharacterTextLoc.Term = "questSelectBalloon_StorySub";
				questManager.questCharacterImage.sprite = questManager.questCharacterImageDictionary["Eden"];
			}
			else
			{
				questManager.questCharacterTextLoc.Term = "questSelectBalloon_StorySub";
				questManager.questCharacterImage.sprite = questManager.questCharacterImageDictionary["Eden"];
			}
			break;
		}
		questManager.ResetScrollViewContents("select");
		questManager.ResetScrollViewContents("reward");
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
