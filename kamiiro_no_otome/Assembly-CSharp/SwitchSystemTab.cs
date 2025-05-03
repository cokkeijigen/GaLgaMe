using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class SwitchSystemTab : StateBehaviour
{
	private SystemSettingManager systemSettingManager;

	public StateLink slotPageLink;

	public StateLink systemTabLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		systemSettingManager = GameObject.Find("System Setting Manager").GetComponent<SystemSettingManager>();
	}

	public override void OnStateBegin()
	{
		bool flag = false;
		Sprite sprite = systemSettingManager.headerTabSpriteArray[1];
		GameObject[] panelGoArray = systemSettingManager.panelGoArray;
		for (int i = 0; i < panelGoArray.Length; i++)
		{
			panelGoArray[i].SetActive(value: false);
		}
		panelGoArray = systemSettingManager.headrTabArray;
		for (int i = 0; i < panelGoArray.Length; i++)
		{
			panelGoArray[i].GetComponent<Image>().sprite = systemSettingManager.headerTabSpriteArray[0];
		}
		switch (PlayerNonSaveDataManager.selectSystemTabName)
		{
		case "save":
			systemSettingManager.headrTabArray[0].GetComponent<Image>().sprite = sprite;
			systemSettingManager.panelGoArray[0].SetActive(value: true);
			flag = false;
			PlayerNonSaveDataManager.selectSlotNum = PlayerDataManager.lastSaveSlotNum;
			PlayerNonSaveDataManager.selectSlotPageNum = PlayerDataManager.lastSaveSlotPageNum;
			if (PlayerNonSaveDataManager.isInterruptedSave)
			{
				systemSettingManager.headerTabCanvasGroup.interactable = false;
				systemSettingManager.headerTabCanvasGroup.alpha = 0.5f;
			}
			else
			{
				systemSettingManager.headerTabCanvasGroup.interactable = true;
				systemSettingManager.headerTabCanvasGroup.alpha = 1f;
			}
			break;
		case "load":
			systemSettingManager.headrTabArray[1].GetComponent<Image>().sprite = sprite;
			systemSettingManager.panelGoArray[0].SetActive(value: true);
			flag = false;
			PlayerNonSaveDataManager.selectSlotNum = PlayerDataManager.lastSaveSlotNum;
			PlayerNonSaveDataManager.selectSlotPageNum = PlayerDataManager.lastSaveSlotPageNum;
			break;
		case "sound":
			systemSettingManager.headrTabArray[2].GetComponent<Image>().sprite = sprite;
			systemSettingManager.panelGoArray[1].SetActive(value: true);
			systemSettingManager.panelGoArray[2].SetActive(value: true);
			systemSettingManager.optionResetButton.interactable = true;
			systemSettingManager.optionResetButton.alpha = 1f;
			flag = true;
			break;
		case "voice1":
			systemSettingManager.headrTabArray[3].GetComponent<Image>().sprite = sprite;
			systemSettingManager.panelGoArray[1].SetActive(value: true);
			systemSettingManager.panelGoArray[3].SetActive(value: true);
			systemSettingManager.optionResetButton.interactable = true;
			systemSettingManager.optionResetButton.alpha = 1f;
			flag = true;
			break;
		case "voice2":
			systemSettingManager.headrTabArray[4].GetComponent<Image>().sprite = sprite;
			systemSettingManager.panelGoArray[1].SetActive(value: true);
			systemSettingManager.panelGoArray[4].SetActive(value: true);
			systemSettingManager.optionResetButton.interactable = true;
			systemSettingManager.optionResetButton.alpha = 1f;
			flag = true;
			break;
		case "text":
			systemSettingManager.headrTabArray[5].GetComponent<Image>().sprite = sprite;
			systemSettingManager.panelGoArray[1].SetActive(value: true);
			systemSettingManager.panelGoArray[5].SetActive(value: true);
			systemSettingManager.optionResetButton.interactable = true;
			systemSettingManager.optionResetButton.alpha = 1f;
			flag = true;
			break;
		case "display":
			systemSettingManager.headrTabArray[6].GetComponent<Image>().sprite = sprite;
			systemSettingManager.panelGoArray[1].SetActive(value: true);
			systemSettingManager.panelGoArray[6].SetActive(value: true);
			systemSettingManager.optionResetButton.interactable = false;
			systemSettingManager.optionResetButton.alpha = 0.5f;
			flag = true;
			break;
		}
		if (flag)
		{
			Transition(systemTabLink);
		}
		else
		{
			Transition(slotPageLink);
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
