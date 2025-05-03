using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using PathologicalGames;
using TMPro;
using UnityEngine;

[AddComponentMenu("")]
public class SetDungeonBattleText : StateBehaviour
{
	private ParameterContainer parameterContainer;

	private DungeonBattleManager dungeonBattleManager;

	private string type;

	public string textSituation;

	public float animationTime;

	public FlexibleInt damageValue;

	public FlexibleInt spValue;

	public FlexibleInt chargeValue;

	public FlexibleInt inputID;

	private int afterHP;

	private int afterSP;

	private int afterCharge;

	private Transform poolGO;

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
		dungeonBattleManager = base.transform.parent.GetComponent<DungeonBattleManager>();
		parameterContainer = GetComponent<ParameterContainer>();
		enemyAgilityPanel = dungeonBattleManager.enemyAgilityParent.GetComponent<RectTransform>();
	}

	public override void OnStateBegin()
	{
		type = parameterContainer.GetStringList("AgilityQueueList")[0].Substring(0, 1);
		float num = animationTime / (float)PlayerDataManager.dungeonBattleSpeed;
		string text = textSituation;
		string text2;
		if (!(text == "AttackDamage"))
		{
			if (!(text == "AttackMiss"))
			{
				return;
			}
			text2 = type;
			if (!(text2 == "p"))
			{
				if (text2 == "e")
				{
					poolGO = PoolManager.Pools["DungeonBattleEffect"].Spawn(dungeonBattleManager.poolEffectArray[2], dungeonBattleManager.damagePointRect[0]);
					dungeonBattleManager.damagePointRect[0].anchoredPosition = new Vector2(450f, 400f);
					afterHP = (int)dungeonBattleManager.playerHpSlider.value;
				}
			}
			else
			{
				poolGO = PoolManager.Pools["DungeonBattleEffect"].Spawn(dungeonBattleManager.poolEffectArray[2], dungeonBattleManager.damagePointRect[1]);
				dungeonBattleManager.damagePointRect[1].anchoredPosition = new Vector2(-450f, 400f);
				afterHP = (int)dungeonBattleManager.enemyHpSlider.value;
			}
			poolGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
			PoolManager.Pools["DungeonBattleEffect"].Despawn(poolGO, despawnTime, dungeonBattleManager.poolManagerGO);
			float time = 0.4f / (float)PlayerDataManager.dungeonBattleSpeed;
			Invoke("InvokeMethod", time);
			return;
		}
		text2 = type;
		if (!(text2 == "p"))
		{
			if (text2 == "e")
			{
				if (dungeonBattleManager.isCriticalAttack)
				{
					poolGO = PoolManager.Pools["DungeonBattleEffect"].Spawn(dungeonBattleManager.poolEffectArray[6], dungeonBattleManager.damagePointRect[0]);
					poolGO.GetComponent<TextMeshProUGUI>().fontSize = 110f;
					SpawnCriticalEffect(dungeonBattleManager.damagePointRect[0]);
				}
				else
				{
					poolGO = PoolManager.Pools["DungeonBattleEffect"].Spawn(dungeonBattleManager.poolEffectArray[1], dungeonBattleManager.damagePointRect[0]);
				}
				float x = Random.Range(400f, 460f);
				dungeonBattleManager.damagePointRect[0].anchoredPosition = new Vector2(x, 400f);
				if (!dungeonBattleManager.isCriticalAttack)
				{
					int enemyID = PlayerStatusDataManager.enemyMember[inputID.value];
					string normalEffectType = GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData data) => data.enemyID == enemyID).normalEffectType;
					string text3 = normalEffectType.Substring(0, 1).ToUpper();
					normalEffectType = "SeAttack" + text3 + normalEffectType.Substring(1);
					MasterAudio.PlaySound(normalEffectType, 1f, null, 0f, null, null);
				}
				else
				{
					MasterAudio.PlaySound("SeAttackEnemyCritical", 1f, null, 0f, null, null);
				}
				dungeonBattleManager.dungeonBattleCanvas.transform.DOShakePosition(shakeTime, shakePower, shakeCount, 10f, snapping: false, fadeOut: false);
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
			}
		}
		else
		{
			if (dungeonBattleManager.isCriticalAttack)
			{
				poolGO = PoolManager.Pools["DungeonBattleEffect"].Spawn(dungeonBattleManager.poolEffectArray[6], dungeonBattleManager.damagePointRect[1]);
				poolGO.GetComponent<TextMeshProUGUI>().fontSize = 130f;
				SpawnCriticalEffect(dungeonBattleManager.damagePointRect[1]);
			}
			else if (dungeonBattleManager.isAttackWeakHit)
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
			float x2 = Random.Range(-400f, -460f);
			dungeonBattleManager.damagePointRect[1].anchoredPosition = new Vector2(x2, 400f);
			if (!dungeonBattleManager.isCriticalAttack)
			{
				string dungeonEffectType = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[inputID.value].dungeonEffectType;
				string text4 = dungeonEffectType.Substring(0, 1).ToUpper();
				dungeonEffectType = "SeAttack" + text4 + dungeonEffectType.Substring(1);
				MasterAudio.PlaySound(dungeonEffectType, 1f, null, 0f, null, null);
			}
			else
			{
				MasterAudio.PlaySound("SeAttackPlayerCritical", 1f, null, 0f, null, null);
			}
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
		}
		afterCharge = Mathf.Clamp(dungeonBattleManager.enemyCharge + chargeValue.value, 0, 100);
		dungeonBattleManager.enemyChargeSlider.DOValue(afterCharge, num - 0.05f).SetEase(Ease.Linear);
		poolGO.GetComponent<TextMeshProUGUI>().text = damageValue.value.ToString();
		poolGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
		PoolManager.Pools["DungeonBattleEffect"].Despawn(poolGO, despawnTime, dungeonBattleManager.poolManagerGO);
	}

	public override void OnStateEnd()
	{
		string text = type;
		if (!(text == "p"))
		{
			if (text == "e")
			{
				dungeonBattleManager.playerHpText.text = afterHP.ToString();
			}
		}
		else
		{
			dungeonBattleManager.enemyHpText.text = afterHP.ToString();
		}
		if (textSituation == "AttackDamage")
		{
			dungeonBattleManager.playerSpSlider.value = afterSP;
			dungeonBattleManager.playerSpText.text = afterSP.ToString();
			PlayerStatusDataManager.characterSp[PlayerDataManager.DungeonHeroineFollowNum] = afterSP;
			dungeonBattleManager.enemyChargeSlider.value = afterCharge;
			dungeonBattleManager.enemyChargeText.text = afterCharge.ToString();
			dungeonBattleManager.enemyCharge = afterCharge;
		}
	}

	public override void OnStateUpdate()
	{
		string text = type;
		if (!(text == "p"))
		{
			if (text == "e")
			{
				dungeonBattleManager.playerHpText.text = Mathf.Floor(dungeonBattleManager.playerHpSlider.value).ToString();
			}
		}
		else
		{
			dungeonBattleManager.enemyHpText.text = Mathf.Floor(dungeonBattleManager.enemyHpSlider.value).ToString();
		}
		if (textSituation == "AttackDamage")
		{
			dungeonBattleManager.playerSpText.text = Mathf.Floor(dungeonBattleManager.playerSpSlider.value).ToString();
			dungeonBattleManager.enemyChargeText.text = Mathf.Floor(dungeonBattleManager.enemyChargeSlider.value).ToString();
		}
	}

	public override void OnStateLateUpdate()
	{
	}

	private void InvokeMethod()
	{
		Transition(stateLink);
	}

	private void SpawnCriticalEffect(Transform parentRect)
	{
		GameObject effectPrefabGo = GameDataManager.instance.skillEffectDataBase.skillEffectDataList.Find((SkillEffectData data) => data.effectName == "criticalHitSpikes").effectPrefabGo;
		Transform transform = PoolManager.Pools["DungeonSkillEffect"].Spawn(effectPrefabGo, parentRect);
		transform.localScale = new Vector2(2f, 2f);
		transform.localPosition = new Vector2(0f, 0f);
		PoolManager.Pools["DungeonSkillEffect"].Despawn(transform, 1f, dungeonBattleManager.skillEffectPoolParentGo.transform);
	}
}
