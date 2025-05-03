using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class SexBattleUiAnimation_Ecstasy : StateBehaviour
{
	public enum Type
	{
		player,
		heroine
	}

	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

	private SexBattleEffectManager sexBattleEffectManager;

	public Type type;

	public float animationTime;

	private int[] currentHpArray = new int[2];

	private int[] afterHpArray = new int[2];

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		sexBattleTurnManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleTurnManager>();
		sexBattleEffectManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleEffectManager>();
	}

	public override void OnStateBegin()
	{
		float num = animationTime / (float)sexBattleManager.battleSpeed;
		_ = sexBattleManager.selectSexSkillData;
		switch (type)
		{
		case Type.player:
		{
			CalcChangeSexBattleParameter();
			float num3 = (float)afterHpArray[0] / (float)PlayerSexStatusDataManager.playerSexMaxHp[0];
			float endValue2 = num3 * 0.88f + 0.06f;
			Debug.Log("エデンの残りHP：" + afterHpArray[0] + "／エデンのHPFill比率：" + num3);
			DOTween.To(() => currentHpArray[0], delegate(int x)
			{
				currentHpArray[0] = x;
			}, afterHpArray[0], num);
			sexBattleManager.playerHpImageArray[0].DOFillAmount(endValue2, num);
			break;
		}
		case Type.heroine:
		{
			CalcChangeSexBattleParameter();
			float num2 = (float)afterHpArray[1] / (float)PlayerSexStatusDataManager.playerSexMaxHp[PlayerNonSaveDataManager.selectSexBattleHeroineId];
			float endValue = num2 * 0.88f + 0.06f;
			Debug.Log("ヒロインの残りHP：" + afterHpArray[1] + "／ヒロインのHPFill比率：" + num2);
			DOTween.To(() => currentHpArray[1], delegate(int x)
			{
				currentHpArray[1] = x;
			}, afterHpArray[1], num);
			sexBattleManager.playerHpImageArray[1].DOFillAmount(endValue, num);
			break;
		}
		}
		float time = num + 0.1f;
		Invoke("InvokeMethod", time);
	}

	public override void OnStateEnd()
	{
		RefreshSexBattleUi();
	}

	public override void OnStateUpdate()
	{
		RefreshSexBattleUi();
	}

	public override void OnStateLateUpdate()
	{
	}

	private void RefreshSexBattleUi()
	{
		sexBattleManager.playerCurrentHpTextArray[0].text = currentHpArray[0].ToString();
		sexBattleManager.playerCurrentHpTextArray[1].text = currentHpArray[1].ToString();
	}

	private void InvokeMethod()
	{
		PlayerSexStatusDataManager.playerSexHp[0] = afterHpArray[0];
		PlayerSexStatusDataManager.playerSexHp[PlayerNonSaveDataManager.selectSexBattleHeroineId] = afterHpArray[1];
		Transition(stateLink);
	}

	private void CalcChangeSexBattleParameter()
	{
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		switch (type)
		{
		case Type.player:
			if (!sexBattleTurnManager.isVictoryPiston)
			{
				currentHpArray[0] = PlayerSexStatusDataManager.playerSexHp[0];
				afterHpArray[0] = PlayerSexStatusDataManager.playerSexMaxHp[0];
			}
			else
			{
				currentHpArray[0] = PlayerSexStatusDataManager.playerSexHp[0];
				afterHpArray[0] = PlayerSexStatusDataManager.playerSexHp[0];
			}
			currentHpArray[1] = PlayerSexStatusDataManager.playerSexHp[selectSexBattleHeroineId];
			afterHpArray[1] = PlayerSexStatusDataManager.playerSexHp[selectSexBattleHeroineId];
			break;
		case Type.heroine:
			currentHpArray[0] = PlayerSexStatusDataManager.playerSexHp[0];
			afterHpArray[0] = PlayerSexStatusDataManager.playerSexHp[0];
			currentHpArray[1] = PlayerSexStatusDataManager.playerSexHp[selectSexBattleHeroineId];
			afterHpArray[1] = PlayerSexStatusDataManager.playerSexMaxHp[selectSexBattleHeroineId];
			break;
		}
		if (!sexBattleTurnManager.isVictoryPiston)
		{
			CalcChangeTrance();
		}
	}

	private void CalcChangeTrance()
	{
		switch (type)
		{
		case Type.player:
		{
			int sexBattleAddTranceValue2 = Random.Range(0, 10);
			sexBattleTurnManager.sexBattleAddSelfTranceValue = -30;
			sexBattleTurnManager.sexBattleAddTranceValue = sexBattleAddTranceValue2;
			break;
		}
		case Type.heroine:
		{
			int sexBattleAddTranceValue = Random.Range(5, 15);
			sexBattleTurnManager.sexBattleAddSelfTranceValue = 10;
			sexBattleTurnManager.sexBattleAddTranceValue = sexBattleAddTranceValue;
			break;
		}
		}
		PlayerSexStatusDataManager.playerSexTrance[0] += sexBattleTurnManager.sexBattleAddSelfTranceValue;
		PlayerSexStatusDataManager.playerSexTrance[0] = Mathf.Clamp(PlayerSexStatusDataManager.playerSexTrance[0], 0, 100);
		float num = (float)PlayerSexStatusDataManager.playerSexTrance[0] * 0.007f + 0.3f;
		sexBattleManager.playerTranceImageArray[0].localScale = new Vector2(num, num);
		sexBattleManager.playerTranceAnimatorArray[0].SetTrigger("addLibido");
		PlayerSexStatusDataManager.playerSexTrance[1] += sexBattleTurnManager.sexBattleAddTranceValue;
		PlayerSexStatusDataManager.playerSexTrance[1] = Mathf.Clamp(PlayerSexStatusDataManager.playerSexTrance[1], 0, 100);
		float num2 = (float)PlayerSexStatusDataManager.playerSexTrance[0] * 0.007f + 0.3f;
		sexBattleManager.playerTranceImageArray[1].localScale = new Vector2(num2, num2);
		sexBattleManager.playerTranceAnimatorArray[1].SetTrigger("addLibido");
		sexBattleTurnManager.SpawnTranceArrow();
	}
}
