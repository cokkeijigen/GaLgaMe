using Arbor;
using DarkTonic.MasterAudio;
using I2.Loc;
using PathologicalGames;
using TMPro;
using UnityEngine;

[AddComponentMenu("")]
public class SetSexSkillFertilizeDamageText : StateBehaviour
{
	private SexTouchStatusManager sexTouchStatusManager;

	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

	private SexBattleMessageTextManager sexBattleMessageTextManager;

	private SexBattleEffectManager sexBattleEffectManager;

	private SexBattleFertilizationManager sexBattleFertilizationManager;

	public float waitTime;

	public float waitTime2;

	public float despawnTime;

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
		string text = sexTouchStatusManager.heroineSexLvStage.ToString();
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		int num = Random.Range(1, 3);
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[0].GetComponent<Localize>().Term = "sexDefense_Heroine" + selectSexBattleHeroineId + "_FertilizePiston3_" + text + num;
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[0].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroupGo_Bottom.SetActive(value: true);
		sexBattleFertilizationManager.CalcHeroineEcstasyProcess();
		string text2 = "Voice_Fertilize_" + selectSexBattleHeroineId + text;
		string text3 = "voice_Fertilize" + selectSexBattleHeroineId + "_In_" + text + num;
		Debug.Log("グループ名；" + text2 + "／音声名：" + text3);
		MasterAudio.PlaySound(text2, 1f, null, 0f, text3 + "(Clone)", null);
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
		float time = waitTime2 / (float)sexBattleManager.battleSpeed;
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[4].GetComponent<Localize>().Term = "sexBattleTarget_" + PlayerNonSaveDataManager.selectSexBattleHeroineId + 0;
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[5].text = sexBattleTurnManager.sexBattleDamageValue.ToString();
		Transform transform = PoolManager.Pools["sexBattlePool"].Spawn(sexBattleEffectManager.sexBattleEffectTextGoArray[0], sexBattleEffectManager.sexBattleEffectSpawnPoint[1]);
		transform.GetComponent<TextMeshProUGUI>().text = sexBattleTurnManager.sexBattleDamageValue.ToString();
		sexBattleEffectManager.SetEffectDeSpawnReserve(transform, isSkillPool: false, despawnTime);
		sexBattleMessageTextManager.sexBattleMessageGroupGo_DamageRaw[1].SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[4].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[5].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[6].gameObject.SetActive(value: true);
		Invoke("InvokeMethod2", time);
	}

	private void InvokeMethod2()
	{
		Transition(stateLink);
	}
}
