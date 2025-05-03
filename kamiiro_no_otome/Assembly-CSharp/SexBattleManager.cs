using System.Collections.Generic;
using System.Linq;
using Arbor;
using Coffee.UIExtensions;
using I2.Loc;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SexBattleManager : SerializedMonoBehaviour
{
	public Camera sexTouchCamera;

	public SexTouchManager sexTouchManager;

	public SexTouchStatusManager sexTouchStatusManager;

	public SexBattleMessageTextManager sexBattleMessageTextManager;

	public SexBattleTurnManager sexBattleTurnManager;

	public SexBattleEffectManager sexBattleEffectManager;

	public SexBattleFertilizationManager sexBattleFertilizationManager;

	public ArborFSM sexBattleArborFSM;

	public GameObject skillButtonGo;

	public GameObject subMenuGroupGo;

	public GameObject menuButtonGo;

	public GameObject cumShotToggleGo;

	public GameObject textVisibleButtonGo;

	public GameObject pistonInfoWindow;

	public Localize pistonInfoLocText;

	public GameObject skipInfoWindow;

	public Image[] playerHpImageArray;

	public RectTransform[] playerTranceImageArray;

	public TextMeshProUGUI[] playerCurrentHpTextArray;

	public TextMeshProUGUI[] playerMaxHpTextArray;

	public TextMeshProUGUI[] playerExtasyLimitTextArray;

	public Animator[] playerTranceAnimatorArray;

	public GameObject skillWindowGo;

	public GameObject skillWindowContentGo;

	public GameObject blackImageGo;

	public GameObject skillWindowApplyButtonGo;

	public int selectSkillScrollIndex;

	public int selectSkillID;

	public Sprite[] skillScrollSpriteArray;

	public ParameterContainer skillWindowSummaryParameter;

	public GameObject skillInfoWindowGo;

	public ParameterContainer skillInfoWindowSummaryParameter;

	public GameObject skillButtonGroupGo;

	public GameObject[] skillButtonGroupParentArray;

	public GameObject commandInfoFrameGo;

	public SexSkillData selectSexSkillData;

	public SexSkillData heroineSexSkillData;

	public List<SexSkillData> playerPistonSkillList;

	public List<SexSkillData> playerCaressSkillList;

	public List<SexSkillData> playerHealSkillList;

	public List<SexSkillData> heroinePistonSkillList;

	public List<SexSkillData> heroineAttackSkillList;

	public List<SexSkillData> heroineCaressSkillList;

	public List<SexSkillData> heroineHealSkillList;

	public GameObject resultCanvasGo;

	public Image[] resultStarImageArray;

	public Sprite[] resultStarSpriteArray;

	public Transform expPrefabSpawnParrentGo;

	public GameObject expPrefabGo;

	public List<Transform> expPrefabSpawnGoList;

	public LayoutElement kizunaGroupLayoutElement;

	public GameObject talismanBonusGroup;

	public GameObject fertilizeBonusGroup;

	public UIParticle uIParticleSex;

	public GameObject resultEffectPrefabGo;

	public Transform resultEffectSpawnGo;

	public Text ecstasyNumText;

	public Text ecstasyTotalScoreText;

	public Text tranceMagNumText;

	public Text tranceBonusNumText;

	public Text talismanBonusNumText;

	public Text fertilizeBonusNumText;

	public Text getTranceNumText;

	public Text getExpText;

	public List<Slider> expSliderList;

	public List<TextMeshProUGUI> lvTextList;

	public bool isResultAnimationEnd;

	public bool isSexBattleDefeat;

	public SexBattleHeroineSpriteData sexHeroineSpriteData;

	public GameObject battleHeroineSpriteGo;

	public SpriteRenderer battleHeroineBeforeSprite;

	public GameObject battleBgSpriteGo;

	public CanvasGroup whiteImageCanvasGroup;

	public GameObject skillScrollPrefabGo;

	public GameObject skillButtonPrefabGo;

	public Transform skillPrefabParent;

	public Dictionary<string, RectTransform> effectPrefabParentDictionary = new Dictionary<string, RectTransform>();

	public int battleSpeed;

	public TextMeshProUGUI speedTmpGo;

	public bool isCounterCumShot;

	public bool[] isFinishDone = new bool[3];

	public bool isAnalSex;

	public bool isHeroineAbsorb;

	public bool isSkillButtonStay;

	public bool isSexBattleTextVisible;

	public void SetSexTouchCanvasGroupInteractable()
	{
		if (sexTouchManager.touchCanvas.activeInHierarchy)
		{
			sexTouchManager.touchCanvasGroup.interactable = false;
		}
	}

	public void SetPlayerUseSkillList()
	{
		playerPistonSkillList.Clear();
		playerCaressSkillList.Clear();
		playerHealSkillList.Clear();
		for (int i = 0; i < PlayerSexStatusDataManager.playerUseSexActiveSkillList[0].Count; i++)
		{
			SexSkillData sexSkillData = PlayerSexStatusDataManager.playerUseSexActiveSkillList[0][i];
			if (sexSkillData.actionType == SexSkillData.ActionType.piston)
			{
				playerPistonSkillList.Add(sexSkillData);
			}
		}
		for (int j = 0; j < PlayerSexStatusDataManager.playerUseSexActiveSkillList[0].Count; j++)
		{
			SexSkillData sexSkillData2 = PlayerSexStatusDataManager.playerUseSexActiveSkillList[0][j];
			if (sexSkillData2.actionType == SexSkillData.ActionType.kiss || sexSkillData2.actionType == SexSkillData.ActionType.caress)
			{
				playerCaressSkillList.Add(sexSkillData2);
			}
		}
		for (int k = 0; k < PlayerSexStatusDataManager.playerUseSexActiveSkillList[0].Count; k++)
		{
			SexSkillData sexSkillData3 = PlayerSexStatusDataManager.playerUseSexActiveSkillList[0][k];
			if (sexSkillData3.actionType == SexSkillData.ActionType.heal)
			{
				playerHealSkillList.Add(sexSkillData3);
			}
		}
	}

	public void SetHeroineUseSkillList()
	{
		heroinePistonSkillList.Clear();
		heroineAttackSkillList.Clear();
		heroineCaressSkillList.Clear();
		heroineHealSkillList.Clear();
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		for (int i = 0; i < PlayerSexStatusDataManager.playerUseSexActiveSkillList[selectSexBattleHeroineId].Count; i++)
		{
			SexSkillData sexSkillData = PlayerSexStatusDataManager.playerUseSexActiveSkillList[selectSexBattleHeroineId][i];
			if ((sexSkillData.counterType != SexSkillData.CounterType.piston && sexSkillData.counterType != SexSkillData.CounterType.pistonAndCaress && sexSkillData.counterType != SexSkillData.CounterType.all) || sexSkillData.skillNeedTrance < PlayerSexStatusDataManager.playerSexTrance[1])
			{
				continue;
			}
			if (sexSkillData.bodyCategory == SexSkillData.BodyCategory.anal)
			{
				if (isAnalSex)
				{
					heroinePistonSkillList.Add(sexSkillData);
				}
			}
			else if (sexSkillData.bodyCategory == SexSkillData.BodyCategory.vagina)
			{
				if (!isAnalSex)
				{
					heroinePistonSkillList.Add(sexSkillData);
				}
			}
			else
			{
				heroinePistonSkillList.Add(sexSkillData);
			}
		}
		for (int j = 0; j < PlayerSexStatusDataManager.playerUseSexActiveSkillList[selectSexBattleHeroineId].Count; j++)
		{
			SexSkillData sexSkillData2 = PlayerSexStatusDataManager.playerUseSexActiveSkillList[selectSexBattleHeroineId][j];
			if ((sexSkillData2.counterType != SexSkillData.CounterType.piston && sexSkillData2.counterType != SexSkillData.CounterType.pistonAndCaress && sexSkillData2.counterType != SexSkillData.CounterType.all) || sexSkillData2.skillNeedTrance < PlayerSexStatusDataManager.playerSexTrance[1] || sexSkillData2.skillPower <= 0)
			{
				continue;
			}
			if (sexSkillData2.bodyCategory == SexSkillData.BodyCategory.anal)
			{
				if (isAnalSex)
				{
					heroineAttackSkillList.Add(sexSkillData2);
				}
			}
			else if (sexSkillData2.bodyCategory == SexSkillData.BodyCategory.vagina)
			{
				if (!isAnalSex)
				{
					heroineAttackSkillList.Add(sexSkillData2);
				}
			}
			else
			{
				heroineAttackSkillList.Add(sexSkillData2);
			}
		}
		for (int k = 0; k < PlayerSexStatusDataManager.playerUseSexActiveSkillList[selectSexBattleHeroineId].Count; k++)
		{
			SexSkillData sexSkillData3 = PlayerSexStatusDataManager.playerUseSexActiveSkillList[selectSexBattleHeroineId][k];
			if ((sexSkillData3.counterType == SexSkillData.CounterType.pistonAndCaress || sexSkillData3.counterType == SexSkillData.CounterType.healAndCaress || sexSkillData3.counterType == SexSkillData.CounterType.all) && sexSkillData3.skillNeedTrance >= PlayerSexStatusDataManager.playerSexTrance[1])
			{
				heroineCaressSkillList.Add(sexSkillData3);
			}
		}
		for (int l = 0; l < PlayerSexStatusDataManager.playerUseSexActiveSkillList[selectSexBattleHeroineId].Count; l++)
		{
			SexSkillData sexSkillData4 = PlayerSexStatusDataManager.playerUseSexActiveSkillList[selectSexBattleHeroineId][l];
			if ((sexSkillData4.counterType == SexSkillData.CounterType.healAndCaress || sexSkillData4.counterType == SexSkillData.CounterType.all) && sexSkillData4.skillNeedTrance >= PlayerSexStatusDataManager.playerSexTrance[1])
			{
				heroineHealSkillList.Add(sexSkillData4);
			}
		}
	}

	public void UnLoadHeroineSpriteData()
	{
		if (sexHeroineSpriteData != null)
		{
			Resources.UnloadAsset(sexHeroineSpriteData);
			Debug.Log("えっちバトルのスプライトデータをアンロード");
		}
	}

	public void SetHeroineSprite(string spriteType)
	{
		int num = 0;
		if (isFinishDone.Any((bool value) => value))
		{
			if (isFinishDone[2])
			{
				num = 3;
			}
			else if (isFinishDone[0])
			{
				num = 1;
			}
			else if (isFinishDone[1])
			{
				num = 2;
			}
		}
		Debug.Log("射精スプライトIndex：" + num);
		if (num == 0 && sexBattleTurnManager.sexBattleHeroineEcstasyCount > 0 && sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
		{
			num = 4;
			Debug.Log("射精前絶頂済み／射精スプライトIndex：" + num);
		}
		switch (spriteType)
		{
		case "idle":
			if (PlayerNonSaveDataManager.isMenstrualDaySexUseCondom)
			{
				if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondomIdleList[num];
				}
				else
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondom_Ahe_IdleList[num];
				}
			}
			else if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgIdleList[num];
			}
			else
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCg_Ahe_IdleList[num];
			}
			break;
		case "absorb":
			if (PlayerNonSaveDataManager.isMenstrualDaySexUseCondom)
			{
				if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondomAbsorbList[num];
				}
				else
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondom_Ahe_AbsorbList[num];
				}
			}
			else if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgAbsorbList[num];
			}
			else
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCg_Ahe_AbsorbList[num];
			}
			break;
		case "piston":
			if (PlayerNonSaveDataManager.isMenstrualDaySexUseCondom)
			{
				if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondomPistonList[num];
				}
				else
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondom_Ahe_PistonList[num];
				}
			}
			else if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgPistonList[num];
			}
			else
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCg_Ahe_PistonList[num];
			}
			break;
		case "hardPiston":
			if (PlayerNonSaveDataManager.isMenstrualDaySexUseCondom)
			{
				if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondomHardPistonList[num];
				}
				else
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondom_Ahe_HardPistonList[num];
				}
			}
			else if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgHardPistonList[num];
			}
			else
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCg_Ahe_HardPistonList[num];
			}
			break;
		case "berserkPiston":
			if (PlayerNonSaveDataManager.isMenstrualDaySexUseCondom)
			{
				if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondomBerserkPistonList[num];
				}
				else
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondom_Ahe_BerserkPistonList[num];
				}
			}
			else if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgBerserkPistonList[num];
			}
			else
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCg_Ahe_BerserkPistonList[num];
			}
			break;
		case "caress":
			if (PlayerNonSaveDataManager.isMenstrualDaySexUseCondom)
			{
				if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondomCaressList[num];
				}
				else
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondom_Ahe_CaressList[num];
				}
			}
			else if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCaressList[num];
			}
			else
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCg_Ahe_CaressList[num];
			}
			break;
		case "kiss":
			if (PlayerNonSaveDataManager.isMenstrualDaySexUseCondom)
			{
				if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondomKissList[num];
				}
				else
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondom_Ahe_KissList[num];
				}
			}
			else if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgKissList[num];
			}
			else
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCg_Ahe_KissList[num];
			}
			break;
		case "cumShot":
			if (PlayerNonSaveDataManager.isMenstrualDaySexUseCondom)
			{
				if (sexBattleFertilizationManager.isInSideCumShot || isCounterCumShot)
				{
					if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
					{
						battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondomCumInShotList[num];
					}
					else
					{
						battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondom_Ahe_CumInShotList[num];
					}
				}
				else if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondomCumOutShotList[num];
				}
				else
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondom_Ahe_CumOutShotList[num];
				}
			}
			else if (sexBattleFertilizationManager.isInSideCumShot || isCounterCumShot)
			{
				if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCumInShotList[num];
				}
				else
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCg_Ahe_CumInShotList[num];
				}
			}
			else if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCumOutShotList[num];
			}
			else
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCg_Ahe_CumOutShotList[num];
			}
			break;
		case "victoryCumShot":
			if (PlayerNonSaveDataManager.isMenstrualDaySexUseCondom)
			{
				if (sexBattleFertilizationManager.isInSideCumShot || isCounterCumShot)
				{
					if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
					{
						battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondomCumInShotList[num];
					}
					else
					{
						battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondom_Ahe_CumInShotList[num];
					}
				}
				else if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondomCumOutShotList[num];
				}
				else
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondom_Ahe_CumOutShotList[num];
				}
			}
			else if (sexBattleFertilizationManager.isInSideCumShot || isCounterCumShot)
			{
				if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCumInShotList[num];
				}
				else if (PlayerNonSaveDataManager.isSexHeroineMenstrualDay)
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCg_Ahe_FertilizeCumInShotList[num];
				}
				else
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCg_Ahe_CumInShotList[num];
				}
			}
			else if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCumOutShotList[num];
			}
			else
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCg_Ahe_CumOutShotList[num];
			}
			break;
		case "ecstasy":
			if (PlayerNonSaveDataManager.isMenstrualDaySexUseCondom)
			{
				if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
				{
					if (PlayerSexStatusDataManager.playerSexExtasyLimit[1] == 0)
					{
						battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondomEcstasyLimitList[num];
					}
					else
					{
						battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondomEcstasyList[num];
					}
				}
				else if (PlayerSexStatusDataManager.playerSexExtasyLimit[1] == 0)
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondom_Ahe_EcstasyLimitList[num];
				}
				else
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondom_Ahe_EcstasyList[num];
				}
			}
			else if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
			{
				if (PlayerSexStatusDataManager.playerSexExtasyLimit[1] == 0)
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgEcstasyLimitList[num];
				}
				else
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgEcstasyList[num];
				}
			}
			else if (PlayerSexStatusDataManager.playerSexExtasyLimit[1] == 0)
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCg_Ahe_EcstasyLimitList[num];
			}
			else
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCg_Ahe_EcstasyList[num];
			}
			break;
		case "victoryEcstasy":
			if (PlayerNonSaveDataManager.isMenstrualDaySexUseCondom)
			{
				if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondomVictoryEcstasyList[num];
				}
				else
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondom_Ahe_VictoryEcstasyList[num];
				}
			}
			else if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgVictoryEcstasyList[num];
			}
			else
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCg_Ahe_VictoryEcstasyList[num];
			}
			break;
		case "battleVictory":
			if (PlayerNonSaveDataManager.isMenstrualDaySexUseCondom)
			{
				if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondomBattleVictoryList[num];
				}
				else
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondom_Ahe_BattleVictoryList[num];
				}
			}
			else if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgBattleVictoryList[num];
			}
			else
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCg_Ahe_BattleVictoryList[num];
			}
			break;
		case "battleDefeat":
			if (PlayerNonSaveDataManager.isMenstrualDaySexUseCondom)
			{
				if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondomBattleDefeatList[num];
				}
				else
				{
					battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgCondom_Ahe_BattleDefeatList[num];
				}
			}
			else if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.A)
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCgBattleDefeatList[num];
			}
			else
			{
				battleHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexHeroineSpriteData.heroineCg_Ahe_BattleDefeatList[num];
			}
			break;
		}
	}

	public bool GetSexBattleTextVisible()
	{
		return isSexBattleTextVisible;
	}

	public void SetSexBattleTextVisible(bool value)
	{
		isSexBattleTextVisible = value;
	}
}
