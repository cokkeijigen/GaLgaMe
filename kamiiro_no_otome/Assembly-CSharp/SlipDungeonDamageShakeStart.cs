using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using PathologicalGames;
using TMPro;
using UnityEngine;

[AddComponentMenu("")]
public class SlipDungeonDamageShakeStart : StateBehaviour
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
		enemyAgilityPanel = dungeonBattleManager.enemyAgilityParent.GetComponent<RectTransform>();
	}

	public override void OnStateBegin()
	{
		int @int = parameterContainer.GetInt("slipDamage");
		isPlayerSlip = parameterContainer.GetBool("isPlayerSlip");
		float duration = animationTime / (float)PlayerDataManager.dungeonBattleSpeed;
		if (isPlayerSlip)
		{
			float duration2 = 0.1f;
			float strength = 0.5f;
			int vibrato = 2;
			poolGO = PoolManager.Pools["DungeonBattleEffect"].Spawn(dungeonBattleManager.poolEffectArray[8], dungeonBattleManager.damagePointRect[0]);
			dungeonBattleManager.damagePointRect[0].anchoredPosition = new Vector2(450f, 400f);
			dungeonBattleManager.dungeonBattleCanvas.transform.DOShakePosition(duration2, strength, vibrato, 10f, snapping: false, fadeOut: false);
			afterHP = Mathf.Clamp(PlayerStatusDataManager.playerAllHp - @int, 1, 99999);
			dungeonBattleManager.playerHpSlider.DOValue(afterHP, duration).SetEase(Ease.Linear).OnComplete(delegate
			{
				PlayerStatusDataManager.playerAllHp = afterHP;
				Transition(stateLink);
			});
		}
		else
		{
			float duration2 = 0.1f;
			float strength = 0.5f;
			int vibrato = 2;
			poolGO = PoolManager.Pools["DungeonBattleEffect"].Spawn(dungeonBattleManager.poolEffectArray[8], dungeonBattleManager.damagePointRect[1]);
			dungeonBattleManager.damagePointRect[1].anchoredPosition = new Vector2(-450f, 400f);
			enemyAgilityPanel.DOShakeAnchorPos(duration2, strength, vibrato, 10f, snapping: false, fadeOut: false);
			afterHP = Mathf.Clamp(PlayerStatusDataManager.enemyAllHp - @int, 1, 99999);
			dungeonBattleManager.enemyHpSlider.DOValue(afterHP, duration).SetEase(Ease.Linear).OnComplete(delegate
			{
				PlayerStatusDataManager.enemyAllHp = afterHP;
				Transition(stateLink);
			});
		}
		poolGO.GetComponent<TextMeshProUGUI>().text = @int.ToString();
		poolGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
		PoolManager.Pools["DungeonBattleEffect"].Despawn(poolGO, despawnTime, dungeonBattleManager.poolManagerGO);
		MasterAudio.PlaySound("SeSlipPoisonDamage", 1f, null, 0f, null, null);
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
