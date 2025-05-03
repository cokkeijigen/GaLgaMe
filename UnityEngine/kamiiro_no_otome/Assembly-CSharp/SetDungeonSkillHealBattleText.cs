using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using PathologicalGames;
using TMPro;
using UnityEngine;

[AddComponentMenu("")]
public class SetDungeonSkillHealBattleText : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private ParameterContainer parameterContainer;

	public float animationTime;

	public InputSlotInt inputHealValue;

	private BattleSkillData skillData;

	private int afterHP;

	private int healValue;

	private Transform spawnGO;

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
		skillData = dungeonBattleManager.battleSkillData;
		inputHealValue.GetValue(ref healValue);
		float duration = animationTime / (float)PlayerDataManager.dungeonBattleSpeed;
		string effectType = skillData.effectType;
		string text = effectType.Substring(0, 1).ToUpper();
		effectType = "SeSkill" + text + effectType.Substring(1);
		switch (skillData.skillType)
		{
		case BattleSkillData.SkillType.heal:
		{
			float num = 0f;
			if (parameterContainer.GetBool("isPlayerSkill"))
			{
				num = 400f;
				afterHP = Mathf.Clamp(PlayerStatusDataManager.playerAllHp + healValue, 0, PlayerStatusDataManager.playerAllMaxHp);
				dungeonBattleManager.playerHpSlider.DOValue(afterHP, duration).SetEase(Ease.Linear).OnComplete(delegate
				{
					PlayerStatusDataManager.playerAllHp = afterHP;
					Transition(stateLink);
				});
			}
			else
			{
				num = -450f;
				afterHP = Mathf.Clamp(PlayerStatusDataManager.enemyAllHp + healValue, 0, PlayerStatusDataManager.enemyAllMaxHp);
				dungeonBattleManager.enemyHpSlider.DOValue(afterHP, duration).SetEase(Ease.Linear).OnComplete(delegate
				{
					PlayerStatusDataManager.enemyAllHp = afterHP;
					Transition(stateLink);
				});
			}
			dungeonBattleManager.damagePointRect[1].anchoredPosition = new Vector2(num, 400f);
			spawnGO = PoolManager.Pools["DungeonBattleEffect"].Spawn(dungeonBattleManager.poolEffectArray[3], dungeonBattleManager.damagePointRect[1]);
			MasterAudio.PlaySound(effectType, 1f, null, 0f, null, null);
			spawnGO.GetComponent<TextMeshProUGUI>().text = healValue.ToString();
			spawnGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
			PoolManager.Pools["DungeonBattleEffect"].Despawn(spawnGO, despawnTime, dungeonBattleManager.poolManagerGO);
			break;
		}
		case BattleSkillData.SkillType.medic:
			dungeonBattleManager.damagePointRect[1].anchoredPosition = new Vector2(450f, 400f);
			spawnGO = PoolManager.Pools["DungeonBattleEffect"].Spawn(dungeonBattleManager.poolEffectArray[6], dungeonBattleManager.damagePointRect[1]);
			MasterAudio.PlaySound(effectType, 1f, null, 0f, null, null);
			spawnGO.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
			PoolManager.Pools["DungeonBattleEffect"].Despawn(spawnGO, despawnTime, dungeonBattleManager.poolManagerGO);
			break;
		}
		float time = 0.4f / (float)PlayerDataManager.dungeonBattleSpeed;
		Invoke("InvokeMethod", time);
	}

	public override void OnStateEnd()
	{
		if (parameterContainer.GetBool("isPlayerSkill"))
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
		if (parameterContainer.GetBool("isPlayerSkill"))
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

	private void InvokeMethod()
	{
		Transition(stateLink);
	}
}
