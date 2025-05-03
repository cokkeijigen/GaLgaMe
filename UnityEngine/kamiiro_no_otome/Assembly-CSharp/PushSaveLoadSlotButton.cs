using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class PushSaveLoadSlotButton : StateBehaviour
{
	public int slotNum;

	private ArborFSM arborFSM;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		arborFSM = GameObject.Find("SaveLoad Manager").GetComponent<ArborFSM>();
	}

	public override void OnStateBegin()
	{
		PlayerNonSaveDataManager.selectSlotNum = slotNum;
		arborFSM.SendTrigger("PushSaveLoadSlot");
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
