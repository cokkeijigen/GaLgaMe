using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class DungeonSkillNameEffect : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private ParameterContainer parameterContainer;

	public float tweenTime;

	public float invokeTime;

	private GameObject originFrameGo;

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
		float time = invokeTime / (float)PlayerDataManager.dungeonBattleSpeed;
		if (!parameterContainer.GetBool("isPlayerSkill"))
		{
			string term = "enemySkill" + battleSkillData.skillID;
			dungeonBattleManager.skillNameTextLoc.Term = term;
			dungeonBattleManager.skillNameFrameGo.SetActive(value: true);
			MasterAudio.PlaySound("SeDungeonSkillStart", 1f, null, 0f, null, null);
			originFrameGo = dungeonBattleManager.enemyAgilityGoList[parameterContainer.GetInt("useSkillCharacterNum")];
			originFrameGo.transform.DOScale(1.1f, tweenTime);
			GameObject effectPrefabGo = GameDataManager.instance.skillEffectDataBase.skillEffectDataList.Find((SkillEffectData data) => data.effectName == "simpleCircleGlowRed").effectPrefabGo;
			dungeonBattleManager.damagePointRect[4].position = originFrameGo.GetComponent<RectTransform>().position;
			Transform transform = PoolManager.Pools["DungeonSkillEffect"].Spawn(effectPrefabGo, dungeonBattleManager.damagePointRect[4]);
			transform.localScale = new Vector3(1f, 1f, 1f);
			transform.localPosition = new Vector2(0f, 0f);
			PoolManager.Pools["DungeonSkillEffect"].Despawn(transform, invokeTime, dungeonBattleManager.skillEffectPoolParentGo.transform);
		}
		Invoke("InvokeMethod", time);
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

	private void InvokeMethod()
	{
		if (!parameterContainer.GetBool("isPlayerSkill"))
		{
			dungeonBattleManager.skillNameFrameGo.SetActive(value: false);
			originFrameGo.transform.DOScale(1f, 0.1f);
		}
		Transition(stateLink);
	}
}
