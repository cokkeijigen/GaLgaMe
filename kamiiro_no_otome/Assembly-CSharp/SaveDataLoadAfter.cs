using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SaveDataLoadAfter : StateBehaviour
{
	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		switch (PlayerDataManager.mapPlaceStatusNum)
		{
		case 0:
		case 1:
		case 2:
			PlayerNonSaveDataManager.loadSceneName = "main";
			if (GameObject.Find("Map Menu Canvas") != null)
			{
				GameObject.Find("Map Menu Canvas").gameObject.SetActive(value: false);
			}
			break;
		case 3:
			PlayerNonSaveDataManager.loadSceneName = "scenario";
			break;
		case 4:
			PlayerNonSaveDataManager.loadSceneName = "scenario";
			break;
		}
		PlayerNonSaveDataManager.isAddTimeFromScenario = false;
		PlayerNonSaveDataManager.isAddTimeFromDungeon = false;
		PlayerNonSaveDataManager.isSaveDataLoad = true;
		PlayerNonSaveDataManager.isInitializeMapData = true;
		PlayerNonSaveDataManager.isScenarioBattle = false;
		PlayerNonSaveDataManager.isBattleMenuRightClickDisable = false;
		PlayerNonSaveDataManager.isUtagePlayBattleBgm = false;
		PlayerNonSaveDataManager.isUtagePlayBattleBgmNonStop = false;
		for (int i = 0; i < PlayerStatusDataManager.partyMemberCount; i++)
		{
			int[] playerHaveWeaponItemID = PlayerInventoryDataEquipAccess.GetPlayerHaveWeaponItemID(i);
			PlayerEquipDataManager.SetEquipPlayerWeapon(playerHaveWeaponItemID[0], playerHaveWeaponItemID[1], i);
		}
		for (int j = 0; j < PlayerStatusDataManager.partyMemberCount; j++)
		{
			int[] playerHaveArmorItemID = PlayerInventoryDataEquipAccess.GetPlayerHaveArmorItemID(j);
			PlayerEquipDataManager.SetEquipPlayerArmor(playerHaveArmorItemID[0], playerHaveArmorItemID[1], j);
		}
		for (int k = 0; k < PlayerStatusDataManager.partyMemberCount; k++)
		{
			int playerHaveAccessoryID = PlayerInventoryDataEquipAccess.GetPlayerHaveAccessoryID(k);
			if (playerHaveAccessoryID != 0)
			{
				PlayerEquipDataManager.SetEquipPlayerAccessory(playerHaveAccessoryID, k);
			}
		}
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			PlayerStatusDataManager.playerPartyMember = new int[2]
			{
				0,
				PlayerDataManager.DungeonHeroineFollowNum
			};
		}
		PlayerNonSaveDataManager.masterAudioFadeTime = 0.4f;
		GameObject.Find("Bgm Play Manager").GetComponent<PlayMakerFSM>().SendEvent("FadeMasterAudioPlaylist");
		Debug.Log("BGMをフェードして停止");
		PlayerNonSaveDataManager.gameStartTime = Time.time;
		ClearPlayerInventoryFactorData();
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

	private void ClearPlayerInventoryFactorData()
	{
		PlayerEquipDataManager.CalcEquipPlayerHaveWeaponFactor(CallBackWeaponMethod, isAllCalc: true);
	}

	private void CallBackWeaponMethod()
	{
		PlayerStatusDataManager.SetUpPlayerStatus(isSetUp: true, CallBackStatusMethod);
	}

	private void CallBackStatusMethod()
	{
		for (int i = 0; i < 5; i++)
		{
			PlayerStatusDataManager.characterHp[i] = PlayerDataManager.playerSaveDataHp[i];
		}
		Debug.Log("Equipデータの更新完了");
		Transition(stateLink);
	}
}
