using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using I2.Loc;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class SetSexBattleVictoryEcstasyText : StateBehaviour
{
	private SexTouchStatusManager sexTouchStatusManager;

	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

	private SexBattleMessageTextManager sexBattleMessageTextManager;

	private SexBattleEffectManager sexBattleEffectManager;

	private SexBattleFertilizationManager sexBattleFertilizationManager;

	public float waitTime;

	public float waitTime2;

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
		string text = "";
		string text2 = sexTouchStatusManager.heroineSexLvStage.ToString();
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		int num = Random.Range(1, 3);
		Random.Range(1, 3);
		text = ((!sexBattleFertilizationManager.isInSideCumShot) ? "Out_" : ((!PlayerNonSaveDataManager.isMenstrualDaySexUseCondom) ? "In_" : "Condom_"));
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[0].GetComponent<Localize>().Term = "sexDefense_Heroine" + selectSexBattleHeroineId + "_VictoryPiston_" + text + text2 + num;
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[1].GetComponent<Localize>().Term = "sexBattleTarget_" + selectSexBattleHeroineId + "1";
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[3].GetComponent<Localize>().Term = "sexBattleLimit_Message";
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[0].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[1].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[3].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroupGo_Bottom.SetActive(value: true);
		string text3 = "Voice_VictoryPiston_" + text + selectSexBattleHeroineId + text2;
		string text4 = "voice_VictoryPiston" + selectSexBattleHeroineId + "_" + text + text2 + num;
		Debug.Log("グループ名；" + text3 + "／音声名：" + text4);
		MasterAudio.PlaySound(text3, 1f, null, 0f, text4 + "(Clone)", null);
		Invoke("InvokeMethod2", time);
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
		Transform transform = PoolManager.Pools["sexSkillPool"].Spawn(sexBattleEffectManager.effectPrefabGoDictionary["ecstasy"], sexBattleManager.effectPrefabParentDictionary["vagina"].transform);
		transform.localScale = new Vector3(1f, 1f, 1f);
		transform.localPosition = new Vector3(0f, 0f, 0f);
		PoolManager.Pools["sexSkillPool"].Despawn(transform, 1f, sexBattleEffectManager.sexBattleEffectPoolParent);
		sexBattleManager.whiteImageCanvasGroup.DOFade(0f, 0.1f).SetEase(Ease.Linear).OnComplete(delegate
		{
			InvokeMethod2();
		});
	}

	private void InvokeMethod()
	{
		float time = waitTime2 / (float)sexBattleManager.battleSpeed;
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[4].GetComponent<Localize>().Term = "sexBattleTarget_" + selectSexBattleHeroineId + "1";
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[6].GetComponent<Localize>().Term = "sexAttack_Ecstasy";
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[4].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[6].gameObject.SetActive(value: true);
		sexBattleTurnManager.sexBattleHeroineEcstasyCount++;
		Invoke("InvokeMethod2", time);
	}

	private void InvokeMethod2()
	{
		Transition(stateLink);
	}
}
