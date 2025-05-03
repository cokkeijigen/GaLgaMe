using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBattleConditionAccess : MonoBehaviour
{
	public static void BattleConditionInititialize()
	{
		PlayerBattleConditionManager.playerIsDead.Clear();
		PlayerBattleConditionManager.playerBuffCondition.Clear();
		PlayerBattleConditionManager.playerBadState.Clear();
		PlayerBattleConditionManager.playerSkillRechargeTurn.Clear();
		for (int i = 0; i < PlayerStatusDataManager.partyMemberCount + 1; i++)
		{
			List<PlayerBattleConditionManager.MemberSkillReChargeTurn> item = new List<PlayerBattleConditionManager.MemberSkillReChargeTurn>();
			PlayerBattleConditionManager.playerSkillRechargeTurn.Add(item);
		}
		for (int j = 0; j < PlayerStatusDataManager.playerPartyMember.Length; j++)
		{
			PlayerBattleConditionManager.MemberIsDead item2 = new PlayerBattleConditionManager.MemberIsDead
			{
				memberNum = j,
				memberID = PlayerStatusDataManager.playerPartyMember[j],
				memberAgility = PlayerStatusDataManager.characterAgility[PlayerStatusDataManager.playerPartyMember[j]],
				isDead = false
			};
			PlayerBattleConditionManager.playerIsDead.Add(item2);
			PlayerBattleConditionManager.playerBuffCondition.Add(new List<PlayerBattleConditionManager.MemberBuffCondition>());
			PlayerBattleConditionManager.playerBadState.Add(new List<PlayerBattleConditionManager.MemberBadState>());
			for (int k = 0; k < PlayerEquipDataManager.playerHaveSkillList[PlayerStatusDataManager.playerPartyMember[j]].Count; k++)
			{
				int skillID = PlayerEquipDataManager.playerHaveSkillList[PlayerStatusDataManager.playerPartyMember[j]][k];
				BattleSkillData battleSkillData = GameDataManager.instance.playerSkillDataBase.skillDataList.Find((BattleSkillData data) => data.skillID == skillID);
				Debug.Log("キャラID：" + PlayerStatusDataManager.playerPartyMember[j] + "／Index：" + k + "／スキルID；" + skillID);
				PlayerBattleConditionManager.MemberSkillReChargeTurn item3 = new PlayerBattleConditionManager.MemberSkillReChargeTurn
				{
					skillID = battleSkillData.skillID,
					needRechargeTurn = 0,
					maxRechargeTurn = battleSkillData.skillRecharge
				};
				PlayerBattleConditionManager.playerSkillRechargeTurn[PlayerStatusDataManager.playerPartyMember[j]].Add(item3);
			}
		}
		for (int l = 0; l < PlayerInventoryDataManager.playerLearnedSkillList.Count; l++)
		{
			int skillID2 = PlayerInventoryDataManager.playerLearnedSkillList[l].skillID;
			BattleSkillData battleSkillData2 = GameDataManager.instance.playerSkillDataBase.skillDataList.Find((BattleSkillData data) => data.skillID == skillID2);
			Debug.Log("習得スキル／Index：" + l + "／スキルID；" + skillID2);
			PlayerBattleConditionManager.MemberSkillReChargeTurn item4 = new PlayerBattleConditionManager.MemberSkillReChargeTurn
			{
				skillID = battleSkillData2.skillID,
				needRechargeTurn = 0,
				maxRechargeTurn = battleSkillData2.skillRecharge
			};
			PlayerBattleConditionManager.playerSkillRechargeTurn[PlayerStatusDataManager.partyMemberCount].Add(item4);
		}
		PlayerBattleConditionManager.enemyIsDead.Clear();
		PlayerBattleConditionManager.enemyBuffCondition.Clear();
		PlayerBattleConditionManager.enemyBadState.Clear();
		for (int m = 0; m < PlayerStatusDataManager.enemyMember.Length; m++)
		{
			PlayerBattleConditionManager.MemberIsDead item5 = new PlayerBattleConditionManager.MemberIsDead
			{
				memberNum = m,
				memberID = PlayerStatusDataManager.enemyMember[m],
				memberAgility = PlayerStatusDataManager.enemyAgility[m],
				isDead = false
			};
			PlayerBattleConditionManager.enemyIsDead.Add(item5);
			PlayerBattleConditionManager.enemyBuffCondition.Add(new List<PlayerBattleConditionManager.MemberBuffCondition>());
			PlayerBattleConditionManager.enemyBadState.Add(new List<PlayerBattleConditionManager.MemberBadState>());
		}
		PlayerBattleConditionManager.playerIsDead = PlayerBattleConditionManager.playerIsDead.OrderByDescending((PlayerBattleConditionManager.MemberIsDead data) => data.memberAgility).ToList();
		PlayerBattleConditionManager.enemyIsDead = PlayerBattleConditionManager.enemyIsDead.OrderByDescending((PlayerBattleConditionManager.MemberIsDead data) => data.memberAgility).ToList();
	}

	public static void DungeonBattleConditionInititialize(bool isEnemyOnly)
	{
		if (!isEnemyOnly)
		{
			PlayerBattleConditionManager.playerIsDead.Clear();
			PlayerBattleConditionManager.playerBuffCondition.Clear();
			PlayerBattleConditionManager.playerBadState.Clear();
			PlayerBattleConditionManager.playerSkillRechargeTurn.Clear();
			PlayerBattleConditionManager.playerIsDead.Add(new PlayerBattleConditionManager.MemberIsDead());
			PlayerBattleConditionManager.playerBuffCondition.Add(new List<PlayerBattleConditionManager.MemberBuffCondition>());
			PlayerBattleConditionManager.playerBadState.Add(new List<PlayerBattleConditionManager.MemberBadState>());
		}
		PlayerBattleConditionManager.enemyIsDead.Clear();
		PlayerBattleConditionManager.enemyBuffCondition.Clear();
		PlayerBattleConditionManager.enemyBadState.Clear();
		PlayerBattleConditionManager.enemyIsDead.Add(new PlayerBattleConditionManager.MemberIsDead());
		PlayerBattleConditionManager.enemyBuffCondition.Add(new List<PlayerBattleConditionManager.MemberBuffCondition>());
		PlayerBattleConditionManager.enemyBadState.Add(new List<PlayerBattleConditionManager.MemberBadState>());
	}

	public static List<int> GetBattleAliveMemberList(List<PlayerBattleConditionManager.MemberIsDead> memberIsDeads)
	{
		List<int> result = new List<int>();
		for (int i = 0; i < memberIsDeads.Count; i++)
		{
			result = (from ano in memberIsDeads.Select((PlayerBattleConditionManager.MemberIsDead con, int index) => new
				{
					Content = con,
					Index = index
				})
				where !ano.Content.isDead
				select ano.Index).ToList();
		}
		return result;
	}

	public static int GetBattleBuffCondition(List<PlayerBattleConditionManager.MemberBuffCondition> memberBuffConditions, string typeName)
	{
		PlayerBattleConditionManager.MemberBuffCondition memberBuffCondition = memberBuffConditions.Find((PlayerBattleConditionManager.MemberBuffCondition data) => data.type == typeName);
		if (memberBuffCondition != null)
		{
			Debug.Log("バフデータを返す：" + typeName + "／パワー：" + memberBuffCondition.power);
			return memberBuffCondition.power;
		}
		Debug.Log("バフデータはない：" + typeName);
		return 0;
	}

	public static int GetBattleBadState(List<PlayerBattleConditionManager.MemberBadState> memberBadStates, string typeName)
	{
		PlayerBattleConditionManager.MemberBadState memberBadState = memberBadStates.Find((PlayerBattleConditionManager.MemberBadState data) => data.type == typeName);
		if (memberBadState != null)
		{
			Debug.Log("状態異常のターンを返す：" + typeName);
			return memberBadState.continutyTurn;
		}
		Debug.Log("状態異常はない：" + typeName);
		return 0;
	}

	public static void ReCalcBuffContinutyTurn()
	{
		UtageBattleSceneManager component = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		for (int i = 0; i < PlayerBattleConditionManager.playerIsDead.Count; i++)
		{
			if (!PlayerBattleConditionManager.playerBuffCondition[i].Any())
			{
				continue;
			}
			for (int num = PlayerBattleConditionManager.playerBuffCondition[i].Count; num > 0; num--)
			{
				int index = num - 1;
				PlayerBattleConditionManager.playerBuffCondition[i][index].continutyTurn--;
				Debug.Log("味方：バフ効果ターンを減少");
				if (PlayerBattleConditionManager.playerBuffCondition[i][index].continutyTurn <= 0)
				{
					string type = PlayerBattleConditionManager.playerBuffCondition[i][index].type;
					component.SetBuffIcon(isPositive: (PlayerBattleConditionManager.playerBuffCondition[i][index].power > 0) ? true : false, buffType: type, num: PlayerBattleConditionManager.playerIsDead[i].memberNum, targetForce: true, setVisible: false);
					Debug.Log("バフ効果を削除");
					PlayerBattleConditionManager.playerBuffCondition[i].RemoveAt(index);
				}
			}
		}
		for (int j = 0; j < PlayerBattleConditionManager.enemyIsDead.Count; j++)
		{
			if (!PlayerBattleConditionManager.enemyBuffCondition[j].Any())
			{
				continue;
			}
			for (int num2 = PlayerBattleConditionManager.enemyBuffCondition[j].Count; num2 > 0; num2--)
			{
				int index2 = num2 - 1;
				PlayerBattleConditionManager.enemyBuffCondition[j][index2].continutyTurn--;
				Debug.Log("敵：バフ効果ターンを減少");
				if (PlayerBattleConditionManager.enemyBuffCondition[j][index2].continutyTurn <= 0)
				{
					string type2 = PlayerBattleConditionManager.enemyBuffCondition[j][index2].type;
					component.SetBuffIcon(isPositive: (PlayerBattleConditionManager.enemyBuffCondition[j][index2].power > 0) ? true : false, buffType: type2, num: PlayerBattleConditionManager.enemyIsDead[j].memberNum, targetForce: false, setVisible: false);
					Debug.Log("バフ効果を削除");
					PlayerBattleConditionManager.enemyBuffCondition[j].RemoveAt(index2);
				}
			}
		}
	}

	public static void ReCalcBadStateTurn(bool isScenairoBattle)
	{
		UtageBattleSceneManager component = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		DungeonBattleManager component2 = GameObject.Find("Dungeon Battle Manager").GetComponent<DungeonBattleManager>();
		for (int i = 0; i < PlayerBattleConditionManager.playerIsDead.Count; i++)
		{
			if (!PlayerBattleConditionManager.playerBadState[i].Any())
			{
				continue;
			}
			for (int num = PlayerBattleConditionManager.playerBadState[i].Count; num > 0; num--)
			{
				int index = num - 1;
				PlayerBattleConditionManager.playerBadState[i][index].continutyTurn--;
				Debug.Log("味方：バフ効果ターンを減少");
				if (PlayerBattleConditionManager.playerBadState[i][index].continutyTurn <= 0)
				{
					string type = PlayerBattleConditionManager.playerBadState[i][index].type;
					if (isScenairoBattle)
					{
						component.SetBadStateIcon(type, i, targetForce: true, setVisible: false);
					}
					else
					{
						component2.SetBadStateIcon(type, 0, setValue: false);
					}
					Debug.Log("バフ効果を削除");
					PlayerBattleConditionManager.playerBadState[i].RemoveAt(index);
				}
			}
		}
		for (int j = 0; j < PlayerBattleConditionManager.enemyIsDead.Count; j++)
		{
			if (!PlayerBattleConditionManager.enemyBadState[j].Any())
			{
				continue;
			}
			for (int num2 = PlayerBattleConditionManager.enemyBadState[j].Count; num2 > 0; num2--)
			{
				int index2 = num2 - 1;
				PlayerBattleConditionManager.enemyBadState[j][index2].continutyTurn--;
				Debug.Log("敵：バフ効果ターンを減少");
				if (PlayerBattleConditionManager.enemyBadState[j][index2].continutyTurn <= 0)
				{
					string type2 = PlayerBattleConditionManager.enemyBadState[j][index2].type;
					if (isScenairoBattle)
					{
						component.SetBadStateIcon(type2, j, targetForce: false, setVisible: false);
					}
					else
					{
						component2.SetBadStateIcon(type2, 1, setValue: false);
					}
					Debug.Log("バフ効果を削除");
					PlayerBattleConditionManager.enemyBadState[j].RemoveAt(index2);
				}
			}
		}
	}

	public static void ResetTargetBuffCondtion(string forceType, int number)
	{
		if (!(forceType == "player"))
		{
			if (forceType == "enemy" && PlayerBattleConditionManager.enemyBuffCondition[number].Any())
			{
				Debug.Log("敵のバフ全削除／Num：" + number);
				PlayerBattleConditionManager.enemyBuffCondition[number].Clear();
			}
			return;
		}
		int index = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == number);
		if (PlayerBattleConditionManager.playerBuffCondition[index].Any())
		{
			Debug.Log("味方のバフ全削除／Num：" + number + "／素早さ順：" + index);
			PlayerBattleConditionManager.playerBuffCondition[index].Clear();
		}
	}

	public static void ResetTargetBadState(string forceType, int number)
	{
		if (!(forceType == "player"))
		{
			if (forceType == "enemy" && PlayerBattleConditionManager.enemyBadState[number].Any())
			{
				Debug.Log("敵の状態異常全削除／Num：" + number);
				PlayerBattleConditionManager.enemyBadState[number].Clear();
			}
			return;
		}
		int index = PlayerBattleConditionManager.playerIsDead.FindIndex((PlayerBattleConditionManager.MemberIsDead data) => data.memberNum == number);
		if (PlayerBattleConditionManager.playerBadState[index].Any())
		{
			Debug.Log("味方の状態異常全削除／Num：" + number + "／素早さ順：" + index);
			PlayerBattleConditionManager.playerBadState[index].Clear();
		}
	}

	public static void SetPlayerUseSkillRecharge(int characterID, int skillID)
	{
		PlayerBattleConditionManager.MemberSkillReChargeTurn memberSkillReChargeTurn = PlayerBattleConditionManager.playerSkillRechargeTurn[characterID].Find((PlayerBattleConditionManager.MemberSkillReChargeTurn data) => data.skillID == skillID);
		memberSkillReChargeTurn.needRechargeTurn = memberSkillReChargeTurn.maxRechargeTurn;
	}

	public static void ReCalcPlayerSkillRecharge()
	{
		for (int i = 0; i < PlayerStatusDataManager.playerPartyMember.Length; i++)
		{
			int index = PlayerStatusDataManager.playerPartyMember[i];
			for (int j = 0; j < PlayerBattleConditionManager.playerSkillRechargeTurn[index].Count; j++)
			{
				if (PlayerBattleConditionManager.playerSkillRechargeTurn[index][j].needRechargeTurn > 0)
				{
					PlayerBattleConditionManager.playerSkillRechargeTurn[index][j].needRechargeTurn--;
				}
			}
		}
		for (int k = 0; k < PlayerBattleConditionManager.playerSkillRechargeTurn[PlayerStatusDataManager.partyMemberCount].Count; k++)
		{
			if (PlayerBattleConditionManager.playerSkillRechargeTurn[PlayerStatusDataManager.partyMemberCount][k].needRechargeTurn > 0)
			{
				PlayerBattleConditionManager.playerSkillRechargeTurn[PlayerStatusDataManager.partyMemberCount][k].needRechargeTurn--;
			}
		}
	}
}
