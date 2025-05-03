using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class OpenSexTouchInfoWindow : StateBehaviour
{
	private SexTouchManager sexTouchManager;

	private SexTouchHeroineDataManager sexTouchHeroineDataManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchManager = GameObject.Find("Sex Touch Manager").GetComponent<SexTouchManager>();
		sexTouchHeroineDataManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchHeroineDataManager>();
	}

	public override void OnStateBegin()
	{
		int heroineID = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		int num = heroineID - 1;
		string term = "";
		string text = "";
		int num2 = 0;
		int index = 0;
		Sprite sprite = null;
		if (PlayerNonSaveDataManager.isSexHeroineMenstrualDay)
		{
			index = 1;
		}
		switch (sexTouchManager.clickSelectAreaPointName)
		{
		case "mouth":
			num2 = PlayerSexStatusDataManager.heroineMouthLv[num];
			term = "sexBodyPassive_mouth" + heroineID + "0" + num2;
			text = "sexBodyTouchInfo_mouth" + heroineID + num2;
			sprite = sexTouchHeroineDataManager.sexTouchHeroineSpriteData.heroineCgMouthList[index];
			break;
		case "handRight":
		case "handLeft":
			num2 = PlayerSexStatusDataManager.heroineHandLv[num];
			text = "sexBodyTouchInfo_hand" + heroineID + num2;
			sprite = sexTouchHeroineDataManager.sexTouchHeroineSpriteData.heroineCgHandList[index];
			break;
		case "tits":
			num2 = PlayerSexStatusDataManager.heroineTitsLv[num];
			term = "sexBodyPassive_tits" + heroineID + "3" + num2;
			text = "sexBodyTouchInfo_tits" + heroineID + num2;
			sprite = sexTouchHeroineDataManager.sexTouchHeroineSpriteData.heroineCgTitsList[index];
			break;
		case "nippleRight":
		{
			num2 = PlayerSexStatusDataManager.heroineNippleLv[num];
			term = "sexBodyPassive_nipple" + heroineID + "4" + num2;
			text = "sexBodyTouchInfo_nipple" + heroineID + num2;
			sprite = sexTouchHeroineDataManager.sexTouchHeroineSpriteData.heroineCgNippleRightList[index];
			SexTouchClickData sexTouchClickData = GameDataManager.instance.sexTouchClickDataBase.sexTouchClickDataList.Find((SexTouchClickData data) => data.areaName == "nippleRight" && data.characterID == heroineID);
			sexTouchManager.touchAreaPointGoArray[sexTouchClickData.areaIndex - 1].GetComponent<RectTransform>().localPosition = sexTouchClickData.areaPointVector2WithInfoWindow;
			SexTouchClickData sexTouchClickData2 = GameDataManager.instance.sexTouchClickDataBase.sexTouchClickDataList.Find((SexTouchClickData data) => data.areaName == "nippleLeft" && data.characterID == heroineID);
			sexTouchManager.touchAreaPointGoArray[sexTouchClickData2.areaIndex - 1].GetComponent<RectTransform>().localPosition = sexTouchClickData2.areaPointVector2WithInfoWindow;
			Debug.Log("タッチ位置：" + sexTouchManager.clickSelectAreaPointName);
			break;
		}
		case "nippleLeft":
		{
			num2 = PlayerSexStatusDataManager.heroineNippleLv[num];
			term = "sexBodyPassive_nipple" + heroineID + "4" + num2;
			text = "sexBodyTouchInfo_nipple" + heroineID + num2;
			sprite = sexTouchHeroineDataManager.sexTouchHeroineSpriteData.heroineCgNippleLeftList[index];
			SexTouchClickData sexTouchClickData3 = GameDataManager.instance.sexTouchClickDataBase.sexTouchClickDataList.Find((SexTouchClickData data) => data.areaName == "nippleRight" && data.characterID == heroineID);
			sexTouchManager.touchAreaPointGoArray[sexTouchClickData3.areaIndex - 1].GetComponent<RectTransform>().localPosition = sexTouchClickData3.areaPointVector2WithInfoWindow;
			SexTouchClickData sexTouchClickData4 = GameDataManager.instance.sexTouchClickDataBase.sexTouchClickDataList.Find((SexTouchClickData data) => data.areaName == "nippleLeft" && data.characterID == heroineID);
			sexTouchManager.touchAreaPointGoArray[sexTouchClickData4.areaIndex - 1].GetComponent<RectTransform>().localPosition = sexTouchClickData4.areaPointVector2WithInfoWindow;
			Debug.Log("タッチ位置：" + sexTouchManager.clickSelectAreaPointName);
			break;
		}
		case "womb":
			num2 = PlayerSexStatusDataManager.heroineWombsLv[num];
			term = "sexBodyPassive_womb" + heroineID + "5" + num2;
			text = "sexBodyTouchInfo_womb" + heroineID + num2;
			if (PlayerNonSaveDataManager.isSexHeroineMenstrualDay)
			{
				if (PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[heroineID] > 0)
				{
					text += "_semen";
					index = 3;
				}
				else
				{
					index = 2;
				}
			}
			else if (PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[heroineID] > 0)
			{
				text += "_semen";
				index = 1;
			}
			else
			{
				index = 0;
			}
			sprite = sexTouchHeroineDataManager.sexTouchHeroineSpriteData.heroineCgWombList[index];
			break;
		case "clitoris":
			num2 = PlayerSexStatusDataManager.heroineClitorisLv[num];
			term = "sexBodyPassive_clitoris" + heroineID + "6" + num2;
			text = "sexBodyTouchInfo_clitoris" + heroineID + num2;
			if (PlayerNonSaveDataManager.selectSexBattleHeroineId == 3)
			{
				Debug.Log("シアのおまんこLV：" + num2);
				if (num2 == 1)
				{
					if (PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[heroineID] > 0)
					{
						text += "_semen";
						index = 3;
					}
					else
					{
						index = 2;
					}
				}
				else if (PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[heroineID] > 0)
				{
					text += "_semen";
					index = 1;
				}
				else
				{
					index = 0;
				}
			}
			else if (PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[heroineID] > 0)
			{
				text += "_semen";
				index = 1;
			}
			else
			{
				index = 0;
			}
			sprite = sexTouchHeroineDataManager.sexTouchHeroineSpriteData.heroineCgClitorisList[index];
			break;
		case "vagina":
			num2 = PlayerSexStatusDataManager.heroineVaginaLv[num];
			term = "sexBodyPassive_vagina" + heroineID + "7" + num2;
			text = "sexBodyTouchInfo_vagina" + heroineID + num2;
			if (PlayerNonSaveDataManager.selectSexBattleHeroineId == 3)
			{
				Debug.Log("シアのおまんこLV：" + num2);
				if (num2 == 1)
				{
					if (PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[heroineID] > 0)
					{
						text += "_semen";
						index = 3;
					}
					else
					{
						index = 2;
					}
				}
				else if (PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[heroineID] > 0)
				{
					text += "_semen";
					index = 1;
				}
				else
				{
					index = 0;
				}
			}
			else if (PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[heroineID] > 0)
			{
				text += "_semen";
				index = 1;
			}
			else
			{
				index = 0;
			}
			sprite = sexTouchHeroineDataManager.sexTouchHeroineSpriteData.heroineCgVaginaList[index];
			break;
		case "anal":
			num2 = PlayerSexStatusDataManager.heroineAnalLv[num];
			term = "sexBodyPassive_anal" + heroineID + "8" + num2;
			text = "sexBodyTouchInfo_anal" + heroineID + num2;
			index = (PlayerNonSaveDataManager.isSexHeroineMenstrualDay ? ((PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[heroineID] <= 0) ? 2 : 3) : ((PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[heroineID] > 0) ? 1 : 0));
			sprite = sexTouchHeroineDataManager.sexTouchHeroineSpriteData.heroineCgAnalList[index];
			break;
		}
		sexTouchManager.touchHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sprite;
		sexTouchManager.selectBodyLv = num2;
		sexTouchManager.currentSexSkillLv = num2;
		sexTouchManager.RefreshBodyHistoryGroup();
		sexTouchManager.bodyClickInfoHeaderLocText.Term = term;
		if (PlayerNonSaveDataManager.isSexHeroineMenstrualDay)
		{
			text += "_menstrualDay";
		}
		sexTouchManager.bodyClickSummaryLocText.Term = text;
		sexTouchManager.bodyClickInfoWindowGo.SetActive(value: true);
		MasterAudio.PlaySound("SeMiniButton", 1f, null, 0f, null, null);
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
