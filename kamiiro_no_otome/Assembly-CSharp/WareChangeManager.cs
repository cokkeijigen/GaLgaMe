using I2.Loc;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class WareChangeManager : SerializedMonoBehaviour
{
	public enum LvStage
	{
		A,
		B
	}

	public SexTouchManager sexTouchManager;

	public WareChangeClickDataBase wareChangeClickDataBase;

	public WareChangeClickData CurrentNudeClickData;

	public WareChangeClickData CurrentWareClickData;

	public GameObject wareChangeCanvasGo;

	public GameObject nudeChangeButtonGo;

	public GameObject wareChangeButtonGo;

	public Image wareChangeBgImage;

	public Image heroineImage;

	public RectTransform balloonRect;

	public Localize balloonTextLoc;

	public LvStage lvStage;

	public bool isSexyWare;

	private int currentListIndex;

	private void Awake()
	{
		wareChangeCanvasGo.SetActive(value: false);
	}

	public bool CheckHeroineEnableWareChange()
	{
		bool result = false;
		int heroineID = PlayerDataManager.DungeonHeroineFollowNum;
		CharacterStatusData characterStatusData = GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == heroineID);
		if (PlayerFlagDataManager.scenarioFlagDictionary[characterStatusData.characterSexyWareFlag])
		{
			result = true;
		}
		Debug.Log("チェックするヒロインID：" + heroineID + "／着替え可能：" + result);
		return result;
	}

	public void SetWareChangeBgImage()
	{
		int heroineID = PlayerDataManager.DungeonHeroineFollowNum;
		WareChangeClickData wareChangeClickData = wareChangeClickDataBase.wareChangeClickDataList.Find((WareChangeClickData data) => data.characterID == heroineID);
		string currentAccessPointName = PlayerDataManager.currentAccessPointName;
		if (!(currentAccessPointName == "Kingdom1"))
		{
			if (currentAccessPointName == "City1")
			{
				switch (PlayerDataManager.currentTimeZone)
				{
				case 0:
				case 1:
					wareChangeBgImage.sprite = wareChangeClickData.wareChange_City1BgList[0];
					break;
				case 2:
					wareChangeBgImage.sprite = wareChangeClickData.wareChange_City1BgList[1];
					break;
				case 3:
					wareChangeBgImage.sprite = wareChangeClickData.wareChange_City1BgList[2];
					break;
				}
			}
		}
		else
		{
			switch (PlayerDataManager.currentTimeZone)
			{
			case 0:
			case 1:
				wareChangeBgImage.sprite = wareChangeClickData.wareChange_Kingdom1BgList[0];
				break;
			case 2:
				wareChangeBgImage.sprite = wareChangeClickData.wareChange_Kingdom1BgList[1];
				break;
			case 3:
				wareChangeBgImage.sprite = wareChangeClickData.wareChange_Kingdom1BgList[2];
				break;
			}
		}
	}

	public void PushStartSexTouchButton()
	{
		sexTouchManager.dialogLocText.gameObject.SetActive(value: true);
		sexTouchManager.menstrualDialogTextGroup.SetActive(value: false);
		sexTouchManager.dialogLocText.Term = "alertStartSexTouch";
		sexTouchManager.dialogTouchButtonGroup.SetActive(value: true);
		sexTouchManager.dialogBattleButtonGroup.SetActive(value: false);
		sexTouchManager.dialogMenstrualButtonGroup.SetActive(value: false);
		sexTouchManager.dialogCumShotButtonGroup.SetActive(value: false);
		sexTouchManager.dialogHeroineVoiceToggleGroup.SetActive(value: false);
		sexTouchManager.dialogCanvas.SetActive(value: true);
	}

	public void PushSexTouchDialogOkButton()
	{
		sexTouchManager.touchCanvas.SetActive(value: true);
		wareChangeCanvasGo.SetActive(value: false);
		sexTouchManager.sexTouchArborFSM.SendTrigger("StartSexTouch");
		sexTouchManager.dialogCanvas.SetActive(value: false);
	}

	public void InitializeWareChangeCharacter()
	{
		int heroineID = PlayerDataManager.DungeonHeroineFollowNum;
		PlayerSexStatusDataManager.SetUpPlayerSexStatus(isBattle: false);
		if (PlayerSexStatusDataManager.heroineSexLv[heroineID - 1] < 4)
		{
			lvStage = LvStage.A;
		}
		else
		{
			lvStage = LvStage.B;
		}
		isSexyWare = false;
		currentListIndex = 0;
		nudeChangeButtonGo.SetActive(value: false);
		Debug.Log("ヌードボタン表示：" + nudeChangeButtonGo.activeInHierarchy);
		CurrentNudeClickData = wareChangeClickDataBase.wareChangeClickDataList.Find((WareChangeClickData data) => data.characterID == heroineID && data.levelStage.ToString() == lvStage.ToString() && data.wareStatus == WareChangeClickData.WareStatus.nude);
		CurrentWareClickData = wareChangeClickDataBase.wareChangeClickDataList.Find((WareChangeClickData data) => data.characterID == heroineID && data.levelStage.ToString() == lvStage.ToString() && data.wareStatus == WareChangeClickData.WareStatus.ware);
		heroineImage.sprite = CurrentNudeClickData.wareChange_IdolSprite;
		heroineImage.GetComponent<RectTransform>().anchoredPosition = CurrentNudeClickData.characterPositionVector2;
		heroineImage.GetComponent<RectTransform>().localScale = new Vector3(CurrentNudeClickData.characterImageScale, CurrentNudeClickData.characterImageScale, CurrentNudeClickData.characterImageScale);
		balloonRect.gameObject.SetActive(value: false);
		Debug.Log("フキダシ表示：" + balloonRect.gameObject.activeInHierarchy);
		balloonRect.anchoredPosition = CurrentNudeClickData.BalloonPositionVector2;
		balloonTextLoc.Term = CurrentNudeClickData.wareChange_TermList[0];
		Debug.Log("ヒロインの立ち絵の初期化完了");
	}

	public void ChangeCharacterImage()
	{
		int num = 0;
		int num2 = currentListIndex;
		if (!isSexyWare)
		{
			num = CurrentNudeClickData.wareChange_SpiteList.Count;
			Debug.Log("裸のセリフ変更");
		}
		else
		{
			num = CurrentWareClickData.wareChange_SpiteList.Count;
			Debug.Log("衣装のセリフ変更");
		}
		while (num2 == currentListIndex)
		{
			num2 = Random.Range(0, num);
		}
		currentListIndex = num2;
		if (!isSexyWare)
		{
			heroineImage.sprite = CurrentNudeClickData.wareChange_SpiteList[num2];
			heroineImage.GetComponent<RectTransform>().anchoredPosition = CurrentNudeClickData.characterPositionVector2;
			heroineImage.GetComponent<RectTransform>().localScale = new Vector3(CurrentNudeClickData.characterImageScale, CurrentNudeClickData.characterImageScale, CurrentNudeClickData.characterImageScale);
			balloonTextLoc.Term = CurrentNudeClickData.wareChange_TermList[num2];
			balloonRect.anchoredPosition = CurrentNudeClickData.BalloonPositionVector2;
		}
		else
		{
			heroineImage.sprite = CurrentWareClickData.wareChange_SpiteList[num2];
			heroineImage.GetComponent<RectTransform>().anchoredPosition = CurrentWareClickData.characterPositionVector2;
			heroineImage.GetComponent<RectTransform>().localScale = new Vector3(CurrentWareClickData.characterImageScale, CurrentWareClickData.characterImageScale, CurrentWareClickData.characterImageScale);
			balloonTextLoc.Term = CurrentWareClickData.wareChange_TermList[num2];
			balloonRect.anchoredPosition = CurrentWareClickData.BalloonPositionVector2;
		}
		balloonRect.gameObject.SetActive(value: true);
	}

	public void CloseWareChangeTalkBallon()
	{
		if (!isSexyWare)
		{
			heroineImage.sprite = CurrentNudeClickData.wareChange_IdolSprite;
			heroineImage.GetComponent<RectTransform>().anchoredPosition = CurrentNudeClickData.characterPositionVector2;
			heroineImage.GetComponent<RectTransform>().localScale = new Vector3(CurrentNudeClickData.characterImageScale, CurrentNudeClickData.characterImageScale, CurrentNudeClickData.characterImageScale);
		}
		else
		{
			heroineImage.sprite = CurrentWareClickData.wareChange_IdolSprite;
			heroineImage.GetComponent<RectTransform>().anchoredPosition = CurrentWareClickData.characterPositionVector2;
			heroineImage.GetComponent<RectTransform>().localScale = new Vector3(CurrentWareClickData.characterImageScale, CurrentWareClickData.characterImageScale, CurrentWareClickData.characterImageScale);
		}
		balloonRect.gameObject.SetActive(value: false);
	}

	public void ChangeCharacterWare(bool isNudeToWare)
	{
		currentListIndex = 0;
		if (isNudeToWare)
		{
			heroineImage.sprite = CurrentWareClickData.wareChange_IdolSprite;
			heroineImage.GetComponent<RectTransform>().anchoredPosition = CurrentWareClickData.characterPositionVector2;
			heroineImage.GetComponent<RectTransform>().localScale = new Vector3(CurrentWareClickData.characterImageScale, CurrentWareClickData.characterImageScale, CurrentWareClickData.characterImageScale);
			balloonTextLoc.Term = CurrentWareClickData.wareChange_TermList[0];
			balloonRect.anchoredPosition = CurrentWareClickData.BalloonPositionVector2;
			nudeChangeButtonGo.SetActive(value: true);
			wareChangeButtonGo.SetActive(value: false);
			isSexyWare = true;
			Debug.Log("着替えボタン押下");
		}
		else
		{
			heroineImage.sprite = CurrentNudeClickData.wareChange_IdolSprite;
			heroineImage.GetComponent<RectTransform>().anchoredPosition = CurrentNudeClickData.characterPositionVector2;
			heroineImage.GetComponent<RectTransform>().localScale = new Vector3(CurrentNudeClickData.characterImageScale, CurrentNudeClickData.characterImageScale, CurrentNudeClickData.characterImageScale);
			balloonTextLoc.Term = CurrentNudeClickData.wareChange_TermList[0];
			balloonRect.anchoredPosition = CurrentNudeClickData.BalloonPositionVector2;
			nudeChangeButtonGo.SetActive(value: false);
			wareChangeButtonGo.SetActive(value: true);
			isSexyWare = false;
			Debug.Log("脱衣ボタン押下");
		}
		balloonRect.gameObject.SetActive(value: false);
	}
}
