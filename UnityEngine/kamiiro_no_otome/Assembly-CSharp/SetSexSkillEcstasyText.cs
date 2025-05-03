using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using I2.Loc;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class SetSexSkillEcstasyText : StateBehaviour
{
	public enum Type
	{
		player,
		heroine
	}

	private SexTouchStatusManager sexTouchStatusManager;

	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

	private SexBattleMessageTextManager sexBattleMessageTextManager;

	private SexBattleEffectManager sexBattleEffectManager;

	private SexBattleFertilizationManager sexBattleFertilizationManager;

	public float waitTime;

	public float waitTime2;

	public Type type;

	public StateLink noLimitLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchStatusManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchStatusManager>();
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		sexBattleTurnManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleTurnManager>();
		sexBattleMessageTextManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleMessageTextManager>();
		sexBattleEffectManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleEffectManager>();
		sexBattleFertilizationManager = GameObject.Find("SexBattle Fertilization Manager").GetComponent<SexBattleFertilizationManager>();
	}

	public override void OnStateBegin()
	{
		float time = waitTime / (float)sexBattleManager.battleSpeed;
		_ = sexBattleManager.selectSexSkillData;
		int num = Random.Range(1, 3);
		int num2 = Random.Range(1, 3);
		switch (type)
		{
		case Type.player:
			sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[0].GetComponent<Localize>().Term = "sexEcstasy_0" + num;
			sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[1].GetComponent<Localize>().Term = "sexBattleTarget_02";
			sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[2].GetComponent<Localize>().Term = "sexEcstasy_Message0" + num2;
			sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[0].gameObject.SetActive(value: true);
			sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[1].gameObject.SetActive(value: true);
			sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[2].gameObject.SetActive(value: true);
			sexBattleMessageTextManager.sexBattleMessageGroupGo_Ecstasy.SetActive(value: true);
			Invoke("EcstasyFlashFadeIn", time);
			break;
		case Type.heroine:
		{
			int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
			string text = sexTouchStatusManager.heroineSexLvStage.ToString();
			string text2 = "";
			text2 = ((PlayerSexStatusDataManager.playerSexExtasyLimit[1] > 1) ? "Ecstasy" : "Ecstasy");
			sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[0].GetComponent<Localize>().Term = "sex" + text2 + "_" + selectSexBattleHeroineId + "_" + text + num;
			sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[1].GetComponent<Localize>().Term = "sexBattleTarget_" + selectSexBattleHeroineId + "1";
			sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[2].GetComponent<Localize>().Term = "sexEcstasy_Message" + selectSexBattleHeroineId + num2;
			sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[0].gameObject.SetActive(value: true);
			sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[1].gameObject.SetActive(value: true);
			sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[2].gameObject.SetActive(value: true);
			sexBattleMessageTextManager.sexBattleMessageGroupGo_Ecstasy.SetActive(value: true);
			string text3 = "Voice_" + text2 + "_" + selectSexBattleHeroineId + text;
			string text4 = "voice_" + text2 + selectSexBattleHeroineId + "_" + text + num;
			Debug.Log("グループ名；" + text3 + "／音声名：" + text4);
			MasterAudio.PlaySound(text3, 1f, null, 0f, text4 + "(Clone)", null);
			Invoke("EcstasyFlashFadeIn", time);
			break;
		}
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

	private void EcstasyFlashFadeIn()
	{
		sexBattleManager.whiteImageCanvasGroup.DOFade(1f, 0.1f).SetEase(Ease.Linear).OnComplete(delegate
		{
			EcstasyFlashFadeOut();
		});
	}

	private void EcstasyFlashFadeOut()
	{
		switch (type)
		{
		case Type.player:
		{
			PlayerSexStatusDataManager.playerSexExtasyLimit[0]--;
			sexBattleManager.playerExtasyLimitTextArray[0].text = PlayerSexStatusDataManager.playerSexExtasyLimit[0].ToString();
			sexBattleManager.SetHeroineSprite("cumShot");
			Transform transform2 = PoolManager.Pools["sexSkillPool"].Spawn(sexBattleEffectManager.effectPrefabGoDictionary["cumShot"], sexBattleManager.effectPrefabParentDictionary["vagina"].transform);
			transform2.localScale = new Vector3(1f, 1f, 1f);
			transform2.localPosition = new Vector3(0f, 0f, 0f);
			PoolManager.Pools["sexSkillPool"].Despawn(transform2, 1f, sexBattleEffectManager.sexBattleEffectPoolParent);
			MasterAudio.FadeBusToVolume("Ambience", 0f, 0.2f, null, willStopAfterFade: true, willResetVolumeAfterFade: true);
			MasterAudio.PlaySound("SexBattle_CumShot_Long", 1f, null, 0f, null, null);
			break;
		}
		case Type.heroine:
		{
			PlayerSexStatusDataManager.playerSexExtasyLimit[1]--;
			PlayerSexStatusDataManager.playerSexExtasyLimit[1] = Mathf.Clamp(PlayerSexStatusDataManager.playerSexExtasyLimit[1], 0, 10);
			sexBattleManager.playerExtasyLimitTextArray[1].text = PlayerSexStatusDataManager.playerSexExtasyLimit[1].ToString();
			sexBattleManager.SetHeroineSprite("ecstasy");
			Debug.Log("ヒロイン絶頂CG／現在の絶頂限界：" + PlayerSexStatusDataManager.playerSexExtasyLimit[1]);
			Transform transform = PoolManager.Pools["sexSkillPool"].Spawn(sexBattleEffectManager.effectPrefabGoDictionary["ecstasy"], sexBattleManager.effectPrefabParentDictionary["vagina"].transform);
			transform.localScale = new Vector3(1f, 1f, 1f);
			transform.localPosition = new Vector3(0f, 0f, 0f);
			PoolManager.Pools["sexSkillPool"].Despawn(transform, 1f, sexBattleEffectManager.sexBattleEffectPoolParent);
			break;
		}
		}
		sexBattleManager.whiteImageCanvasGroup.DOFade(0f, 0.1f).SetEase(Ease.Linear).OnComplete(delegate
		{
			InvokeMethod();
		});
	}

	private void InvokeMethod()
	{
		float time = waitTime2 / (float)sexBattleManager.battleSpeed;
		switch (type)
		{
		case Type.player:
			sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[3].GetComponent<Localize>().Term = "sexBattleTarget_01";
			sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[4].GetComponent<Localize>().Term = "sexAttack_CumShot";
			sexBattleTurnManager.sexBattlePlayerEcstasyCount++;
			if (sexBattleFertilizationManager.isInSideCumShot)
			{
				sexBattleManager.isFinishDone[0] = true;
			}
			else
			{
				sexBattleManager.isFinishDone[1] = true;
			}
			if (sexBattleManager.isFinishDone[0] && sexBattleManager.isFinishDone[1])
			{
				sexBattleManager.isFinishDone[2] = true;
			}
			break;
		case Type.heroine:
		{
			int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
			sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[3].GetComponent<Localize>().Term = "sexBattleTarget_" + selectSexBattleHeroineId + "1";
			sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[4].GetComponent<Localize>().Term = "sexAttack_Ecstasy";
			sexBattleTurnManager.sexBattleHeroineEcstasyCount++;
			if (sexBattleTurnManager.sexBattleHeroineEcstasyCount == 1 && PlayerSexStatusDataManager.heroineSexLv[PlayerNonSaveDataManager.selectSexBattleHeroineId - 1] >= 4 && !PlayerDataManager.isHeroineSexVoiceLowStage)
			{
				sexTouchStatusManager.heroineSexLvStage = SexTouchStatusManager.HeroineSexLvStage.B;
			}
			sexBattleFertilizationManager.CalcHeroineEcstasyProcess();
			break;
		}
		}
		sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[3].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[4].gameObject.SetActive(value: true);
		Invoke("InvokeMethod2", time);
	}

	private void InvokeMethod2()
	{
		bool flag = false;
		switch (type)
		{
		case Type.player:
		{
			string text = "";
			int num = Random.Range(1, 3);
			string text2 = sexTouchStatusManager.heroineSexLvStage.ToString();
			if (sexBattleFertilizationManager.isInSideCumShot)
			{
				if (PlayerNonSaveDataManager.isSexHeroineMenstrualDay && PlayerNonSaveDataManager.isMenstrualDaySexUseCondom)
				{
					text = "Condom_";
				}
				else
				{
					text = "In_";
					if (sexTouchStatusManager.heroineSexLvStage == SexTouchStatusManager.HeroineSexLvStage.B && PlayerNonSaveDataManager.isSexHeroineMenstrualDay)
					{
						num = 2;
					}
				}
			}
			else
			{
				text = "Out_";
			}
			int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
			sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[5].GetComponent<Localize>().Term = "sexCounter_Heroine" + selectSexBattleHeroineId + "_CumShot_" + text + text2 + num;
			sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[5].gameObject.SetActive(value: true);
			string text3 = "Voice_CumShot_" + text + selectSexBattleHeroineId + text2;
			string text4 = "voice_CumShot" + selectSexBattleHeroineId + "_" + text + text2 + num;
			Debug.Log("グループ名；" + text3 + "／音声名：" + text4);
			MasterAudio.PlaySound(text3, 1f, null, 0f, text4 + "(Clone)", null);
			if (PlayerSexStatusDataManager.playerSexExtasyLimit[0] <= 0)
			{
				flag = true;
			}
			break;
		}
		case Type.heroine:
			if (PlayerSexStatusDataManager.playerSexExtasyLimit[1] <= 0)
			{
				flag = true;
			}
			break;
		}
		Debug.Log("タイプ：" + type.ToString() + "／絶頂限界である：" + flag);
		if (flag)
		{
			Transition(noLimitLink);
		}
		else
		{
			Transition(stateLink);
		}
	}
}
