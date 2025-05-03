using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

[AddComponentMenu("")]
public class DijkstraManager : SerializedMonoBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private WorldMapAccessManager worldMapAccessManager;

	private DijkstraNode startNode;

	private DijkstraNode goalNode;

	public List<GameObject> nodeGoList = new List<GameObject>();

	public List<GameObject> shortestRouteList = new List<GameObject>();

	public List<Sprite> characterAngleImageList = new List<Sprite>();

	private DijkstraNode calcCurrentNode;

	public List<bool> completedNodeCountList = new List<bool>();

	public void Awake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		worldMapAccessManager = GameObject.Find("WorldMap Access Manager").GetComponent<WorldMapAccessManager>();
		nodeGoList.Clear();
		int childCount = totalMapAccessManager.worldAreaParentGo.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			GameObject item = totalMapAccessManager.worldAreaParentGo.transform.GetChild(i).gameObject;
			nodeGoList.Add(item);
		}
		int childCount2 = totalMapAccessManager.destinationNodeGroupGo.transform.childCount;
		for (int j = 0; j < childCount2; j++)
		{
			GameObject item2 = totalMapAccessManager.destinationNodeGroupGo.transform.GetChild(j).gameObject;
			nodeGoList.Add(item2);
		}
	}

	public void SetGoalNode(GameObject nodeGo)
	{
		Transform transform = totalMapAccessManager.worldAreaParentGo.transform.Find(PlayerDataManager.currentAccessPointName);
		if (transform != null)
		{
			startNode = transform.GetComponent<DijkstraNode>();
			Debug.Log("スタートノードを代入：" + transform.name);
		}
		if (nodeGo != null)
		{
			goalNode = nodeGo.GetComponent<DijkstraNode>();
			Debug.Log("ゴールノードを代入：" + nodeGo.name);
		}
		shortestRouteList.Clear();
		completedNodeCountList.Clear();
		CalcMapRoute();
	}

	private void CalcMapRoute()
	{
		for (int i = 0; i < nodeGoList.Count; i++)
		{
			DijkstraNode component = nodeGoList[i].GetComponent<DijkstraNode>();
			component.nodeStatus = DijkstraNode.NodeStatus.NotYet;
			component.toGoalNodeCost = float.MaxValue;
			component.toGoalNodeGo = null;
		}
		startNode.nodeStatus = DijkstraNode.NodeStatus.Completed;
		completedNodeCountList.Add(item: true);
		startNode.toGoalNodeCost = 0f;
		calcCurrentNode = startNode;
		Debug.Log("初期化完了");
		int num = 0;
		while (completedNodeCountList.Count != nodeGoList.Count)
		{
			CalcToCurrentCost(calcCurrentNode);
			SetCompleteMinNode();
			num++;
			if (num > 100)
			{
				Debug.Log("ループなので終了");
				break;
			}
		}
		SetShortestRouteList();
	}

	private void CalcToCurrentCost(DijkstraNode currentNode)
	{
		new List<DijkstraNode>();
		for (int i = 0; i < currentNode.adjacentNodeList.Count; i++)
		{
			if (currentNode.adjacentNodeList == null)
			{
				continue;
			}
			DijkstraNode component = currentNode.adjacentNodeList[i].GetComponent<DijkstraNode>();
			if (component.nodeStatus != DijkstraNode.NodeStatus.Completed)
			{
				float num = worldMapAccessManager.GetNeedAccessDay(currentNode.gameObject.name, currentNode.adjacentNodeList[i].gameObject.name);
				float num2 = worldMapAccessManager.GetNeedAccessTime(currentNode.gameObject.name, currentNode.adjacentNodeList[i].gameObject.name);
				if (num2 >= 4f)
				{
					int num3 = Mathf.FloorToInt(num2 / 4f);
					num += (float)num3;
					num2 -= (float)(num3 * 4);
				}
				num2 /= 10f;
				float num4 = num + num2 + currentNode.toGoalNodeCost;
				int num5 = (int)num4;
				float num6 = num4 % 1f * 10f;
				if (num6 >= 4f)
				{
					int num7 = Mathf.FloorToInt(num6 / 4f);
					num5 += num7;
					num6 -= (float)(num7 * 4);
					num4 = (float)num5 + num6 / 10f;
				}
				Debug.Log("カレントノード：" + currentNode.gameObject.name + "／辿ったノード名：" + currentNode.adjacentNodeList[i].gameObject.name + "／合計コスト：" + num4);
				if (component.toGoalNodeCost > num4)
				{
					component.toGoalNodeCost = num4;
					component.toGoalNodeGo = currentNode.gameObject;
					Debug.Log("辿ったノード名：" + component.gameObject.name + "／現在地からの合計距離を代入：" + num4);
				}
			}
		}
	}

	private void SetCompleteMinNode()
	{
		List<DijkstraNode> list = new List<DijkstraNode>();
		for (int i = 0; i < nodeGoList.Count; i++)
		{
			DijkstraNode component = nodeGoList[i].GetComponent<DijkstraNode>();
			if (component.nodeStatus != DijkstraNode.NodeStatus.Completed)
			{
				list.Add(component);
			}
		}
		List<DijkstraNode> list2 = list.OrderBy((DijkstraNode v) => v.toGoalNodeCost).ToList();
		list2[0].nodeStatus = DijkstraNode.NodeStatus.Completed;
		completedNodeCountList.Add(item: true);
		Debug.Log("全ノードの中から、一番コストが低いノードを確定済みにする：" + list2[0].gameObject.name);
		calcCurrentNode = list2[0];
	}

	private void SetShortestRouteList()
	{
		shortestRouteList.Add(goalNode.gameObject);
		GameObject toGoalNodeGo = goalNode.toGoalNodeGo;
		int num = 0;
		while (toGoalNodeGo != null)
		{
			shortestRouteList.Add(toGoalNodeGo);
			toGoalNodeGo = toGoalNodeGo.GetComponent<DijkstraNode>().toGoalNodeGo;
			num++;
			if (num > 100)
			{
				Debug.Log("ループなので終了");
				break;
			}
		}
		shortestRouteList.Reverse();
		Debug.Log("ゴールからルートを逆算する");
	}
}
