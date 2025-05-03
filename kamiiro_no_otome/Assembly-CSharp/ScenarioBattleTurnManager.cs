using System.Collections.Generic;
using System.Linq;
using Arbor;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioBattleTurnManager : SerializedMonoBehaviour
{
	public UtageBattleSceneManager utageBattleSceneManager;

	public ScenarioBattleSkillManager scenarioBattleSkillManager;

	public ScenarioBattleUiManager scenarioBattleUiManager;

	public ArborFSM turnFSM;

	public int playerAttackCount;

	public int enemyAttackCount;

	public int playerTargetNum;

	public int enemyTargetNum;

	public int playerFocusTargetNum;

	public int battleUseItemID;

	public string battleUseItemCategory;

	public int battleUseItemCount;

	public int battleEnableUseItemMaxNum;

	public TextMeshProUGUI itemEnableUseCountText;

	public int battleUseSkillID;

	public int useSkillPartyMemberID;

	public List<int> useSkillEnemyMemberNumList;

	public List<SkillAttackHitData> skillAttackHitDataList;

	public List<SkillAttackHitData> skillAttackHitDataSubList;

	public List<int> allAttackHitMemberList;

	public List<int> allSubAttackHitMemberList;

	public List<float> allAttackDamageList;

	public List<float> allSubAttackDamageList;

	public bool[] isUseSkillCheckArray = new bool[5];

	public Color characterImageDisableColor = new Color(0.4f, 0.4f, 0.4f);

	public Material scenarioBattleUI;

	public Material scenarioBattleUI_Player;

	public Material scenarioBattleUI_Enemy;

	public Material tmpNormalMaterial;

	public Material tmpNormalOutlineMaterial;

	public Material tmpBlackMaterial;

	public Material tmpBlackAndOutlineMaterial;

	public Material tmpBoldMaterial;

	public Material tmpBoldBlackAndOutlineMaterial;

	public Material tmp1mMaterial;

	public Material tmp1mBlackAndOutlineMaterial;

	public List<int> enemyNormalAttackDamageList;

	public List<int> enemySkillAttackDamageList;

	public List<bool> factorEffectSuccessList = new List<bool> { false, false, false, false };

	public bool isCriticalHit;

	public bool isParrySuccess;

	public bool isNormalWeaklHit;

	public bool isNormalResistHit;

	public bool isMedicineNoTarget;

	public bool isSupportSkill;

	public bool isSupportHeal;

	public bool isUseSkillPlayer;

	public bool isAllTargetSkill;

	public bool isRetreat;

	public string setFrameTypeName;

	public string setUiMaterialTypeName;

	public int elapsedTurnCount;

	public ItemData useItemData;

	public BattleSkillData playerSkillData;

	public BattleSkillData enemySkillData;

	public BattleSkillData useSkillData;

	public void PushNormalAttackButton()
	{
		battleUseSkillID = 0;
		turnFSM.SendTrigger("PushNormalAttack");
	}

	public void SetSelectCharacterFrameColor(string type)
	{
		List<GameObject> playerFrameGoList = utageBattleSceneManager.playerFrameGoList;
		foreach (GameObject item in playerFrameGoList)
		{
			item.GetComponent<CanvasGroup>().interactable = true;
		}
		switch (type)
		{
		case "alive":
		{
			int k;
			for (k = 0; k < PlayerStatusDataManager.playerPartyMember.Length; k++)
			{
				SetSkillButtonInteractable(playerFrameGoList[k], isEnable: false, k);
				bool isDead = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == k).isDead;
				playerFrameGoList[k].GetComponent<ParameterContainer>();
				if (isDead)
				{
					SetMiniFrameInteractable(playerFrameGoList[k], isEnable: false, k);
				}
				else
				{
					SetMiniFrameInteractable(playerFrameGoList[k], isEnable: true, k);
				}
			}
			Debug.Log("味方生存者用アイテムorスキルを選択中");
			break;
		}
		case "dead":
		{
			int n;
			for (n = 0; n < PlayerStatusDataManager.playerPartyMember.Length; n++)
			{
				SetSkillButtonInteractable(playerFrameGoList[n], isEnable: false, n);
				bool isDead4 = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == n).isDead;
				playerFrameGoList[n].GetComponent<ParameterContainer>();
				if (!isDead4)
				{
					SetMiniFrameInteractable(playerFrameGoList[n], isEnable: false, n);
				}
				else
				{
					SetMiniFrameInteractable(playerFrameGoList[n], isEnable: true, n);
				}
			}
			Debug.Log("味方蘇生用アイテムorスキルを選択中");
			break;
		}
		case "reset":
		{
			int l;
			for (l = 0; l < PlayerStatusDataManager.playerPartyMember.Length; l++)
			{
				bool isDead2 = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == l).isDead;
				ParameterContainer component = playerFrameGoList[l].GetComponent<ParameterContainer>();
				if (isDead2)
				{
					component.GetGameObject("characterImage").GetComponent<Image>().color = characterImageDisableColor;
					component.GetGameObject("frameImage").GetComponent<Image>().color = characterImageDisableColor;
					component.GetGameObject("statusGroupGo").GetComponent<CanvasGroup>().alpha = 0.5f;
				}
				else
				{
					component.GetGameObject("characterImage").GetComponent<Image>().color = new Color(1f, 1f, 1f);
					component.GetGameObject("frameImage").GetComponent<Image>().color = new Color(1f, 1f, 1f);
					component.GetGameObject("statusGroupGo").GetComponent<CanvasGroup>().alpha = 1f;
				}
				playerFrameGoList[l].GetComponent<CanvasGroup>().interactable = !isDead2;
				playerFrameGoList[l].GetComponent<Button>().interactable = false;
				SetSkillButtonSpriteAndInteractable(l);
			}
			Debug.Log("インタラクティブ初期化");
			break;
		}
		case "action":
		{
			int m;
			for (m = 0; m < PlayerStatusDataManager.playerPartyMember.Length; m++)
			{
				bool isDead3 = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == m).isDead;
				ParameterContainer component2 = playerFrameGoList[m].GetComponent<ParameterContainer>();
				if (isDead3)
				{
					component2.GetGameObject("characterImage").GetComponent<Image>().color = characterImageDisableColor;
					component2.GetGameObject("frameImage").GetComponent<Image>().color = characterImageDisableColor;
					component2.GetGameObject("statusGroupGo").GetComponent<CanvasGroup>().alpha = 0.5f;
				}
				else
				{
					component2.GetGameObject("characterImage").GetComponent<Image>().color = new Color(1f, 1f, 1f);
					component2.GetGameObject("frameImage").GetComponent<Image>().color = new Color(1f, 1f, 1f);
					component2.GetGameObject("statusGroupGo").GetComponent<CanvasGroup>().alpha = 1f;
				}
			}
			Debug.Log("表示をリセット");
			break;
		}
		case "select":
		{
			for (int num = 0; num < PlayerStatusDataManager.playerPartyMember.Length; num++)
			{
				if (isUseSkillCheckArray[num])
				{
					SetSkillButtonInteractable(playerFrameGoList[num], isEnable: false, num);
				}
				else
				{
					SetSkillButtonInteractable(playerFrameGoList[num], isEnable: true, num);
				}
			}
			Debug.Log("スキルorアイテムのスクロール欄表示中");
			break;
		}
		case "self":
		{
			for (int i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
			{
				SetSkillButtonInteractable(playerFrameGoList[i], isEnable: false, i);
			}
			int j;
			for (j = 0; j < PlayerStatusDataManager.playerPartyMember.Length; j++)
			{
				_ = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == j).memberID;
				playerFrameGoList[j].GetComponent<ParameterContainer>();
				if (j != useSkillPartyMemberID)
				{
					SetMiniFrameInteractable(playerFrameGoList[j], isEnable: false, j);
				}
				else
				{
					SetMiniFrameInteractable(playerFrameGoList[j], isEnable: true, j);
				}
			}
			Debug.Log("自分対象のスキル選択中");
			break;
		}
		}
	}

	public void SetMiniFrameInteractable(GameObject targetGo, bool isEnable, int partyNum)
	{
		ParameterContainer component = targetGo.GetComponent<ParameterContainer>();
		if (isEnable)
		{
			component.GetGameObject("characterImage").GetComponent<Image>().color = new Color(1f, 1f, 1f);
			component.GetGameObject("frameImage").GetComponent<Image>().color = new Color(1f, 1f, 1f);
			utageBattleSceneManager.playerFrameGoList[partyNum].GetComponent<CanvasGroup>().interactable = true;
			scenarioBattleUiManager.CopyBattlePlayerMaterial(targetGo, 1f);
			SetSkillButtonSpriteAndInteractable(partyNum);
		}
		else
		{
			component.GetGameObject("characterImage").GetComponent<Image>().color = characterImageDisableColor;
			component.GetGameObject("frameImage").GetComponent<Image>().color = characterImageDisableColor;
			utageBattleSceneManager.playerFrameGoList[partyNum].GetComponent<CanvasGroup>().interactable = false;
			scenarioBattleUiManager.CopyBattlePlayerMaterial(targetGo, 0.4f);
			SetSkillButtonSpriteAndInteractable(partyNum);
		}
		Debug.Log("キャラ絵の明るさとインタラクティブ：" + isEnable + "／パーティ番号：" + partyNum);
	}

	public void SetSkillButtonSpriteAndInteractable(int partyNum)
	{
		ParameterContainer component = utageBattleSceneManager.playerFrameGoList[partyNum].GetComponent<ParameterContainer>();
		bool isDead = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == partyNum).isDead;
		int @int = component.GetInt("characterID");
		List<PlayerBattleConditionManager.MemberSkillReChargeTurn> source = PlayerBattleConditionManager.playerSkillRechargeTurn[partyNum].Where((PlayerBattleConditionManager.MemberSkillReChargeTurn data) => data.needRechargeTurn == 0).ToList();
		if (@int == 0)
		{
			if (PlayerNonSaveDataManager.resultScenarioName == "M_Main_001-1")
			{
				if (isDead)
				{
					component.GetGameObject("chargeCircleGo").SetActive(value: false);
					SetSkillButtonSprite("dead", component, partyNum);
				}
				else if (source.Any())
				{
					component.GetGameObject("chargeCircleGo").SetActive(value: true);
					if (isUseSkillCheckArray[@int])
					{
						SetSkillButtonSprite("used", component, partyNum);
					}
					else
					{
						SetSkillButtonSprite("normal", component, partyNum);
					}
				}
				else
				{
					component.GetGameObject("chargeCircleGo").SetActive(value: false);
					if (isUseSkillCheckArray[@int])
					{
						SetSkillButtonSprite("used", component, partyNum);
					}
					else
					{
						SetSkillButtonSprite("reCharge", component, partyNum);
					}
				}
				return;
			}
			List<PlayerBattleConditionManager.MemberSkillReChargeTurn> source2 = PlayerBattleConditionManager.playerSkillRechargeTurn[PlayerStatusDataManager.partyMemberCount].Where((PlayerBattleConditionManager.MemberSkillReChargeTurn data) => data.needRechargeTurn == 0).ToList();
			if (isDead)
			{
				component.GetGameObject("chargeCircleGo").SetActive(value: false);
				SetSkillButtonSprite("dead", component, partyNum);
			}
			else if (source.Any() || source2.Any())
			{
				component.GetGameObject("chargeCircleGo").SetActive(value: true);
				if (isUseSkillCheckArray[@int])
				{
					SetSkillButtonSprite("used", component, partyNum);
				}
				else
				{
					SetSkillButtonSprite("normal", component, partyNum);
				}
			}
			else
			{
				component.GetGameObject("chargeCircleGo").SetActive(value: false);
				if (isUseSkillCheckArray[@int])
				{
					SetSkillButtonSprite("used", component, partyNum);
				}
				else
				{
					SetSkillButtonSprite("reCharge", component, partyNum);
				}
			}
		}
		else if (isDead)
		{
			component.GetGameObject("chargeCircleGo").SetActive(value: false);
			SetSkillButtonSprite("dead", component, partyNum);
		}
		else if (source.Any() || !isDead)
		{
			component.GetGameObject("chargeCircleGo").SetActive(value: true);
			if (isUseSkillCheckArray[@int])
			{
				SetSkillButtonSprite("used", component, partyNum);
			}
			else
			{
				SetSkillButtonSprite("normal", component, partyNum);
			}
		}
		else
		{
			component.GetGameObject("chargeCircleGo").SetActive(value: false);
			if (isUseSkillCheckArray[@int])
			{
				SetSkillButtonSprite("used", component, partyNum);
			}
			else
			{
				SetSkillButtonSprite("reCharge", component, partyNum);
			}
		}
	}

	private void SetSkillButtonSprite(string type, ParameterContainer param, int partyNum)
	{
		param.GetGameObject("statusGroupParentGo").GetComponent<PlayMakerFSM>().SendEvent("SkillButtonRefresh");
		switch (type)
		{
		case "normal":
			param.GetGameObject("skillButton").GetComponent<CanvasGroup>().blocksRaycasts = true;
			param.GetGameObject("skillButton").GetComponent<CanvasGroup>().interactable = true;
			param.GetGameObject("skillButton").GetComponent<Image>().raycastTarget = true;
			Debug.Log("スキルボタン有効：" + partyNum);
			break;
		case "used":
			param.GetGameObject("skillButton").GetComponent<CanvasGroup>().blocksRaycasts = true;
			param.GetGameObject("skillButton").GetComponent<CanvasGroup>().interactable = false;
			param.GetGameObject("skillButton").GetComponent<Image>().raycastTarget = false;
			Debug.Log("スキルボタン使用済み：" + partyNum);
			break;
		case "reCharge":
			param.GetGameObject("skillButton").GetComponent<CanvasGroup>().blocksRaycasts = true;
			param.GetGameObject("skillButton").GetComponent<CanvasGroup>().interactable = false;
			param.GetGameObject("skillButton").GetComponent<Image>().raycastTarget = false;
			Debug.Log("スキルボタンチャージ中：" + partyNum);
			break;
		case "dead":
			param.GetGameObject("skillButton").GetComponent<CanvasGroup>().blocksRaycasts = true;
			param.GetGameObject("skillButton").GetComponent<CanvasGroup>().interactable = false;
			param.GetGameObject("skillButton").GetComponent<Image>().raycastTarget = false;
			Debug.Log("スキルボタン死亡中：" + partyNum);
			break;
		}
	}

	public void SetSkillButtonInteractable(GameObject targetGo, bool isEnable, int partyNum)
	{
		ParameterContainer component = targetGo.GetComponent<ParameterContainer>();
		if (isEnable)
		{
			component.GetInt("partyMemberNum");
			int @int = component.GetInt("characterID");
			List<PlayerBattleConditionManager.MemberSkillReChargeTurn> source = PlayerBattleConditionManager.playerSkillRechargeTurn[@int].Where((PlayerBattleConditionManager.MemberSkillReChargeTurn data) => data.needRechargeTurn == 0).ToList();
			if (@int == 0)
			{
				if (PlayerNonSaveDataManager.resultScenarioName == "M_Main_001-1")
				{
					if (source.Any())
					{
						component.GetGameObject("chargeCircleGo").SetActive(value: true);
					}
					else
					{
						component.GetGameObject("chargeCircleGo").SetActive(value: false);
					}
				}
				else
				{
					List<PlayerBattleConditionManager.MemberSkillReChargeTurn> source2 = PlayerBattleConditionManager.playerSkillRechargeTurn[PlayerStatusDataManager.partyMemberCount].Where((PlayerBattleConditionManager.MemberSkillReChargeTurn data) => data.needRechargeTurn == 0).ToList();
					if (source.Any() || source2.Any())
					{
						component.GetGameObject("chargeCircleGo").SetActive(value: true);
					}
					else
					{
						component.GetGameObject("chargeCircleGo").SetActive(value: false);
					}
				}
			}
			else
			{
				component.GetGameObject("chargeCircleGo").SetActive(source.Any());
			}
			component.GetGameObject("skillButton").GetComponent<CanvasGroup>().blocksRaycasts = true;
			component.GetGameObject("skillButton").GetComponent<CanvasGroup>().interactable = !isUseSkillCheckArray[@int];
			component.GetGameObject("skillButton").GetComponent<Image>().raycastTarget = !isUseSkillCheckArray[@int];
			Debug.Log("スキルボタン通常表示：" + partyNum);
		}
		else
		{
			component.GetGameObject("skillButton").GetComponent<CanvasGroup>().blocksRaycasts = false;
			component.GetGameObject("skillButton").GetComponent<CanvasGroup>().interactable = false;
			component.GetGameObject("skillButton").GetComponent<Image>().raycastTarget = false;
			component.GetGameObject("chargeCircleGo").SetActive(value: false);
			Debug.Log("スキルボタン暗転表示：" + partyNum);
		}
	}
}
