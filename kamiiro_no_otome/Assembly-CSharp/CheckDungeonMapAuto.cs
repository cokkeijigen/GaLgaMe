using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckDungeonMapAuto : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	public StateLink autoLink;

	public StateLink normalLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
	}

	public override void OnStateBegin()
	{
		if (PlayerDataManager.isDungeonMapAuto)
		{
			dungeonMapManager.routeButtonGroup.SetActive(value: false);
			dungeonMapManager.autoAlertGroup.SetActive(value: true);
			Transition(autoLink);
			return;
		}
		CanvasGroup[] mapCanvasGroupArray = dungeonMapManager.mapCanvasGroupArray;
		foreach (CanvasGroup obj in mapCanvasGroupArray)
		{
			obj.interactable = true;
			obj.blocksRaycasts = true;
			obj.alpha = 1f;
		}
		dungeonMapManager.miniCardGroupCanvasGroup.interactable = true;
		dungeonMapManager.miniCardGroupCanvasGroup.blocksRaycasts = true;
		dungeonMapManager.miniCardGroupCanvasGroup.alpha = 1f;
		dungeonMapManager.routeButtonGroup.SetActive(value: true);
		dungeonMapManager.autoAlertGroup.SetActive(value: false);
		Transition(normalLink);
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
