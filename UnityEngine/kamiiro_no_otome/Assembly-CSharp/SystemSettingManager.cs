using System.Collections.Generic;
using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utage;

public class SystemSettingManager : MonoBehaviour
{
	public ParameterContainer optionParam;

	public GameObject[] panelGoArray;

	public GameObject[] headrTabArray;

	public CanvasGroup headerTabCanvasGroup;

	public CanvasGroup optionResetButton;

	public Sprite[] headerTabSpriteArray;

	public GameObject[] savePageButtonArray;

	public Sprite[] savePageButtonSpriteArray;

	public GameObject[] saveSlotButtonArray;

	public Sprite[] saveSlotButtonSpriteArray;

	public GameObject[] soundGroupGoArray;

	public GameObject[] voiceGroupGoArray;

	public GameObject[] subVoiceGroupGoArray;

	public GameObject[] mobVoiceGroupGoArray;

	public GameObject[] textGroupGoArray;

	public GameObject[] otherGroupGoArray;

	public Slider bgmVolumeSlider;

	public Slider hBgmVolumeSlider;

	private void Awake()
	{
		optionParam.SetFloat("preBgmVolume", PlayerOptionsDataManager.optionsBgmVolume * 10f);
		optionParam.SetFloat("preHBgmVolume", PlayerOptionsDataManager.optionsHBgmVolume * 10f);
		optionParam.SetFloat("preSeVolume", PlayerOptionsDataManager.optionsSeVolume * 10f);
		optionParam.SetFloat("preAmbienceVolume", PlayerOptionsDataManager.optionsAmbienceVolume * 10f);
		optionParam.SetFloat("preVoiceVolume1", PlayerOptionsDataManager.optionsVoice1Volume * 10f);
		optionParam.SetFloat("preVoiceVolume2", PlayerOptionsDataManager.optionsVoice2Volume * 10f);
		optionParam.SetFloat("preVoiceVolume3", PlayerOptionsDataManager.optionsVoice3Volume * 10f);
		optionParam.SetFloat("preVoiceVolume4", PlayerOptionsDataManager.optionsVoice4Volume * 10f);
		optionParam.SetFloat("preVoiceVolume5", PlayerOptionsDataManager.optionsVoice5Volume * 10f);
		optionParam.SetFloat("preSubVoiceVolume1", PlayerOptionsDataManager.optionsSubVoice1Volume * 10f);
		optionParam.SetFloat("preSubVoiceVolume2", PlayerOptionsDataManager.optionsSubVoice2Volume * 10f);
		optionParam.SetFloat("preSubVoiceVolume3", PlayerOptionsDataManager.optionsSubVoice3Volume * 10f);
		optionParam.SetFloat("preSubVoiceVolume4", PlayerOptionsDataManager.optionsSubVoice4Volume * 10f);
		optionParam.SetFloat("preSubVoiceVolume5", PlayerOptionsDataManager.optionsSubVoice5Volume * 10f);
		optionParam.SetFloat("preSubVoiceVolume6", PlayerOptionsDataManager.optionsSubVoice6Volume * 10f);
		optionParam.SetFloat("preSubVoiceVolume7", PlayerOptionsDataManager.optionsSubVoice7Volume * 10f);
		optionParam.SetFloat("preSubVoiceVolume8", PlayerOptionsDataManager.optionsSubVoice8Volume * 10f);
		optionParam.SetFloat("preMobVoiceVolume1", PlayerOptionsDataManager.optionsMobVoice1Volume * 10f);
		optionParam.SetFloat("preMobVoiceVolume2", PlayerOptionsDataManager.optionsMobVoice2Volume * 10f);
		optionParam.SetFloat("preMobVoiceVolume3", PlayerOptionsDataManager.optionsMobVoice3Volume * 10f);
		optionParam.SetFloat("preMobVoiceVolume4", PlayerOptionsDataManager.optionsMobVoice4Volume * 10f);
		optionParam.SetFloat("preMobVoiceVolume5", PlayerOptionsDataManager.optionsMobVoice5Volume * 10f);
		optionParam.SetFloat("preMobVoiceVolume6", PlayerOptionsDataManager.optionsMobVoice6Volume * 10f);
		optionParam.SetFloat("preMobVoiceVolume7", PlayerOptionsDataManager.optionsMobVoice7Volume * 10f);
		optionParam.SetFloat("preMobVoiceVolume8", PlayerOptionsDataManager.optionsMobVoice8Volume * 10f);
		optionParam.SetBool("preAllVoiceDisable", PlayerOptionsDataManager.isAllVoiceDisable);
		optionParam.SetBool("preLucyVoiceTypeSoft", PlayerOptionsDataManager.isLucyVoiceTypeSoft);
		optionParam.SetBool("preLucyVoiceTypeSexy", PlayerOptionsDataManager.isLucyVoiceTypeSexy);
		optionParam.SetFloat("preTextSpeed", PlayerOptionsDataManager.optionsTextSpeed * 10f);
		optionParam.SetFloat("preAutoTextSpeed", PlayerOptionsDataManager.optionsAutoTextSpeed * 10f);
		optionParam.SetBool("preMouseWheelSend", PlayerOptionsDataManager.optionsMouseWheelSend);
		optionParam.SetBool("preMouseWheelBacklog", PlayerOptionsDataManager.optionsMouseWheelBacklog);
		optionParam.SetFloat("preMouseWheelPower", PlayerOptionsDataManager.optionsMouseWheelPower * 10f);
		optionParam.SetBool("preVoiceStopTypeNext", PlayerOptionsDataManager.optionsVoiceStopTypeNext);
		optionParam.SetBool("preVoiceStopTypeClick", PlayerOptionsDataManager.optionsVoiceStopTypeClick);
		optionParam.SetInt("preWindowSize", PlayerOptionsDataManager.optionsWindowSize);
		optionParam.SetBool("preFullScreenMode", PlayerOptionsDataManager.optionsFullScreenMode);
	}

