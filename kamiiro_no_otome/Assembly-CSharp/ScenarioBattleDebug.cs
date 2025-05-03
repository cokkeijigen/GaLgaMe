using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ScenarioBattleDebug : StateBehaviour
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
		PlayerInventoryDataManager.haveItemList.Clear();
		Debug.Log("所持アイテムリストをクリア");
		PlayerEquipDataManager.playerEquipSkillList[5].Clear();
		Debug.Log("装備武器のスキルをクリア");
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
