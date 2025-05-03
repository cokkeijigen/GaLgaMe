using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class SexBattleUiAnimation_CaressHeal : StateBehaviour
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
		_ = sexBattleManager.selectSexSkillData;
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
		currentHpArray[0] = PlayerSexStatusDataManager.playerSexHp[0];
		afterHpArray[0] = PlayerSexStatusDataManager.playerSexHp[0] + sexBattleTurnManager.sexBattleHealValue;
		afterHpArray[0] = Mathf.Clamp(afterHpArray[0], 0, PlayerSexStatusDataManager.playerSexMaxHp[0]);
	}
}
