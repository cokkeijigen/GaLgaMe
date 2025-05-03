using System.Collections.Generic;
using System.Linq;
using Arbor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class SetCharacterFrameTypeToScrollSelect : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleUiManager scenarioBattleUiManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleUiManager = GameObject.Find("Battle Ui Manager").GetComponent<ScenarioBattleUiManager>();
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
			scenarioBattleUiManager.SetFrameSkillButtonInteractable(component, value: false);
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
			}
			bool isDead = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == i).isDead;
			int memberID = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == i).memberID;
			if (isDead)
			{
				component.GetGameObject("characterImage").GetComponent<Image>().color = scenarioBattleTurnManager.characterImageDisableColor;
				component.GetGameObject("frameImage").GetComponent<Image>().color = scenarioBattleTurnManager.characterImageDisableColor;
				utageBattleSceneManager.playerFrameGoList[i].GetComponent<CanvasGroup>().interactable = false;
				utageBattleSceneManager.playerFrameGoList[i].GetComponent<Button>().interactable = false;
				component.GetGameObject("skillButton").GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
				component.GetVariable<UguiImage>("skillIconImage").image.color = new Color(0.4f, 0.4f, 0.4f);
				component.GetGameObject("talismanButton").GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
				component.GetVariable<UguiImage>("talismanIconImage").image.color = new Color(0.4f, 0.4f, 0.4f);
				foreach (GameObject gameObject in component.GetGameObjectList("halfSkillButtonGoList"))
				{
					gameObject.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
				}
				foreach (UguiImage variable in component.GetVariableList<UguiImage>("halfSkillIconImageList"))
				{
					variable.image.color = new Color(0.4f, 0.4f, 0.4f);
				}
				foreach (GameObject gameObject2 in component.GetGameObjectList("partyHalfSkillButtonGoList"))
				{
					gameObject2.GetComponent<Image>().color = new Color(0.8f, 0.8f, 0.8f);
				}
				foreach (UguiImage variable2 in component.GetVariableList<UguiImage>("partyHalfSkillIconImageList"))
				{
					variable2.image.color = new Color(0.4f, 0.4f, 0.4f);
				}
				scenarioBattleUiManager.CopyBattlePlayerMaterial(playerFrameGoList[i], 0.4f);
				continue;
			}
			component.GetGameObject("characterImage").GetComponent<Image>().color = new Color(1f, 1f, 1f);
			component.GetGameObject("frameImage").GetComponent<Image>().color = new Color(1f, 1f, 1f);
			utageBattleSceneManager.playerFrameGoList[i].GetComponent<CanvasGroup>().interactable = true;
			if (component.GetBool("isSupportCharacter"))
			{
				return;
			}
			utageBattleSceneManager.playerFrameGoList[i].GetComponent<Button>().interactable = true;
			if (scenarioBattleTurnManager.isUseSkillCheckArray[memberID])
			{
				component.GetGameObject("skillButton").GetComponent<Image>().color = new Color(1f, 1f, 1f);
				component.GetVariable<UguiImage>("skillIconImage").image.color = new Color(0.9f, 0.9f, 0.9f);
				component.GetGameObject("talismanButton").GetComponent<Image>().color = new Color(1f, 1f, 1f);
				component.GetVariable<UguiImage>("talismanIconImage").image.color = new Color(0.9f, 0.9f, 0.9f);
				foreach (GameObject gameObject3 in component.GetGameObjectList("halfSkillButtonGoList"))
				{
					gameObject3.GetComponent<Image>().color = new Color(1f, 1f, 1f);
				}
				foreach (UguiImage variable3 in component.GetVariableList<UguiImage>("halfSkillIconImageList"))
				{
					variable3.image.color = new Color(0.9f, 0.9f, 0.9f);
				}
				foreach (GameObject gameObject4 in component.GetGameObjectList("partyHalfSkillButtonGoList"))
				{
					gameObject4.GetComponent<Image>().color = new Color(1f, 1f, 1f);
				}
				foreach (UguiImage variable4 in component.GetVariableList<UguiImage>("partyHalfSkillIconImageList"))
				{
					variable4.image.color = new Color(0.9f, 0.9f, 0.9f);
				}
			}
			else
			{
				component.GetGameObject("skillButton").GetComponent<Image>().color = new Color(1f, 1f, 1f);
				component.GetVariable<UguiImage>("skillIconImage").image.color = new Color(1f, 1f, 1f);
				component.GetGameObject("talismanButton").GetComponent<Image>().color = new Color(1f, 1f, 1f);
				component.GetVariable<UguiImage>("talismanIconImage").image.color = new Color(1f, 1f, 1f);
				foreach (GameObject gameObject5 in component.GetGameObjectList("halfSkillButtonGoList"))
				{
					gameObject5.GetComponent<Image>().color = new Color(1f, 1f, 1f);
				}
				foreach (UguiImage variable5 in component.GetVariableList<UguiImage>("halfSkillIconImageList"))
				{
					variable5.image.color = new Color(1f, 1f, 1f);
				}
				foreach (GameObject gameObject6 in component.GetGameObjectList("partyHalfSkillButtonGoList"))
				{
					gameObject6.GetComponent<Image>().color = new Color(1f, 1f, 1f);
				}
				foreach (UguiImage variable6 in component.GetVariableList<UguiImage>("partyHalfSkillIconImageList"))
				{
					variable6.image.color = new Color(1f, 1f, 1f);
				}
			}
			scenarioBattleUiManager.CopyBattlePlayerMaterial(playerFrameGoList[i], 1f);
		}
		Debug.Log("スキルorアイテム欄で選択中");
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
}
