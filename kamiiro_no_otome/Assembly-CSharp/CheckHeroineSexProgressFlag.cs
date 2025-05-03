using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckHeroineSexProgressFlag : StateBehaviour
{
	private SexTouchManager sexTouchManager;

	private SexTouchStatusManager sexTouchStatusManager;

	private SexTouchHeroineDataManager sexTouchHeroineDataManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchManager = GameObject.Find("Sex Touch Manager").GetComponent<SexTouchManager>();
		sexTouchStatusManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchStatusManager>();
		sexTouchHeroineDataManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchHeroineDataManager>();
	}

	public override void OnStateBegin()
	{
		PlayerSexStatusDataManager.SetUpPlayerSexStatus(isBattle: false);
		PlayerSexBattleConditionAccess.SexBattleConditionInititialize();
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		int heroineIndex = selectSexBattleHeroineId - 1;
		if (PlayerSexStatusDataManager.heroineTouchSexFlagArray[heroineIndex])
		{
			sexTouchManager.commandButtonArray[0].gameObject.SetActive(value: true);
			if (PlayerSexStatusDataManager.heroineTouchCumShotFlagArray[heroineIndex])
			{
				sexTouchManager.commandButtonArray[1].gameObject.SetActive(value: true);
			}
		}
		foreach (SexHeroinePassiveData item in GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[heroineIndex].sexHeroinePassiveDataList.Where((SexHeroinePassiveData data) => data.bodyCategory == SexHeroinePassiveData.BodyCategory.mouth && data.skillUnlockLv == PlayerSexStatusDataManager.heroineMouthLv[heroineIndex]))
		{
			sexTouchHeroineDataManager.heroineHaveSexPassiveMouthList.Add(item.skillID);
		}
		foreach (SexHeroinePassiveData item2 in GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[heroineIndex].sexHeroinePassiveDataList.Where((SexHeroinePassiveData data) => data.bodyCategory == SexHeroinePassiveData.BodyCategory.hand && data.skillUnlockLv == PlayerSexStatusDataManager.heroineHandLv[heroineIndex]))
		{
			sexTouchHeroineDataManager.heroineHaveSexPassiveHandList.Add(item2.skillID);
		}
		foreach (SexHeroinePassiveData item3 in GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[heroineIndex].sexHeroinePassiveDataList.Where((SexHeroinePassiveData data) => data.bodyCategory == SexHeroinePassiveData.BodyCategory.tits && data.skillUnlockLv == PlayerSexStatusDataManager.heroineTitsLv[heroineIndex]))
		{
			sexTouchHeroineDataManager.heroineHaveSexPassiveTitsList.Add(item3.skillID);
		}
		foreach (SexHeroinePassiveData item4 in GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[heroineIndex].sexHeroinePassiveDataList.Where((SexHeroinePassiveData data) => data.bodyCategory == SexHeroinePassiveData.BodyCategory.nipple && data.skillUnlockLv == PlayerSexStatusDataManager.heroineNippleLv[heroineIndex]))
		{
			sexTouchHeroineDataManager.heroineHaveSexPassiveNippleList.Add(item4.skillID);
		}
		foreach (SexHeroinePassiveData item5 in GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[heroineIndex].sexHeroinePassiveDataList.Where((SexHeroinePassiveData data) => data.bodyCategory == SexHeroinePassiveData.BodyCategory.womb && data.skillUnlockLv == PlayerSexStatusDataManager.heroineWombsLv[heroineIndex]))
		{
			sexTouchHeroineDataManager.heroineHaveSexPassiveWombsList.Add(item5.skillID);
		}
		foreach (SexHeroinePassiveData item6 in GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[heroineIndex].sexHeroinePassiveDataList.Where((SexHeroinePassiveData data) => data.bodyCategory == SexHeroinePassiveData.BodyCategory.clitoris && data.skillUnlockLv == PlayerSexStatusDataManager.heroineClitorisLv[heroineIndex]))
		{
			sexTouchHeroineDataManager.heroineHaveSexPassiveClitorisList.Add(item6.skillID);
		}
		foreach (SexHeroinePassiveData item7 in GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[heroineIndex].sexHeroinePassiveDataList.Where((SexHeroinePassiveData data) => data.bodyCategory == SexHeroinePassiveData.BodyCategory.vagina && data.skillUnlockLv == PlayerSexStatusDataManager.heroineVaginaLv[heroineIndex]))
		{
			sexTouchHeroineDataManager.heroineHaveSexPassiveVaginaList.Add(item7.skillID);
		}
		foreach (SexHeroinePassiveData item8 in GameDataManager.instance.heroineSexPassiveDataBase.sexHeroinePassiveDataAllList[heroineIndex].sexHeroinePassiveDataList.Where((SexHeroinePassiveData data) => data.bodyCategory == SexHeroinePassiveData.BodyCategory.anal && data.skillUnlockLv == PlayerSexStatusDataManager.heroineAnalLv[heroineIndex]))
		{
			sexTouchHeroineDataManager.heroineHaveSexPassiveAnalList.Add(item8.skillID);
		}
		for (int i = 0; i < PlayerSexStatusDataManager.playerUseSexActiveSkillList[selectSexBattleHeroineId].Count; i++)
		{
			SexSkillData sexSkillData = PlayerSexStatusDataManager.playerUseSexActiveSkillList[selectSexBattleHeroineId][i];
			if (sexSkillData.bodyCategory == SexSkillData.BodyCategory.mouth)
			{
				sexTouchHeroineDataManager.heroineHaveSexSkillMouthList.Add(sexSkillData.skillID);
			}
		}
		for (int j = 0; j < PlayerSexStatusDataManager.playerUseSexActiveSkillList[selectSexBattleHeroineId].Count; j++)
		{
			SexSkillData sexSkillData2 = PlayerSexStatusDataManager.playerUseSexActiveSkillList[selectSexBattleHeroineId][j];
			if (sexSkillData2.bodyCategory == SexSkillData.BodyCategory.hand)
			{
				sexTouchHeroineDataManager.heroineHaveSexSkillHandList.Add(sexSkillData2.skillID);
			}
		}
		for (int k = 0; k < PlayerSexStatusDataManager.playerUseSexActiveSkillList[selectSexBattleHeroineId].Count; k++)
		{
			SexSkillData sexSkillData3 = PlayerSexStatusDataManager.playerUseSexActiveSkillList[selectSexBattleHeroineId][k];
			if (sexSkillData3.bodyCategory == SexSkillData.BodyCategory.tits)
			{
				sexTouchHeroineDataManager.heroineHaveSexSkillTitsList.Add(sexSkillData3.skillID);
			}
		}
		for (int l = 0; l < PlayerSexStatusDataManager.playerUseSexActiveSkillList[selectSexBattleHeroineId].Count; l++)
		{
			SexSkillData sexSkillData4 = PlayerSexStatusDataManager.playerUseSexActiveSkillList[selectSexBattleHeroineId][l];
			if (sexSkillData4.bodyCategory == SexSkillData.BodyCategory.nipple)
			{
				sexTouchHeroineDataManager.heroineHaveSexSkillNippleList.Add(sexSkillData4.skillID);
			}
		}
		for (int m = 0; m < PlayerSexStatusDataManager.playerUseSexActiveSkillList[selectSexBattleHeroineId].Count; m++)
		{
			SexSkillData sexSkillData5 = PlayerSexStatusDataManager.playerUseSexActiveSkillList[selectSexBattleHeroineId][m];
			if (sexSkillData5.bodyCategory == SexSkillData.BodyCategory.womb)
			{
				sexTouchHeroineDataManager.heroineHaveSexSkillWombsList.Add(sexSkillData5.skillID);
			}
		}
		for (int n = 0; n < PlayerSexStatusDataManager.playerUseSexActiveSkillList[selectSexBattleHeroineId].Count; n++)
		{
			SexSkillData sexSkillData6 = PlayerSexStatusDataManager.playerUseSexActiveSkillList[selectSexBattleHeroineId][n];
			if (sexSkillData6.bodyCategory == SexSkillData.BodyCategory.vagina || sexSkillData6.bodyCategory == SexSkillData.BodyCategory.both)
			{
				sexTouchHeroineDataManager.heroineHaveSexSkillVaginaList.Add(sexSkillData6.skillID);
			}
		}
		sexTouchStatusManager.SetHeroineSexLvStage();
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
