using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckInfoWindowBodyHistory : StateBehaviour
{
	private SexTouchManager sexTouchManager;

	private SexTouchHeroineDataManager sexTouchHeroineDataManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchManager = GameObject.Find("Sex Touch Manager").GetComponent<SexTouchManager>();
		sexTouchHeroineDataManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchHeroineDataManager>();
	}

	public override void OnStateBegin()
	{
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		string term = "";
		string text = "";
		int num = 0;
		switch (sexTouchManager.clickSelectAreaPointName)
		{
		case "mouth":
			num = sexTouchManager.currentSexSkillLv;
			term = "sexBodyPassive_mouth" + selectSexBattleHeroineId + "0" + num;
			text = "sexBodyTouchInfo_mouth" + selectSexBattleHeroineId + num;
			break;
		case "handRight":
		case "handLeft":
			num = sexTouchManager.currentSexSkillLv;
			text = "sexBodyTouchInfo_hand" + selectSexBattleHeroineId + num;
			break;
		case "tits":
			num = sexTouchManager.currentSexSkillLv;
			term = "sexBodyPassive_tits" + selectSexBattleHeroineId + "3" + num;
			text = "sexBodyTouchInfo_tits" + selectSexBattleHeroineId + num;
			break;
		case "nippleRight":
			num = sexTouchManager.currentSexSkillLv;
			term = "sexBodyPassive_nipple" + selectSexBattleHeroineId + "4" + num;
			text = "sexBodyTouchInfo_nipple" + selectSexBattleHeroineId + num;
			break;
		case "nippleLeft":
			num = sexTouchManager.currentSexSkillLv;
			term = "sexBodyPassive_nipple" + selectSexBattleHeroineId + "4" + num;
			text = "sexBodyTouchInfo_nipple" + selectSexBattleHeroineId + num;
			break;
		case "womb":
			num = sexTouchManager.currentSexSkillLv;
			term = "sexBodyPassive_womb" + selectSexBattleHeroineId + "5" + num;
			text = "sexBodyTouchInfo_womb" + selectSexBattleHeroineId + num;
			if (PlayerNonSaveDataManager.isSexHeroineMenstrualDay)
			{
				if (PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[selectSexBattleHeroineId] > 0)
				{
					text += "_semen";
				}
			}
			else if (PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[selectSexBattleHeroineId] > 0)
			{
				text += "_semen";
			}
			break;
		case "clitoris":
			num = sexTouchManager.currentSexSkillLv;
			term = "sexBodyPassive_clitoris" + selectSexBattleHeroineId + "6" + num;
			text = "sexBodyTouchInfo_clitoris" + selectSexBattleHeroineId + num;
			break;
		case "vagina":
			num = sexTouchManager.currentSexSkillLv;
			term = "sexBodyPassive_vagina" + selectSexBattleHeroineId + "7" + num;
			text = "sexBodyTouchInfo_vagina" + selectSexBattleHeroineId + num;
			if (PlayerNonSaveDataManager.selectSexBattleHeroineId == 3)
			{
				Debug.Log("シアのおまんこLV：" + num);
				if (num == 1)
				{
					if (PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[selectSexBattleHeroineId] > 0)
					{
						text += "_semen";
					}
				}
				else if (PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[selectSexBattleHeroineId] > 0)
				{
					text += "_semen";
				}
			}
			else if (PlayerSexStatusDataManager.heroineRemainingSemenCount_vagina[selectSexBattleHeroineId] > 0)
			{
				text += "_semen";
			}
			break;
		case "anal":
			num = sexTouchManager.currentSexSkillLv;
			term = "sexBodyPassive_anal" + selectSexBattleHeroineId + "8" + num;
			text = "sexBodyTouchInfo_anal" + selectSexBattleHeroineId + num;
			if (PlayerNonSaveDataManager.isSexHeroineMenstrualDay)
			{
				if (PlayerSexStatusDataManager.heroineRemainingSemenCount_anal[selectSexBattleHeroineId] > 0)
				{
					text += "_semen";
				}
			}
			else if (PlayerSexStatusDataManager.heroineRemainingSemenCount_anal[selectSexBattleHeroineId] > 0)
			{
				text += "_semen";
			}
			break;
		}
		sexTouchManager.bodyClickInfoHeaderLocText.Term = term;
		if (PlayerNonSaveDataManager.isSexHeroineMenstrualDay)
		{
			text += "_menstrualDay";
		}
		sexTouchManager.bodyClickSummaryLocText.Term = text;
		sexTouchManager.RefreshBodyHistoryGroup();
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
