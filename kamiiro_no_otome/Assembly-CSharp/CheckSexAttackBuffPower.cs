using System.Collections.Generic;
using System.Linq;
using Arbor;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class CheckSexAttackBuffPower : StateBehaviour
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

	public StateLink noBuffPowerLink;

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
		SexSkillData sexSkillData = null;
		switch (type)
		{
		case Type.player:
			sexSkillData = sexBattleManager.selectSexSkillData;
			break;
		case Type.heroine:
			sexSkillData = sexBattleManager.heroineSexSkillData;
			break;
		}
		if (sexSkillData.buffType == SexSkillData.BuffType.none)
		{
			Debug.Log("バフ効果なし");
			Transition(noBuffPowerLink);
		}
		else
		{
			int num = Random.Range(0, 100);
			Debug.Log("バフ効果命中率：" + sexSkillData.subProbability + " / スキル命中ランダム：" + num);
			if (sexSkillData.subProbability >= num)
			{
				switch (type)
				{
				case Type.player:
					SetSexBattleTopMessage_BuffPower();
					break;
				case Type.heroine:
					SetSexBattleBottomMessage_BuffPower();
					break;
				}
			}
			else
			{
				switch (type)
				{
				case Type.player:
					sexBattleMessageTextManager.sexBattleMessageGroup_Top[6].GetComponent<Localize>().Term = "battleTextNoEffect";
					sexBattleMessageTextManager.sexBattleMessageGroupGo_SubPowerRaw[0].SetActive(value: true);
					sexBattleMessageTextManager.sexBattleMessageGroup_Top[6].gameObject.SetActive(value: true);
					break;
				case Type.heroine:
					sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[7].GetComponent<Localize>().Term = "battleTextNoEffect";
					sexBattleMessageTextManager.sexBattleMessageGroupGo_SubPowerRaw[1].SetActive(value: true);
					sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[7].gameObject.SetActive(value: true);
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

	private void SetSexBattleTopMessage_BuffPower()
	{
		SexSkillData selectSexSkillData = sexBattleManager.selectSexSkillData;
		switch (selectSexSkillData.buffType)
		{
		case SexSkillData.BuffType.critical:
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[6].GetComponent<Localize>().Term = "sexBattleTarget_02";
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[7].GetComponent<Localize>().Term = "sexBuff_Critial";
			break;
		case SexSkillData.BuffType.healPower:
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[6].GetComponent<Localize>().Term = "sexBattleTarget_02";
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[7].GetComponent<Localize>().Term = "sexBuff_HealPower";
			break;
		case SexSkillData.BuffType.regeneration:
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[6].GetComponent<Localize>().Term = "sexBattleTarget_01";
			sexBattleMessageTextManager.sexBattleMessageGroup_Top[7].GetComponent<Localize>().Term = "sexBuff_Regeneration";
			break;
		}
		sexBattleMessageTextManager.sexBattleMessageGroupGo_SubPowerRaw[0].SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Top[6].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Top[7].gameObject.SetActive(value: true);
		AddSexBattleBuffPower(selectSexSkillData);
	}

	private void SetSexBattleBottomMessage_BuffPower()
	{
		SexSkillData heroineSexSkillData = sexBattleManager.heroineSexSkillData;
		int selectSexBattleHeroineId = PlayerNonSaveDataManager.selectSexBattleHeroineId;
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[7].GetComponent<Localize>().Term = "sexBattleTarget_" + selectSexBattleHeroineId + 2;
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[9].GetComponent<Localize>().Term = "sexSubPower_AddSubPower";
		switch (heroineSexSkillData.buffType)
		{
		case SexSkillData.BuffType.attack:
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[8].GetComponent<Localize>().Term = "sexBuff_Attack";
			break;
		case SexSkillData.BuffType.critical:
			sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[8].GetComponent<Localize>().Term = "sexBuff_Critial";
			break;
		}
		sexBattleMessageTextManager.sexBattleMessageGroupGo_SubPowerRaw[1].SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[7].gameObject.SetActive(value: true);
		sexBattleMessageTextManager.sexBattleMessageGroup_Bottom[8].gameObject.SetActive(value: true);
		AddSexBattleBuffPower(heroineSexSkillData);
	}

	private void AddSexBattleBuffPower(SexSkillData skillData)
	{
		List<int> list = new List<int>();
		List<List<PlayerSexStatusDataManager.MemberSexBuffCondition>> playerSexBuffCondition = PlayerSexStatusDataManager.playerSexBuffCondition;
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
		list = (from ano in playerSexBuffCondition[index2].Select((PlayerSexStatusDataManager.MemberSexBuffCondition con, int index) => new
			{
				Content = con,
				Index = index
			})
			where ano.Content.type == skillData.buffType.ToString()
			select ano.Index).ToList();
		if (list.Any())
		{
			playerSexBuffCondition[index2][list[0]].continutyTurn = skillData.skillContinuityTurn;
			Debug.Log("付与効果ターン増加");
			return;
		}
		PlayerSexStatusDataManager.MemberSexBuffCondition item = new PlayerSexStatusDataManager.MemberSexBuffCondition
		{
			type = skillData.buffType.ToString(),
			power = skillData.subPower,
			continutyTurn = skillData.skillContinuityTurn + 1
		};
		playerSexBuffCondition[index2].Add(item);
		sexBattleEffectManager.SetBuffIcon(skillData.buffType.ToString(), targetIsHeroine, setVisible: true);
		Debug.Log("バフ効果データ追加");
	}
}
