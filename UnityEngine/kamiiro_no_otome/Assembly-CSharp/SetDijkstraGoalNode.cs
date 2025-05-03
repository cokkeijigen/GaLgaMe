using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetDijkstraGoalNode : StateBehaviour
{
	private DijkstraManager dijkstraManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dijkstraManager = GameObject.Find("Dijkstra Manager").GetComponent<DijkstraManager>();
	}

	public override void OnStateBegin()
	{
		dijkstraManager.SetGoalNode(PlayerNonSaveDataManager.selectAccessPointGO);
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
