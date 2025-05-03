using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetAgilityQueue : StateBehaviour
{
	private DungeonItemManager dungeonItemManager;

	private ParameterContainer parameterContainer;

	public string queueType;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonItemManager = GameObject.Find("Dungeon Item Manager").GetComponent<DungeonItemManager>();
		parameterContainer = GameObject.Find("Dungeon Agility Manager").GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		string text = queueType;
		if (!(text == "item"))
		{
			if (text == "retreat")
			{
				parameterContainer.GetStringList("AgilityQueueList").Add("retreat");
			}
		}
		else
		{
			parameterContainer.GetStringList("AgilityQueueList").Add("item");
			dungeonItemManager.useItemWindowGo.SetActive(value: false);
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
