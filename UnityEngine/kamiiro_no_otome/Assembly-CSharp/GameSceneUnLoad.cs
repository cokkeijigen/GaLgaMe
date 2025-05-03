using Arbor;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("")]
public class GameSceneUnLoad : StateBehaviour
{
	public StateLink nextState;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		Debug.Log("シーン破棄：" + base.gameObject.scene.name);
		SceneManager.UnloadSceneAsync(base.gameObject.scene.name);
		Transition(nextState);
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
