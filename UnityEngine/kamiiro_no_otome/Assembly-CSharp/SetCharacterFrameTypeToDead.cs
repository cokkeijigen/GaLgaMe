using System.Collections.Generic;
using System.Linq;
using Arbor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class SetCharacterFrameTypeToDead : StateBehaviour
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
			if (!PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == i).isDead)
			{
				component.GetGameObject("characterImage").GetComponent<Image>().color = scenarioBattleTurnManager.characterImageDisableColor;
				component.GetGameObject("frameImage").GetComponent<Image>().color = scenarioBattleTurnManager.characterImageDisableColor;
				utageBattleSceneManager.playerFrameGoList[i].GetComponent<CanvasGroup>().interactable = false;
				if (!component.GetBool("isSupportCharacter"))
				{
					utageBattleSceneManager.playerFrameGoList[i].GetComponent<Button>().interactable = false;
					scenarioBattleUiManager.CopyBattlePlayerMaterial(playerFrameGoList[i], 0.4f);
					component.GetGameObject("skillButton").GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f);
					component.GetVariable<UguiImage>("skillIconImage").image.color = new Color(0.4f, 0.4f, 0.4f);
					component.GetGameObject("talismanButton").GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f);
					component.GetVariable<UguiImage>("talismanIconImage").image.color = new Color(0.4f, 0.4f, 0.4f);
					foreach (GameObject gameObject in component.GetGameObjectList("halfSkillButtonGoList"))
					{
						gameObject.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f);
					}
					foreach (UguiImage variable in component.GetVariableList<UguiImage>("halfSkillIconImageList"))
					{
						variable.image.color = new Color(0.4f, 0.4f, 0.4f);
					}
					foreach (GameObject gameObject2 in component.GetGameObjectList("partyHalfSkillButtonGoList"))
					{
						gameObject2.GetComponent<Image>().color = new Color(0.4f, 0.4f, 0.4f);
					}
					foreach (UguiImage variable2 in component.GetVariableList<UguiImage>("partyHalfSkillIconImageList"))
					{
						variable2.image.color = new Color(0.4f, 0.4f, 0.4f);
					}
					component.GetVariable<I2LocalizeComponent>("talismanTextLoc").localize.GetComponent<TextMeshProUGUI>().color = scenarioBattleTurnManager.characterImageDisableColor;
					foreach (TmpText item2 in component.GetVariableList<TmpText>("skillButtonTmpList").ToList())
					{
						item2.textMeshProUGUI.color = scenarioBattleTurnManager.characterImageDisableColor;
					}
				}
				else
				{
					scenarioBattleUiManager.CopyBattlePlayerMaterial(playerFrameGoList[i], 0.4f);
					foreach (TmpText item3 in component.GetVariableList<TmpText>("tmpBoldList").ToList())
					{
						item3.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpBoldBlackAndOutlineMaterial;
					}
				}
				foreach (TmpText item4 in component.GetVariableList<TmpText>("tmpNumList").ToList())
				{
					item4.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmpBlackAndOutlineMaterial;
				}
				foreach (TmpText item5 in component.GetVariableList<TmpText>("tmp1mList").ToList())
				{
					item5.textMeshProUGUI.fontMaterial = scenarioBattleTurnManager.tmp1mBlackAndOutlineMaterial;
				}
			}
			else
			{
				component.GetGameObject("characterImage").GetComponent<Image>().color = new Color(1f, 1f, 1f);
				component.GetGameObject("frameImage").GetComponent<Image>().color = new Color(1f, 1f, 1f);
				utageBattleSceneManager.playerFrameGoList[i].GetComponent<CanvasGroup>().interactable = true;
				utageBattleSceneManager.playerFrameGoList[i].GetComponent<Button>().interactable = true;
				scenarioBattleUiManager.CopyBattlePlayerMaterial(playerFrameGoList[i], 1f);
			}
		}
		Debug.Log("味方蘇生用アイテムorスキルを選択中");
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
