using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetDungeonGetItemWindowlHilight : StateBehaviour
{
	private DungeonItemInfoManager dungeonItemInfoManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonItemInfoManager = GameObject.Find("Dungeon Item Info Manager").GetComponent<DungeonItemInfoManager>();
	}

	public override void OnStateBegin()
	{
		if (PlayerNonSaveDataManager.isDungeonGetItemHighlight)
		{
			dungeonItemInfoManager.dungeonInfoWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(1260f, 630f);
			dungeonItemInfoManager.dungeonExInfoFrame.sizeDelta = new Vector2(1220f, 120f);
			dungeonItemInfoManager.dungeonExInfoHighlightTextGo.SetActive(value: true);
		}
		else
		{
			dungeonItemInfoManager.dungeonInfoWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(1260f, 610f);
			dungeonItemInfoManager.dungeonExInfoFrame.sizeDelta = new Vector2(1220f, 100f);
			dungeonItemInfoManager.dungeonExInfoHighlightTextGo.SetActive(value: false);
		}
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
