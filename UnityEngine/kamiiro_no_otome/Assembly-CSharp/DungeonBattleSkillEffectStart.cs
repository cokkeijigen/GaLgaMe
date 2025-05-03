using Arbor;
using DG.Tweening;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class DungeonBattleSkillEffectStart : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private ParameterContainer parameterContainer;

	public float animationTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonBattleManager = GetComponentInParent<DungeonBattleManager>();
		parameterContainer = GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		BattleSkillData battleSkillData = null;
		battleSkillData = dungeonBattleManager.battleSkillData;
		if (battleSkillData.skillType == BattleSkillData.SkillType.attack || battleSkillData.skillType == BattleSkillData.SkillType.magicAttack || battleSkillData.skillType == BattleSkillData.SkillType.mixAttack || battleSkillData.skillType == BattleSkillData.SkillType.chargeAttack || battleSkillData.skillType == BattleSkillData.SkillType.deBuff)
		{
			parameterContainer.SetBool("isPositiveSkill", value: false);
		}
		else
		{
			parameterContainer.SetBool("isPositiveSkill", value: true);
		}
		float num = animationTime / (float)PlayerDataManager.dungeonBattleSpeed;
		_ = battleSkillData.spawnTransformV2;
		Vector2 anchoredPosition;
		GameObject prefab;
		if (parameterContainer.GetBool("isPlayerSkill"))
		{
			if (PlayerDataManager.isDungeonHeroineFollow)
			{
				if (battleSkillData.skillID == 80000)
				{
					Debug.Log("TPスキップ");
				}
				else
				{
					int num2 = 0;
					dungeonBattleManager.playerSpSlider.DOValue(num2, num - 0.05f).SetEase(Ease.Linear);
					Debug.Log("チャージなのでSP初期化");
				}
			}
			anchoredPosition = ((!parameterContainer.GetBool("isPositiveSkill")) ? new Vector2(dungeonBattleManager.damagePointRect[2].anchoredPosition.x, dungeonBattleManager.damagePointRect[2].anchoredPosition.y) : new Vector2(dungeonBattleManager.damagePointRect[5].anchoredPosition.x, dungeonBattleManager.damagePointRect[5].anchoredPosition.y));
			prefab = GameDataManager.instance.skillEffectDataBase.skillEffectDataList.Find((SkillEffectData data) => data.effectName == battleSkillData.effectType.ToString()).effectPrefabGo;
		}
		else
		{
			int num3 = 0;
			dungeonBattleManager.enemyChargeSlider.DOValue(num3, num - 0.05f).SetEase(Ease.Linear);
			if (parameterContainer.GetBool("isPositiveSkill"))
			{
				anchoredPosition = new Vector2(dungeonBattleManager.damagePointRect[2].anchoredPosition.x, dungeonBattleManager.damagePointRect[2].anchoredPosition.y);
				prefab = GameDataManager.instance.skillEffectDataBase.skillEffectDataList.Find((SkillEffectData data) => data.effectName == battleSkillData.effectType.ToString()).effectPrefabGo;
			}
			else if (battleSkillData.skillType == BattleSkillData.SkillType.deBuff)
			{
				anchoredPosition = new Vector2(dungeonBattleManager.damagePointRect[5].anchoredPosition.x, dungeonBattleManager.damagePointRect[5].anchoredPosition.y);
				prefab = dungeonBattleManager.debuffSkillEffectPrefabGo;
			}
			else
			{
				anchoredPosition = new Vector2(dungeonBattleManager.damagePointRect[5].anchoredPosition.x, dungeonBattleManager.damagePointRect[5].anchoredPosition.y);
				prefab = GameDataManager.instance.skillEffectDataBase.skillEffectDataList.Find((SkillEffectData data) => data.effectName == battleSkillData.effectType.ToString()).effectPrefabGo;
			}
		}
		dungeonBattleManager.damagePointRect[3].anchoredPosition = anchoredPosition;
		Transform transform = PoolManager.Pools["DungeonSkillEffect"].Spawn(prefab, dungeonBattleManager.damagePointRect[3]);
		transform.localPosition = new Vector2(0f, 0f);
		if (battleSkillData.skillID == 80000)
		{
			transform.localScale = new Vector3(2f, 2f, 2f);
		}
		else if (battleSkillData.skillType == BattleSkillData.SkillType.deBuff)
		{
			transform.localScale = new Vector3(2f, 2f, 2f);
		}
		else
		{
			transform.localScale = new Vector3(3f, 3f, 3f);
		}
		PoolManager.Pools["DungeonSkillEffect"].Despawn(transform, battleSkillData.animationTime, dungeonBattleManager.skillEffectPoolParentGo.transform);
		Transition(stateLink);
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
	}

	public override void OnStateLateUpdate()
	{
	}
}
