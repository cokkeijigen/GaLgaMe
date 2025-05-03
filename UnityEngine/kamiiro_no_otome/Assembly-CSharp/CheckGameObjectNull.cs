using System.Collections;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckGameObjectNull : StateBehaviour
{
	public string findGameObjectName;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		StartCoroutine("CheckGameObjectNullCoroutine");
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

	private IEnumerator CheckGameObjectNullCoroutine()
	{
		while (!(GameObject.Find(findGameObjectName) != null))
		{
			Debug.Log(findGameObjectName + "は存在しない");
			yield return new WaitForSeconds(0.1f);
		}
		Debug.Log(findGameObjectName + "は存在する");
		Transition(stateLink);
	}
}
