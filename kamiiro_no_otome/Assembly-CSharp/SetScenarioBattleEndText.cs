using System.Linq;
using Arbor;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class SetScenarioBattleEndText : StateBehaviour
{
	public enum Type
	{
		player,
		enemy
	}

	private UtageBattleSceneManager utageBattleSceneManager;

	public Type type;

	public float waitTime;

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
		switch (type)
		{
		case Type.player:
		{
			if (PlayerStatusDataManager.playerPartyMember.Contains(0))
			{
				if (PlayerStatusDataManager.playerPartyMember.Length > 1)
				{
					utageBattleSceneManager.battleTextArray2[0].GetComponent<Localize>().Term = "character0";
					utageBattleSceneManager.battleTextArray2[1].GetComponent<Localize>().Term = "characterMulti";
					utageBattleSceneManager.battleTextArray2[2].GetComponent<Localize>().Term = "battleTextPlayerAllDeath";
					utageBattleSceneManager.battleTextArray2[0].SetActive(value: true);
					utageBattleSceneManager.battleTextArray2[1].SetActive(value: true);
					utageBattleSceneManager.battleTextArray2[2].SetActive(value: true);
				}
				else
				{
					utageBattleSceneManager.battleTextArray2[0].GetComponent<Localize>().Term = "character0";
					utageBattleSceneManager.battleTextArray2[1].GetComponent<Localize>().Term = "battleTextPlayerAllDeath";
					utageBattleSceneManager.battleTextArray2[0].SetActive(value: true);
					utageBattleSceneManager.battleTextArray2[1].SetActive(value: true);
				}
				break;
			}
			int num = PlayerStatusDataManager.playerPartyMember[0];
			if (PlayerStatusDataManager.playerPartyMember.Length > 1)
			{
				utageBattleSceneManager.battleTextArray2[0].GetComponent<Localize>().Term = "character" + num;
				utageBattleSceneManager.battleTextArray2[1].GetComponent<Localize>().Term = "characterMulti";
				utageBattleSceneManager.battleTextArray2[2].GetComponent<Localize>().Term = "battleTextPlayerAllDeath";
				utageBattleSceneManager.battleTextArray2[0].SetActive(value: true);
				utageBattleSceneManager.battleTextArray2[1].SetActive(value: true);
				utageBattleSceneManager.battleTextArray2[2].SetActive(value: true);
			}
			else
			{
				utageBattleSceneManager.battleTextArray2[0].GetComponent<Localize>().Term = "character" + num;
				utageBattleSceneManager.battleTextArray2[1].GetComponent<Localize>().Term = "battleTextPlayerAllDeath";
				utageBattleSceneManager.battleTextArray2[0].SetActive(value: true);
				utageBattleSceneManager.battleTextArray2[1].SetActive(value: true);
			}
			break;
		}
		case Type.enemy:
			utageBattleSceneManager.battleTextPanel.SetActive(value: false);
			break;
		}
		Invoke("InvokeMethod", waitTime);
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
