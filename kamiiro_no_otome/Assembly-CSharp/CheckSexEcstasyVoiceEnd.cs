using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class CheckSexEcstasyVoiceEnd : StateBehaviour
{
	public enum Type
	{
		CumShot = 0,
		Fertilize = 1,
		VictoryPiston = 2,
		BerserkAfter = 3,
		Ecstasy = 5,
		EcstasyLimit = 6,
		VictoryEcstasy = 7,
		Victory = 8,
		Defeat = 9
	}

	private SexTouchStatusManager sexTouchStatusManager;

	private SexBattleManager sexBattleManager;

	private SexBattleFertilizationManager sexBattleFertilizationManager;

	private bool isPlayStart;

	private string checkVoiceGroupName;

	public Type type;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchStatusManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchStatusManager>();
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		sexBattleFertilizationManager = GameObject.Find("SexBattle Fertilization Manager").GetComponent<SexBattleFertilizationManager>();
	}

	public override void OnStateBegin()
	{
		isPlayStart = false;
		string text = sexTouchStatusManager.heroineSexLvStage.ToString();
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		sexBattleManager.skipInfoWindow.SetActive(value: true);
		switch (type)
		{
		case Type.CumShot:
			if (sexBattleFertilizationManager.isInSideCumShot)
			{
				if (PlayerNonSaveDataManager.isSexHeroineMenstrualDay && PlayerNonSaveDataManager.isMenstrualDaySexUseCondom)
				{
					checkVoiceGroupName = "Voice_CumShot_Condom_" + selectSexBattleHeroineId + text;
				}
				else
				{
					checkVoiceGroupName = "Voice_CumShot_In_" + selectSexBattleHeroineId + text;
				}
			}
			else
			{
				checkVoiceGroupName = "Voice_CumShot_Out_" + selectSexBattleHeroineId + text;
			}
			break;
		case Type.Fertilize:
			checkVoiceGroupName = "Voice_Fertilize_" + selectSexBattleHeroineId + text;
			break;
		case Type.VictoryPiston:
			if (sexBattleFertilizationManager.isInSideCumShot)
			{
				if (PlayerNonSaveDataManager.isSexHeroineMenstrualDay && PlayerNonSaveDataManager.isMenstrualDaySexUseCondom)
				{
					checkVoiceGroupName = "Voice_VictoryPiston_Condom_" + selectSexBattleHeroineId + text;
				}
				else
				{
					checkVoiceGroupName = "Voice_VictoryPiston_In_" + selectSexBattleHeroineId + text;
				}
			}
			else
			{
				checkVoiceGroupName = "Voice_VictoryPiston_Out_" + selectSexBattleHeroineId + text;
			}
			break;
		case Type.BerserkAfter:
			checkVoiceGroupName = "Voice_BerserkPiston_" + selectSexBattleHeroineId;
			break;
		case Type.Ecstasy:
			checkVoiceGroupName = "Voice_Ecstasy_" + selectSexBattleHeroineId + text;
			break;
		case Type.EcstasyLimit:
			checkVoiceGroupName = "Voice_EcstasyLimit_" + selectSexBattleHeroineId + text;
			break;
		case Type.VictoryEcstasy:
			checkVoiceGroupName = "Voice_EcstasyLimit_" + selectSexBattleHeroineId + text;
			break;
		case Type.Victory:
			checkVoiceGroupName = "Voice_Victory_" + selectSexBattleHeroineId + text;
			break;
		case Type.Defeat:
			checkVoiceGroupName = "Voice_Defeat_" + selectSexBattleHeroineId + text;
			break;
		}
		Debug.Log("音声終了待ち：" + checkVoiceGroupName);
		isPlayStart = true;
	}

	public override void OnStateEnd()
	{
		isPlayStart = false;
		sexBattleManager.skipInfoWindow.SetActive(value: false);
	}

	public override void OnStateUpdate()
	{
		if ((!MasterAudio.IsSoundGroupPlaying(checkVoiceGroupName) && isPlayStart) || Input.GetButtonDown("Fire2"))
		{
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
