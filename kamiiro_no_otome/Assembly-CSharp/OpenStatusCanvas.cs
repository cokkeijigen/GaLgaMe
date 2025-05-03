using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class OpenStatusCanvas : StateBehaviour
{
	private StatusManager statusManager;

	private StatusCustomManager statusCustomManager;

	private SexStatusManager sexStatusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GameObject.Find("Status Manager").GetComponentInParent<StatusManager>();
		statusCustomManager = GameObject.Find("Status Custom Manager").GetComponentInParent<StatusCustomManager>();
		sexStatusManager = GameObject.Find("Sex Status Manager").GetComponent<SexStatusManager>();
	}

	public override void OnStateBegin()
	{
		PlayerEquipDataManager.CheckPlayerSkillLevelUnLock();
		Image[] menuButtonArray = statusManager.menuButtonArray;
		for (int i = 0; i < menuButtonArray.Length; i++)
		{
			menuButtonArray[i].sprite = statusManager.menuButtonSpriteArray[0];
		}
		statusManager.statusCanvasArray[0].SetActive(value: true);
		statusManager.statusCanvasArray[1].SetActive(value: true);
		statusCustomManager.statusOverlayCanvas.SetActive(value: false);
		sexStatusManager.passiveAlertFrameGo.SetActive(value: false);
		sexStatusManager.sexLvLockAlertFrameGo.SetActive(value: false);
		statusManager.SetCharacterButtonVisible();
		statusManager.characterJoinIdList.Clear();
		for (int j = 0; j < 5; j++)
		{
			string characterDungeonFollowUnLockFlag = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[j].characterDungeonFollowUnLockFlag;
			if (PlayerFlagDataManager.scenarioFlagDictionary[characterDungeonFollowUnLockFlag])
			{
				statusManager.characterJoinIdList.Add(j);
			}
		}
		statusManager.characterSexUnlockIdList.Clear();
		int k;
		for (k = 0; k < 5; k++)
		{
			CharacterStatusData characterStatusData = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == k);
			if (PlayerFlagDataManager.scenarioFlagDictionary[characterStatusData.characterDungeonSexUnLockFlag])
			{
				statusManager.characterSexUnlockIdList.Add(k);
			}
		}
		switch (PlayerNonSaveDataManager.selectStatusCanvasName)
		{
		case "equipItem":
		{
			GameObject[] itemViewArray = sexStatusManager.sexStatusViewArray;
			for (int i = 0; i < itemViewArray.Length; i++)
			{
				itemViewArray[i].SetActive(value: false);
			}
			itemViewArray = statusManager.skillViewArray;
			for (int i = 0; i < itemViewArray.Length; i++)
			{
				itemViewArray[i].SetActive(value: false);
			}
			itemViewArray = statusManager.itemViewArray;
			for (int i = 0; i < itemViewArray.Length; i++)
			{
				itemViewArray[i].SetActive(value: true);
			}
			statusManager.selectItemCategoryNum = 7;
			statusManager.itemCategoryTabGoArray[0].SetActive(value: true);
			statusManager.itemCategoryTabGoArray[1].SetActive(value: false);
			statusManager.menuButtonArray[0].sprite = statusManager.menuButtonSpriteArray[1];
			Transition(stateLink);
			break;
		}
		case "item":
		{
			GameObject[] itemViewArray = sexStatusManager.sexStatusViewArray;
			for (int i = 0; i < itemViewArray.Length; i++)
			{
				itemViewArray[i].SetActive(value: false);
			}
			itemViewArray = statusManager.skillViewArray;
			for (int i = 0; i < itemViewArray.Length; i++)
			{
				itemViewArray[i].SetActive(value: false);
			}
			itemViewArray = statusManager.itemViewArray;
			for (int i = 0; i < itemViewArray.Length; i++)
			{
				itemViewArray[i].SetActive(value: true);
			}
			statusManager.selectItemCategoryNum = 0;
			statusManager.itemCategoryTabGoArray[0].SetActive(value: false);
			statusManager.itemCategoryTabGoArray[1].SetActive(value: true);
			statusManager.menuButtonArray[1].sprite = statusManager.menuButtonSpriteArray[1];
			Transition(stateLink);
			break;
		}
		case "skill":
		{
			GameObject[] itemViewArray = statusManager.itemViewArray;
			for (int i = 0; i < itemViewArray.Length; i++)
			{
				itemViewArray[i].SetActive(value: false);
			}
			itemViewArray = sexStatusManager.sexStatusViewArray;
			for (int i = 0; i < itemViewArray.Length; i++)
			{
				itemViewArray[i].SetActive(value: false);
			}
			itemViewArray = statusManager.skillViewArray;
			for (int i = 0; i < itemViewArray.Length; i++)
			{
				itemViewArray[i].SetActive(value: true);
			}
			statusManager.menuButtonArray[2].sprite = statusManager.menuButtonSpriteArray[1];
			statusCustomManager.statusOverlayCanvas.SetActive(value: true);
			statusCustomManager.overlayBlackImageGo.SetActive(value: false);
			statusCustomManager.customWindowArray[0].SetActive(value: false);
			statusCustomManager.customWindowArray[1].SetActive(value: false);
			statusCustomManager.customWindowArray[2].SetActive(value: true);
			statusManager.skillFSM.SendTrigger("OpenSkillCanvas");
			break;
		}
		case "sexStatus":
		{
			PlayerSexStatusDataManager.SetUpPlayerSexStatus(isBattle: false);
			GameObject[] itemViewArray = statusManager.itemViewArray;
			for (int i = 0; i < itemViewArray.Length; i++)
			{
				itemViewArray[i].SetActive(value: false);
			}
			itemViewArray = statusManager.skillViewArray;
			for (int i = 0; i < itemViewArray.Length; i++)
			{
				itemViewArray[i].SetActive(value: false);
			}
			itemViewArray = sexStatusManager.sexStatusViewArray;
			for (int i = 0; i < itemViewArray.Length; i++)
			{
				itemViewArray[i].SetActive(value: true);
			}
			statusManager.menuButtonArray[3].sprite = statusManager.menuButtonSpriteArray[1];
			statusCustomManager.statusOverlayCanvas.SetActive(value: true);
			statusCustomManager.overlayBlackImageGo.SetActive(value: false);
			statusCustomManager.customWindowArray[0].SetActive(value: false);
			statusCustomManager.customWindowArray[1].SetActive(value: false);
			statusCustomManager.customWindowArray[2].SetActive(value: false);
			if (!statusManager.CheckCharacterSexUnLock(statusManager.selectCharacterNum))
			{
				sexStatusManager.selectSexSkillCharacterTabIndex = 0;
				statusManager.selectCharacterListIndex = 0;
			}
			else
			{
				switch (statusManager.selectCharacterNum)
				{
				case 0:
					sexStatusManager.selectSexSkillCharacterTabIndex = statusManager.selectCharacterNum;
					break;
				case 2:
				case 3:
				case 4:
					sexStatusManager.selectSexSkillCharacterTabIndex = statusManager.selectCharacterNum - 1;
					break;
				}
			}
			statusManager.sexStatusFSM.SendTrigger("OpenSexStatusCanvas");
			break;
		}
		case "close":
			statusManager.statusCanvasArray[0].SetActive(value: false);
			statusManager.statusCanvasArray[1].SetActive(value: false);
			statusCustomManager.statusOverlayCanvas.SetActive(value: false);
			break;
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
