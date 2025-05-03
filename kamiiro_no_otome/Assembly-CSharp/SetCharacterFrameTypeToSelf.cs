using System.Collections.Generic;
using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class SetCharacterFrameTypeToSelf : StateBehaviour
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
		for (int i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
		{
			ParameterContainer component = playerFrameGoList[i].GetComponent<ParameterContainer>();
			int memberNum = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == scenarioBattleTurnManager.useSkillPartyMemberID).memberNum;
			scenarioBattleUiManager.SetFrameSkillButtonInteractable(component, value: false);
			if (i == memberNum)
			{
				component.GetGameObject("characterImage").GetComponent<Image>().color = new Color(1f, 1f, 1f);
				component.GetGameObject("frameImage").GetComponent<Image>().color = new Color(1f, 1f, 1f);
				utageBattleSceneManager.playerFrameGoList[i].GetComponent<CanvasGroup>().interactable = true;
				utageBattleSceneManager.playerFrameGoList[i].GetComponent<Button>().interactable = true;
				scenarioBattleUiManager.CopyBattlePlayerMaterial(playerFrameGoList[i], 1f);
			}
			else
			{
				component.GetGameObject("characterImage").GetComponent<Image>().color = scenarioBattleTurnManager.characterImageDisableColor;
				component.GetGameObject("frameImage").GetComponent<Image>().color = scenarioBattleTurnManager.characterImageDisableColor;
				utageBattleSceneManager.playerFrameGoList[i].GetComponent<CanvasGroup>().interactable = false;
				utageBattleSceneManager.playerFrameGoList[i].GetComponent<Button>().interactable = false;
				scenarioBattleUiManager.CopyBattlePlayerMaterial(playerFrameGoList[i], 0.4f);
			}
		}
		Debug.Log("自分対象のスキル選択中");
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
