using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class DebugItemSortSend : StateBehaviour
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
		PlayerInventoryDataAccess.HaveItemListSortAll();
		PlayerInventoryDataManager.haveWeaponList.Find((HaveWeaponData data) => data.equipCharacter == 0).weaponIncludeMp = 23;
		PlayerEquipDataManager.CalcEquipPlayerHaveWeaponFactor(CallBackWeaponMethod, isAllCalc: true);
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

	private void CallBackWeaponMethod()
	{
		PlayerStatusDataManager.SetUpPlayerStatus(isSetUp: true, CallBackStatusMethod);
	}

	private void CallBackStatusMethod()
	{
		Debug.Log("Equipデータの更新完了");
		Transition(stateLink);
	}
}
