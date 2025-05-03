using Arbor;
using HutongGames.PlayMaker;
using UnityEngine;

[AddComponentMenu("")]
public class CulcDungeonRouteBonus : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonMapStatusManager dungeonMapStatusManager;

	public PlayMakerFSM bonusFSM;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GetComponent<DungeonMapManager>();
		dungeonMapStatusManager = GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapStatusManager>();
	}

	public override void OnStateBegin()
	{
		CanvasGroup[] mapCanvasGroupArray = dungeonMapManager.mapCanvasGroupArray;
		foreach (CanvasGroup obj in mapCanvasGroupArray)
		{
			obj.interactable = false;
			obj.blocksRaycasts = false;
			obj.alpha = 0.5f;
		}
		dungeonMapManager.miniCardGroupCanvasGroup.interactable = false;
		dungeonMapManager.miniCardGroupCanvasGroup.blocksRaycasts = false;
		dungeonMapManager.miniCardGroupCanvasGroup.alpha = 0.5f;
		dungeonMapStatusManager.skipInfoWindowGo.SetActive(value: false);
		if (!dungeonMapManager.isBossRouteSelect)
		{
			string[] array = new string[3];
			for (int j = 0; j < 3; j++)
			{
				array[j] = dungeonMapManager.selectCardList[j].multiType.ToString();
			}
			FsmArray fsmArray = bonusFSM.FsmVariables.GetFsmArray("actionTypeArray");
			object[] values = array;
			fsmArray.Values = values;
			bonusFSM.SendEvent("CheckDungeonBonus");
		}
		else
		{
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
