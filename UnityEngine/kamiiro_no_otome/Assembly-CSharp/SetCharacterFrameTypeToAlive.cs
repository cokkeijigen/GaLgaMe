using System.Collections.Generic;
using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class SetCharacterFrameTypeToAlive : StateBehaviour
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
			if (PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == i).isDead)
			{
				component.GetGameObject("characterImage").GetComponent<Image>().color = scenarioBattleTurnManager.characterImageDisableColor;
				component.GetGameObject("frameImage").GetComponent<Image>().color = scenarioBattleTurnManager.characterImageDisableColor;
				utageBattleSceneManager.playerFrameGoList[i].GetComponent<CanvasGroup>().interactable = false;
				utageBattleSceneManager.playerFrameGoList[i].GetComponent<Button>().interactable = false;
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
			scenarioBattleUiManager.CopyBattlePlayerMaterial(playerFrameGoList[i], 1f);
		}
		Debug.Log("味方生存者用アイテムorスキルを選択中");
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
