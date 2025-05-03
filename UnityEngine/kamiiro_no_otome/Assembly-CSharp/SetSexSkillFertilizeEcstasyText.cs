using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using I2.Loc;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class SetSexSkillFertilizeEcstasyText : StateBehaviour
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
		sexBattleMessageTextManager.sexBattleMessageGroup_Fertilize[0].GetComponent<Localize>().Term = "sexFertilizeEcstacy_0" + num;
		sexBattleMessageTextManager.sexBattleMessageGroup_Fertilize[1].GetComponent<Localize>().Term = "sexBattleTarget_02";
		sexBattleMessageTextManager.sexBattleMessageGroup_Fertilize[2].GetComponent<Localize>().Term = "sexEcstasy_Message0" + num2;
		sexBattleMessageTextManager.sexBattleMessageGroup_Fertilize[0].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Fertilize[1].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Fertilize[2].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroupGo_Fertilize.SetActive(value: true);
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
		sexBattleFertilizationManager.ChangeInsideCumShotToggle(0);
		sexBattleManager.SetHeroineSprite("cumShot");
		Transform transform = PoolManager.Pools["sexSkillPool"].Spawn(sexBattleEffectManager.effectPrefabGoDictionary["cumShot"], sexBattleManager.effectPrefabParentDictionary["vagina"].transform);
		transform.localScale = new Vector3(1f, 1f, 1f);
		transform.localPosition = new Vector3(0f, 0f, 0f);
		PoolManager.Pools["sexSkillPool"].Despawn(transform, 1f, sexBattleEffectManager.sexBattleEffectPoolParent);
		MasterAudio.FadeBusToVolume("Ambience", 0f, 0.2f, null, willStopAfterFade: true, willResetVolumeAfterFade: true);
		MasterAudio.PlaySound("SexBattle_CumShot_Long", 1f, null, 0f, null, null);
		sexBattleManager.whiteImageCanvasGroup.DOFade(0f, 0.1f).SetEase(Ease.Linear).OnComplete(delegate
		{
			InvokeMethod();
		});
	}

	private void InvokeMethod()
	{
		float time = waitTime2 / (float)sexBattleManager.battleSpeed;
		sexBattleMessageTextManager.sexBattleMessageGroup_Fertilize[3].GetComponent<Localize>().Term = "sexBattleTarget_01";
		sexBattleMessageTextManager.sexBattleMessageGroup_Fertilize[4].GetComponent<Localize>().Term = "sexAttack_CumShot";
		PlayerSexStatusDataManager.playerSexExtasyLimit[0]--;
		sexBattleManager.playerExtasyLimitTextArray[0].text = PlayerSexStatusDataManager.playerSexExtasyLimit[0].ToString();
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
		sexBattleMessageTextManager.sexBattleMessageGroup_Fertilize[3].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Fertilize[4].gameObject.SetActive(value: true);
		Invoke("InvokeMethod2", time);
	}

	private void InvokeMethod2()
	{
		Transition(stateLink);
	}
}
