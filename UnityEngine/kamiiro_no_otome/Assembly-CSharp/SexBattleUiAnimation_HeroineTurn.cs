using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class SexBattleUiAnimation_HeroineTurn : StateBehaviour
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
		SexSkillData sexSkillData = null;
		sexSkillData = sexBattleManager.heroineSexSkillData;
		switch (sexSkillData.skillType)
		{
		case SexSkillData.SkillType.sexAttack:
		{
			CalcChangeSexBattleParameter();
			float num5 = (float)afterHpArray[0] / (float)PlayerSexStatusDataManager.playerSexMaxHp[0];
			float endValue = num5 * 0.88f + 0.06f;
			Debug.Log("エデンの残りHP：" + afterHpArray[0] + "／エデンのHPFill比率：" + num5);
			DOTween.To(() => currentHpArray[0], delegate(int x)
			{
				currentHpArray[0] = x;
			}, afterHpArray[0], num);
			sexBattleManager.playerHpImageArray[0].DOFillAmount(endValue, num);
			break;
		}
		case SexSkillData.SkillType.buff:
		case SexSkillData.SkillType.deBuff:
		case SexSkillData.SkillType.none:
		{
			num = 0f;
			int num2 = Random.Range(sexSkillData.heroineAffectPower / 2, sexSkillData.heroineAffectPower);
			if (sexSkillData.heroineAffect == SexSkillData.TranceAffect.tranceUp)
			{
				sexBattleTurnManager.sexBattleAddTranceValue = num2;
			}
			else if (PlayerSexBattleConditionAccess.GetSexBattleSubPower(PlayerSexStatusDataManager.playerSexSubPower[1], "trance") == 0)
			{
				sexBattleTurnManager.sexBattleAddTranceValue = Mathf.RoundToInt(num2 * -1);
			}
			else
			{
				sexBattleTurnManager.sexBattleAddTranceValue = 0;
			}
			int num3 = 0;
			if (sexSkillData.playerAffect == SexSkillData.TranceAffect.tranceUp)
			{
				if (PlayerSexStatusDataManager.playerSexBuffCondition[0].Count > 0 && PlayerSexBattleConditionAccess.GetSexBattleSubPower(PlayerSexStatusDataManager.playerSexSubPower[0], "desire") > 0)
				{
					int num4 = Random.Range(0, 25);
					num3 += num4;
				}
				sexBattleTurnManager.sexBattleAddSelfTranceValue = num3;
			}
			else
			{
				sexBattleTurnManager.sexBattleAddSelfTranceValue = num3 * -1;
			}
			CalcChangeSexBattleParameter();
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
	}

	private void InvokeMethod()
	{
		if (sexBattleManager.heroineSexSkillData.skillType == SexSkillData.SkillType.sexAttack)
		{
			PlayerSexStatusDataManager.playerSexHp[0] = afterHpArray[0];
		}
		Transition(stateLink);
	}

	private void CalcChangeSexBattleParameter()
	{
		SexSkillData heroineSexSkillData = sexBattleManager.heroineSexSkillData;
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		switch (heroineSexSkillData.skillType)
		{
		case SexSkillData.SkillType.sexAttack:
			currentHpArray[0] = PlayerSexStatusDataManager.playerSexHp[0];
			afterHpArray[0] = PlayerSexStatusDataManager.playerSexHp[0] - sexBattleTurnManager.sexBattleDamageValue;
			afterHpArray[0] = Mathf.Clamp(afterHpArray[0], 0, PlayerSexStatusDataManager.playerSexMaxHp[0]);
			currentHpArray[1] = PlayerSexStatusDataManager.playerSexHp[selectSexBattleHeroineId];
			afterHpArray[1] = PlayerSexStatusDataManager.playerSexHp[selectSexBattleHeroineId];
			CalcChangeTrance();
			break;
		case SexSkillData.SkillType.buff:
		case SexSkillData.SkillType.deBuff:
		case SexSkillData.SkillType.none:
			currentHpArray[0] = PlayerSexStatusDataManager.playerSexHp[0];
			afterHpArray[0] = PlayerSexStatusDataManager.playerSexHp[0];
			currentHpArray[1] = PlayerSexStatusDataManager.playerSexHp[selectSexBattleHeroineId];
			afterHpArray[1] = PlayerSexStatusDataManager.playerSexHp[selectSexBattleHeroineId];
			CalcChangeTrance();
			break;
		}
	}

	private void CalcChangeTrance()
	{
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