	public void ResetOptionsData()
	{
		switch (PlayerNonSaveDataManager.selectSystemTabName)
		{
		case "sound":
		{
			GameObject[] array = soundGroupGoArray;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].GetComponent<ArborFSM>().SendTrigger("ResetOptionsData");
			}
			break;
		}
		case "voice1":
		{
			GameObject[] array = voiceGroupGoArray;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].GetComponent<ArborFSM>().SendTrigger("ResetOptionsData");
			}
			break;
		}
		case "voice2":
		{
			GameObject[] array = subVoiceGroupGoArray;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].GetComponent<ArborFSM>().SendTrigger("ResetOptionsData");
			}
			array = mobVoiceGroupGoArray;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].GetComponent<ArborFSM>().SendTrigger("ResetOptionsData");
			}
			break;
		}
		case "text":
		{
			GameObject[] array = textGroupGoArray;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].GetComponent<ArborFSM>().SendTrigger("ResetOptionsData");
			}
			array = otherGroupGoArray;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].GetComponent<ArborFSM>().SendTrigger("ResetOptionsData");
			}
			break;
		}
		}
	}

	public void TestPreBgmVolume(string type)
	{
		PlaylistController component = GameObject.Find("PlaylistController").GetComponent<PlaylistController>();
		AdvConfig advConfig = null;
		GameObject gameObject = GameObject.Find("AdvEngine");
		if (gameObject != null)
		{
			advConfig = gameObject.GetComponent<AdvConfig>();
		}
		if (!(type == "testApply"))
		{
			if (type == "testRevert")
			{
				if (advConfig != null)
				{
					advConfig.BgmVolume = PlayerOptionsDataManager.optionsBgmVolume;
				}
				component.PlaylistVolume = PlayerOptionsDataManager.optionsBgmVolume;
			}
		}
		else
		{
			if (advConfig != null)
			{
				advConfig.BgmVolume = bgmVolumeSlider.value / 10f;
			}
			component.PlaylistVolume = bgmVolumeSlider.value / 10f;
		}
	}

	public void SetSubAndMobVoiceVolumeParameter(string voiceName, int index, Slider slider)
	{
		if (!(voiceName == "subVoice"))
		{
			if (voiceName == "mobVoice")
			{
				IList<float> floatList = optionParam.GetFloatList("preMobVoiceVolumeList");
				floatList[index - 1] = slider.value;
				optionParam.SetFloatList("preMobVoiceVolumeList", floatList);
				Debug.Log("モブ音量／インデックス：" + index + "／スライダ：" + slider.value);
			}
		}
		else
		{
			IList<float> floatList2 = optionParam.GetFloatList("preSubVoiceVolumeList");
			floatList2[index - 1] = slider.value;
			optionParam.SetFloatList("preSubVoiceVolumeList", floatList2);
			Debug.Log("サブ音量／インデックス：" + index + "／スライダ：" + slider.value);
		}
	}

	public void PlayLucyVoiceSample(string voiceType, bool isSexSample)
	{
		if (!(voiceType == "soft"))
		{
			if (voiceType == "sexy")
			{
				if (!isSexSample)
				{
					int num = Random.Range(0, 4);
					string[] array = new string[4] { "_attack10", "_buff10", "_heal10", "_charge10" };
					string variationName = "optionBattleVoice" + array[num] + "_sexy(Clone)";
					MasterAudio.PlaySound("optionBattleVoice1_sexy", 1f, null, 0f, variationName, null);
				}
				else
				{
					string variationName2 = "optionSexVoice1-" + Random.Range(1, 5) + "_sexy(Clone)";
					MasterAudio.PlaySound("optionSexVoice1_sexy", 1f, null, 0f, variationName2, null);
				}
			}
		}
		else if (!isSexSample)
		{
			int num2 = Random.Range(0, 4);
			string[] array2 = new string[4] { "_attack10", "_buff10", "_heal10", "_charge10" };
			string variationName3 = "optionBattleVoice" + array2[num2] + "(Clone)";
			MasterAudio.PlaySound("optionBattleVoice1", 1f, null, 0f, variationName3, null);
		}
		else
		{
			string variationName4 = "optionSexVoice1-" + Random.Range(1, 5) + "(Clone)";
			MasterAudio.PlaySound("optionSexVoice1", 1f, null, 0f, variationName4, null);
		}
	}

	public void PutBackUiScreenLevel()
	{
		PlayerNonSaveDataManager.openUiScreenLevel = PlayerNonSaveDataManager.beforeUiScreenLevel;
	}

	public bool GetInterruptedSave()
	{
		return PlayerNonSaveDataManager.isInterruptedSave;
	}

	public void CloseInterruptedSave()
	{
		if (PlayerNonSaveDataManager.isInterruptedAfterSave)
		{
			PlayerNonSaveDataManager.openDialogName = "saveAfter";
		}
		GameObject.Find("Interrupted Dialog Manager").GetComponent<InterruptedDialogManager>().OpenInterruptedDialog();
		MasterAudio.PlaySound("SeWindowClose", 1f, null, 0f, null, null);
		SceneManager.UnloadSceneAsync("systemUI");
	}

	public void UnLoadSystemUiScene()
	{
		SceneManager.UnloadSceneAsync("systemUI");
	}

	public void SetMainHeroineAllVoceVolume(int value)
	{
		for (int i = 0; i < 5; i++)
		{
			voiceGroupGoArray[i].GetComponent<Slider>().value += value;
		}
	}

	public void SetSubHeroineAllVoceVolume(int value)
	{
		for (int i = 0; i < subVoiceGroupGoArray.Length; i++)
		{
			subVoiceGroupGoArray[i].GetComponent<Slider>().value += value;
		}
		for (int j = 0; j < mobVoiceGroupGoArray.Length; j++)
		{
			mobVoiceGroupGoArray[j].GetComponent<Slider>().value += value;
		}
	}
}
