using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckDungeonCardMouseExit : StateBehaviour
{
	public float invokeTime;

	public StateLink enterLink;

	public StateLink exitLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>().isCardMouseOver = false;
		Invoke("CheckMouseOver", invokeTime);
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

	private void CheckMouseOver()
	{
		if (GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>().isCardMouseOver)
		{
			Transition(enterLink);
		}
		else
		{
			Transition(exitLink);
		}
	}
}
