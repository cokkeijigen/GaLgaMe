using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class SaveLoadPanelRefresh : StateBehaviour
{
	private SystemSettingManager systemSettingManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		systemSettingManager = GameObject.Find("System Setting Manager").GetComponent<SystemSettingManager>();
	}

	public override void OnStateBegin()
	{
		ES3.CacheFile("SaveFile.es3");
		ES3Settings settings = new ES3Settings("SaveFile.es3", ES3.Location.Cache);
		int num = PlayerDataManager.lastSaveSlotPageNum * 9 + PlayerDataManager.lastSaveSlotNum + 1;
		for (int i = 0; i < systemSettingManager.saveSlotButtonArray.Length; i++)
		{
			int num2 = PlayerNonSaveDataManager.selectSlotPageNum * 9 + i + 1;
			ParameterContainer component = systemSettingManager.saveSlotButtonArray[i].GetComponent<ParameterContainer>();
			component.GetGameObject("slotNumText").GetComponent<Text>().text = "No." + num2;
			if (ES3.KeyExists("saveDayTime" + num2, settings))
			{
				component.GetGameObject("dayTimeText").GetComponent<Text>().text = ES3.Load<string>("saveDayTime" + num2, settings);
				component.GetVariable<UguiTextVariable>("totalDayText").text.text = ES3.Load<int>("currentTotalDay" + num2, settings).ToString();
				component.GetVariable<I2LocalizeComponent>("timeZoneTextLoc").localize.Term = "systemTimeZone" + ES3.Load<int>("currentTimeZone" + num2, settings);
				float num3 = ES3.Load("gamePlayTime" + num2, 0f, settings);
				int value = (int)(num3 / 3600f);
				value = Mathf.Clamp(value, 0, 99);
				int num4 = (int)(num3 / 60f) % 60;
				int num5 = (int)(num3 % 60f);
				component.GetVariable<UguiTextVariable>("gamePlayTimeText").text.text = $"{value:00}:{num4:00}:{num5:00}";
				component.GetVariable<I2LocalizeComponent>("placeTextLoc").localize.Term = ES3.Load<string>("savePlaceName" + num2, settings);
				component.GetVariable<UguiTextVariable>("lvText").text.text = ES3.Load<int>("savePlayerLv" + num2, settings).ToString();
				int num6 = ES3.Load<int>("playerHaveMoney" + num2, settings);
				component.GetVariable<UguiTextVariable>("moneyText").text.text = $"{num6:#,0}";
				component.GetVariableList<I2LocalizeComponent>("shopRankTextLocList")[0].localize.Term = ES3.Load<string>("saveShopRankName0" + num2, "shopRank_first1", settings);
				component.GetVariableList<I2LocalizeComponent>("shopRankTextLocList")[1].localize.Term = ES3.Load<string>("saveShopRankName1" + num2, "shopRank_second1", settings);
				bool num7 = ES3.Load<bool>("isDungeonHeroineFollow" + num2, settings);
				int num8 = ES3.Load<int>("DungeonHeroineFollowNum" + num2, settings);
				if (num7)
				{
					component.GetVariable<I2LocalizeComponent>("followNameText").localize.Term = "character" + num8;
				}
				else
				{
					component.GetVariable<I2LocalizeComponent>("followNameText").localize.Term = "nothing";
				}
				string selectSystemTabName = PlayerNonSaveDataManager.selectSystemTabName;
				if (!(selectSystemTabName == "save"))
				{
					if (selectSystemTabName == "load")
					{
						systemSettingManager.saveSlotButtonArray[i].GetComponent<Image>().sprite = systemSettingManager.saveSlotButtonSpriteArray[2];
					}
				}
				else
				{
					systemSettingManager.saveSlotButtonArray[i].GetComponent<Image>().sprite = systemSettingManager.saveSlotButtonSpriteArray[1];
				}
				if (num2 == num)
				{
					component.GetGameObject("lastSaveIcon").SetActive(value: true);
				}
				else
				{
					component.GetGameObject("lastSaveIcon").SetActive(value: false);
				}
			}
			else
			{
				systemSettingManager.saveSlotButtonArray[i].GetComponent<Image>().sprite = systemSettingManager.saveSlotButtonSpriteArray[0];
				component.GetGameObject("dayTimeText").GetComponent<Text>().text = "NO DATA";
				component.GetVariable<UguiTextVariable>("monthDayText").text.text = "---";
				component.GetVariable<UguiTextVariable>("totalDayText").text.text = "---";
				component.GetVariable<I2LocalizeComponent>("timeZoneTextLoc").localize.Term = "noStatus";
				component.GetVariable<I2LocalizeComponent>("placeTextLoc").localize.Term = "noStatus";
				component.GetVariable<UguiTextVariable>("lvText").text.text = "---";
				component.GetVariable<UguiTextVariable>("moneyText").text.text = "---";
				component.GetVariableList<I2LocalizeComponent>("shopRankTextLocList")[0].localize.Term = "noStatus";
				component.GetVariableList<I2LocalizeComponent>("shopRankTextLocList")[1].localize.Term = "noStatus";
				component.GetVariable<I2LocalizeComponent>("followNameText").localize.Term = "noStatus";
				component.GetGameObject("lastSaveIcon").SetActive(value: false);
				Debug.Log("セーブデータなし");
			}
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
