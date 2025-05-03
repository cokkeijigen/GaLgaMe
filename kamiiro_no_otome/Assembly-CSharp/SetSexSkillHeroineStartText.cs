using Arbor;
using DarkTonic.MasterAudio;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class SetSexSkillHeroineStartText : StateBehaviour
{
	private SexTouchStatusManager sexTouchStatusManager;

	private SexBattleManager sexBattleManager;

	private SexBattleMessageTextManager sexBattleMessageTextManager;

	public float waitTime;

	public float waitTime2;

	private string voiceNameString;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchStatusManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchStatusManager>();
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		sexBattleMessageTextManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleMessageTextManager>();
	}

	public override void OnStateBegin()
	{
		float time = waitTime / (float)sexBattleManager.battleSpeed;
		SexSkillData heroineSexSkillData = sexBattleManager.heroineSexSkillData;
		sexTouchStatusManager.heroineSexLvStage.ToString();
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		string text = "";
		voiceNameString = "";
		switch (heroineSexSkillData.counterType)
		{
		case SexSkillData.CounterType.piston:
		{
			int heroineCounterText = Random.Range(1, 3);
			int num6 = Random.Range(1, 4);
			switch (heroineSexSkillData.narrativePartString)
			{
			case "tits":
			case "nipple":
			case "portio":
			case "lotion":
			case "vagina":
			case "anal":
				SetHeroineCounterText(heroineCounterText);
				text = "Voice_Piston_" + selectSexBattleHeroineId;
				voiceNameString = "voice_Piston" + selectSexBattleHeroineId + "_" + num6;
				break;
			case "hardMooch":
				sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[0].GetComponent<Localize>().Term = "sexCounter_Heroine" + selectSexBattleHeroineId + "_HardMooch" + heroineCounterText;
				text = "Voice_Caress_" + selectSexBattleHeroineId;
				voiceNameString = "voice_Caress" + selectSexBattleHeroineId + "_" + heroineCounterText;
				break;
			case "report":
				sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[0].GetComponent<Localize>().Term = "sexCounter_Heroine" + selectSexBattleHeroineId + "_Report" + heroineCounterText;
				text = "Voice_Caress_" + selectSexBattleHeroineId;
				voiceNameString = "voice_Caress" + selectSexBattleHeroineId + "_" + heroineCounterText;
				break;
			case "hardMove":
				sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[0].GetComponent<Localize>().Term = "sexCounter_Heroine" + selectSexBattleHeroineId + "_HardMove" + heroineCounterText;
				text = "Voice_HardPiston_" + selectSexBattleHeroineId;
				voiceNameString = "voice_HardPiston" + selectSexBattleHeroineId + "_" + heroineCounterText;
				break;
			case "veryHardMove":
				sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[0].GetComponent<Localize>().Term = "sexCounter_Heroine" + selectSexBattleHeroineId + "_VeryHardMove" + heroineCounterText;
				text = "Voice_HardPiston_" + selectSexBattleHeroineId;
				voiceNameString = "voice_HardPiston" + selectSexBattleHeroineId + "_" + heroineCounterText;
				break;
			case "ultimateHardMove":
				sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[0].GetComponent<Localize>().Term = "sexCounter_Heroine" + selectSexBattleHeroineId + "_UltimateHardMove" + heroineCounterText;
				text = "Voice_HardPiston_" + selectSexBattleHeroineId;
				voiceNameString = "voice_HardPiston" + selectSexBattleHeroineId + "_" + heroineCounterText;
				break;
			}
			break;
		}
		case SexSkillData.CounterType.pistonAndCaress:
		{
			int heroineCounterText2 = Random.Range(1, 4);
			SetHeroineCounterText(heroineCounterText2);
			text = "Voice_Caress_" + selectSexBattleHeroineId;
			voiceNameString = "voice_Caress" + selectSexBattleHeroineId + "_" + heroineCounterText2;
			break;
		}
		case SexSkillData.CounterType.all:
		{
			string narrativePartString = heroineSexSkillData.narrativePartString;
			if (!(narrativePartString == "kiss"))
			{
				if (narrativePartString == "concentration")
				{
					int num4 = Random.Range(1, 3);
					sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[0].GetComponent<Localize>().Term = "sexCounter_Heroine" + selectSexBattleHeroineId + "_Concentration" + num4;
					text = "Voice_Caress_" + selectSexBattleHeroineId;
					voiceNameString = "voice_Caress" + selectSexBattleHeroineId + "_" + num4;
				}
			}
			else
			{
				int num5 = Random.Range(1, 3);
				sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[0].GetComponent<Localize>().Term = "sexCounter_Heroine" + selectSexBattleHeroineId + "_Kiss" + num5;
				text = "Voice_Kiss_" + selectSexBattleHeroineId;
				voiceNameString = "voice_Kiss" + selectSexBattleHeroineId + "_" + num5;
			}
			break;
		}
		case SexSkillData.CounterType.healAndCaress:
		{
			int num2 = Random.Range(1, 3);
			int num3 = Random.Range(1, 4);
			string narrativePartString = heroineSexSkillData.narrativePartString;
			if (!(narrativePartString == "mooch"))
			{
				if (narrativePartString == "breath")
				{
					sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[0].GetComponent<Localize>().Term = "sexCounter_Heroine" + selectSexBattleHeroineId + "_Breath" + num2;
					text = "Voice_Caress_" + selectSexBattleHeroineId;
					voiceNameString = "voice_Caress" + selectSexBattleHeroineId + "_" + num3;
				}
			}
			else
			{
				sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[0].GetComponent<Localize>().Term = "sexCounter_Heroine" + selectSexBattleHeroineId + "_Mooch" + num2;
				text = "Voice_Caress_" + selectSexBattleHeroineId;
				voiceNameString = "voice_Caress" + selectSexBattleHeroineId + "_" + num3;
			}
			break;
		}
		case SexSkillData.CounterType.absorb:
		{
			int num = Random.Range(1, 3);
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[0].GetComponent<Localize>().Term = "sexCounter_Heroine" + selectSexBattleHeroineId + "_Absorb" + num;
			text = "Voice_Absorb_" + selectSexBattleHeroineId;
			voiceNameString = "voice_Absorb" + selectSexBattleHeroineId + "_" + num;
			break;
		}
		}
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[0].gameObject.SetActive(value: true);
		Debug.Log("グループ名；" + text + "／音声名：" + voiceNameString);
		MasterAudio.PlaySound(text, 1f, null, 0f, voiceNameString + "(Clone)", null);
		Invoke("InvokeMethod", time);
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

	private void InvokeMethod()
	{
		float time = waitTime2 / (float)sexBattleManager.battleSpeed;
		SexSkillData heroineSexSkillData = sexBattleManager.heroineSexSkillData;
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		int num = Random.Range(0, 2);
		switch (heroineSexSkillData.narrativePartString)
		{
		case "tits":
		case "nipple":
		case "vagina":
		case "anal":
		{
			string passiveBodyCategoryName = GetPassiveBodyCategoryName(heroineSexSkillData.narrativePartString, selectSexBattleHeroineId);
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[1].GetComponent<Localize>().Term = "sexBattleTarget_" + selectSexBattleHeroineId + 2;
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[2].GetComponent<Localize>().Term = "sexBodyPassive_" + passiveBodyCategoryName;
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[2].gameObject.SetActive(value: true);
			break;
		}
		case "lotion":
		case "portio":
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[1].GetComponent<Localize>().Term = "sexBattleTarget_" + selectSexBattleHeroineId + 2;
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[2].GetComponent<Localize>().Term = "sexBodyCounter_" + heroineSexSkillData.narrativePartString;
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[2].gameObject.SetActive(value: true);
			break;
		case "voice":
		case "mooch":
		case "breath":
		case "kiss":
		case "hand":
		case "manNipple":
		case "concentration":
		case "hardMooch":
		case "report":
		case "hardMove":
		case "veryHardMove":
		case "ultimateHardMove":
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[1].GetComponent<Localize>().Term = "sexBattleTarget_" + selectSexBattleHeroineId + 1;
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[2].gameObject.SetActive(value: false);
			break;
		}
		string text = heroineSexSkillData.narrativePartString.Substring(0, 1).ToUpper() + heroineSexSkillData.narrativePartString.Substring(1);
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[1].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[3].GetComponent<Localize>().Term = "sexCounter_" + text + num;
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[3].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroupGo_Bottom.SetActive(value: true);
		Invoke("InvokeMethod2", time);
	}

	private void InvokeMethod2()
	{
		Transition(stateLink);
	}

	private void SetHeroineCounterText(int random)
	{
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		int num = PlayerSexStatusDataManager.playerSexHp[selectSexBattleHeroineId] / PlayerSexStatusDataManager.playerSexMaxHp[selectSexBattleHeroineId];
		if ((float)num > 0.66f)
		{
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[0].GetComponent<Localize>().Term = "sexCounter_Heroine" + selectSexBattleHeroineId + "_Normal" + random;
		}
		else if ((float)num > 0.33f)
		{
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[0].GetComponent<Localize>().Term = "sexCounter_Heroine" + selectSexBattleHeroineId + "_High" + random;
		}
		else
		{
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[0].GetComponent<Localize>().Term = "sexCounter_Heroine" + selectSexBattleHeroineId + "_Max" + random;
		}
	}

	private string GetPassiveBodyCategoryName(string narrativePart, int heroineID)
	{
		int num = 0;
		string result = "";
		switch (narrativePart)
		{
		case "tits":
			result = narrativePart + heroineID + 3 + PlayerSexStatusDataManager.heroineTitsLv[heroineID - 1];
			break;
		case "nipple":
			result = narrativePart + heroineID + 4 + PlayerSexStatusDataManager.heroineNippleLv[heroineID - 1];
			break;
		case "vagina":
			result = narrativePart + heroineID + 7 + PlayerSexStatusDataManager.heroineVaginaLv[heroineID - 1];
			break;
		case "anal":
			result = narrativePart + heroineID + 8 + PlayerSexStatusDataManager.heroineAnalLv[heroineID - 1];
			break;
		}
		return result;
	}
}
