using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("")]
public class CheckSceneContains : StateBehaviour
{
	private int loadedCount;

	public List<string> checkNameList = new List<string>();

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		CheckLoadCount();
		if (loadedCount >= checkNameList.Count)
		{
			GameObject.Find("Scenario Manager").GetComponent<UtageAddSceneManager>().GetArborComponent();
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}

	private void CheckLoadCount()
	{
		loadedCount = 0;
		for (int i = 0; i < SceneManager.sceneCount; i++)
		{
			string name = SceneManager.GetSceneAt(i).name;
			if (checkNameList.Any((string data) => data == name))
			{
				loadedCount++;
			}
		}
	}
}
