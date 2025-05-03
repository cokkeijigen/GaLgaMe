using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using PathologicalGames;
using TMPro;
using UnityEngine;

[AddComponentMenu("")]
public class ComboDungeonDamageShakeStart : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	public FlexibleInt damageValue;

	public FlexibleInt spValue;

	public FlexibleInt chargeValue;

	public FlexibleInt inputID;

	public float animationTime;

	private int afterHP;

	private int afterSP;

	private int afterCharge;

	private Transform poolComboGo;

	private Transform poolGo;

	public float despawnTime;

	public float shakeTime;

	public float shakePower;

	public float enemyShakePower;

	public int shakeCount;

	private RectTransform enemyAgilityPanel;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GameObject.Find("Dungeon Battle Manager").GetComponent<DungeonBattleManager>();
		enemyAgilityPanel = dungeonBattleManager.enemyAgilityParent.GetComponent<RectTransform>();
	}

	public override void OnStateBegin()
	{
		float num = animationTime / (float)PlayerDataManager.dungeonBattleSpeed;
		poolGo = PoolManager.Pools["DungeonBattleEffect"].Spawn(dungeonBattleManager.poolEffectArray[7], dungeonBattleManager.damagePointRect[1]);
		float x = Random.Range(-400f, -460f);
		dungeonBattleManager.damagePointRect[1].anchoredPosition = new Vector2(x, 400f);
		GameObject effectPrefabGo = GameDataManager.instance.skillEffectDataBase.skillEffectDataList.Find((SkillEffectData data) => data.effectName == "comboHitSpikes").effectPrefabGo;
		Transform transform = PoolManager.Pools["DungeonSkillEffect"].Spawn(effectPrefabGo, dungeonBattleManager.damagePointRect[1]);
		transform.localScale = new Vector2(2f, 2f);
		transform.localPosition = new Vector2(0f, 0f);
		PoolManager.Pools["DungeonSkillEffect"].Despawn(transform, 1f, dungeonBattleManager.skillEffectPoolParentGo.transform);
		string normalEffectType = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[inputID.value].normalEffectType;
		MasterAudio.PlaySound("SeAttack" + normalEffectType, 1f, null, 0f, null, null);
		enemyAgilityPanel.DOShakeAnchorPos(shakeTime, enemyShakePower, shakeCount, 10f, snapping: false, fadeOut: false);
		afterHP = Mathf.Clamp(PlayerStatusDataManager.enemyAllHp - damageValue.value, 0, 99999);
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			afterSP = Mathf.Clamp(PlayerStatusDataManager.characterSp[PlayerDataManager.DungeonHeroineFollowNum] + spValue.value, 0, 100);
			dungeonBattleManager.playerSpSlider.DOValue(afterSP, num - 0.05f).SetEase(Ease.Linear);
		}
		dungeonBattleManager.enemyHpSlider.DOValue(afterHP, num).SetEase(Ease.Linear).OnComplete(delegate
		{
			PlayerStatusDataManager.enemyAllHp = afterHP;
			Transition(stateLink);
		});
		afterCharge = Mathf.Clamp(dungeonBattleManager.enemyCharge + chargeValue.value, 0, 100);
		dungeonBattleManager.enemyChargeSlider.DOValue(afterCharge, num - 0.05f).SetEase(Ease.Linear);
		poolGo.GetComponent<TextMeshProUGUI>().text = damageValue.value.ToString();
		poolGo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
		PoolManager.Pools["DungeonBattleEffect"].Despawn(poolGo, despawnTime, dungeonBattleManager.poolManagerGO);
	}

	public override void OnStateEnd()
	{
		dungeonBattleManager.enemyHpText.text = afterHP.ToString();
		dungeonBattleManager.playerSpSlider.value = afterSP;
		dungeonBattleManager.playerSpText.text = afterSP.ToString();
		PlayerStatusDataManager.characterSp[PlayerDataManager.DungeonHeroineFollowNum] = afterSP;
		dungeonBattleManager.enemyChargeSlider.value = afterCharge;
		dungeonBattleManager.enemyChargeText.text = afterCharge.ToString();
		dungeonBattleManager.enemyCharge = afterCharge;
	}

	public override void OnStateUpdate()
	{
		dungeonBattleManager.enemyHpText.text = Mathf.Floor(dungeonBattleManager.enemyHpSlider.value).ToString();
		dungeonBattleManager.playerSpText.text = Mathf.Floor(dungeonBattleManager.playerSpSlider.value).ToString();
		dungeonBattleManager.enemyChargeText.text = Mathf.Floor(dungeonBattleManager.enemyChargeSlider.value).ToString();
	}

	public override void OnStateLateUpdate()
	{
	}
}
