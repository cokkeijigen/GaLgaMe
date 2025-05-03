using Arbor;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("")]
public class CheckScenarioBattleTutorial : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
	}

	public override void OnStateBegin()
	{
		if (!PlayerFlagDataManager.tutorialFlagDictionary["scenarioBattle1"])
		{
			PlayerNonSaveDataManager.selectTutorialName = "scenarioBattle1";
			Invoke("InvokeMethod", 0.3f);
			Debug.Log("コマンド戦闘のチュートリアル開始");
		}
		else if (!PlayerFlagDataManager.tutorialFlagDictionary["scenarioBattle3"])
		{
			PlayerNonSaveDataManager.selectTutorialName = "scenarioBattle3";
			Invoke("InvokeMethod", 0.3f);
			Debug.Log("コマンド戦闘二戦目のチュートリアル開始");
		}
		else if (PlayerNonSaveDataManager.isChargeAttackTutorial)
		{
			PlayerNonSaveDataManager.selectTutorialName = "chargeAttack";
			Invoke("InvokeMethod", 0.3f);
			Debug.Log("チャージ攻撃のチュートリアル開始");
		}
		else
		{
			Transition(stateLink);
		}
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
		SceneManager.LoadSceneAsync("tutorialUI", LoadSceneMode.Additive);
	}
}
