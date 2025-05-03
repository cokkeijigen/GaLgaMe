using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using I2.Loc;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class SetSexBattleVictoryEcstasyText2 : StateBehaviour
{
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
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		sexBattleTurnManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleTurnManager>();
		sexBattleMessageTextManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleMessageTextManager>();
		sexBattleEffectManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleEffectManager>();
		sexBattleFertilizationManager = GameObject.Find("SexBattle Fertilization Manager").GetComponent<SexBattleFertilizationManager>();
	}

	public override void OnStateBegin()
	{
		float time = waitTime / (float)sexBattleManager.battleSpeed;
		int num = Random.Range(1, 3);
		int num2 = Random.Range(1, 3);
		sexBattleMessageTextManager.sexBattleMessageGroup_Victory[0].GetComponent<Localize>().Term = "sexEcstasy_0" + num;
		sexBattleMessageTextManager.sexBattleMessageGroup_Victory[1].GetComponent<Localize>().Term = "sexBattleTarget_02";
		sexBattleMessageTextManager.sexBattleMessageGroup_Victory[3].GetComponent<Localize>().Term = "sexEcstasy_Message0" + num2;
		sexBattleMessageTextManager.sexBattleMessageGroup_Victory[0].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Victory[1].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Victory[3].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroupGo_Victory.SetActive(value: true);
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
		sexBattleManager.SetHeroineSprite("victoryCumShot");
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
		sexBattleMessageTextManager.sexBattleMessageGroup_Victory[4].GetComponent<Localize>().Term = "sexBattleTarget_01";
		sexBattleMessageTextManager.sexBattleMessageGroup_Victory[6].GetComponent<Localize>().Term = "sexAttack_CumShot";
		sexBattleMessageTextManager.sexBattleMessageGroup_Victory[4].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Victory[6].gameObject.SetActive(value: true);
		sexBattleManager.playerExtasyLimitTextArray[0].text = "0";
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
		Invoke("InvokeMethod2", time);
	}

	private void InvokeMethod2()
	{
		Transition(stateLink);
	}
}
