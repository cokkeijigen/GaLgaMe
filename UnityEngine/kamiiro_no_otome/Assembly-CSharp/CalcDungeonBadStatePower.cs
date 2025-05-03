using System.Collections.Generic;
using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class CalcDungeonBadStatePower : StateBehaviour
{
	private DungeonBattleManager dungeonBattleManager;

	private ParameterContainer parameterContainer;

	public float animationTime;

	private int afterSP;

	private int fighterCount;

	private BattleSkillData battleSkillData;

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
		string typeName = battleSkillData.subType.ToString();
		int num = 0;
		float num2 = animationTime / (float)PlayerDataManager.dungeonBattleSpeed;
		new List<int>();
		List<List<PlayerBattleConditionManager.MemberBadState>> list = new List<List<PlayerBattleConditionManager.MemberBadState>>();
		if (battleSkillData.subType == BattleSkillData.SubType.stagger)
		{
			if (PlayerDataManager.isDungeonHeroineFollow)
			{
				float num3 = PlayerStatusDataManager.characterSp[PlayerDataManager.DungeonHeroineFollowNum];
				if (num3 != 0f)
				{
					num3 /= 2f;
					afterSP = Mathf.FloorToInt(num3);
					PlayerStatusDataManager.characterSp[PlayerDataManager.DungeonHeroineFollowNum] = afterSP;
					dungeonBattleManager.playerSpSlider.DOValue(afterSP, num2 - 0.05f).SetEase(Ease.Linear);
					Debug.Log($"よろけ／対象Num：{PlayerDataManager.DungeonHeroineFollowNum}／SP：{afterSP}");
				}
			}
		}
		else
		{
			if (parameterContainer.GetBool("isPlayerSkill"))
			{
				list = PlayerBattleConditionManager.enemyBadState;
				num = 1;
			}
			else
			{
				list = PlayerBattleConditionManager.playerBadState;
				num = 0;
			}
			int num4 = list[0].FindIndex((PlayerBattleConditionManager.MemberBadState data) => data.type == typeName);
			if (num4 >= 0)
			{
				list[0][num4].continutyTurn = battleSkillData.skillContinuity * fighterCount;
				Debug.Log("状態異常ターン増加／" + list[0][num4].type + list[0][num4].continutyTurn);
			}
			else
			{
				PlayerBattleConditionManager.MemberBadState memberBadState = new PlayerBattleConditionManager.MemberBadState
				{
					type = battleSkillData.subType.ToString(),
					continutyTurn = battleSkillData.skillContinuity * fighterCount
				};
				list[0].Add(memberBadState);
				dungeonBattleManager.SetBadStateIcon(battleSkillData.subType.ToString(), num, setValue: true);
				Debug.Log("状態異常データ追加／" + memberBadState.type + "：" + memberBadState.continutyTurn);
			}
		}
		Transition(stateLink);
	}

	public override void OnStateEnd()
	{
		if (battleSkillData.subType == BattleSkillData.SubType.stagger)
		{
			dungeonBattleManager.playerSpSlider.value = afterSP;
			dungeonBattleManager.playerSpText.text = afterSP.ToString();
			PlayerStatusDataManager.characterSp[PlayerDataManager.DungeonHeroineFollowNum] = afterSP;
		}
	}

	public override void OnStateUpdate()
	{
		if (battleSkillData.subType == BattleSkillData.SubType.stagger)
		{
			dungeonBattleManager.playerSpSlider.value = afterSP;
			dungeonBattleManager.playerSpText.text = afterSP.ToString();
			PlayerStatusDataManager.characterSp[PlayerDataManager.DungeonHeroineFollowNum] = afterSP;
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
