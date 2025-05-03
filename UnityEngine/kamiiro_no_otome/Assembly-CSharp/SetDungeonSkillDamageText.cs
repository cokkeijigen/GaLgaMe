using Arbor;
using DG.Tweening;
using PathologicalGames;
using TMPro;
using UnityEngine;

[AddComponentMenu("")]
public class SetDungeonSkillDamageText : StateBehaviour
{
	private ParameterContainer parameterContainer;

	private DungeonBattleManager dungeonBattleManager;

	public float animationTime;

	public FlexibleInt damageValue;

	public FlexibleInt spValue;

	public FlexibleInt chargeValue;

	private int afterHP;

	private int afterSP;

	private int afterCharge;

	private Transform poolGO;

	public float despawnTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = base.transform.parent.GetComponent<DungeonBattleManager>();
		parameterContainer = GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		float num = animationTime / (float)PlayerDataManager.dungeonBattleSpeed;
		if (parameterContainer.GetBool("isPlayerSkill"))
		{
			if (dungeonBattleManager.isAttackWeakHit)
			{
				poolGO = PoolManager.Pools["DungeonBattleEffect"].Spawn(dungeonBattleManager.poolEffectArray[9], dungeonBattleManager.damagePointRect[1]);
			}
			else if (dungeonBattleManager.isAttackResistHit)
			{
				poolGO = PoolManager.Pools["DungeonBattleEffect"].Spawn(dungeonBattleManager.poolEffectArray[10], dungeonBattleManager.damagePointRect[1]);
			}
			else
			{
				poolGO = PoolManager.Pools["DungeonBattleEffect"].Spawn(dungeonBattleManager.poolEffectArray[0], dungeonBattleManager.damagePointRect[1]);
			}
			dungeonBattleManager.damagePointRect[1].anchoredPosition = new Vector2(-450f, 400f);
			afterHP = Mathf.Clamp(PlayerStatusDataManager.enemyAllHp - damageValue.value, 0, 99999);
			afterSP = 0;
			dungeonBattleManager.enemyHpSlider.DOValue(afterHP, num).SetEase(Ease.Linear).OnComplete(delegate
			{
				PlayerStatusDataManager.enemyAllHp = afterHP;
				Transition(stateLink);
			});
			afterCharge = Mathf.Clamp(dungeonBattleManager.enemyCharge + chargeValue.value, 0, 100);
			dungeonBattleManager.enemyChargeSlider.DOValue(afterCharge, num - 0.05f).SetEase(Ease.Linear);
		}
		else
		{
			poolGO = PoolManager.Pools["DungeonBattleEffect"].Spawn(dungeonBattleManager.poolEffectArray[1], dungeonBattleManager.damagePointRect[0]);
			dungeonBattleManager.damagePointRect[0].anchoredPosition = new Vector2(450f, 400f);
			afterHP = Mathf.Clamp(PlayerStatusDataManager.playerAllHp - damageValue.value, 0, 99999);
			if (PlayerDataManager.isDungeonHeroineFollow)
			{
				afterSP = Mathf.Clamp(PlayerStatusDataManager.characterSp[PlayerDataManager.DungeonHeroineFollowNum] + spValue.value, 0, 100);
				dungeonBattleManager.playerSpSlider.DOValue(afterSP, num - 0.05f).SetEase(Ease.Linear);
			}
			dungeonBattleManager.playerHpSlider.DOValue(afterHP, num).SetEase(Ease.Linear).OnComplete(delegate
			{
				PlayerStatusDataManager.playerAllHp = afterHP;
				Transition(stateLink);
			});
			afterCharge = 0;
		}
		if (dungeonBattleManager.battleSkillData.skillID == 80000)
		{
			poolGO.GetComponent<TextMeshProUGUI>().text = "KILL";
		}
		else
		{
			poolGO.GetComponent<TextMeshProUGUI>().text = damageValue.value.ToString();
		}
		poolGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
		PoolManager.Pools["DungeonBattleEffect"].Despawn(poolGO, despawnTime, dungeonBattleManager.poolManagerGO);
	}

	public override void OnStateEnd()
	{
		if (parameterContainer.GetBool("isPlayerSkill"))
		{
			dungeonBattleManager.enemyHpText.text = afterHP.ToString();
		}
		else
		{
			dungeonBattleManager.playerHpText.text = afterHP.ToString();
		}
		if (dungeonBattleManager.battleSkillData.skillID == 80000)
		{
			Debug.Log("TPスキップ");
		}
		else
		{
			dungeonBattleManager.playerSpSlider.value = afterSP;
			dungeonBattleManager.playerSpText.text = afterSP.ToString();
			PlayerStatusDataManager.characterSp[PlayerDataManager.DungeonHeroineFollowNum] = afterSP;
		}
		dungeonBattleManager.enemyChargeSlider.value = afterCharge;
		dungeonBattleManager.enemyChargeText.text = afterCharge.ToString();
		dungeonBattleManager.enemyCharge = afterCharge;
	}

	public override void OnStateUpdate()
	{
		if (parameterContainer.GetBool("isPlayerSkill"))
		{
			dungeonBattleManager.enemyHpText.text = Mathf.Floor(dungeonBattleManager.enemyHpSlider.value).ToString();
		}
		else
		{
			dungeonBattleManager.playerHpText.text = Mathf.Floor(dungeonBattleManager.playerHpSlider.value).ToString();
		}
		dungeonBattleManager.playerSpText.text = Mathf.Floor(dungeonBattleManager.playerSpSlider.value).ToString();
		dungeonBattleManager.enemyChargeText.text = Mathf.Floor(dungeonBattleManager.enemyChargeSlider.value).ToString();
	}

	public override void OnStateLateUpdate()
	{
	}
}
