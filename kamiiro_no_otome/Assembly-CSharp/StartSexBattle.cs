using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class StartSexBattle : StateBehaviour
{
	private WareChangeManager wareChangeManager;

	private SexTouchManager sexTouchManager;

	private SexTouchStatusManager sexTouchStatusManager;

	private SexTouchHeroineDataManager sexTouchHeroineDataManager;

	private SexBattleManager sexBattleManager;

	private SexBattleFertilizationManager sexBattleFertilizationManager;

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
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		sexBattleFertilizationManager = GameObject.Find("SexBattle Fertilization Manager").GetComponent<SexBattleFertilizationManager>();
	}

	public override void OnStateBegin()
	{
		sexBattleFertilizationManager.InitializeFertilization();
		sexTouchStatusManager.SetHeroineSexLvStage();
		if (PlayerDataManager.isHeroineSexVoiceLowStage)
		{
			sexTouchStatusManager.heroineSexLvStage = SexTouchStatusManager.HeroineSexLvStage.A;
		}
		wareChangeManager.wareChangeCanvasGo.SetActive(value: false);
		sexTouchManager.touchCanvas.SetActive(value: false);
		sexTouchManager.battleCanvas.SetActive(value: true);
		sexTouchManager.dialogCanvas.SetActive(value: false);
		sexBattleManager.isSexBattleTextVisible = true;
		sexBattleManager.textVisibleButtonGo.GetComponent<PlayMakerFSM>().SendEvent("InitializeSexBattleTextVisble");
		sexBattleManager.pistonInfoWindow.SetActive(value: false);
		sexBattleManager.commandInfoFrameGo.SetActive(value: false);
		string text = "";
		text = ((PlayerDataManager.mapPlaceStatusNum != 2) ? "_dungeon" : "_town");
		sexBattleManager.battleHeroineBeforeSprite.color = new Color(1f, 1f, 1f, 0f);
		string path = "Sex Sprite/sexHeroineSpriteData_" + PlayerNonSaveDataManager.selectSexBattleHeroineId + text;
		sexBattleManager.sexHeroineSpriteData = Resources.Load<SexBattleHeroineSpriteData>(path);
		if (PlayerNonSaveDataManager.isMenstrualDaySexUseCondom)
		{
			sexBattleManager.battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexBattleManager.sexHeroineSpriteData.heroineCgCondomIdleList[0];
		}
		else
		{
			sexBattleManager.battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexBattleManager.sexHeroineSpriteData.heroineCgIdleList[0];
		}
		Sprite sprite = null;
		SexTouchBgSpriteData sexTouchBgSpriteData = sexTouchManager.sexTouchBgDataBase.sexTouchBgSpriteDataList.Find((SexTouchBgSpriteData data) => data.placeName == PlayerDataManager.currentPlaceName);
		switch (PlayerDataManager.currentTimeZone)
		{
		case 0:
		case 1:
			sprite = sexTouchBgSpriteData.sexBattleBgList[0];
			break;
		case 2:
			sprite = sexTouchBgSpriteData.sexBattleBgList[1];
			break;
		case 3:
			sprite = sexTouchBgSpriteData.sexBattleBgList[2];
			break;
		}
		sexBattleManager.battleBgSpriteGo.GetComponent<SpriteRenderer>().sprite = sprite;
		sexBattleManager.effectPrefabParentDictionary["head"].anchoredPosition = new Vector2(sexBattleManager.sexHeroineSpriteData.headPointV2.x, sexBattleManager.sexHeroineSpriteData.headPointV2.y);
		sexBattleManager.effectPrefabParentDictionary["tits"].anchoredPosition = new Vector2(sexBattleManager.sexHeroineSpriteData.titsPointV2.x, sexBattleManager.sexHeroineSpriteData.titsPointV2.y);
		sexBattleManager.effectPrefabParentDictionary["vagina"].anchoredPosition = new Vector2(sexBattleManager.sexHeroineSpriteData.vaginaPointV2.x, sexBattleManager.sexHeroineSpriteData.vaginaPointV2.y);
		sexTouchStatusManager.beforePlayerLibido = PlayerDataManager.playerLibido;
		sexBattleManager.battleSpeed = 1;
		sexBattleManager.speedTmpGo.text = "1";
		for (int i = 0; i < sexBattleManager.isFinishDone.Length; i++)
		{
			sexBattleManager.isFinishDone[i] = false;
		}
		sexBattleManager.sexBattleEffectManager.ResetBuffAndSubPowerIcon(targetIsHeroine: false);
		sexBattleManager.sexBattleEffectManager.ResetBuffAndSubPowerIcon(targetIsHeroine: true);
		sexBattleManager.sexBattleMessageTextManager.ResetBattleTextMessage();
		PlayerNonSaveDataManager.battleResultDialogType = "sexBattle";
		PlayerSexStatusDataManager.SetUpPlayerSexStatus(isBattle: true);
		sexBattleManager.SetPlayerUseSkillList();
		sexBattleManager.SetHeroineUseSkillList();
		SexBattleInitializeEnd();
		SpawnSkillButtonGo();
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

	private void SpawnSkillButtonGo()
	{
		SexSkillData sexSkillData = null;
		int skillID = 0;
		bool flag = false;
		for (int i = 0; i < sexBattleManager.playerPistonSkillList.Count; i++)
		{
			skillID = sexBattleManager.playerPistonSkillList[i].skillID;
			sexSkillData = GameDataManager.instance.sexSkillDataBase.sexSkillDataList.Find((SexSkillData data) => data.skillID == skillID);
			if (skillID == 10)
			{
				if (!PlayerFlagDataManager.CheckScenarioFlagIsClear(GameDataManager.instance.characterStatusDataBase.characterStatusDataList.Find((CharacterStatusData data) => data.characteID == PlayerNonSaveDataManager.selectSexBattleHeroineId).characterBerserkPistonFlag))
				{
					Debug.Log("バーサークピストン未解放");
					continue;
				}
				Debug.Log("バーサークピストン解放済み");
			}
			if (skillID == 200)
			{
				if (!PlayerNonSaveDataManager.isSexHeroineMenstrualDay || !PlayerNonSaveDataManager.isSexHeroineEnableFertilization || PlayerNonSaveDataManager.isMenstrualDaySexUseCondom)
				{
					flag = false;
					continue;
				}
				flag = true;
			}
			else
			{
				flag = false;
			}
			Transform transform = PoolManager.Pools["sexBattlePool"].Spawn(sexBattleManager.skillButtonPrefabGo, sexBattleManager.skillButtonGroupParentArray[0].transform);
			transform.localScale = new Vector2(1f, 1f);
			ParameterContainer component = transform.GetComponent<ParameterContainer>();
			component.SetInt("skillID", skillID);
			component.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = "sexSkill" + skillID;
			component.GetVariable<UguiTextVariable>("reChargeTurnText").text.text = "0";
			if (sexSkillData.skillNeedTrance > 30)
			{
				CanvasGroup component2 = transform.GetComponent<CanvasGroup>();
				component2.interactable = false;
				component2.alpha = 0.5f;
			}
			Sprite sprite = GameDataManager.instance.itemCategoryDataBase.skillCategoryIconDictionary[sexSkillData.skillType.ToString()];
			component.GetVariable<UguiImage>("iconImage").image.sprite = sprite;
			if (flag)
			{
				transform.SetParent(sexBattleManager.skillButtonGroupParentArray[1].transform);
			}
		}
		for (int j = 0; j < sexBattleManager.playerCaressSkillList.Count; j++)
		{
			Transform transform2 = PoolManager.Pools["sexBattlePool"].Spawn(sexBattleManager.skillButtonPrefabGo, sexBattleManager.skillButtonGroupParentArray[1].transform);
			transform2.localScale = new Vector2(1f, 1f);
			skillID = sexBattleManager.playerCaressSkillList[j].skillID;
			sexSkillData = GameDataManager.instance.sexSkillDataBase.sexSkillDataList.Find((SexSkillData data) => data.skillID == skillID);
			ParameterContainer component3 = transform2.GetComponent<ParameterContainer>();
			component3.SetInt("skillID", skillID);
			component3.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = "sexSkill" + skillID;
			component3.GetVariable<UguiTextVariable>("reChargeTurnText").text.text = "0";
			if (sexSkillData.skillNeedTrance > 30)
			{
				CanvasGroup component4 = transform2.GetComponent<CanvasGroup>();
				component4.interactable = false;
				component4.alpha = 0.5f;
			}
			Sprite sprite2 = GameDataManager.instance.itemCategoryDataBase.skillCategoryIconDictionary[sexSkillData.skillType.ToString()];
			component3.GetVariable<UguiImage>("iconImage").image.sprite = sprite2;
		}
		for (int k = 0; k < sexBattleManager.playerHealSkillList.Count; k++)
		{
			Transform transform3 = PoolManager.Pools["sexBattlePool"].Spawn(sexBattleManager.skillButtonPrefabGo, sexBattleManager.skillButtonGroupParentArray[2].transform);
			transform3.localScale = new Vector2(1f, 1f);
			skillID = sexBattleManager.playerHealSkillList[k].skillID;
			sexSkillData = GameDataManager.instance.sexSkillDataBase.sexSkillDataList.Find((SexSkillData data) => data.skillID == skillID);
			ParameterContainer component5 = transform3.GetComponent<ParameterContainer>();
			component5.SetInt("skillID", skillID);
			component5.GetVariable<I2LocalizeComponent>("nameTextLoc").localize.Term = "sexSkill" + skillID;
			component5.GetVariable<UguiTextVariable>("reChargeTurnText").text.text = "0";
			if (sexSkillData.skillNeedTrance > 30)
			{
				CanvasGroup component6 = transform3.GetComponent<CanvasGroup>();
				component6.interactable = false;
				component6.alpha = 0.5f;
			}
			Sprite sprite3 = GameDataManager.instance.itemCategoryDataBase.skillCategoryIconDictionary[sexSkillData.skillType.ToString()];
			component5.GetVariable<UguiImage>("iconImage").image.sprite = sprite3;
		}
	}

	private void SexBattleInitializeEnd()
	{
		PlayerSexBattleConditionAccess.SexBattleConditionInititialize();
		for (int i = 0; i < 2; i++)
		{
			sexBattleManager.playerHpImageArray[i].fillAmount = 0.94f;
			sexBattleManager.playerTranceImageArray[i].localScale = new Vector2(0.3f, 0.3f);
			sexBattleManager.playerExtasyLimitTextArray[i].text = PlayerSexStatusDataManager.playerSexExtasyLimit[i].ToString();
		}
		sexBattleManager.playerCurrentHpTextArray[0].text = PlayerSexStatusDataManager.playerSexHp[0].ToString();
		sexBattleManager.playerCurrentHpTextArray[1].text = PlayerSexStatusDataManager.playerSexHp[PlayerNonSaveDataManager.selectSexBattleHeroineId].ToString();
		sexBattleManager.playerMaxHpTextArray[0].text = PlayerSexStatusDataManager.playerSexHp[0].ToString();
		sexBattleManager.playerMaxHpTextArray[1].text = PlayerSexStatusDataManager.playerSexHp[PlayerNonSaveDataManager.selectSexBattleHeroineId].ToString();
		sexBattleManager.skillWindowGo.SetActive(value: false);
		sexBattleManager.blackImageGo.SetActive(value: false);
		sexBattleManager.skillInfoWindowGo.SetActive(value: false);
		sexBattleManager.skipInfoWindow.SetActive(value: false);
		for (int j = 0; j < 3; j++)
		{
			if (sexBattleManager.skillButtonGroupParentArray[j].transform.childCount > 0)
			{
				Transform[] array = new Transform[sexBattleManager.skillButtonGroupParentArray[j].transform.childCount];
				for (int k = 0; k < sexBattleManager.skillButtonGroupParentArray[j].transform.childCount; k++)
				{
					array[k] = sexBattleManager.skillButtonGroupParentArray[j].transform.GetChild(k);
				}
				for (int l = 0; l < array.Length; l++)
				{
					PoolManager.Pools["sexBattlePool"].Despawn(array[l], 0f, sexBattleManager.skillPrefabParent);
				}
			}
		}
		sexBattleManager.resultCanvasGo.SetActive(value: false);
	}
}
