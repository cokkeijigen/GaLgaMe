using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CalcBattleItemPower : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ItemData itemData;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
	}

	public override void OnStateBegin()
	{
		scenarioBattleTurnManager.skillAttackHitDataList.Clear();
		scenarioBattleTurnManager.isMedicineNoTarget = false;
		itemData = scenarioBattleTurnManager.useItemData;
		string text = itemData.category.ToString();
		switch (itemData.target)
		{
		case ItemData.Target.solo:
			scenarioBattleTurnManager.isAllTargetSkill = false;
			break;
		case ItemData.Target.all:
			scenarioBattleTurnManager.isAllTargetSkill = true;
			break;
		}
		switch (text)
		{
		case "potion":
		case "allPotion":
		case "elixir":
			CalcHealSkill();
			break;
		case "medicine":
			CalcMedicSkill();
			break;
		case "revive":
			CalcReviveSkill();
			break;
		case "mpPotion":
		case "allMpPotion":
			CalcMpHealSkill();
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

	private void CalcHealSkill()
	{
		int num = itemData.itemPower;
		_ = scenarioBattleTurnManager.playerTargetNum;
		if (scenarioBattleTurnManager.isSupportHeal)
		{
			num /= 2;
		}
		Debug.Log("アイテムのパワー：" + num);
		if (itemData.target == ItemData.Target.all)
		{
			for (int i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
			{
				int id = PlayerBattleConditionManager.playerIsDead[i].memberID;
				int memberNum = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id).memberNum;
				if (!PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id).isDead)
				{
					SkillAttackHitData item = new SkillAttackHitData
					{
						isHealHit = true,
						memberID = id,
						memberNum = memberNum,
						healValue = num
					};
					scenarioBattleTurnManager.skillAttackHitDataList.Add(item);
				}
			}
		}
		else
		{
			int memberID = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.playerTargetNum).memberID;
			SkillAttackHitData item2 = new SkillAttackHitData
			{
				isHealHit = true,
				memberID = memberID,
				memberNum = scenarioBattleTurnManager.playerTargetNum,
				healValue = num
			};
			scenarioBattleTurnManager.skillAttackHitDataList.Add(item2);
		}
	}

	private void CalcMedicSkill()
	{
		List<int> list = new List<int>();
		switch (itemData.itemPower)
		{
		case 0:
		{
			if (itemData.target == ItemData.Target.all)
			{
				bool flag3 = false;
				for (int k = 0; k < PlayerBattleConditionManager.playerIsDead.Count; k++)
				{
					int id3 = PlayerBattleConditionManager.playerIsDead[k].memberID;
					int memberNum3 = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id3).memberNum;
					list = (from ano in PlayerBattleConditionManager.playerBadState[k].Select((PlayerBattleConditionManager.MemberBadState con, int index) => new
						{
							Content = con,
							Index = index
						})
						where ano.Content.type == "poison"
						select ano.Index).ToList();
					if (list.Any())
					{
						PlayerBattleConditionManager.playerBadState[k][list[0]].continutyTurn = 0;
						utageBattleSceneManager.SetBadStateIcon("poison", memberNum3, targetForce: true, setVisible: false);
						flag3 = true;
					}
				}
				if (!flag3)
				{
					scenarioBattleTurnManager.isMedicineNoTarget = true;
				}
				break;
			}
			int index4 = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.playerTargetNum);
			if (PlayerBattleConditionManager.playerBadState[index4].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "poison").continutyTurn > 0)
			{
				PlayerBattleConditionManager.playerBadState[index4].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "poison").continutyTurn = 0;
			}
			else
			{
				scenarioBattleTurnManager.isMedicineNoTarget = true;
			}
			break;
		}
		case 1:
		{
			if (itemData.target == ItemData.Target.all)
			{
				bool flag2 = false;
				for (int j = 0; j < PlayerBattleConditionManager.playerIsDead.Count; j++)
				{
					int id2 = PlayerBattleConditionManager.playerIsDead[j].memberID;
					int memberNum2 = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id2).memberNum;
					list = (from ano in PlayerBattleConditionManager.playerBadState[j].Select((PlayerBattleConditionManager.MemberBadState con, int index) => new
						{
							Content = con,
							Index = index
						})
						where ano.Content.type == "paralyze"
						select ano.Index).ToList();
					if (list.Any())
					{
						PlayerBattleConditionManager.playerBadState[j][list[0]].continutyTurn = 0;
						utageBattleSceneManager.SetBadStateIcon("paralyze", memberNum2, targetForce: true, setVisible: false);
						flag2 = true;
					}
				}
				if (!flag2)
				{
					scenarioBattleTurnManager.isMedicineNoTarget = true;
				}
				break;
			}
			int index3 = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.playerTargetNum);
			if (PlayerBattleConditionManager.playerBadState[index3].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "paralyze").continutyTurn > 0)
			{
				PlayerBattleConditionManager.playerBadState[scenarioBattleTurnManager.playerTargetNum].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "paralyze").continutyTurn = 0;
			}
			else
			{
				scenarioBattleTurnManager.isMedicineNoTarget = true;
			}
			break;
		}
		case 2:
		{
			if (itemData.target == ItemData.Target.all)
			{
				bool flag = false;
				for (int i = 0; i < PlayerBattleConditionManager.playerIsDead.Count; i++)
				{
					int id = PlayerBattleConditionManager.playerIsDead[i].memberID;
					int memberNum = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id).memberNum;
					list = (from ano in PlayerBattleConditionManager.playerBadState[i].Select((PlayerBattleConditionManager.MemberBadState con, int index) => new
						{
							Content = con,
							Index = index
						})
						where ano.Content.type == "poison"
						select ano.Index).ToList();
					if (list.Any())
					{
						PlayerBattleConditionManager.playerBadState[i][list[0]].continutyTurn = 0;
						utageBattleSceneManager.SetBadStateIcon("poison", memberNum, targetForce: true, setVisible: false);
						flag = true;
					}
					list = (from ano in PlayerBattleConditionManager.playerBadState[i].Select((PlayerBattleConditionManager.MemberBadState con, int index) => new
						{
							Content = con,
							Index = index
						})
						where ano.Content.type == "paralyze"
						select ano.Index).ToList();
					if (list.Any())
					{
						PlayerBattleConditionManager.playerBadState[i][list[0]].continutyTurn = 0;
						utageBattleSceneManager.SetBadStateIcon("paralyze", i, targetForce: true, setVisible: false);
						flag = true;
					}
				}
				if (!flag)
				{
					scenarioBattleTurnManager.isMedicineNoTarget = true;
				}
				break;
			}
			int index2 = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.playerTargetNum);
			int continutyTurn = PlayerBattleConditionManager.playerBadState[index2].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "poison").continutyTurn;
			if (continutyTurn > 0)
			{
				PlayerBattleConditionManager.playerBadState[index2].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "poison").continutyTurn = 0;
			}
			int continutyTurn2 = PlayerBattleConditionManager.playerBadState[index2].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "paralyze").continutyTurn;
			if (continutyTurn2 > 0)
			{
				PlayerBattleConditionManager.playerBadState[index2].Find((PlayerBattleConditionManager.MemberBadState data) => data.type == "paralyze").continutyTurn = 0;
			}
			if (continutyTurn == 0 && continutyTurn2 == 0)
			{
				scenarioBattleTurnManager.isMedicineNoTarget = true;
			}
			break;
		}
		}
	}

	private void CalcReviveSkill()
	{
		if (itemData.target == ItemData.Target.all)
		{
			int i;
			for (i = 0; i < PlayerBattleConditionManager.playerIsDead.Count; i++)
			{
				if (!PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == i).isDead)
				{
					PlayerBattleConditionManager.playerIsDead[i].isDead = false;
					int id = PlayerBattleConditionManager.playerIsDead[i].memberID;
					int memberNum = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id).memberNum;
					int num = PlayerStatusDataManager.characterMaxHp[id] * (itemData.itemPower / 100);
					SkillAttackHitData item = new SkillAttackHitData
					{
						isHealHit = true,
						memberID = id,
						memberNum = memberNum,
						healValue = num
					};
					scenarioBattleTurnManager.skillAttackHitDataList.Add(item);
					utageBattleSceneManager.playerFrameGoList[i].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("hpGroup").slider.value = num;
				}
				scenarioBattleTurnManager.SetMiniFrameInteractable(utageBattleSceneManager.playerFrameGoList[i], isEnable: true, i);
				utageBattleSceneManager.SetBadStateIcon("death", i, targetForce: true, setVisible: false);
			}
		}
		else
		{
			int playerTargetNum = scenarioBattleTurnManager.playerTargetNum;
			int id2 = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.playerTargetNum).memberID;
			PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id2).isDead = false;
			float num2 = (float)itemData.itemPower / 100f;
			int num = Mathf.FloorToInt((float)PlayerStatusDataManager.characterMaxHp[id2] * num2);
			Debug.Log("蘇生後のHP：" + num + "／倍率：" + num2);
			SkillAttackHitData item2 = new SkillAttackHitData
			{
				isHealHit = true,
				memberID = id2,
				memberNum = playerTargetNum,
				healValue = num
			};
			scenarioBattleTurnManager.skillAttackHitDataList.Add(item2);
			utageBattleSceneManager.playerFrameGoList[playerTargetNum].GetComponent<ParameterContainer>().GetVariable<SliderAndTmpText>("hpGroup").slider.value = num;
			scenarioBattleTurnManager.SetMiniFrameInteractable(utageBattleSceneManager.playerFrameGoList[playerTargetNum], isEnable: true, playerTargetNum);
			utageBattleSceneManager.SetBadStateIcon("death", scenarioBattleTurnManager.playerTargetNum, targetForce: true, setVisible: false);
		}
	}

	private void CalcMpHealSkill()
	{
		int num = itemData.itemPower;
		_ = scenarioBattleTurnManager.playerTargetNum;
		if (scenarioBattleTurnManager.isSupportHeal)
		{
			num /= 2;
		}
		Debug.Log("アイテムのパワー：" + num);
		if (itemData.target == ItemData.Target.all)
		{
			for (int i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
			{
				int id = PlayerBattleConditionManager.playerIsDead[i].memberID;
				int memberNum = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id).memberNum;
				if (!PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == id).isDead)
				{
					SkillAttackHitData item = new SkillAttackHitData
					{
						isHealHit = true,
						memberID = id,
						memberNum = memberNum,
						healValue = num
					};
					scenarioBattleTurnManager.skillAttackHitDataList.Add(item);
				}
			}
		}
		else
		{
			int memberID = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == scenarioBattleTurnManager.playerTargetNum).memberID;
			SkillAttackHitData item2 = new SkillAttackHitData
			{
				isHealHit = true,
				memberID = memberID,
				memberNum = scenarioBattleTurnManager.playerTargetNum,
				healValue = num
			};
			scenarioBattleTurnManager.skillAttackHitDataList.Add(item2);
		}
	}
}
