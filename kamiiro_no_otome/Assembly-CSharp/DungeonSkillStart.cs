using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class DungeonSkillStart : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private ParameterContainer parameterContainer;

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
		int id = 0;
		string text = GameObject.Find("Dungeon Agility Manager").GetComponent<ParameterContainer>().GetStringList("AgilityQueueList")[0];
		string text2 = text.Substring(0, 1);
		int num = int.Parse(text.Substring(1));
		switch (text2)
		{
		case "c":
		{
			parameterContainer.SetBool("isPlayerSkill", value: true);
			parameterContainer.SetInt("useSkillCharacterID", PlayerStatusDataManager.playerPartyMember[1]);
			parameterContainer.SetInt("useSkillCharacterNum", num);
			string text3 = PlayerStatusDataManager.playerPartyMember[1] + "0000";
			int skillId = int.Parse(text3);
			Debug.Log("使用チャージスキル：" + text3 + "／" + skillId);
			CalcPlayerSkillData(skillId);
			break;
		}
		case "e":
		{
			parameterContainer.SetBool("isPlayerSkill", value: false);
			parameterContainer.SetInt("useSkillCharacterID", PlayerStatusDataManager.enemyMember[num]);
			parameterContainer.SetInt("useSkillCharacterNum", num);
			id = PlayerStatusDataManager.enemyMember[num];
			int randomValue = RandomManager.GetRandomValue(GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData data) => data.enemyID == id).useSkillDictionary);
			CalcEnemySkillData(randomValue);
			break;
		}
		case "t":
			parameterContainer.SetBool("isPlayerSkill", value: true);
			parameterContainer.SetInt("useSkillCharacterID", 0);
			parameterContainer.SetInt("useSkillCharacterNum", 0);
			Debug.Log("TPスキップスキル使用");
			CalcPlayerSkillData(80000);
			break;
		}
		dungeonBattleManager.isSkillAttack = true;
		parameterContainer.SetBool("isHitDebuffEffect", value: false);
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

	private void CalcEnemySkillData(int skillId)
	{
		int num = 0;
		BattleSkillData battleSkillData = null;
		num = GameDataManager.instance.enemySkillDataBase.skillDataList.FindIndex((BattleSkillData item) => item.skillID == skillId);
		if (num != -1)
		{
			battleSkillData = GameDataManager.instance.enemySkillDataBase.skillDataList[num];
			Debug.Log("スキルデータ取得：" + battleSkillData.skillName);
		}
		dungeonBattleManager.battleSkillData = battleSkillData;
	}

	private void CalcPlayerSkillData(int skillId)
	{
		int num = 0;
		BattleSkillData battleSkillData = null;
		num = GameDataManager.instance.playerSkillDataBase.skillDataList.FindIndex((BattleSkillData item) => item.skillID == skillId);
		if (num != -1)
		{
			battleSkillData = GameDataManager.instance.playerSkillDataBase.skillDataList[num];
			Debug.Log("スキルデータ取得：" + battleSkillData.skillName);
		}
		dungeonBattleManager.battleSkillData = battleSkillData;
	}
}
