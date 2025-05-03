using Arbor;
using UnityEngine;

public class SexScheduleManager : MonoBehaviour
{
	public Canvas sexScheduleCanvas;

	public GameObject sexScheduleButtonGo;

	public ParameterContainer[] heroineScheduleParamArray;

	public Sprite[] heroineScheduleIconSpriteArray;

	private void Start()
	{
		bool active = false;
		int i;
		for (i = 2; i < 5; i++)
		{
			CharacterStatusData characterStatusData = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == i);
			if (PlayerFlagDataManager.scenarioFlagDictionary[characterStatusData.characterMenstruationViewFlag])
			{
				active = true;
				break;
			}
		}
		sexScheduleButtonGo.SetActive(active);
	}

	public void CheckHeroineScheduleVisible()
	{
		int i;
		for (i = 2; i < 5; i++)
		{
			CharacterStatusData characterStatusData = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == i);
			SetHeroineScheduleVisible(i, PlayerFlagDataManager.scenarioFlagDictionary[characterStatusData.characterMenstruationViewFlag]);
		}
		sexScheduleCanvas.gameObject.SetActive(value: true);
	}

	private void SetHeroineScheduleVisible(int heroineID, bool isVisible)
	{
		if (isVisible)
		{
			heroineScheduleParamArray[heroineID - 1].GetVariable<UguiImage>("iconImage").image.sprite = heroineScheduleIconSpriteArray[heroineID];
			heroineScheduleParamArray[heroineID - 1].GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = "character" + heroineID;
			heroineScheduleParamArray[heroineID - 1].GetGameObject("unKnownGroup").SetActive(value: false);
			if (heroineID == 3)
			{
				string characterMenstruationFlag = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[3].characterMenstruationFlag;
				if (PlayerFlagDataManager.scenarioFlagDictionary[characterMenstruationFlag])
				{
					heroineScheduleParamArray[heroineID - 1].GetGameObjectList("scheduleGroupArray")[0].SetActive(value: false);
					heroineScheduleParamArray[heroineID - 1].GetGameObjectList("scheduleGroupArray")[1].SetActive(value: true);
				}
				else
				{
					heroineScheduleParamArray[heroineID - 1].GetGameObjectList("scheduleGroupArray")[0].SetActive(value: true);
					heroineScheduleParamArray[heroineID - 1].GetGameObjectList("scheduleGroupArray")[1].SetActive(value: false);
				}
			}
			else
			{
				heroineScheduleParamArray[heroineID - 1].GetGameObject("scheduleGroup").SetActive(value: true);
			}
			Debug.Log("生理周期表示：" + heroineID);
		}
		else
		{
			CharacterStatusData characterStatusData = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == heroineID);
			SetHeroineNameVisible(heroineID, PlayerFlagDataManager.scenarioFlagDictionary[characterStatusData.characterDungeonFollowUnLockFlag]);
			Debug.Log("生理周期非表示：" + heroineID);
		}
	}

	private void SetHeroineNameVisible(int heroineID, bool isVisible)
	{
		heroineScheduleParamArray[heroineID - 1].GetGameObject("unKnownGroup").SetActive(value: true);
		if (isVisible)
		{
			heroineScheduleParamArray[heroineID - 1].GetVariable<UguiImage>("iconImage").image.sprite = heroineScheduleIconSpriteArray[heroineID];
			heroineScheduleParamArray[heroineID - 1].GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = "character" + heroineID;
			if (heroineID == 3)
			{
				heroineScheduleParamArray[heroineID - 1].GetGameObjectList("scheduleGroupArray")[0].SetActive(value: false);
				heroineScheduleParamArray[heroineID - 1].GetGameObjectList("scheduleGroupArray")[1].SetActive(value: false);
			}
			else
			{
				heroineScheduleParamArray[heroineID - 1].GetGameObject("scheduleGroup").SetActive(value: false);
			}
		}
		else
		{
			heroineScheduleParamArray[heroineID - 1].GetVariable<UguiImage>("iconImage").image.sprite = heroineScheduleIconSpriteArray[0];
			heroineScheduleParamArray[heroineID - 1].GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = "unKnown";
			if (heroineID == 3)
			{
				heroineScheduleParamArray[heroineID - 1].GetGameObjectList("scheduleGroupArray")[0].SetActive(value: false);
				heroineScheduleParamArray[heroineID - 1].GetGameObjectList("scheduleGroupArray")[1].SetActive(value: false);
			}
			else
			{
				heroineScheduleParamArray[heroineID - 1].GetGameObject("scheduleGroup").SetActive(value: false);
			}
		}
	}
}
