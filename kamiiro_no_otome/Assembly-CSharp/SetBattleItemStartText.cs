using Arbor;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class SetBattleItemStartText : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	public float waitTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
	}

	public override void OnStateBegin()
	{
		float time = waitTime / (float)utageBattleSceneManager.battleSpeed;
		utageBattleSceneManager.battleTextPanel.SetActive(value: true);
		utageBattleSceneManager.battleTextGroupArray[2].SetActive(value: false);
		utageBattleSceneManager.battleTopText.SetActive(value: false);
		if (PlayerStatusDataManager.playerPartyMember.Length > 1)
		{
			string allTargetTerm = scenarioBattleSkillManager.GetAllTargetTerm();
			utageBattleSceneManager.battleTextArray2[0].GetComponent<Localize>().Term = allTargetTerm;
		}
		else
		{
			utageBattleSceneManager.battleTextArray2[0].GetComponent<Localize>().Term = "character0";
		}
		utageBattleSceneManager.battleTextArray2[1].GetComponent<Localize>().Term = "textConjunctionIs";
		utageBattleSceneManager.battleTextArray2[2].GetComponent<Localize>().Term = scenarioBattleTurnManager.battleUseItemCategory + scenarioBattleTurnManager.battleUseItemID;
		utageBattleSceneManager.battleTextArray2[3].GetComponent<Localize>().Term = "battleTextUseItem";
		GameObject[] battleTextArray = utageBattleSceneManager.battleTextArray2;
		for (int i = 0; i < battleTextArray.Length; i++)
		{
			battleTextArray[i].SetActive(value: true);
		}
		Invoke("InvokeMethod", time);
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

	private void InvokeMethod()
	{
		Transition(stateLink);
	}
}
