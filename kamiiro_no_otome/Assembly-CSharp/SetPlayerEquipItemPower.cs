using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetPlayerEquipItemPower : StateBehaviour
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
		PlayerEquipDataManager.CalcEquipPlayerHaveWeaponFactor(CallBackWeaponMethod, isAllCalc: true);
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
		if (PlayerNonSaveDataManager.isScenarioBattle)
		{
			PlayerStatusDataManager.SetUpPlayerStatus(isSetUp: true, CallBackStatusMethod);
		}
		else if (PlayerNonSaveDataManager.isDungeonScnearioBattle || PlayerDataManager.isSelectDungeon)
		{
			PlayerStatusDataManager.SetUpPlayerStatus(isSetUp: false, CallBackStatusMethod);
		}
		else
		{
			PlayerStatusDataManager.SetUpPlayerStatus(isSetUp: false, CallBackStatusMethod);
		}
	}

	private void CallBackStatusMethod()
	{
		Debug.Log("Equipデータの更新完了");
		Transition(stateLink);
	}
}
