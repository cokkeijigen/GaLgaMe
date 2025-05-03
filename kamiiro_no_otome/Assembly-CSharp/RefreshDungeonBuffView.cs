using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class RefreshDungeonBuffView : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonMapStatusManager dungeonMapStatusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GetComponentInParent<DungeonMapManager>();
		dungeonMapStatusManager = GetComponent<DungeonMapStatusManager>();
	}

	public override void OnStateBegin()
	{
		if (dungeonMapStatusManager.dungeonBuffAttack > 0)
		{
			SetBuffFrameText(0, dungeonMapStatusManager.dungeonBuffAttack, isNegative: false);
		}
		else if (dungeonMapStatusManager.dungeonBuffAttack < 0)
		{
			SetBuffFrameText(0, dungeonMapStatusManager.dungeonBuffAttack, isNegative: true);
		}
		else
		{
			dungeonMapStatusManager.dungeonBuffFrameArray[0].SetActive(value: false);
			dungeonMapStatusManager.dungeonBattleBuffFrameArray[0].SetActive(value: false);
		}
		if (dungeonMapStatusManager.dungeonBuffDefense > 0)
		{
			SetBuffFrameText(1, dungeonMapStatusManager.dungeonBuffDefense, isNegative: false);
		}
		else if (dungeonMapStatusManager.dungeonBuffDefense < 0)
		{
			SetBuffFrameText(1, dungeonMapStatusManager.dungeonBuffDefense, isNegative: true);
		}
		else
		{
			dungeonMapStatusManager.dungeonBuffFrameArray[1].SetActive(value: false);
			dungeonMapStatusManager.dungeonBattleBuffFrameArray[1].SetActive(value: false);
		}
		if (dungeonMapStatusManager.dungeonDeBuffAgility < 0)
		{
			SetBuffFrameText(2, dungeonMapStatusManager.dungeonDeBuffAgility, isNegative: true);
		}
		else
		{
			dungeonMapStatusManager.dungeonBuffFrameArray[2].SetActive(value: false);
			dungeonMapStatusManager.dungeonBattleBuffFrameArray[2].SetActive(value: false);
		}
		if (dungeonMapStatusManager.dungeonBuffRetreat > 0)
		{
			SetBuffFrameText(3, dungeonMapStatusManager.dungeonBuffRetreat, isNegative: false);
		}
		else if (dungeonMapStatusManager.dungeonBuffRetreat < 0)
		{
			SetBuffFrameText(3, dungeonMapStatusManager.dungeonBuffRetreat, isNegative: true);
		}
		else
		{
			dungeonMapStatusManager.dungeonBuffFrameArray[3].SetActive(value: false);
			dungeonMapStatusManager.dungeonBattleBuffFrameArray[3].SetActive(value: false);
		}
		if (PlayerDataManager.rareDropRateRaisePowerNum > 0 && PlayerDataManager.rareDropRateRaiseRaimingDaysNum > 0)
		{
			SetBuffFrameText(4, PlayerDataManager.rareDropRateRaisePowerNum, isNegative: false);
		}
		else
		{
			dungeonMapStatusManager.dungeonBuffFrameArray[4].SetActive(value: false);
			dungeonMapStatusManager.dungeonBattleBuffFrameArray[4].SetActive(value: false);
		}
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

	private void SetBuffFrameText(int index, int power, bool isNegative)
	{
		dungeonMapStatusManager.dungeonBuffFrameArray[index].SetActive(value: true);
		dungeonMapStatusManager.dungeonBattleBuffFrameArray[index].SetActive(value: true);
		if (!isNegative)
		{
			dungeonMapStatusManager.dungeonBuffSymbolTextArray[index].text = "+";
			dungeonMapStatusManager.dungeonBattleBuffSymbolTextArray[index].text = "+";
			dungeonMapStatusManager.dungeonBuffNumTextArray[index].text = power.ToString();
			dungeonMapStatusManager.dungeonBattleBuffNumTextArray[index].text = power.ToString();
		}
		else
		{
			dungeonMapStatusManager.dungeonBuffSymbolTextArray[index].text = "-";
			dungeonMapStatusManager.dungeonBattleBuffSymbolTextArray[index].text = "-";
			power = -power;
		}
		dungeonMapStatusManager.dungeonBuffNumTextArray[index].text = power.ToString();
		dungeonMapStatusManager.dungeonBattleBuffNumTextArray[index].text = power.ToString();
	}
}
