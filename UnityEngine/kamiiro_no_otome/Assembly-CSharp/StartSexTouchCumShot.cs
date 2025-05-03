using System.Linq;
using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class StartSexTouchCumShot : StateBehaviour
{
	private SexTouchManager sexTouchManager;

	private SexTouchStatusManager sexTouchStatusManager;

	private SexTouchHeroineDataManager sexTouchHeroineDataManager;

	private bool isSkiped;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchManager = GameObject.Find("Sex Touch Manager").GetComponent<SexTouchManager>();
		sexTouchStatusManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchStatusManager>();
		sexTouchHeroineDataManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchHeroineDataManager>();
	}

	public override void OnStateBegin()
	{
		isSkiped = false;
		sexTouchManager.touchCanvasGroup.interactable = false;
		sexTouchStatusManager.isFellatioClick = true;
		sexTouchManager.dialogCanvas.SetActive(value: false);
		Button[] commandButtonArray = sexTouchManager.commandButtonArray;
		for (int i = 0; i < commandButtonArray.Length; i++)
		{
			commandButtonArray[i].gameObject.SetActive(value: false);
		}
		commandButtonArray = sexTouchManager.bodyCategoryButtonArray;
		for (int i = 0; i < commandButtonArray.Length; i++)
		{
			commandButtonArray[i].gameObject.SetActive(value: false);
		}
		GameObject[] heroineSkillWindowArray = sexTouchManager.heroineSkillWindowArray;
		for (int i = 0; i < heroineSkillWindowArray.Length; i++)
		{
			heroineSkillWindowArray[i].SetActive(value: false);
		}
		sexTouchManager.inputInfoFrame.SetActive(value: false);
		sexTouchManager.menuButton.gameObject.SetActive(value: false);
		sexTouchManager.guideButton.SetActive(value: false);
		sexTouchManager.touchSeButton.SetActive(value: false);
		sexTouchManager.bodyClickInfoWindowGo.SetActive(value: false);
		sexTouchManager.touchAreaGroupParentGo.GetComponent<CanvasGroup>().alpha = 0f;
		heroineSkillWindowArray = sexTouchManager.touchAreaPointGoArray;
		for (int i = 0; i < heroineSkillWindowArray.Length; i++)
		{
			heroineSkillWindowArray[i].GetComponent<ArborFSM>().enabled = false;
		}
		Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		Sprite sprite = null;
		SexTouchBgSpriteData sexTouchBgSpriteData = sexTouchManager.sexTouchBgDataBase.sexTouchBgSpriteDataList.Find((SexTouchBgSpriteData data) => data.placeName == PlayerDataManager.currentPlaceName);
		switch (PlayerDataManager.currentTimeZone)
		{
		case 0:
		case 1:
			sprite = sexTouchBgSpriteData.sexTouchBgFaceList[0];
			break;
		case 2:
			sprite = sexTouchBgSpriteData.sexTouchBgFaceList[1];
			break;
		case 3:
			sprite = sexTouchBgSpriteData.sexTouchBgFaceList[2];
			break;
		}
		sexTouchManager.touchBgSpriteGo.GetComponent<SpriteRenderer>().sprite = sprite;
		sexTouchManager.touchHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexTouchHeroineDataManager.sexTouchHeroineSpriteData.heroineCgFellatioList[0];
		SexTouchClickData sexTouchClickData = GameDataManager.instance.sexTouchClickDataBase.sexTouchClickDataList.Where((SexTouchClickData data) => data.bodyCategory == SexTouchClickData.BodyCategory.fellatio && data.characterID == PlayerNonSaveDataManager.selectSexBattleHeroineId).ToList().Find((SexTouchClickData data) => data.areaIndex == 1);
		Debug.Log("身体検査フェラクリック位置：" + sexTouchClickData.characterName + "／Vector2_x：" + sexTouchClickData.areaPointVector2.x + "／Vector2_y：" + sexTouchClickData.areaPointVector2.y);
		sexTouchManager.touchAreaPointGoArray[0].GetComponent<RectTransform>().anchoredPosition = sexTouchClickData.areaPointVector2;
		sexTouchStatusManager.playerLibidoGroupGo.SetActive(value: true);
		sexTouchStatusManager.playerLibidoHeartRect.localScale = new Vector2(0.2f, 0.2f);
		sexTouchManager.skipInfoFrame.SetActive(value: true);
		sexTouchManager.textVisibleButtonGo.SetActive(value: true);
		sexTouchManager.textVisibleButtonGo.GetComponent<PlayMakerFSM>().SendEvent("InitializeSexTouchTextVisible");
		if (PlayerDataManager.isHeroineSexVoiceLowStage)
		{
			sexTouchStatusManager.heroineSexLvStage = SexTouchStatusManager.HeroineSexLvStage.A;
		}
		string text = "";
		string text2 = "Voice_Fellatio_Start_" + PlayerNonSaveDataManager.selectSexBattleHeroineId;
		int num = Random.Range(1, 3);
		text = "voice_Fellatio" + PlayerNonSaveDataManager.selectSexBattleHeroineId + "_Start_" + num;
		sexTouchManager.cumShotTextLoc.gameObject.SetActive(value: true);
		sexTouchManager.cumShotTextLoc.Term = "voice_Fellatio" + PlayerNonSaveDataManager.selectSexBattleHeroineId + "_Start_" + num;
		PlaySoundResult playSoundResult = MasterAudio.PlaySound(text2, 1f, null, 0f, text + "(Clone)", null);
		Debug.Log("音声グループ：" + text2 + "／バリエーション：" + text);
		if (playSoundResult != null && playSoundResult.SoundPlayed)
		{
			playSoundResult.ActingVariation.SoundFinished += InvokeMethod;
		}
	}

	public override void OnStateEnd()
	{
		sexTouchManager.skipInfoFrame.SetActive(value: false);
	}

	public override void OnStateUpdate()
	{
		if (Input.GetButtonDown("Fire2"))
		{
			isSkiped = true;
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}

	private void InvokeMethod()
	{
		if (!isSkiped)
		{
			Transition(stateLink);
		}
	}
}
