using Arbor;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("")]
public class CheckSceneContainsByName : StateBehaviour
{
	public string checkSceneName;

	public StateLink containLink;

	public StateLink notContainLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		bool flag = false;
		for (int i = 0; i < SceneManager.sceneCount; i++)
		{
			if (SceneManager.GetSceneAt(i).name == checkSceneName)
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			Transition(containLink);
		}
		else
		{
			Transition(notContainLink);
		}
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
