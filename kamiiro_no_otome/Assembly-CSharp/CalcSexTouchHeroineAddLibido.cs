using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcSexTouchHeroineAddLibido : StateBehaviour
{
	private SexTouchManager sexTouchManager;

	private SexTouchStatusManager sexTouchStatusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchManager = GameObject.Find("Sex Touch Manager").GetComponent<SexTouchManager>();
		sexTouchStatusManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchStatusManager>();
	}

	public override void OnStateBegin()
	{
		int heroineID = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		int num = heroineID - 1;
		int num2 = 0;
		float num3 = 0f;
		float num4 = 0f;
		SexTouchClickData sexTouchClickData = GameDataManager.instance.sexTouchClickDataBase.sexTouchClickDataList.Find((SexTouchClickData data) => data.characterID == heroineID && data.areaName == sexTouchManager.clickSelectAreaPointName);
		switch (sexTouchManager.clickSelectAreaPointName)
		{
		case "mouth":
			num2 = PlayerSexStatusDataManager.heroineMouthLv[num];
			num3 = sexTouchStatusManager.heroineClickPointRepeatCountDictionary["mouth"];
			CalcRepeatClickCount("mouth");
			break;
		case "tits":
			num2 = PlayerSexStatusDataManager.heroineTitsLv[num];
			num3 = sexTouchStatusManager.heroineClickPointRepeatCountDictionary["tits"];
			CalcRepeatClickCount("tits");
			break;
		case "nippleRight":
		case "nippleLeft":
			num2 = PlayerSexStatusDataManager.heroineNippleLv[num];
			num3 = sexTouchStatusManager.heroineClickPointRepeatCountDictionary["nipple"];
			CalcRepeatClickCount("nipple");
			break;
		case "womb":
			num2 = PlayerSexStatusDataManager.heroineWombsLv[num];
			num3 = sexTouchStatusManager.heroineClickPointRepeatCountDictionary["womb"];
			CalcRepeatClickCount("womb");
			break;
		case "clitoris":
			num2 = PlayerSexStatusDataManager.heroineClitorisLv[num];
			num3 = sexTouchStatusManager.heroineClickPointRepeatCountDictionary["clitoris"];
			CalcRepeatClickCount("clitoris");
			break;
		case "vagina":
			num2 = PlayerSexStatusDataManager.heroineVaginaLv[num];
			num3 = sexTouchStatusManager.heroineClickPointRepeatCountDictionary["vagina"];
			CalcRepeatClickCount("vagina");
			break;
		case "anal":
			num2 = PlayerSexStatusDataManager.heroineAnalLv[num];
			num3 = sexTouchStatusManager.heroineClickPointRepeatCountDictionary["anal"];
			CalcRepeatClickCount("anal");
			break;
		}
		num4 = ((sexTouchStatusManager.heroineLibidoPoint < 30f) ? ((float)sexTouchClickData.areaSensitivityList[0] / 100f * (float)sexTouchClickData.areaLibidoUpNum) : ((!(sexTouchStatusManager.heroineLibidoPoint < 60f)) ? ((float)sexTouchClickData.areaSensitivityList[2] / 100f * (float)sexTouchClickData.areaLibidoUpNum) : ((float)sexTouchClickData.areaSensitivityList[1] / 100f * (float)sexTouchClickData.areaLibidoUpNum)));
		float num5 = 1f - num3 / 5f;
		float num6 = 1f + (float)num2 / 10f;
		sexTouchStatusManager.heroineLibidoAddPoint = num4 * num6 * num5;
		Debug.Log("連続クリック倍率：" + num5 + "／LV係数：" + num6 + "／加算値：" + num4);
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

	private void CalcRepeatClickCount(string clickPointName)
	{
		foreach (KeyValuePair<string, float> item in new Dictionary<string, float>(sexTouchStatusManager.heroineClickPointRepeatCountDictionary))
		{
			if (item.Key == clickPointName)
			{
				sexTouchStatusManager.heroineClickPointRepeatCountDictionary[item.Key] += 1f;
			}
			else
			{
				sexTouchStatusManager.heroineClickPointRepeatCountDictionary[item.Key] -= 0.5f;
			}
			sexTouchStatusManager.heroineClickPointRepeatCountDictionary[item.Key] = Mathf.Clamp(sexTouchStatusManager.heroineClickPointRepeatCountDictionary[item.Key], 0f, 5f);
		}
	}
}
