using Arbor;
using DarkTonic.MasterAudio;
using I2.Loc;
using PathologicalGames;
using TMPro;
using UnityEngine;

[AddComponentMenu("")]
public class SetSexSkillDamageText : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

	private SexBattleMessageTextManager sexBattleMessageTextManager;

	private SexBattleEffectManager sexBattleEffectManager;

	public float waitTime;

	public float waitTime2;

	public float despawnTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		sexBattleTurnManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleTurnManager>();
		sexBattleMessageTextManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleMessageTextManager>();
		sexBattleEffectManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleEffectManager>();
	}

	public override void OnStateBegin()
	{
		float time = waitTime / (float)sexBattleManager.battleSpeed;
		SetSexBattleTopMessage_HeroineVoice();
		sexBattleMessageTextManager.sexBattleMessageGroup_Top[2].gameObject.SetActive(value: true);
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

	private void SetSexBattleTopMessage_HeroineVoice()
	{
		SexSkillData selectSexSkillData = sexBattleManager.selectSexSkillData;
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		int num = 0;
		string text = "";
		string text2 = "";
		switch (selectSexSkillData.actionType)
		{
		case SexSkillData.ActionType.piston:
		{
			string text4 = "";
			switch (selectSexSkillData.narrativePartString)
			{
			case "piston":
				num = Random.Range(1, 4);
				text4 = "_Piston";
				text = "Voice_Piston_" + selectSexBattleHeroineId;
				text2 = "voice_Piston" + selectSexBattleHeroineId + "_" + num;
				break;
			case "hardPiston":
				num = Random.Range(1, 4);
				text4 = "_HardPiston";
				text = "Voice_Piston_" + selectSexBattleHeroineId;
				text2 = "voice_Piston" + selectSexBattleHeroineId + "_" + num;
				break;
			case "gSpotPiston":
				num = Random.Range(1, 4);
				text4 = "_GSpotPiston";
				text = "Voice_HardPiston_" + selectSexBattleHeroineId;
				text2 = "voice_HardPiston" + selectSexBattleHeroineId + "_" + num;
				break;
			case "portioPiston":
				num = Random.Range(1, 4);
				text4 = "_PortioPiston";
				text = "Voice_HardPiston_" + selectSexBattleHeroineId;
				text2 = "voice_HardPiston" + selectSexBattleHeroineId + "_" + num;
				break;
			case "fertilizePiston":
				num = Random.Range(1, 4);
				text4 = ((!sexBattleTurnManager.isFertilizeRepeatPiston) ? "_FertilizePiston1_" : "_FertilizePiston2_");
				text = "Voice_HardPiston_" + selectSexBattleHeroineId;
				text2 = "voice_HardPiston" + selectSexBattleHeroineId + "_" + num;
				break;
			case "berserkPiston":
				num = Random.Range(1, 3);
				text4 = "_BerserkPiston";
				text = "Voice_BerserkPiston_" + selectSexBattleHeroineId;
				text2 = "voice_BerserkPiston_After" + selectSexBattleHeroineId + "_" + num;
				break;
			}
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[2].GetComponent<Localize>().Term = "sexDefense_Heroine" + selectSexBattleHeroineId + text4 + num;
			break;
		}
		case SexSkillData.ActionType.kiss:
			num = Random.Range(1, 3);
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[2].GetComponent<Localize>().Term = "sexDefense_Heroine" + selectSexBattleHeroineId + "_Kiss" + num;
			text = "Voice_Kiss_" + selectSexBattleHeroineId;
			text2 = "voice_Kiss" + selectSexBattleHeroineId + "_" + num;
			break;
		case SexSkillData.ActionType.caress:
		{
			num = Random.Range(1, 4);
			string text3 = "";
			int num2 = PlayerSexStatusDataManager.playerSexMaxHp[selectSexBattleHeroineId] / PlayerSexStatusDataManager.playerSexHp[selectSexBattleHeroineId];
			text3 = (((double)num2 <= 0.3) ? "_Max" : ((!((double)num2 <= 0.6)) ? "_Normal" : "_High"));
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[2].GetComponent<Localize>().Term = "sexDefense_Heroine" + selectSexBattleHeroineId + text3 + num;
			text = "Voice_Caress_" + selectSexBattleHeroineId;
			text2 = "voice_Caress" + selectSexBattleHeroineId + "_" + num;
			break;
		}
		case SexSkillData.ActionType.heal:
			num = Random.Range(1, 4);
			switch (selectSexSkillData.narrativePartString)
			{
			case "voice":
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[2].GetComponent<Localize>().Term = "sexDefense_Heroine" + selectSexBattleHeroineId + "_Term" + num;
				break;
			case "breath":
			case "concentration":
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[2].GetComponent<Localize>().Term = "sexDefense_Heroine" + selectSexBattleHeroineId + "_Heal" + num;
				break;
			}
			text = "Voice_Caress_" + selectSexBattleHeroineId;
			text2 = "voice_Caress" + selectSexBattleHeroineId + "_" + num;
			break;
		}
		Debug.Log("グループ名；" + text + "／音声名：" + text2);
		MasterAudio.PlaySound(text, 1f, null, 0f, text2 + "(Clone)", null);
	}

	private void InvokeMethod()
	{
		float time = waitTime2 / (float)sexBattleManager.battleSpeed;
		if (sexBattleManager.selectSexSkillData.skillType == SexSkillData.SkillType.heal)
		{
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[3].GetComponent<Localize>().Term = "sexBattleHealDamage1";
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[4].text = sexBattleTurnManager.sexBattleHealValue.ToString();
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[5].GetComponent<Localize>().Term = "sexBattleHealDamage2";
			Transform transform = PoolManager.Pools["sexBattlePool"].Spawn(sexBattleEffectManager.sexBattleEffectTextGoArray[1], sexBattleEffectManager.sexBattleEffectSpawnPoint[0]);
			transform.GetComponent<TextMeshProUGUI>().text = sexBattleTurnManager.sexBattleHealValue.ToString();
			sexBattleEffectManager.SetEffectDeSpawnReserve(transform, isSkillPool: false, despawnTime);
			sexBattleMessageTextManager.sexBattleMessageGroupGo_Self.SetActive(value: false);
		}
		else
		{
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[3].GetComponent<Localize>().Term = "sexBattleTarget_" + PlayerNonSaveDataManager.selectSexBattleHeroineId + 0;
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[4].text = sexBattleTurnManager.sexBattleDamageValue.ToString();
			int num = 0;
			if (sexBattleTurnManager.isCriticalSuccess)
			{
				num = 2;
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[5].GetComponent<Localize>().Term = "sexBattleAddCriticalDamage";
			}
			else
			{
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[5].GetComponent<Localize>().Term = "sexBattleAddDamage";
			}
			Transform transform2 = PoolManager.Pools["sexBattlePool"].Spawn(sexBattleEffectManager.sexBattleEffectTextGoArray[num], sexBattleEffectManager.sexBattleEffectSpawnPoint[1]);
			transform2.GetComponent<TextMeshProUGUI>().text = sexBattleTurnManager.sexBattleDamageValue.ToString();
			sexBattleEffectManager.SetEffectDeSpawnReserve(transform2, isSkillPool: false, despawnTime);
		}
		sexBattleMessageTextManager.sexBattleMessageGroupGo_DamageRaw[0].SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Top[3].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Top[4].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Top[5].gameObject.SetActive(value: true);
		Invoke("InvokeMethod2", time);
	}

	private void InvokeMethod2()
	{
		Transition(stateLink);
	}
}
