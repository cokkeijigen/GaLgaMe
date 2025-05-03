using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class UseBattleSkillWindow : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	private ScenarioBattleUiManager scenarioBattleUiManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = GameObject.Find("Scenario Battle Manager").GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
		scenarioBattleUiManager = GameObject.Find("Battle Ui Manager").GetComponent<ScenarioBattleUiManager>();
	}

	public override void OnStateBegin()
	{
		BattleSkillData battleSkillData = null;
		scenarioBattleTurnManager.skillAttackHitDataList.Clear();
		scenarioBattleTurnManager.skillAttackHitDataSubList.Clear();
		if (scenarioBattleTurnManager.isUseSkillPlayer)
		{
			scenarioBattleSkillManager.skillWindow.SetActive(value: false);
			scenarioBattleSkillManager.commandClickSummaryWindow.SetActive(value: false);
			scenarioBattleSkillManager.blackImageGoArray[0].SetActive(value: false);
			scenarioBattleSkillManager.blackImageGoArray[1].SetActive(value: false);
			GameObject[] commandButtonGroup = utageBattleSceneManager.commandButtonGroup;
			for (int i = 0; i < commandButtonGroup.Length; i++)
			{
				commandButtonGroup[i].SetActive(value: false);
			}
			utageBattleSceneManager.battleCanvas.GetComponent<CanvasGroup>().interactable = false;
			utageBattleSceneManager.SetEnemyTargetGroupVisble(isVisible: false);
			scenarioBattleTurnManager.setFrameTypeName = "reset";
			scenarioBattleUiManager.uiFSM.SendTrigger("SetCharacterFrameType");
			utageBattleSceneManager.SetSelectFrameEnable(isEnable: true);
			scenarioBattleUiManager.SetMaterialEffect("none");
			int characterID = scenarioBattleTurnManager.useSkillPartyMemberID;
			int memberNum = PlayerBattleConditionManager.playerIsDead.Find((PlayerBattleConditionManager.MemberIsDead data) => data.memberID == characterID).memberNum;
			if (characterID == 0)
			{
				switch (scenarioBattleSkillManager.selectSkillMpType)
				{
				case 0:
					characterID = 0;
					break;
				case 1:
					characterID = PlayerStatusDataManager.partyMemberCount;
					break;
				}
			}
			scenarioBattleTurnManager.isUseSkillCheckArray[scenarioBattleTurnManager.useSkillPartyMemberID] = true;
			PlayerBattleConditionAccess.SetPlayerUseSkillRecharge(characterID, scenarioBattleTurnManager.battleUseSkillID);
			battleSkillData = scenarioBattleTurnManager.playerSkillData;
			int useMP = battleSkillData.useMP;
			if (characterID == 0)
			{
				int weaponID = PlayerEquipDataManager.playerEquipWeaponID[0];
				HaveWeaponData haveWeaponData = PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.itemID == weaponID && data.equipCharacter == 0);
				haveWeaponData.weaponIncludeMp = Mathf.Clamp(haveWeaponData.weaponIncludeMp -= useMP, 0, haveWeaponData.weaponIncludeMaxMp);
				utageBattleSceneManager.playerFrameGoList[0].GetComponent<ArborFSM>().SendTrigger("ChangeCharacterFrameSlider");
				Debug.Log("スキルでTP消費");
			}
			else
			{
				if (characterID == PlayerStatusDataManager.partyMemberCount)
				{
					characterID = 0;
				}
				PlayerStatusDataManager.characterMp[characterID] = Mathf.Clamp(PlayerStatusDataManager.characterMp[characterID] -= useMP, 0, PlayerStatusDataManager.characterMaxMp[characterID]);
				utageBattleSceneManager.playerFrameGoList[memberNum].GetComponent<ArborFSM>().SendTrigger("ChangeCharacterFrameSlider");
				Debug.Log("スキルでMP消費");
			}
			scenarioBattleTurnManager.useSkillData = battleSkillData;
			Debug.Log("味方のスキル／ID：" + battleSkillData.skillID);
		}
		else
		{
			battleSkillData = scenarioBattleTurnManager.enemySkillData;
			scenarioBattleTurnManager.useSkillData = battleSkillData;
			Debug.Log("敵のスキル／ID：" + battleSkillData.skillID);
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
