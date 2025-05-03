using Arbor;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class SetScenarioBattleStartText : StateBehaviour
{
	public enum Type
	{
		player,
		enemy,
		support
	}

	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	public Type type;

	public float waitTime;

	private string forceName;

	private int countValue;

	public StateLink stateLink;

	public StateLink enemySkillLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
	}

	public override void OnStateBegin()
	{
		float time = waitTime / (float)utageBattleSceneManager.battleSpeed;
		utageBattleSceneManager.battleTextPanel.SetActive(value: true);
		utageBattleSceneManager.battleTextGroupArray[2].SetActive(value: false);
		switch (type)
		{
		case Type.player:
		{
			countValue = scenarioBattleTurnManager.playerAttackCount;
			int num3 = Random.Range(0, 2);
			string text = "characterAttack" + PlayerBattleConditionManager.playerIsDead[countValue].memberID + num3;
			if (PlayerStatusDataManager.enemyMember[scenarioBattleTurnManager.playerTargetNum] >= 900)
			{
				text += "_Human";
			}
			utageBattleSceneManager.battleTopText.GetComponent<Localize>().Term = text;
			forceName = "character";
			Invoke("AttackStartText", time);
			break;
		}
		case Type.enemy:
		{
			countValue = scenarioBattleTurnManager.enemyAttackCount;
			int memberNum = PlayerBattleConditionManager.enemyIsDead[countValue].memberNum;
			Debug.Log("敵の攻撃Num：" + memberNum);
			if (PlayerStatusDataManager.enemyChargeTurnList[memberNum] >= PlayerStatusDataManager.enemyMaxChargeTurnList[memberNum])
			{
				PlayerStatusDataManager.enemyChargeTurnList[memberNum] = 0;
				scenarioBattleTurnManager.useSkillEnemyMemberNumList.Add(memberNum);
				Debug.Log("敵のスキル開始");
				utageBattleSceneManager.battleTopText.GetComponent<Localize>().Term = "empty";
				GoSkillLink();
			}
			else
			{
				utageBattleSceneManager.battleTopText.GetComponent<Localize>().Term = "enemyAttack" + PlayerBattleConditionManager.enemyIsDead[countValue].memberID;
				forceName = "enemy";
				Invoke("AttackStartText", time);
			}
			break;
		}
		case Type.support:
		{
			int num = Random.Range(0, 2);
			int supportAttackMemberId = utageBattleSceneManager.supportAttackMemberId;
			utageBattleSceneManager.battleTopText.GetComponent<Localize>().Term = "characterSupport" + supportAttackMemberId + num;
			forceName = "support";
			switch (supportAttackMemberId)
			{
			case 1:
			{
				int num2 = Random.Range(0, 2);
				int itemId = 0;
				if (num2 == 1)
				{
					itemId = 20;
				}
				ItemData useItemData = GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData data) => data.itemID == itemId);
				scenarioBattleTurnManager.useItemData = useItemData;
				scenarioBattleTurnManager.playerTargetNum = 0;
				Invoke("HealStartText", time);
				break;
			}
			case 5:
				Invoke("AttackStartText", time);
				break;
			}
			break;
		}
		}
		utageBattleSceneManager.battleTopText.SetActive(value: true);
		Debug.Log("テキストパネル表示");
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

	private void AttackStartText()
	{
		switch (forceName)
		{
		case "character":
			utageBattleSceneManager.battleTextArray2[0].GetComponent<Localize>().Term = forceName + PlayerBattleConditionManager.playerIsDead[countValue].memberID;
			break;
		case "enemy":
			utageBattleSceneManager.battleTextArray2[0].GetComponent<Localize>().Term = forceName + PlayerBattleConditionManager.enemyIsDead[countValue].memberID;
			break;
		case "support":
		{
			int supportAttackMemberId = utageBattleSceneManager.supportAttackMemberId;
			utageBattleSceneManager.battleTextArray2[0].GetComponent<Localize>().Term = "character" + supportAttackMemberId;
			break;
		}
		}
		utageBattleSceneManager.battleTextArray2[1].GetComponent<Localize>().Term = "battleTextAttack";
		utageBattleSceneManager.battleTextArray2[0].SetActive(value: true);
		utageBattleSceneManager.battleTextArray2[1].SetActive(value: true);
		float time = waitTime / (float)utageBattleSceneManager.battleSpeed;
		Invoke("InvokeMethod", time);
	}

	private void HealStartText()
	{
		int supportAttackMemberId = utageBattleSceneManager.supportAttackMemberId;
		utageBattleSceneManager.battleTextArray2[0].GetComponent<Localize>().Term = "character" + supportAttackMemberId;
		utageBattleSceneManager.battleTextArray2[1].GetComponent<Localize>().Term = "battleTextSupport";
		utageBattleSceneManager.battleTextArray2[0].SetActive(value: true);
		utageBattleSceneManager.battleTextArray2[1].SetActive(value: true);
		float time = waitTime / (float)utageBattleSceneManager.battleSpeed;
		Invoke("InvokeMethod", time);
	}

	private void InvokeMethod()
	{
		Transition(stateLink);
	}

	private void GoSkillLink()
	{
		Transition(enemySkillLink);
	}
}
