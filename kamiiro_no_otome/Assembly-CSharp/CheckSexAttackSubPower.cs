using System.Collections.Generic;
using System.Linq;
using Arbor;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class CheckSexAttackSubPower : StateBehaviour
{
	public enum Type
	{
		player,
		heroine
	}

	private SexBattleManager sexBattleManager;

	private SexBattleTurnManager sexBattleTurnManager;

	private SexBattleMessageTextManager sexBattleMessageTextManager;

	private SexBattleEffectManager sexBattleEffectManager;

	public Type type;

	public StateLink stateLink;

	public StateLink noSubPowerLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		sexBattleTurnManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleTurnManager>();
		sexBattleMessageTextManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleMessageTextManager>();
		sexBattleEffectManager = GameObject.Find("SexBattle Turn Manager").GetComponent<SexBattleEffectManager>();
	}

	public override void OnStateBegin()
	{
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		SexSkillData sexSkillData = null;
		switch (type)
		{
		case Type.player:
			sexSkillData = sexBattleManager.heroineSexSkillData;
			break;
		case Type.heroine:
			sexSkillData = sexBattleManager.selectSexSkillData;
			break;
		}
		if (sexSkillData.subType == SexSkillData.SubType.none)
		{
			Debug.Log("付与効果なし");
			Transition(noSubPowerLink);
		}
		else
		{
			int num = Random.Range(0, 100);
			Debug.Log("付与効果命中率：" + sexSkillData.subProbability + " / スキル命中ランダム：" + num);
			if (sexSkillData.subProbability >= num)
			{
				switch (type)
				{
				case Type.player:
					SetSexBattleBottomMessage_SubPower();
					break;
				case Type.heroine:
					if (sexSkillData.subType == SexSkillData.SubType.absorb && PlayerSexStatusDataManager.playerSexHp[selectSexBattleHeroineId] > 0)
					{
						if ((float)(PlayerSexStatusDataManager.playerSexMaxHp[selectSexBattleHeroineId] / PlayerSexStatusDataManager.playerSexHp[selectSexBattleHeroineId]) <= 0.5f)
						{
							SetSexBattleBottomMessage_SubPower();
							break;
						}
						sexBattleMessageTextManager.sexBattleMessageGroupGo_SubPowerRaw[0].SetActive(value: true);
						sexBattleMessageTextManager.sexBattleMessageGroup_Top[6].GetComponent<Localize>().Term = "battleTextNoEffect";
						sexBattleMessageTextManager.sexBattleMessageGroup_Top[6].gameObject.SetActive(value: true);
					}
					else
					{
						SetSexBattleTopMessage_SubPower();
					}
					break;
				}
			}
			else
			{
				switch (type)
				{
				case Type.player:
					sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[7].GetComponent<Localize>().Term = "battleTextNoEffect";
					sexBattleMessageTextManager.sexBattleMessageGroupGo_SubPowerRaw[1].SetActive(value: true);
					sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[7].gameObject.SetActive(value: true);
					break;
				case Type.heroine:
					sexBattleMessageTextManager.sexBattleMessageGroup_Top[6].GetComponent<Localize>().Term = "battleTextNoEffect";
					sexBattleMessageTextManager.sexBattleMessageGroupGo_SubPowerRaw[0].SetActive(value: true);
					sexBattleMessageTextManager.sexBattleMessageGroup_Top[6].gameObject.SetActive(value: true);
					break;
				}
			}
		}
		Invoke("InvokeMethod", 0.3f);
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
		Transition(stateLink);
	}

	private void SetSexBattleTopMessage_SubPower()
	{
		SexSkillData selectSexSkillData = sexBattleManager.selectSexSkillData;
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		if (selectSexSkillData.skillID != 200)
		{
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[6].GetComponent<Localize>().Term = "sexBattleTarget_" + selectSexBattleHeroineId + 1;
			switch (selectSexSkillData.subType)
			{
			case SexSkillData.SubType.trance:
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[7].GetComponent<Localize>().Term = "sexSubPower_Trance";
				break;
			case SexSkillData.SubType.absorb:
				sexBattleMessageTextManager.sexBattleMessageGroup_Top[7].GetComponent<Localize>().Term = "sexSubPower_Absorb";
				break;
			}
			sexBattleMessageTextManager.sexBattleMessageGroupGo_SubPowerRaw[0].SetActive(value: true);
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[6].gameObject.SetActive(value: true);
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[7].gameObject.SetActive(value: true);
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[8].gameObject.SetActive(value: true);
		}
		else
		{
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[7].GetComponent<Localize>().Term = "sexBattleTarget_" + selectSexBattleHeroineId + 1;
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[8].GetComponent<Localize>().Term = "sexSubPower_Trance";
			sexBattleMessageTextManager.sexBattleMessageGroupGo_SubPowerRaw[1].SetActive(value: true);
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[7].gameObject.SetActive(value: true);
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[8].gameObject.SetActive(value: true);
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[9].gameObject.SetActive(value: true);
		}
		AddSexBattleSubPower(selectSexSkillData);
	}

	private void SetSexBattleBottomMessage_SubPower()
	{
		SexSkillData heroineSexSkillData = sexBattleManager.heroineSexSkillData;
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[7].GetComponent<Localize>().Term = "sexBattleTarget_01";
		switch (heroineSexSkillData.subType)
		{
		case SexSkillData.SubType.crazy:
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[8].GetComponent<Localize>().Term = "sexSubPower_Crazy";
			break;
		case SexSkillData.SubType.enhancement:
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[8].GetComponent<Localize>().Term = "sexSubPower_Enhancement";
			break;
		case SexSkillData.SubType.pistonOnly:
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[8].GetComponent<Localize>().Term = "sexSubPower_PistonOnly";
			break;
		case SexSkillData.SubType.titsOnly:
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[8].GetComponent<Localize>().Term = "sexSubPower_TitsOnly";
			break;
		case SexSkillData.SubType.desire:
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[8].GetComponent<Localize>().Term = "sexSubPower_Desire";
			break;
		}
		sexBattleMessageTextManager.sexBattleMessageGroupGo_SubPowerRaw[1].SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[7].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[8].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[9].gameObject.SetActive(value: true);
		AddSexBattleSubPower(heroineSexSkillData);
	}

	private void AddSexBattleSubPower(SexSkillData skillData)
	{
		List<int> list = new List<int>();
		List<List<PlayerSexStatusDataManager.MemberSexSubPower>> playerSexSubPower = PlayerSexStatusDataManager.playerSexSubPower;
		int index2 = 0;
		bool targetIsHeroine = false;
		switch (type)
		{
		case Type.player:
			index2 = 0;
			break;
		case Type.heroine:
			index2 = 1;
			targetIsHeroine = true;
			break;
		}
		list = (from ano in playerSexSubPower[index2].Select((PlayerSexStatusDataManager.MemberSexSubPower con, int index) => new
			{
				Content = con,
				Index = index
			})
			where ano.Content.type == skillData.subType.ToString()
			select ano.Index).ToList();
		if (list.Any())
		{
			playerSexSubPower[index2][list[0]].continutyTurn = skillData.skillContinuityTurn;
			Debug.Log("付与効果ターン増加");
			return;
		}
		PlayerSexStatusDataManager.MemberSexSubPower item = new PlayerSexStatusDataManager.MemberSexSubPower
		{
			type = skillData.subType.ToString(),
			continutyTurn = skillData.skillContinuityTurn
		};
		playerSexSubPower[index2].Add(item);
		sexBattleEffectManager.SetSubPowerIcon(skillData.subType.ToString(), targetIsHeroine, setVisible: true);
		Debug.Log("付与効果データ追加");
	}
}
