using Arbor;
using DarkTonic.MasterAudio;
using I2.Loc;
using UnityEngine;

[AddComponentMenu("")]
public class SetScenarioSkillStartText : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	private string playVoiceGroupName;

	private string playVoiceName;

	private int randomValue;

	private int countValue;

	public float waitTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
	}

	public override void OnStateBegin()
	{
		playVoiceGroupName = "";
		playVoiceName = "";
		float time = waitTime / (float)utageBattleSceneManager.battleSpeed;
		BattleSkillData battleSkillData = null;
		utageBattleSceneManager.battleTextPanel.SetActive(value: true);
		utageBattleSceneManager.battleTextGroupArray[2].SetActive(value: false);
		if (scenarioBattleSkillManager.isUseChargeSkill)
		{
			int index = Random.Range(0, scenarioBattleSkillManager.chargeSkillCharacterId.Count);
			int num = scenarioBattleSkillManager.chargeSkillCharacterId[index];
			randomValue = Random.Range(0, 2);
			utageBattleSceneManager.battleTopText.GetComponent<Localize>().Term = "playerSkillText_charge" + num + randomValue;
			if (!PlayerOptionsDataManager.isAllVoiceDisable)
			{
				switch (num)
				{
				case 1:
					playVoiceGroupName = "battleVoice_" + num;
					playVoiceName = "battleVoice_charge" + num + randomValue;
					if (PlayerOptionsDataManager.defaultLucyVoiceTypeSexy)
					{
						playVoiceName += "_sexy(Clone)";
					}
					else
					{
						playVoiceName += "(Clone)";
					}
					MasterAudio.PlaySound(playVoiceGroupName, 1f, null, 0f, playVoiceName, null);
					scenarioBattleSkillManager.playingSKillVoiceGroupName = playVoiceGroupName;
					break;
				case 2:
				case 3:
				case 4:
					playVoiceGroupName = "battleVoice_" + num;
					playVoiceName = "battleVoice_charge" + num + randomValue + "(Clone)";
					MasterAudio.PlaySound(playVoiceGroupName, 1f, null, 0f, playVoiceName, null);
					scenarioBattleSkillManager.playingSKillVoiceGroupName = playVoiceGroupName;
					break;
				}
			}
			Invoke("ChargeAttackStartText", time);
		}
		else if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			battleSkillData = GameDataManager.instance.playerSkillDataBase.skillDataList.Find((BattleSkillData data) => data.skillID == scenarioBattleTurnManager.battleUseSkillID);
			int useSkillPartyMemberID = scenarioBattleTurnManager.useSkillPartyMemberID;
			randomValue = Random.Range(0, 2);
			utageBattleSceneManager.battleTopText.GetComponent<Localize>().Term = "playerSkillText_" + battleSkillData.skillType.ToString() + useSkillPartyMemberID + randomValue;
			if (!PlayerOptionsDataManager.isAllVoiceDisable)
			{
				switch (useSkillPartyMemberID)
				{
				case 1:
				{
					string skillTypeName2 = GetSkillTypeName(battleSkillData.skillType);
					playVoiceGroupName = "battleVoice_" + useSkillPartyMemberID;
					playVoiceName = "battleVoice_" + skillTypeName2 + useSkillPartyMemberID + randomValue;
					if (PlayerOptionsDataManager.defaultLucyVoiceTypeSexy)
					{
						playVoiceName += "_sexy(Clone)";
					}
					else
					{
						playVoiceName += "(Clone)";
					}
					Debug.Log("グループ名：" + playVoiceGroupName + "／ボイス名：" + playVoiceName);
					MasterAudio.PlaySound(playVoiceGroupName, 1f, null, 0f, playVoiceName, null);
					scenarioBattleSkillManager.playingSKillVoiceGroupName = playVoiceGroupName;
					break;
				}
				case 2:
				case 3:
				case 4:
				{
					string skillTypeName = GetSkillTypeName(battleSkillData.skillType);
					playVoiceGroupName = "battleVoice_" + useSkillPartyMemberID;
					playVoiceName = "battleVoice_" + skillTypeName + useSkillPartyMemberID + randomValue + "(Clone)";
					Debug.Log("グループ名：" + playVoiceGroupName + "／ボイス名：" + playVoiceName);
					MasterAudio.PlaySound(playVoiceGroupName, 1f, null, 0f, playVoiceName, null);
					scenarioBattleSkillManager.playingSKillVoiceGroupName = playVoiceGroupName;
					break;
				}
				}
			}
			Invoke("AttackStartText", time);
		}
		else
		{
			countValue = scenarioBattleTurnManager.enemyAttackCount;
			utageBattleSceneManager.battleTopText.GetComponent<Localize>().Term = "enemySkillText" + PlayerBattleConditionManager.enemyIsDead[countValue].memberID;
			Invoke("AttackStartText", time);
		}
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

	private void AttackStartText()
	{
		float time = waitTime / (float)utageBattleSceneManager.battleSpeed;
		if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			utageBattleSceneManager.battleTextArray2[0].GetComponent<Localize>().Term = "character" + scenarioBattleTurnManager.useSkillPartyMemberID;
			utageBattleSceneManager.battleTextArray2[1].GetComponent<Localize>().Term = "textConjunctionIs";
			utageBattleSceneManager.battleTextArray2[2].GetComponent<Localize>().Term = "playerSkill" + scenarioBattleTurnManager.battleUseSkillID;
			utageBattleSceneManager.battleTextArray2[3].GetComponent<Localize>().Term = "battleTextUseItem";
		}
		else
		{
			utageBattleSceneManager.battleTextArray2[0].GetComponent<Localize>().Term = "enemy" + PlayerBattleConditionManager.enemyIsDead[scenarioBattleTurnManager.enemyAttackCount].memberID;
			utageBattleSceneManager.battleTextArray2[1].GetComponent<Localize>().Term = "battleTextEnemySkillAttack";
			utageBattleSceneManager.battleTextArray2[2].GetComponent<Localize>().Term = "enemySkill" + scenarioBattleTurnManager.battleUseSkillID;
			utageBattleSceneManager.battleTextArray2[3].GetComponent<Localize>().Term = "exclamationMark";
		}
		utageBattleSceneManager.battleTopText.SetActive(value: true);
		GameObject[] battleTextArray = utageBattleSceneManager.battleTextArray2;
		for (int i = 0; i < battleTextArray.Length; i++)
		{
			battleTextArray[i].SetActive(value: true);
		}
		Invoke("InvokeMethod", time);
	}

	private void ChargeAttackStartText()
	{
		float time = waitTime / (float)utageBattleSceneManager.battleSpeed;
		utageBattleSceneManager.battleTextArray2[0].GetComponent<Localize>().Term = "characterAll";
		utageBattleSceneManager.battleTextArray2[1].GetComponent<Localize>().Term = "textConjunctionIs";
		utageBattleSceneManager.battleTextArray2[2].GetComponent<Localize>().Term = "playerSkill" + scenarioBattleTurnManager.battleUseSkillID;
		utageBattleSceneManager.battleTextArray2[3].GetComponent<Localize>().Term = "battleTextUseItem";
		utageBattleSceneManager.battleTopText.SetActive(value: true);
		GameObject[] battleTextArray = utageBattleSceneManager.battleTextArray2;
		for (int i = 0; i < battleTextArray.Length; i++)
		{
			battleTextArray[i].SetActive(value: true);
		}
		Invoke("InvokeMethod", time);
	}

	private void InvokeMethod()
	{
		Transition(stateLink);
	}

	private string GetSkillTypeName(BattleSkillData.SkillType skillType)
	{
		string result = "";
		switch (skillType)
		{
		case BattleSkillData.SkillType.attack:
		case BattleSkillData.SkillType.magicAttack:
		case BattleSkillData.SkillType.mixAttack:
			result = "attack";
			break;
		case BattleSkillData.SkillType.buff:
			result = "buff";
			break;
		case BattleSkillData.SkillType.deBuff:
			result = "deBuff";
			break;
		case BattleSkillData.SkillType.heal:
			result = "heal";
			break;
		case BattleSkillData.SkillType.medic:
			result = "medic";
			break;
		case BattleSkillData.SkillType.revive:
			result = "revive";
			break;
		}
		return result;
	}
}
