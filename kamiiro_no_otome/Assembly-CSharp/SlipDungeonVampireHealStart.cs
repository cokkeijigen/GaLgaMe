using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using PathologicalGames;
using TMPro;
using UnityEngine;

[AddComponentMenu("")]
public class SlipDungeonVampireHealStart : StateBehaviour
{
	private ParameterContainer parameterContainer;

	private DungeonBattleManager dungeonBattleManager;

	private bool isPlayerSlip;

	public float animationTime;

	private int afterHP;

	private Transform poolGO;

	public float despawnTime;

	private RectTransform enemyAgilityPanel;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = base.transform.parent.GetComponent<DungeonBattleManager>();
		parameterContainer = GameObject.Find("Dungeon Agility Manager").GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		int @int = parameterContainer.GetInt("slipDamage");
		isPlayerSlip = parameterContainer.GetBool("isPlayerSlip");
		float duration = animationTime / (float)PlayerDataManager.dungeonBattleSpeed;
		if (isPlayerSlip)
		{
			poolGO = PoolManager.Pools["DungeonBattleEffect"].Spawn(dungeonBattleManager.poolEffectArray[3], dungeonBattleManager.damagePointRect[0]);
			dungeonBattleManager.damagePointRect[0].anchoredPosition = new Vector2(450f, 400f);
			afterHP = Mathf.Clamp(PlayerStatusDataManager.playerAllHp + @int, 0, 99999);
			dungeonBattleManager.playerHpSlider.DOValue(afterHP, duration).SetEase(Ease.Linear).OnComplete(delegate
			{
				PlayerStatusDataManager.playerAllHp = afterHP;
				Transition(stateLink);
			});
		}
		else
		{
			poolGO = PoolManager.Pools["DungeonBattleEffect"].Spawn(dungeonBattleManager.poolEffectArray[4], dungeonBattleManager.damagePointRect[1]);
			dungeonBattleManager.damagePointRect[1].anchoredPosition = new Vector2(-450f, 400f);
			afterHP = Mathf.Clamp(PlayerStatusDataManager.enemyAllHp + @int, 0, 99999);
			dungeonBattleManager.enemyHpSlider.DOValue(afterHP, duration).SetEase(Ease.Linear).OnComplete(delegate
			{
				PlayerStatusDataManager.enemyAllHp = afterHP;
				Transition(stateLink);
			});
		}
		poolGO.GetComponent<TextMeshProUGUI>().text = @int.ToString();
		poolGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
		PoolManager.Pools["DungeonBattleEffect"].Despawn(poolGO, despawnTime, dungeonBattleManager.poolManagerGO);
		MasterAudio.PlaySound("SeSkillHeal1", 1f, null, 0f, null, null);
		Transition(stateLink);
	}

	public override void OnStateEnd()
	{
		if (isPlayerSlip)
		{
			dungeonBattleManager.playerHpText.text = afterHP.ToString();
		}
		else
		{
			dungeonBattleManager.enemyHpText.text = afterHP.ToString();
		}
	}

	public override void OnStateUpdate()
	{
		if (isPlayerSlip)
		{
			dungeonBattleManager.playerHpText.text = Mathf.Floor(dungeonBattleManager.playerHpSlider.value).ToString();
		}
		else
		{
			dungeonBattleManager.enemyHpText.text = Mathf.Floor(dungeonBattleManager.enemyHpSlider.value).ToString();
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
