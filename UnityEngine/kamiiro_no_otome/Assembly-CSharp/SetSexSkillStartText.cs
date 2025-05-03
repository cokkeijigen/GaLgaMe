using Arbor;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class SetSexSkillStartText : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

	private SexBattleMessageTextManager sexBattleMessageTextManager;

	public float waitTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		sexBattleTurnManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleTurnManager>();
		sexBattleMessageTextManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleMessageTextManager>();
	}

	public override void OnStateBegin()
	{
		float time = waitTime / (float)sexBattleManager.battleSpeed;
		SexSkillData selectSexSkillData = sexBattleManager.selectSexSkillData;
		sexBattleMessageTextManager.sexBattleMessageGroupGo_Top.SetActive(value: true);
		switch (selectSexSkillData.actionType)
		{
		case SexSkillData.ActionType.piston:
			switch (selectSexSkillData.narrativePartString)
			{
			case "piston":
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[0].GetComponent<Localize>().Term = "sexBattleTarget_02";
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[1].GetComponent<Localize>().Term = "sexAttack_PistonStart0";
				sexBattleTurnManager.sexBattlePistonCount += 4;
				break;
			case "hardPiston":
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[0].GetComponent<Localize>().Term = "sexBattleTarget_02";
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[1].GetComponent<Localize>().Term = "sexAttack_PistonStart1";
				sexBattleTurnManager.sexBattlePistonCount += 4;
				break;
			case "gSpotPiston":
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[0].GetComponent<Localize>().Term = "sexBattleTarget_01";
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[1].GetComponent<Localize>().Term = "sexAttack_PistonStart2";
				sexBattleTurnManager.sexBattlePistonCount += 4;
				break;
			case "portioPiston":
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[0].GetComponent<Localize>().Term = "sexBattleTarget_01";
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[1].GetComponent<Localize>().Term = "sexAttack_PistonStart3";
				sexBattleTurnManager.sexBattlePistonCount += 4;
				break;
			case "fertilizePiston":
			{
				int num4 = Random.Range(1, 3);
				if (sexBattleTurnManager.isFertilizeRepeatPiston)
				{
					sexBattleMessageTextManager.ResetBattleTextMessage();
					sexBattleMessageTextManager.sexBattleMessageGroup_Top[0].GetComponent<Localize>().Term = "sexBattleTarget_02";
					sexBattleMessageTextManager.sexBattleMessageGroup_Top[1].GetComponent<Localize>().Term = "sexAttack_PistonStart200_2";
					sexBattleMessageTextManager.sexBattleMessageGroup_Top[9].GetComponent<Localize>().Term = "sexFertilize2_0" + num4;
					sexBattleMessageTextManager.sexBattleMessageGroup_Top[9].gameObject.SetActive(value: true);
					int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
					sexBattleMessageTextManager.sexBattleMessageGroup_Top[2].GetComponent<Localize>().Term = "sexDefense_Heroine" + selectSexBattleHeroineId + "_FertilizePiston2_" + num4;
					sexBattleMessageTextManager.sexBattleMessageGroup_Top[2].gameObject.SetActive(value: true);
				}
				else
				{
					sexBattleMessageTextManager.sexBattleMessageGroup_Top[0].GetComponent<Localize>().Term = "sexBattleTarget_02";
					sexBattleMessageTextManager.sexBattleMessageGroup_Top[1].GetComponent<Localize>().Term = "sexAttack_PistonStart200_1";
					sexBattleMessageTextManager.sexBattleMessageGroup_Top[9].GetComponent<Localize>().Term = "sexFertilize1_0" + num4;
					sexBattleMessageTextManager.sexBattleMessageGroup_Top[9].gameObject.SetActive(value: true);
				}
				sexBattleTurnManager.sexBattlePistonCount += 4;
				break;
			}
			case "berserkPiston":
			{
				int num3 = Random.Range(1, 3);
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[0].GetComponent<Localize>().Term = "sexBattleTarget_02";
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[1].GetComponent<Localize>().Term = "sexAttack_PistonStart10";
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[9].GetComponent<Localize>().Term = "sexBerserk_0" + num3;
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[9].gameObject.SetActive(value: true);
				break;
			}
			}
			break;
		case SexSkillData.ActionType.kiss:
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[0].GetComponent<Localize>().Term = "sexBattleTarget_01";
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[1].GetComponent<Localize>().Term = "sexAttack_Kiss";
			break;
		case SexSkillData.ActionType.caress:
			switch (selectSexSkillData.narrativePartString)
			{
			case "tits":
			{
				int num2 = Random.Range(0, 2);
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[0].GetComponent<Localize>().Term = "sexBattleTarget_01";
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[1].GetComponent<Localize>().Term = "sexAttack_Tits" + num2;
				break;
			}
			case "nipple":
			{
				int num = Random.Range(0, 2);
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[0].GetComponent<Localize>().Term = "sexBattleTarget_01";
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[1].GetComponent<Localize>().Term = "sexAttack_Nipple" + num;
				break;
			}
			case "clitoris":
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[0].GetComponent<Localize>().Term = "sexBattleTarget_01";
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[1].GetComponent<Localize>().Term = "sexAttack_Clitoris";
				break;
			}
			break;
		case SexSkillData.ActionType.heal:
			switch (selectSexSkillData.narrativePartString)
			{
			case "voice":
			case "breath":
			case "concentration":
			{
				string text = selectSexSkillData.narrativePartString.Substring(0, 1).ToUpper() + selectSexSkillData.narrativePartString.Substring(1);
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[0].GetComponent<Localize>().Term = "sexBattleTarget_01";
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[1].GetComponent<Localize>().Term = "sexAttack_" + text;
				break;
			}
			}
			break;
		}
		sexBattleMessageTextManager.sexBattleMessageGroup_Top[0].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Top[1].gameObject.SetActive(value: true);
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
