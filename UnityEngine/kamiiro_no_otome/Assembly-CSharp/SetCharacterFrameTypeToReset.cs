using System.Collections.Generic;
using System.Linq;
using Arbor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class SetCharacterFrameTypeToReset : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleUiManager scenarioBattleUiManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleUiManager = GameObject.Find("Battle Ui Manager").GetComponent<ScenarioBattleUiManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
	}

	public override void OnStateBegin()
	{
		List<GameObject> playerFrameGoList = utageBattleSceneManager.playerFrameGoList;
		foreach (GameObject item in playerFrameGoList)
		{
			item.GetComponent<CanvasGroup>().interactable = true;
		}
		int i;
		for (i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
		{
			ParameterContainer component = playerFrameGoList[i].GetComponent<ParameterContainer>();
			int memberID = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == i).memberID;
			component.GetGameObject("skillButton").GetComponent<Image>().color = new Color(1f, 1f, 1f);
			component.GetVariable<UguiImage>("skillIconImage").image.color = new Color(1f, 1f, 1f);
			foreach (TmpText item2 in component.GetVariableList<TmpText>("tmpNumList").ToList())
			{
				item2.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpNormalOutlineMaterial;
			}
			foreach (TmpText item3 in component.GetVariableList<TmpText>("tmp1mList").ToList())
			{
				item3.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmp1mMaterial;
			}
			if (!component.GetBool("isSupportCharacter"))
			{
				component.GetVariable<I2LocalizeComponent>("talismanTextLoc").localize.GetComponent<TextMeshProUGUI>().color = Color.white;
				foreach (TmpText item4 in component.GetVariableList<TmpText>("skillButtonTmpList").ToList())
				{
					item4.textMeshProUGUI.color = Color.white;
				}
				component.GetGameObject("statusGroupParentGo").GetComponent<BattleSkillButtonManagerForPM>().skillButtonFSM.SendEvent("SkillButtonRefresh");
			}
			if (PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == i).isDead)
			{
				component.GetGameObject("characterImage").GetComponent<Image>().color = scenarioBattleTurnManager.characterImageDisableColor;
				component.GetGameObject("frameImage").GetComponent<Image>().color = scenarioBattleTurnManager.characterImageDisableColor;
				utageBattleSceneManager.playerFrameGoList[i].GetComponent<CanvasGroup>().interactable = false;
				utageBattleSceneManager.playerFrameGoList[i].GetComponent<Button>().interactable = false;
				scenarioBattleUiManager.CopyBattlePlayerMaterial(playerFrameGoList[i], 0.4f);
				scenarioBattleUiManager.SetFrameSkillButtonInteractable(component, value: false);
				Debug.Log("スキルボタン死亡中：" + memberID);
				continue;
			}
			component.GetGameObject("characterImage").GetComponent<Image>().color = new Color(1f, 1f, 1f);
			component.GetGameObject("frameImage").GetComponent<Image>().color = new Color(1f, 1f, 1f);
			scenarioBattleUiManager.CopyBattlePlayerMaterial(playerFrameGoList[i], 1f);
			utageBattleSceneManager.playerFrameGoList[i].GetComponent<CanvasGroup>().interactable = true;
			if (component.GetBool("isSupportCharacter"))
			{
				return;
			}
			utageBattleSceneManager.playerFrameGoList[i].GetComponent<Button>().interactable = false;
			List<PlayerBattleConditionManager.MemberSkillReChargeTurn> list = PlayerBattleConditionManager.playerSkillRechargeTurn[memberID].Where((PlayerBattleConditionManager.MemberSkillReChargeTurn data) => data.needRechargeTurn == 0).ToList();
			List<PlayerBattleConditionManager.MemberSkillReChargeTurn> list2 = PlayerBattleConditionManager.playerSkillRechargeTurn[PlayerStatusDataManager.partyMemberCount].Where((PlayerBattleConditionManager.MemberSkillReChargeTurn data) => data.needRechargeTurn == 0).ToList();
			Debug.Log("ID：" + memberID + "／自分のスキル：" + list.Count + "／タリスマンスキル：" + list2.Count);
			if (memberID == 0)
			{
				if (PlayerNonSaveDataManager.resultScenarioName == "M_Main_001-1")
				{
					if (list.Any())
					{
						if (!scenarioBattleTurnManager.isUseSkillCheckArray[0])
						{
							SetSkillButtonInteractable(component, isEnable: true);
							Debug.Log("表示初期化／スキル使用可能：" + 0);
						}
						else
						{
							SetSkillButtonInteractable(component, isEnable: false);
							Debug.Log("表示初期化／スキル使用済み：" + 0);
						}
					}
					else
					{
						SetSkillButtonInteractable(component, isEnable: false);
						Debug.Log("表示初期化／チャージ中：" + 0);
					}
				}
				else if (list.Any() || list2.Any())
				{
					if (!scenarioBattleTurnManager.isUseSkillCheckArray[0])
					{
						SetSkillButtonInteractable(component, isEnable: true);
						Debug.Log("表示初期化／スキル使用可能：" + 0);
					}
					else
					{
						SetSkillButtonInteractable(component, isEnable: false);
						Debug.Log("表示初期化／スキル使用済み：" + 0);
					}
				}
				else
				{
					SetSkillButtonInteractable(component, isEnable: false);
					Debug.Log("表示初期化／チャージ中：" + 0);
				}
			}
			else if (list.Any())
			{
				if (!scenarioBattleTurnManager.isUseSkillCheckArray[memberID])
				{
					SetSkillButtonInteractable(component, isEnable: true);
					Debug.Log("表示初期化／スキル使用可能：" + memberID);
				}
				else
				{
					SetSkillButtonInteractable(component, isEnable: false);
					Debug.Log("表示初期化／スキル使用済み：" + memberID);
				}
			}
			else
			{
				SetSkillButtonInteractable(component, isEnable: false);
				Debug.Log("表示初期化／チャージ中：" + memberID);
			}
		}
		Debug.Log("フレーム表示を初期化する");
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

	private void SetSkillButtonInteractable(ParameterContainer param, bool isEnable)
	{
		foreach (GameObject gameObject in param.GetGameObjectList("halfSkillButtonGoList"))
		{
			gameObject.GetComponent<CanvasGroup>().blocksRaycasts = isEnable;
			gameObject.GetComponent<CanvasGroup>().interactable = isEnable;
		}
		foreach (UguiImage variable in param.GetVariableList<UguiImage>("halfSkillIconImageList"))
		{
			variable.image.raycastTarget = isEnable;
		}
		foreach (GameObject gameObject2 in param.GetGameObjectList("partyHalfSkillButtonGoList"))
		{
			gameObject2.GetComponent<CanvasGroup>().blocksRaycasts = isEnable;
			gameObject2.GetComponent<CanvasGroup>().interactable = isEnable;
		}
		foreach (UguiImage variable2 in param.GetVariableList<UguiImage>("partyHalfSkillIconImageList"))
		{
			variable2.image.raycastTarget = isEnable;
		}
		param.GetGameObject("talismanButton").GetComponent<CanvasGroup>().blocksRaycasts = isEnable;
		param.GetGameObject("talismanButton").GetComponent<CanvasGroup>().interactable = isEnable;
		param.GetGameObject("talismanButton").GetComponent<Image>().raycastTarget = isEnable;
		param.GetGameObject("skillButton").GetComponent<CanvasGroup>().blocksRaycasts = isEnable;
		param.GetGameObject("skillButton").GetComponent<CanvasGroup>().interactable = isEnable;
		param.GetGameObject("skillButton").GetComponent<Image>().raycastTarget = isEnable;
	}
}
