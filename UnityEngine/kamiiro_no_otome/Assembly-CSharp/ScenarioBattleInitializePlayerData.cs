using System.Collections.Generic;
using Arbor;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class ScenarioBattleInitializePlayerData : StateBehaviour
{
	private UtageBattleSceneManager utageBattleSceneManager;

	private ScenarioBattleTurnManager scenarioBattleTurnManager;

	private ScenarioBattleSkillManager scenarioBattleSkillManager;

	private ScenarioBattleData scenarioBattleData;

	public GameObject managerGo;

	public GameObject characterFrameGo;

	public GameObject supportFrameGo;

	private int weaponIndex;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		utageBattleSceneManager = managerGo.GetComponent<UtageBattleSceneManager>();
		scenarioBattleTurnManager = GameObject.Find("Battle Turn Manager").GetComponent<ScenarioBattleTurnManager>();
		scenarioBattleSkillManager = GameObject.Find("Battle Skill Manager").GetComponent<ScenarioBattleSkillManager>();
	}

	public override void OnStateBegin()
	{
		utageBattleSceneManager.isCharacterButtonSetUp.Clear();
		utageBattleSceneManager.playerFrameGoList.Clear();
		utageBattleSceneManager.enemyButtonGoList.Clear();
		scenarioBattleTurnManager.useSkillEnemyMemberNumList.Clear();
		utageBattleSceneManager.enemyImageGoList.Clear();
		scenarioBattleTurnManager.playerAttackCount = 0;
		scenarioBattleTurnManager.enemyAttackCount = 0;
		scenarioBattleTurnManager.battleUseSkillID = 0;
		scenarioBattleTurnManager.isUseSkillPlayer = false;
		scenarioBattleTurnManager.enemyNormalAttackDamageList = new List<int> { 0, 0, 0, 0, 0 };
		scenarioBattleTurnManager.enemySkillAttackDamageList = new List<int> { 0, 0, 0, 0, 0 };
		for (int i = 0; i < scenarioBattleTurnManager.isUseSkillCheckArray.Length; i++)
		{
			scenarioBattleTurnManager.isUseSkillCheckArray[i] = false;
		}
		scenarioBattleTurnManager.playerFocusTargetNum = 9;
		scenarioBattleTurnManager.elapsedTurnCount = 0;
		this.scenarioBattleData = GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData data) => data.scenarioName == PlayerNonSaveDataManager.resultScenarioName);
		if (this.scenarioBattleData.isSupportCharacterTakeDamage)
		{
			PlayerStatusDataManager.playerPartyMember = new List<int>(this.scenarioBattleData.battleCharacterID) { this.scenarioBattleData.supportBattleCharacterID }.ToArray();
		}
		else
		{
			PlayerStatusDataManager.playerPartyMember = this.scenarioBattleData.battleCharacterID.ToArray();
		}
		for (int j = 0; j < PlayerStatusDataManager.characterMp.Length; j++)
		{
			PlayerStatusDataManager.characterMp[j] = PlayerStatusDataManager.characterMaxMp[j];
		}
		for (int k = 0; k < PlayerStatusDataManager.characterSp.Length; k++)
		{
			PlayerStatusDataManager.characterSp[k] = 0;
		}
		for (int l = 0; l < this.scenarioBattleData.battleCharacterID.Count; l++)
		{
			utageBattleSceneManager.isCharacterButtonSetUp.Add(item: false);
			Transform transform = PoolManager.Pools["BattleObject"].Spawn(characterFrameGo, utageBattleSceneManager.playerFrameParent.transform);
			ParameterContainer component = transform.GetComponent<ParameterContainer>();
			component.SetInt("characterID", this.scenarioBattleData.battleCharacterID[l]);
			component.SetInt("partyMemberNum", l);
			component.SetBool("isSelectEffect", value: false);
			component.GetGameObject("skillButton").GetComponent<Image>().sprite = scenarioBattleSkillManager.skillButtonSpriteArray[0];
			component.GetVariable<UguiImage>("skillIconImage").image.sprite = scenarioBattleSkillManager.skillButtonIconSpriteArray[0];
			component.GetVariable<UguiImage>("skillIconImage").image.color = new Color(1f, 1f, 1f, 1f);
			component.GetVariable<I2LocalizeComponent>("chargeTextLoc").localize.Term = "skillButton_enable";
			transform.localScale = new Vector3(1f, 1f, 1f);
			utageBattleSceneManager.playerFrameGoList.Add(transform.gameObject);
		}
		PlayerStatusDataManager.CalkPlayerChargeStatus();
		utageBattleSceneManager.supportAttackMemberId = this.scenarioBattleData.supportBattleCharacterID;
		if (this.scenarioBattleData.supportBattleCharacterID != int.MaxValue)
		{
			Transform transform2 = PoolManager.Pools["BattleObject"].Spawn(supportFrameGo, utageBattleSceneManager.playerFrameParent.transform);
			ParameterContainer component2 = transform2.GetComponent<ParameterContainer>();
			component2.SetInt("characterID", this.scenarioBattleData.supportBattleCharacterID);
			component2.SetBool("isSelectEffect", value: false);
			component2.GetGameObject("skillButton").GetComponent<Image>().sprite = scenarioBattleSkillManager.skillButtonSpriteArray[0];
			component2.GetVariable<UguiImage>("skillIconImage").image.sprite = scenarioBattleSkillManager.skillButtonIconSpriteArray[0];
			component2.GetVariable<UguiImage>("skillIconImage").image.color = new Color(1f, 1f, 1f, 1f);
			component2.GetVariable<I2LocalizeComponent>("chargeTextLoc").localize.Term = "skillButton_enable";
			transform2.localScale = new Vector3(1f, 1f, 1f);
			utageBattleSceneManager.playerFrameGoList.Add(transform2.gameObject);
		}
		if (!utageBattleSceneManager.isStatusBackUp)
		{
			for (int m = 0; m < PlayerStatusDataManager.partyMemberCount; m++)
			{
				PlayerNonSaveDataManager.beforePlayerHp[m] = PlayerStatusDataManager.characterHp[m];
				PlayerNonSaveDataManager.beforePlayerMp[m] = PlayerStatusDataManager.characterMp[m];
			}
			weaponIndex = PlayerInventoryDataManager.haveWeaponList.FindIndex((HaveWeaponData data) => data.equipCharacter == 0);
			PlayerNonSaveDataManager.beforeEquipWeaponTp = PlayerInventoryDataManager.haveWeaponList[weaponIndex].weaponIncludeMp;
			PlayerNonSaveDataManager.beforeHaveItemSortIdList.Clear();
			PlayerNonSaveDataManager.beforeHaveItemIdList.Clear();
			PlayerNonSaveDataManager.beforeHaveItemHaveNumList.Clear();
			for (int n = 0; n < PlayerInventoryDataManager.haveItemList.Count; n++)
			{
				PlayerNonSaveDataManager.beforeHaveItemSortIdList.Add(PlayerInventoryDataManager.haveItemList[n].itemSortID);
				PlayerNonSaveDataManager.beforeHaveItemIdList.Add(PlayerInventoryDataManager.haveItemList[n].itemID);
				PlayerNonSaveDataManager.beforeHaveItemHaveNumList.Add(PlayerInventoryDataManager.haveItemList[n].haveCountNum);
			}
			utageBattleSceneManager.isStatusBackUp = true;
			Debug.Log("再戦用のステータスをバックアップ完了");
		}
		PlayerInventoryDataManager.haveWeaponList[weaponIndex].weaponIncludeMp = PlayerNonSaveDataManager.beforeEquipWeaponTp;
		PlayerInventoryDataManager.haveItemList.Clear();
		for (int num = 0; num < PlayerNonSaveDataManager.beforeHaveItemSortIdList.Count; num++)
		{
			HaveItemData item = new HaveItemData
			{
				itemSortID = PlayerNonSaveDataManager.beforeHaveItemSortIdList[num],
				itemID = PlayerNonSaveDataManager.beforeHaveItemIdList[num],
				haveCountNum = PlayerNonSaveDataManager.beforeHaveItemHaveNumList[num]
			};
			PlayerInventoryDataManager.haveItemList.Add(item);
		}
		PlayerInventoryDataAccess.HaveItemListSort();
		scenarioBattleTurnManager.battleUseItemCount = 0;
		scenarioBattleTurnManager.battleEnableUseItemMaxNum = Mathf.Clamp(PlayerStatusDataManager.playerPartyMember.Length, 0, 2);
		scenarioBattleTurnManager.itemEnableUseCountText.text = scenarioBattleTurnManager.battleEnableUseItemMaxNum.ToString();
		utageBattleSceneManager.itemButton.GetComponent<ArborFSM>().SendTrigger("CheckItemUseCount");
		ScenarioBattleData scenarioBattleData = null;
		if (PlayerNonSaveDataManager.resultScenarioName == "MH_Rina_004-1" && PlayerFlagDataManager.scenarioFlagDictionary["MH_Shia_012"])
		{
			scenarioBattleData = GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData data) => data.scenarioName == PlayerNonSaveDataManager.resultScenarioName + "A");
			Debug.Log("コマンド戦闘／最初の対リィナ戦で、シア編終了済み");
		}
		else
		{
			scenarioBattleData = GameDataManager.instance.scenarioBattleDataBase.scenarioBattleDataList.Find((ScenarioBattleData data) => data.scenarioName == PlayerNonSaveDataManager.resultScenarioName);
			Debug.Log("コマンド戦闘／通常のシナリオコマンド戦闘");
		}
		PlayerStatusDataManager.enemyMember = scenarioBattleData.battleEnemyID.ToArray();
		for (int num2 = 0; num2 < PlayerStatusDataManager.enemyChargeTurnList.Count; num2++)
		{
			PlayerStatusDataManager.enemyChargeTurnList[num2] = 0;
		}
		PlayerStatusDataManager.SetUpEnemyStatus(CallBackMethodEnemy);
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

	private void CallBackMethodEnemy()
	{
		Debug.Log("コールバックで呼ばれた");
		PlayerBattleConditionAccess.BattleConditionInititialize();
		utageBattleSceneManager.SetUpEnemyImage();
		utageBattleSceneManager.SetUpEnemyGroup();
		Transition(stateLink);
	}
}
