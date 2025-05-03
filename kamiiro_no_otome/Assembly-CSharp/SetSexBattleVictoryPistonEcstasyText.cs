using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using I2.Loc;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class SetSexBattleVictoryPistonEcstasyText : StateBehaviour
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
		string text = sexTouchStatusManager.heroineSexLvStage.ToString();
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		int num = Random.Range(1, 3);
		int num2 = Random.Range(1, 3);
		sexBattleMessageTextManager.sexBattleMessageGroup_Fertilize[0].GetComponent<Localize>().Term = "sexEcstasyLimit_" + selectSexBattleHeroineId + "_" + text + num;
		sexBattleMessageTextManager.sexBattleMessageGroup_Fertilize[1].GetComponent<Localize>().Term = "sexBattleTarget_" + selectSexBattleHeroineId + "1";
		sexBattleMessageTextManager.sexBattleMessageGroup_Fertilize[2].GetComponent<Localize>().Term = "sexEcstasy_Message" + selectSexBattleHeroineId + num2;
		sexBattleMessageTextManager.sexBattleMessageGroup_Fertilize[0].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Fertilize[1].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Fertilize[2].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroupGo_Fertilize.SetActive(value: true);
		string text2 = "Voice_EcstasyLimit_" + selectSexBattleHeroineId + text;
		string text3 = "voice_EcstasyLimit" + selectSexBattleHeroineId + "_" + text + num;
		Debug.Log("グループ名；" + text2 + "／音声名：" + text3);
		MasterAudio.PlaySound(text2, 1f, null, 0f, text3 + "(Clone)", null);
		Invoke("EcstasyFlashFadeIn", time);
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
		sexBattleManager.SetHeroineSprite("victoryEcstasy");
		Transform transform = PoolManager.Pools["sexSkillPool"].Spawn(sexBattleEffectManager.effectPrefabGoDictionary["ecstasy"], sexBattleManager.effectPrefabParentDictionary["vagina"].transform);
		transform.localScale = new Vector3(1f, 1f, 1f);
		transform.localPosition = new Vector3(0f, 0f, 0f);
		PoolManager.Pools["sexSkillPool"].Despawn(transform, 1f, sexBattleEffectManager.sexBattleEffectPoolParent);
		sexBattleManager.whiteImageCanvasGroup.DOFade(0f, 0.1f).SetEase(Ease.Linear).OnComplete(delegate
		{
			InvokeMethod();
		});
	}

	private void InvokeMethod()
	{
		float time = waitTime2 / (float)sexBattleManager.battleSpeed;
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		sexBattleMessageTextManager.sexBattleMessageGroup_Fertilize[3].GetComponent<Localize>().Term = "sexBattleTarget_" + selectSexBattleHeroineId + "1";
		sexBattleMessageTextManager.sexBattleMessageGroup_Fertilize[4].GetComponent<Localize>().Term = "sexAttack_Ecstasy";
		sexBattleMessageTextManager.sexBattleMessageGroup_Fertilize[3].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Fertilize[4].gameObject.SetActive(value: true);
		sexBattleTurnManager.sexBattleHeroineEcstasyCount++;
		Invoke("InvokeMethod2", time);
	}

	private void InvokeMethod2()
	{
		Transition(stateLink);
	}
}
