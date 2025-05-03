using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class ChatPrefabsDespawn : StateBehaviour
{
	public GameObject poolParent;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("ChatText");
		foreach (GameObject gameObject in array)
		{
			gameObject.GetComponent<CanvasGroup>().alpha = 0f;
			gameObject.transform.SetParent(poolParent.transform);
			PoolManager.Pools["ChatPool"].Despawn(gameObject.transform);
		}
		Debug.Log("チャットPrefabを全て削除");
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
