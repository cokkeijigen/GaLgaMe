using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class SexTouchInitializeData : StateBehaviour
{
	private WareChangeManager wareChangeManager;

	private SexTouchManager sexTouchManager;

	private SexTouchStatusManager sexTouchStatusManager;

	private SexTouchHeroineDataManager sexTouchHeroineDataManager;

	private bool isInitialized;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		wareChangeManager = GameObject.Find("Ware Change Manager").GetComponent<WareChangeManager>();
		sexTouchManager = GameObject.Find("Sex Touch Manager").GetComponent<SexTouchManager>();
		sexTouchStatusManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchStatusManager>();
		sexTouchHeroineDataManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchHeroineDataManager>();
	}

	public override void OnStateBegin()
	{
		PlayerNonSaveDataManager.currentSceneName = "sex";
		wareChangeManager.wareChangeCanvasGo.SetActive(value: false);
		sexTouchManager.touchCanvas.SetActive(value: true);
		sexTouchManager.battleCanvas.SetActive(value: false);
		sexTouchManager.dialogCanvas.SetActive(value: false);
		sexTouchManager.textVisibleButtonGo.SetActive(value: false);
		sexTouchManager.cumShotTextLoc.gameObject.SetActive(value: false);
		sexTouchStatusManager.heroineLibidoPoint = 0f;
		sexTouchStatusManager.heroineLibidoHeartRect.localScale = new Vector2(0.2f, 0.2f);
		sexTouchStatusManager.playerLibidoGroupGo.SetActive(value: false);
		sexTouchStatusManager.beforePlayerLibido = PlayerDataManager.playerLibido;
		PlayerNonSaveDataManager.isSexEnd = true;
		sexTouchManager.selectBodyCategoryNum = 0;
		sexTouchManager.SetBodyCategoryButtonInteractable();
		sexTouchManager.bodyClickInfoWindowGo.SetActive(value: false);
		sexTouchManager.inputInfoFrame.SetActive(value: true);
		sexTouchManager.cumShotInfoFrame.SetActive(value: false);
		sexTouchManager.skipInfoFrame.SetActive(value: false);
		sexTouchManager.commandButtonArray[0].gameObject.SetActive(value: false);
		sexTouchManager.commandButtonArray[1].gameObject.SetActive(value: false);
		sexTouchManager.isTouchAreaGuideVisible = false;
		sexTouchManager.guideButton.GetComponent<PlayMakerFSM>().SendEvent("InitializeGuideButton");
		sexTouchManager.isTouchSePlaying = false;
		sexTouchManager.touchSeButton.GetComponent<PlayMakerFSM>().SendEvent("InitializeTouchSeButton");
		sexTouchStatusManager.touchVoiceCurrentTime = sexTouchStatusManager.touchVoiceThresholdTime;
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
		sexTouchManager.touchHeroineBeforeSprite.color = new Color(1f, 1f, 1f, 0f);
		string path = "Sex Sprite/sexTouchHeroineSpriteData_" + PlayerNonSaveDataManager.selectSexBattleHeroineId;
		sexTouchHeroineDataManager.sexTouchHeroineSpriteData = Resources.Load<SexTouchHeroineSpriteData>(path);
		path = "Sex Voice/DynamicSoundGroup_SexTouch_" + PlayerNonSaveDataManager.selectSexBattleHeroineId;
		sexTouchHeroineDataManager.dynamicSoundGroup = Resources.Load<DynamicSoundGroupCreator>(path);
		sexTouchManager.touchHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexTouchHeroineDataManager.sexTouchHeroineSpriteData.heroineCgHeadBaseList[0];
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (sexTouchHeroineDataManager.dynamicSoundGroup != null && !isInitialized)
		{
			isInitialized = true;
			Object.Instantiate(sexTouchHeroineDataManager.dynamicSoundGroup);
			GameObject.Find("Bgm Play Manager").GetComponent<MasterAudioCustomManager>().ChangeMasterAudioVoiceVolume();
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
