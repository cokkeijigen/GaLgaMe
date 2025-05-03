using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class SexBattleUiAnimation_PistonAfter : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

	private SexBattleEffectManager sexBattleEffectManager;

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
		CalcChangeSexBattleParameter();
		float num2 = (float)afterHpArray[0] / (float)PlayerSexStatusDataManager.playerSexMaxHp[0];
		float endValue = num2 * 0.88f + 0.06f;
		Debug.Log("エデンの残りHP：" + afterHpArray[0] + "／エデンのHPFill比率：" + num2);
		DOTween.To(() => currentHpArray[0], delegate(int x)
		{
			currentHpArray[0] = x;
		}, afterHpArray[0], num);
		sexBattleManager.playerHpImageArray[0].DOFillAmount(endValue, num);
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
	}

	private void InvokeMethod()
	{
		PlayerSexStatusDataManager.playerSexHp[0] = afterHpArray[0];
		Transition(stateLink);
	}

	private void CalcChangeSexBattleParameter()
	{
		int min = 1;
		if (sexBattleTurnManager.isFertilizeRepeatPiston)
		{
			min = 0;
		}
		currentHpArray[0] = PlayerSexStatusDataManager.playerSexHp[0];
		afterHpArray[0] = PlayerSexStatusDataManager.playerSexHp[0] - sexBattleTurnManager.sexBattleSelfDamageValue;
		afterHpArray[0] = Mathf.Clamp(afterHpArray[0], min, PlayerSexStatusDataManager.playerSexMaxHp[0]);
		CalcChangeTrance();
	}

	private void CalcChangeTrance()
	{
		float f = (float)sexBattleTurnManager.sexBattleAddSelfTranceValue * 0.3f;
		f = Mathf.RoundToInt(f);
		float num = (float)sexBattleTurnManager.sexBattleAddTranceValue * 0.3f;
		num = Mathf.RoundToInt(f);
		PlayerSexStatusDataManager.playerSexTrance[0] += (int)f;
		PlayerSexStatusDataManager.playerSexTrance[0] = Mathf.Clamp(PlayerSexStatusDataManager.playerSexTrance[0], 0, 100);
		float num2 = (float)PlayerSexStatusDataManager.playerSexTrance[0] * 0.007f + 0.3f;
		sexBattleManager.playerTranceImageArray[0].localScale = new Vector2(num2, num2);
		sexBattleManager.playerTranceAnimatorArray[0].SetTrigger("addLibido");
		PlayerSexStatusDataManager.playerSexTrance[1] += (int)num;
		PlayerSexStatusDataManager.playerSexTrance[1] = Mathf.Clamp(PlayerSexStatusDataManager.playerSexTrance[1], 0, 100);
		float num3 = (float)PlayerSexStatusDataManager.playerSexTrance[0] * 0.007f + 0.3f;
		sexBattleManager.playerTranceImageArray[1].localScale = new Vector2(num3, num3);
		sexBattleManager.playerTranceAnimatorArray[1].SetTrigger("addLibido");
	}
}
