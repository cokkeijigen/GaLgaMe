using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class DisposeFactorDataFromList : StateBehaviour
{
	private CraftManager craftManager;

	private FactorDisposalManager factorDisposalManager;

	public StateLink nextState;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		factorDisposalManager = GameObject.Find("Factor Disposal Manager").GetComponent<FactorDisposalManager>();
	}

	public override void OnStateBegin()
	{
		factorDisposalManager.disposalFrameGo.GetComponent<FactorDisposalButtonClickAction>();
		switch (craftManager.selectCategoryNum)
		{
		case 0:
			switch (PlayerNonSaveDataManager.selectCraftCanvasName)
			{
			case "craft":
				PlayerInventoryFactorManager.PlayerHaveWeaponFactorRemove(craftManager.clickedItemID, craftManager.clickedUniqueID, factorDisposalManager.tempSelectUniqueID);
				break;
			case "merge":
				PlayerInventoryFactorManager.PlayerHavePartyWeaponFactorRemove(craftManager.clickedItemID, factorDisposalManager.tempSelectUniqueID);
				break;
			}
			break;
		case 1:
			switch (PlayerNonSaveDataManager.selectCraftCanvasName)
			{
			case "craft":
				PlayerInventoryFactorManager.PlayerHaveArmorFactorRemove(craftManager.clickedItemID, craftManager.clickedUniqueID, factorDisposalManager.tempSelectUniqueID);
				break;
			case "merge":
				PlayerInventoryFactorManager.PlayerHavePartyArmorFactorRemove(craftManager.clickedItemID, factorDisposalManager.tempSelectUniqueID);
				break;
			}
			break;
		}
		Transition(nextState);
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
