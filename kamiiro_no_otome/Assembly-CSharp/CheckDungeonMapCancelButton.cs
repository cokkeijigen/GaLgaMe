using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckDungeonMapCancelButton : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonGetItemManager dungeonGetItemManager;

	private GameObject dialogCanvas;

	private PlayMakerFSM dialogManagerFSM;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GetComponentInParent<DungeonMapManager>();
		dungeonGetItemManager = GameObject.Find("Dungeon Get Item Manager").GetComponent<DungeonGetItemManager>();
		dialogCanvas = GameObject.Find("DontDestroy Group").transform.Find("Dialog Canvas").gameObject;
		dialogManagerFSM = GameObject.Find("Dialog Manager").GetComponent<PlayMakerFSM>();
	}

	public override void OnStateBegin()
	{
		if (!dungeonMapManager.dungeonBattleCanvas.activeInHierarchy)
		{
			if (dungeonGetItemManager.mapOverlayCanvas.activeInHierarchy)
			{
				Debug.Log("獲得アイテムを閉じる");
				dungeonGetItemManager.GetComponent<ArborFSM>().SendTrigger("CloseGetItemWindow");
			}
			else if (dialogCanvas.activeInHierarchy)
			{
				Debug.Log("ダイアログを閉じる");
				dialogManagerFSM.SendEvent("OpenDialog");
			}
			Transition(stateLink);
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
}
