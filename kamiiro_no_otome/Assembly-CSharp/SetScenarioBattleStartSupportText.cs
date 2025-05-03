using Arbor;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class SetScenarioBattleStartSupportText : StateBehaviour
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

	public int supportSkipRate;

	private bool isSupportSKip;

	public StateLink stateLink;

	public StateLink noActionLink;

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
		isSupportSKip = false;
		float time = waitTime / (float)utageBattleSceneManager.battleSpeed;
		utageBattleSceneManager.battleTextPanel.SetActive(value: true);
		utageBattleSceneManager.battleTextGroupArray[2].SetActive(value: false);
		int supportAttackMemberId = utageBattleSceneManager.supportAttackMemberId;
		switch (supportAttackMemberId)
		{
		case 1:
		{
			scenarioBattleTurnManager.isSupportHeal = true;
			int num2 = Random.Range(0, 100);
			int itemId = 0;
			if (num2 < 70)
			{
				itemId = 0;
			}
			else
			{
				itemId = 20;
			}
			ItemData useItemData = GameDataManager.instance.itemDataBase.itemDataList.Find((ItemData data) => data.itemID == itemId);
			scenarioBattleTurnManager.useItemData = useItemData;
			scenarioBattleTurnManager.playerTargetNum = 0;
			if (scenarioBattleTurnManager.elapsedTurnCount == 0)
			{
				isSupportSKip = true;
			}
			else
			{
				int num3 = Random.Range(0, 100);
				Debug.Log("サポートの援護スキップ確率：" + supportSkipRate + "／ランダム：" + num3);
				if (num3 < supportSkipRate)
				{
					isSupportSKip = true;
				}
			}
			int num4 = Random.Range(0, 2);
			if (isSupportSKip)
			{
				utageBattleSceneManager.battleTopText.GetComponent<Localize>().Term = "characterSupport_Skip" + supportAttackMemberId + num4;
			}
			else
			{
				utageBattleSceneManager.battleTopText.GetComponent<Localize>().Term = "characterSupport" + supportAttackMemberId + num4;
			}
			Invoke("HealStartText", time);
			break;
		}
		case 5:
		{
			int num = Random.Range(0, 2);
			utageBattleSceneManager.battleTopText.GetComponent<Localize>().Term = "characterSupport" + supportAttackMemberId + num;
			Invoke("AttackStartText", time);
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
		int supportAttackMemberId = utageBattleSceneManager.supportAttackMemberId;
		utageBattleSceneManager.battleTextArray2[0].GetComponent<Localize>().Term = "character" + supportAttackMemberId;
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
		if (isSupportSKip)
		{
			utageBattleSceneManager.battleTextArray2[1].GetComponent<Localize>().Term = "battleTextSupportSkip";
		}
		else
		{
			utageBattleSceneManager.battleTextArray2[1].GetComponent<Localize>().Term = "battleTextSupport";
		}
		utageBattleSceneManager.battleTextArray2[0].SetActive(value: true);
		utageBattleSceneManager.battleTextArray2[1].SetActive(value: true);
		waitTime = 0.5f;
		float time = waitTime / (float)utageBattleSceneManager.battleSpeed;
		Invoke("InvokeMethod", time);
	}

	private void InvokeMethod()
	{
		if (isSupportSKip)
		{
			Transition(noActionLink);
		}
		else
		{
			Transition(stateLink);
		}
	}
}
