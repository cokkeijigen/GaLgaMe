using System.Collections.Generic;
using System.Linq;
using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class CalcDungeonBuffSkillPower : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private ParameterContainer parameterContainer;

	private BattleSkillData battleSkillData;

	private int fighterCount;

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
		battleSkillData = dungeonBattleManager.battleSkillData;
		fighterCount = PlayerStatusDataManager.playerPartyMember.Length + PlayerStatusDataManager.enemyMember.Length;
		int id = parameterContainer.GetInt("useSkillCharacterID");
		string typeName = battleSkillData.buffType.ToString();
		int num = 0;
		List<int> list = new List<int>();
		List<List<PlayerBattleConditionManager.MemberBuffCondition>> list2 = new List<List<PlayerBattleConditionManager.MemberBuffCondition>>();
		if (parameterContainer.GetBool("isPlayerSkill"))
		{
			list2 = PlayerBattleConditionManager.enemyBuffCondition;
			num = 0;
			string normalEffectType = GameDataManager.instance.characterStatusDataBase.characterStatusDataList[id].normalEffectType;
			MasterAudio.PlaySound("SeAttack" + normalEffectType, 1f, null, 0f, null, null);
		}
		else
		{
			list2 = PlayerBattleConditionManager.playerBuffCondition;
			num = 1;
			string normalEffectType2 = GameDataManager.instance.battleEnemyDataBase.enemyDataList.Find((BattleEnemyData data) => data.enemyID == id).normalEffectType;
			MasterAudio.PlaySound("SeAttack" + normalEffectType2, 1f, null, 0f, null, null);
		}
		int num2;
		if (parameterContainer.GetBool("isHitDebuffEffect"))
		{
			num2 = battleSkillData.subSkillPower;
			Debug.Log("付与攻撃");
		}
		else
		{
			num2 = battleSkillData.skillPower;
			Debug.Log("デバフスキル");
		}
		list = (from ano in list2[0].Select((PlayerBattleConditionManager.MemberBuffCondition con, int index) => new
			{
				Content = con,
				Index = index
			})
			where ano.Content.type == typeName
			select ano.Index).ToList();
		if (list.Any())
		{
			if (list2[0][list[0]].power >= 0)
			{
				int index2 = list[0];
				list2[0].RemoveAt(index2);
				dungeonBattleManager.SetBuffIcon(battleSkillData.buffType.ToString(), num, setValue: true, num2);
			}
			else if (list2[0][list[0]].power > battleSkillData.skillPower)
			{
				list2[0][list[0]].power = battleSkillData.skillPower;
				list2[0][list[0]].continutyTurn = battleSkillData.skillContinuity * fighterCount;
			}
			else
			{
				list2[0][list[0]].continutyTurn = battleSkillData.skillContinuity * fighterCount;
			}
		}
		else
		{
			AddNewBuffData(num2, num, list2);
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

	private void AddNewBuffData(int skillPower, int forceIndex, List<List<PlayerBattleConditionManager.MemberBuffCondition>> targetBuffList)
	{
		PlayerBattleConditionManager.MemberBuffCondition item = new PlayerBattleConditionManager.MemberBuffCondition
		{
			type = battleSkillData.buffType.ToString(),
			power = skillPower,
			continutyTurn = battleSkillData.skillContinuity * fighterCount
		};
		targetBuffList[0].Add(item);
		dungeonBattleManager.SetBuffIcon(battleSkillData.buffType.ToString(), forceIndex, setValue: true, skillPower);
		Debug.Log("バフデータ追加");
	}
}
