using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class SetDungeonBattleShake : StateBehaviour
{
	private ParameterContainer parameterContainer;

	private DungeonBattleManager dungeonBattleManager;

	private string type;

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
		type = GameObject.Find("Dungeon Agility Manager").GetComponent<ParameterContainer>().GetStringList("AgilityQueueList")[0].Substring(0, 1);
		int id = parameterContainer.GetInt("useSkillCharacterID");
		int skillId = dungeonBattleManager.battleSkillData.skillID;
		switch (type)
		{
		case "p":
		case "c":
		case "t":
			if (dungeonBattleManager.isSkillAttack)
			{
				string effectType2 = GameDataManager.instance.playerSkillDataBase.skillDataList.Find((BattleSkillData data) => data.skillID == skillId).effectType;
				string text3 = effectType2.Substring(0, 1).ToUpper();
				effectType2 = "SeSkill" + text3 + effectType2.Substring(1);
				MasterAudio.PlaySound(effectType2, 1f, null, 0f, null, null);
			}
			else if (!dungeonBattleManager.isCriticalAttack)
			{
				string normalEffectType2 = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[id].normalEffectType;
				string text4 = normalEffectType2.Substring(0, 1).ToUpper();
				normalEffectType2 = "SeAttack" + text4 + normalEffectType2.Substring(1);
				MasterAudio.PlaySound(normalEffectType2, 1f, null, 0f, null, null);
			}
			else
			{
				MasterAudio.PlaySound("SeAttackPlayerCritical", 1f, null, 0f, null, null);
			}
			enemyAgilityPanel.DOShakeAnchorPos(shakeTime, enemyShakePower, shakeCount, 10f, snapping: false, fadeOut: false);
			break;
		case "e":
			if (dungeonBattleManager.isSkillAttack)
			{
				string effectType = GameDataManager.instance.enemySkillDataBase.skillDataList.Find((BattleSkillData data) => data.skillID == skillId).effectType;
				string text = effectType.Substring(0, 1).ToUpper();
				effectType = "SeSkill" + text + effectType.Substring(1);
				MasterAudio.PlaySound(effectType, 1f, null, 0f, null, null);
			}
			else if (!dungeonBattleManager.isCriticalAttack)
			{
				string normalEffectType = GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData data) => data.enemyID == id).normalEffectType;
				string text2 = normalEffectType.Substring(0, 1).ToUpper();
				normalEffectType = "SeAttack" + text2 + normalEffectType.Substring(1);
				MasterAudio.PlaySound(normalEffectType, 1f, null, 0f, null, null);
			}
			else
			{
				MasterAudio.PlaySound("SeAttackEnemyCritical", 1f, null, 0f, null, null);
			}
			dungeonBattleManager.dungeonBattleCanvas.transform.DOShakePosition(shakeTime, shakePower, shakeCount, 10f, snapping: false, fadeOut: false);
			break;
		}
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
