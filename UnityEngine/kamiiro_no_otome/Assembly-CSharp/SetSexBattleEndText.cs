using Arbor;
using DarkTonic.MasterAudio;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class SetSexBattleEndText : StateBehaviour
{
	public enum Type
	{
		victory = 1,
		victoryAfter,
		defeat
	}

	private SexTouchStatusManager sexTouchStatusManager;

	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

	private SexBattleMessageTextManager sexBattleMessageTextManager;

	private SexBattleFertilizationManager sexBattleFertilizationManager;

	public Type type;

	private bool isSkiped;

	private PlaySoundResult playSoundResult;

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
		sexBattleFertilizationManager = GameObject.Find("SexBattle Fertilization Manager").GetComponent<SexBattleFertilizationManager>();
	}

	public override void OnStateBegin()
	{
		isSkiped = false;
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		string text = sexTouchStatusManager.heroineSexLvStage.ToString();
		sexBattleManager.skipInfoWindow.SetActive(value: true);
		switch (type)
		{
		case Type.victory:
		{
			sexBattleMessageTextManager.ResetBattleTextMessage();
			int num4 = Random.Range(1, 3);
			int num5 = Random.Range(1, 3);
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[0].GetComponent<Localize>().Term = "sexVictoryLimit2_" + selectSexBattleHeroineId + "_" + text + num4;
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[2].GetComponent<Localize>().Term = "sexBattleVictory_0" + num5;
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[0].gameObject.SetActive(value: true);
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[2].gameObject.SetActive(value: true);
			sexBattleTurnManager.isVictoryPiston = true;
			sexBattleTurnManager.sexBattleRemainCumShotCount = PlayerSexStatusDataManager.playerSexExtasyLimit[0];
			PlayerSexStatusDataManager.playerSexHp[0] = 0;
			PlayerSexStatusDataManager.playerSexExtasyLimit[0] = 0;
			sexBattleManager.SetHeroineSprite("absorb");
			string text6 = "Voice_Absorb_" + selectSexBattleHeroineId;
			string text7 = "voice_Absorb" + selectSexBattleHeroineId + "_" + num4;
			Debug.Log("グループ名；" + text6 + "／音声名：" + text7);
			playSoundResult = MasterAudio.PlaySound(text6, 1f, null, 0f, text7 + "(Clone)", null);
			break;
		}
		case Type.victoryAfter:
		{
			int num3 = Random.Range(1, 3);
			Random.Range(1, 3);
			sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[0].GetComponent<Localize>().Term = "sexEcstasyVictory_" + selectSexBattleHeroineId + "_" + text + num3;
			sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[1].GetComponent<Localize>().Term = "sexBattleVictory_Message";
			sexBattleMessageTextManager.sexBattleMessageGroupGo_Ecstasy.SetActive(value: true);
			sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[0].gameObject.SetActive(value: true);
			sexBattleMessageTextManager.sexBattleMessageGroup_Ecstasy[1].gameObject.SetActive(value: true);
			sexBattleManager.SetHeroineSprite("battleVictory");
			string text4 = "Voice_Victory_" + selectSexBattleHeroineId + text;
			string text5 = "voice_Victory" + selectSexBattleHeroineId + "_" + text + num3;
			Debug.Log("グループ名；" + text4 + "／音声名：" + text5);
			playSoundResult = MasterAudio.PlaySound(text4, 1f, null, 0f, text5 + "(Clone)", null);
			break;
		}
		case Type.defeat:
		{
			sexBattleMessageTextManager.ResetBattleTextMessage();
			int num = Random.Range(1, 3);
			int num2 = Random.Range(1, 3);
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[0].GetComponent<Localize>().Term = "sexEcstasyLimit_0" + num;
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[9].GetComponent<Localize>().Term = "sexCumShotAfter_" + selectSexBattleHeroineId + "_" + text + num2;
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[2].GetComponent<Localize>().Term = "sexBattleDefeat_Message";
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[0].gameObject.SetActive(value: true);
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[9].gameObject.SetActive(value: true);
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[2].gameObject.SetActive(value: true);
			sexBattleManager.SetHeroineSprite("battleDefeat");
			string text2 = "Voice_Defeat_" + selectSexBattleHeroineId + text;
			string text3 = "voice_Defeat" + selectSexBattleHeroineId + "_" + text + num2 + "(Clone)";
			Debug.Log("グループ名；" + text2 + "／音声名：" + text3);
			playSoundResult = MasterAudio.PlaySound(text2, 1f, null, 0f, text3, null);
			break;
		}
		}
		if (playSoundResult != null && playSoundResult.SoundPlayed)
		{
			playSoundResult.ActingVariation.SoundFinished += InvokeMethod;
		}
	}

	public override void OnStateEnd()
	{
		sexBattleManager.skipInfoWindow.SetActive(value: false);
	}

	public override void OnStateUpdate()
	{
		if (Input.GetButtonDown("Fire2"))
		{
			isSkiped = true;
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}

	private void InvokeMethod()
	{
		if (!isSkiped)
		{
			Transition(stateLink);
		}
	}
}
