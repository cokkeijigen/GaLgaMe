using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSexBattleConditionAccess : MonoBehaviour
{
	public static void SexBattleConditionInititialize()
	{
		PlayerSexStatusDataManager.playerSexBuffCondition.Clear();
		PlayerSexStatusDataManager.playerSexSubPower.Clear();
		PlayerSexStatusDataManager.playerSexSkillRechargeTurn.Clear();
		for (int i = 0; i < 2; i++)
		{
			PlayerSexStatusDataManager.playerSexBuffCondition.Add(new List<PlayerSexStatusDataManager.MemberSexBuffCondition>());
			PlayerSexStatusDataManager.playerSexSubPower.Add(new List<PlayerSexStatusDataManager.MemberSexSubPower>());
			List<PlayerSexStatusDataManager.MemberSexSkillReChargeTurn> item = new List<PlayerSexStatusDataManager.MemberSexSkillReChargeTurn>();
			PlayerSexStatusDataManager.playerSexSkillRechargeTurn.Add(item);
		}
		for (int j = 0; j < PlayerSexStatusDataManager.playerUseSexActiveSkillList[0].Count; j++)
		{
			SexSkillData sexSkillData = PlayerSexStatusDataManager.playerUseSexActiveSkillList[0][j];
			Debug.Log("キャラID：" + 0 + "／Index：" + j + "／えっちスキルID；" + sexSkillData.skillID);
			PlayerSexStatusDataManager.MemberSexSkillReChargeTurn item2 = new PlayerSexStatusDataManager.MemberSexSkillReChargeTurn
			{
				skillID = sexSkillData.skillID,
				needRechargeTurn = 0,
				maxRechargeTurn = sexSkillData.skillRecharge
			};
			PlayerSexStatusDataManager.playerSexSkillRechargeTurn[0].Add(item2);
		}
		for (int k = 0; k < PlayerSexStatusDataManager.playerUseSexActiveSkillList[PlayerNonSaveDataManager.selectSexBattleHeroineId].Count; k++)
		{
			SexSkillData sexSkillData2 = PlayerSexStatusDataManager.playerUseSexActiveSkillList[PlayerNonSaveDataManager.selectSexBattleHeroineId][k];
			Debug.Log("キャラID：" + PlayerNonSaveDataManager.selectSexBattleHeroineId + "／Index：" + k + "／えっちスキルID；" + sexSkillData2.skillID);
			PlayerSexStatusDataManager.MemberSexSkillReChargeTurn item3 = new PlayerSexStatusDataManager.MemberSexSkillReChargeTurn
			{
				skillID = sexSkillData2.skillID,
				needRechargeTurn = 0,
				maxRechargeTurn = sexSkillData2.skillRecharge
			};
			PlayerSexStatusDataManager.playerSexSkillRechargeTurn[1].Add(item3);
		}
	}

	public static int GetSexBattleBuffCondition(List<PlayerSexStatusDataManager.MemberSexBuffCondition> memberSexBuffConditions, string typeName)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < memberSexBuffConditions.Count; i++)
		{
			list = (from ano in memberSexBuffConditions.Select((PlayerSexStatusDataManager.MemberSexBuffCondition con, int index) => new
				{
					Content = con,
					Index = index
				})
				where ano.Content.type == typeName
				select ano.Index).ToList();
		}
		if (list.Any())
		{
			Debug.Log("バフデータを返す");
			return memberSexBuffConditions[list[0]].power;
		}
		Debug.Log("バフデータはない");
		return 0;
	}

	public static int GetSexBattleSubPower(List<PlayerSexStatusDataManager.MemberSexSubPower> memberSexSubPower, string typeName)
	{
		List<int> list = new List<int>();
		for (int i = 0; i < memberSexSubPower.Count; i++)
		{
			list = (from ano in memberSexSubPower.Select((PlayerSexStatusDataManager.MemberSexSubPower con, int index) => new
				{
					Content = con,
					Index = index
				})
				where ano.Content.type == typeName
				select ano.Index).ToList();
		}
		if (list.Any())
		{
			Debug.Log("付与効果のターンを返す");
			return memberSexSubPower[list[0]].continutyTurn;
		}
		Debug.Log("付与効果はない");
		return 0;
	}

	public static void ReCalcSexBuffContinutyTurn()
	{
		List<int> list = new List<int>();
		SexBattleEffectManager component = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleEffectManager>();
		for (int i = 0; i < 2; i++)
		{
			list = (from ano in PlayerSexStatusDataManager.playerSexBuffCondition[i].Select((PlayerSexStatusDataManager.MemberSexBuffCondition con, int index) => new
				{
					Content = con,
					Index = index
				})
				where ano.Content.continutyTurn > 0
				select ano.Index).ToList();
			if (!list.Any())
			{
				continue;
			}
			for (int num = list.Count; num > 0; num--)
			{
				int index2 = num - 1;
				PlayerSexStatusDataManager.playerSexBuffCondition[i][list[index2]].continutyTurn--;
				Debug.Log("味方：バフ効果ターンを減少");
				if (PlayerSexStatusDataManager.playerSexBuffCondition[i][list[index2]].continutyTurn <= 0)
				{
					bool targetIsHeroine = false;
					if (i != 0)
					{
						targetIsHeroine = true;
					}
					string type = PlayerSexStatusDataManager.playerSexBuffCondition[i][list[index2]].type;
					_ = PlayerSexStatusDataManager.playerSexBuffCondition[i][list[index2]].power;
					component.SetBuffIcon(type, targetIsHeroine, setVisible: false);
					Debug.Log("バフ効果を削除");
					PlayerSexStatusDataManager.playerSexBuffCondition[i].RemoveAt(list[index2]);
				}
			}
		}
	}

	public static void ReCalcSubPowerTurn()
	{
		List<int> list = new List<int>();
		SexBattleEffectManager component = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleEffectManager>();
		for (int i = 0; i < 2; i++)
		{
			list = (from ano in PlayerSexStatusDataManager.playerSexSubPower[i].Select((PlayerSexStatusDataManager.MemberSexSubPower con, int index) => new
				{
					Content = con,
					Index = index
				})
				where ano.Content.continutyTurn > 0
				select ano.Index).ToList();
			if (!list.Any())
			{
				continue;
			}
			for (int num = list.Count; num > 0; num--)
			{
				int index2 = num - 1;
				PlayerSexStatusDataManager.playerSexSubPower[i][list[index2]].continutyTurn--;
				Debug.Log("味方：状態異常ターンを減少");
				if (PlayerSexStatusDataManager.playerSexSubPower[i][list[index2]].continutyTurn <= 0)
				{
					bool targetIsHeroine = false;
					if (i != 0)
					{
						targetIsHeroine = true;
					}
					string type = PlayerSexStatusDataManager.playerSexSubPower[i][list[index2]].type;
					component.SetSubPowerIcon(type, targetIsHeroine, setVisible: false);
					Debug.Log("状態異常を削除");
					PlayerSexStatusDataManager.playerSexSubPower[i].RemoveAt(list[index2]);
				}
			}
		}
	}

	public static void SetPlayerUseSexSkillRecharge(int characterID, int skillID)
	{
		PlayerSexStatusDataManager.MemberSexSkillReChargeTurn memberSexSkillReChargeTurn = PlayerSexStatusDataManager.playerSexSkillRechargeTurn[characterID].Find((PlayerSexStatusDataManager.MemberSexSkillReChargeTurn data) => data.skillID == skillID);
		memberSexSkillReChargeTurn.needRechargeTurn = memberSexSkillReChargeTurn.maxRechargeTurn + 1;
	}

	public static void ReCalcPlayerSexSkillRecharge()
	{
		for (int i = 0; i < 2; i++)
		{
			for (int j = 0; j < PlayerSexStatusDataManager.playerSexSkillRechargeTurn[i].Count; j++)
			{
				if (PlayerSexStatusDataManager.playerSexSkillRechargeTurn[i][j].needRechargeTurn > 0)
				{
					PlayerSexStatusDataManager.playerSexSkillRechargeTurn[i][j].needRechargeTurn--;
				}
			}
		}
	}

	public static void ResetSexBuffCondtion(string forceType)
	{
		new List<int>();
		if (!(forceType == "player"))
		{
			if (forceType == "heroine" && (from ano in PlayerSexStatusDataManager.playerSexBuffCondition[1].Select((PlayerSexStatusDataManager.MemberSexBuffCondition con, int index) => new
				{
					Content = con,
					Index = index
				})
				where ano.Content.continutyTurn > 0
				select ano.Index).ToList().Any())
			{
				Debug.Log("ヒロインのバフ全削除");
				PlayerSexStatusDataManager.playerSexBuffCondition[1].Clear();
			}
		}
		else if ((from ano in PlayerSexStatusDataManager.playerSexBuffCondition[0].Select((PlayerSexStatusDataManager.MemberSexBuffCondition con, int index) => new
			{
				Content = con,
				Index = index
			})
			where ano.Content.continutyTurn > 0
			select ano.Index).ToList().Any())
		{
			Debug.Log("エデンのバフ全削除");
			PlayerSexStatusDataManager.playerSexBuffCondition[0].Clear();
		}
	}

	public static void ResetSexSubPower(string forceType)
	{
		new List<int>();
		if (!(forceType == "player"))
		{
			if (forceType == "heroine" && (from ano in PlayerSexStatusDataManager.playerSexSubPower[1].Select((PlayerSexStatusDataManager.MemberSexSubPower con, int index) => new
				{
					Content = con,
					Index = index
				})
				where ano.Content.continutyTurn > 0
				select ano.Index).ToList().Any())
			{
				Debug.Log("ヒロインの状態異常全削除");
				PlayerSexStatusDataManager.playerSexSubPower[1].Clear();
			}
		}
		else if ((from ano in PlayerSexStatusDataManager.playerSexSubPower[0].Select((PlayerSexStatusDataManager.MemberSexSubPower con, int index) => new
			{
				Content = con,
				Index = index
			})
			where ano.Content.continutyTurn > 0
			select ano.Index).ToList().Any())
		{
			Debug.Log("エデンの状態異常全削除");
			PlayerSexStatusDataManager.playerSexSubPower[0].Clear();
		}
	}
}
