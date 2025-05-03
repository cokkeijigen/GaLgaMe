using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckDungeonFloorClear : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	public float waitTIme;

	public StateLink clearLink;

	public StateLink notClearLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GetComponent<DungeonMapManager>();
	}

	public override void OnStateBegin()
	{
		dungeonMapManager.isMimicBattle = false;
		dungeonMapManager.thisFloorActionNum++;
		if (dungeonMapManager.thisFloorActionNum >= 3)
		{
			Invoke("InvokeClearMethod", waitTIme);
		}
		else
		{
			Invoke("InvokeNotClearMethod", waitTIme);
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

	private void InvokeClearMethod()
	{
		Transition(clearLink);
	}

	private void InvokeNotClearMethod()
	{
		Transition(notClearLink);
	}
}
