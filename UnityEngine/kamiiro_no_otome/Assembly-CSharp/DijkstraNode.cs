using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("")]
public class DijkstraNode : MonoBehaviour
{
	public enum NodeStatus
	{
		NotYet,
		Completed
	}

	public List<GameObject> adjacentNodeList = new List<GameObject>();

	public float toGoalNodeCost;

	public GameObject toGoalNodeGo;

	public NodeStatus nodeStatus;
}
