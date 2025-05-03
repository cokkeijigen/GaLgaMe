using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class DebugStatusADDED : StateBehaviour
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
		PlayerDataManager.playerHaveMoney += 1000000;
		PlayerInventoryDataAccess.PlayerHaveItemAdd(200, 200, 32);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(201, 201, 31);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(202, 202, 31);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(300, 300, 32);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(301, 301, 33);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(302, 302, 33);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(500, 500, 50);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(680, 680, 34);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(681, 681, 35);
		PlayerStatusDataManager.characterLv[1] = 1;
		PlayerStatusDataManager.characterLv[2] = 1;
		PlayerStatusDataManager.characterLv[3] = 1;
		PlayerStatusDataManager.characterLv[4] = 1;
		PlayerInventoryDataAccess.PlayerHaveItemAdd(701, 701, 1);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(703, 703, 12);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(705, 705, 13);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(708, 708, 14);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(712, 712, 14);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(713, 713, 14);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(720, 720, 21);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(721, 721, 22);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(722, 722, 23);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(800, 800, 15);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(803, 803, 16);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(804, 804, 17);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(808, 808, 18);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(812, 812, 18);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(813, 813, 18);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(820, 820, 21);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(821, 821, 22);
		PlayerInventoryDataAccess.PlayerHaveItemAdd(822, 822, 23);
		int instanceID = PlayerInventoryDataEquipAccess.PlayerHaveWeaponAdd_INSTANCE(1000, isEquip: false);
		int instanceID2 = PlayerInventoryDataEquipAccess.PlayerHaveWeaponAdd_INSTANCE(1010, isEquip: false);
		PlayerInventoryDataAccess.HaveItemListSortAll();
		PlayerInventoryFactorManager.SetPlayerHaveWeaponFactor(1000, 0, 0, 1);
		PlayerInventoryFactorManager.SetPlayerHaveWeaponFactor(1000, 0, 10, 2);
		PlayerInventoryFactorManager.SetPlayerHaveWeaponFactor(1000, instanceID, 20, 1);
		PlayerInventoryFactorManager.SetPlayerHaveWeaponFactor(1000, instanceID, 30, 2);
		PlayerInventoryFactorManager.SetPlayerHaveWeaponFactor(1000, instanceID, 40, 1);
		PlayerInventoryFactorManager.SetPlayerHaveWeaponFactor(1000, instanceID, 50, 2);
		PlayerInventoryFactorManager.SetPlayerHaveWeaponFactor(1010, 0, 0, 1);
		PlayerInventoryFactorManager.SetPlayerHaveWeaponFactor(1010, 0, 0, 1);
		PlayerInventoryFactorManager.SetPlayerHaveWeaponFactor(1010, 0, 0, 2);
		PlayerInventoryFactorManager.SetPlayerHaveWeaponFactor(1010, 0, 30, 2);
		PlayerInventoryFactorManager.SetPlayerHaveWeaponFactor(1010, 0, 20, 1);
		PlayerInventoryFactorManager.SetPlayerHaveWeaponFactor(1010, instanceID2, 40, 3);
		PlayerInventoryFactorManager.SetPlayerHaveWeaponFactor(1010, instanceID2, 50, 3);
		PlayerInventoryFactorManager.SetPlayerHaveWeaponFactor(1010, instanceID2, 60, 3);
		PlayerInventoryFactorManager.SetPlayerHaveWeaponFactor(1010, instanceID2, 70, 3);
		int instanceID3 = PlayerInventoryDataEquipAccess.PlayerHaveArmorAdd_INSTANCE(2000, isEquip: false);
		PlayerInventoryFactorManager.SetPlayerHaveArmorFactor(2000, 0, 200, 1);
		PlayerInventoryFactorManager.SetPlayerHaveArmorFactor(2000, 0, 210, 2);
		PlayerInventoryFactorManager.SetPlayerHaveArmorFactor(2000, instanceID3, 220, 1);
		PlayerInventoryFactorManager.SetPlayerHaveArmorFactor(2000, instanceID3, 230, 2);
		PlayerInventoryFactorManager.SetPlayerHaveArmorFactor(2000, instanceID3, 240, 1);
		PlayerInventoryFactorManager.SetPlayerHaveArmorFactor(2010, 0, 200, 1);
		PlayerInventoryFactorManager.SetPlayerHaveArmorFactor(2010, 0, 210, 1);
		PlayerInventoryFactorManager.SetPlayerHaveArmorFactor(2010, 0, 230, 1);
		PlayerInventoryFactorManager.SetPlayerHaveArmorFactor(2010, 0, 240, 1);
		PlayerInventoryFactorManager.SetPlayerHaveArmorFactor(2010, 0, 250, 2);
		PlayerInventoryFactorManager.SetPlayerHavePartyWeaponFactor(1310, 0, 1);
		PlayerInventoryFactorManager.SetPlayerHavePartyWeaponFactor(1310, 10, 2);
		PlayerInventoryFactorManager.SetPlayerHavePartyWeaponFactor(1310, 20, 1);
		PlayerInventoryFactorManager.SetPlayerHavePartyWeaponFactor(1310, 40, 2);
		PlayerInventoryFactorManager.SetPlayerHavePartyWeaponFactor(1310, 50, 1);
		PlayerInventoryFactorManager.SetPlayerHavePartyWeaponFactor(1310, 70, 2);
		PlayerInventoryFactorManager.SetPlayerHavePartyWeaponFactor(1400, 20, 1);
		PlayerInventoryFactorManager.SetPlayerHavePartyWeaponFactor(1400, 30, 2);
		PlayerInventoryFactorManager.SetPlayerHavePartyArmorFactor(2300, 230, 1);
		PlayerInventoryFactorManager.SetPlayerHavePartyArmorFactor(2300, 240, 1);
		PlayerInventoryFactorManager.SetPlayerHavePartyArmorFactor(2400, 250, 2);
		PlayerInventoryFactorManager.SetPlayerHavePartyArmorFactor(2400, 260, 2);
		PlayerInventoryFactorManager.SetPlayerHavePartyArmorFactor(2510, 270, 1);
		PlayerInventoryFactorManager.SetPlayerHavePartyArmorFactor(2510, 500, 1);
		PlayerInventoryFactorManager.SetPlayerHavePartyArmorFactor(2510, 210, 1);
		PlayerInventoryFactorManager.SetPlayerHavePartyArmorFactor(2610, 210, 1);
		PlayerInventoryFactorManager.SetPlayerHavePartyArmorFactor(2610, 220, 2);
		PlayerInventoryFactorManager.SetPlayerHavePartyArmorFactor(2610, 250, 1);
		PlayerInventoryFactorManager.SetPlayerHavePartyArmorFactor(2610, 600, 2);
		PlayerInventoryFactorManager.SetPlayerHavePartyArmorFactor(2610, 270, 1);
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
