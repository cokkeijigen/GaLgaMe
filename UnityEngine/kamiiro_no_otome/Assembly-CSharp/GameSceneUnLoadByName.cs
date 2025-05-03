using Arbor;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("")]
public class GameSceneUnLoadByName : StateBehaviour
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
		SceneManager.sceneUnloaded += SceneUnloaded;
		Debug.Log("シーン破棄：" + PlayerNonSaveDataManager.unLoadSceneName);
		SceneManager.UnloadSceneAsync(PlayerNonSaveDataManager.unLoadSceneName);
	}

	public override void OnStateEnd()
	{
		SceneManager.sceneUnloaded -= SceneUnloaded;
	}

	public override void OnStateUpdate()
	{
	}

	public override void OnStateLateUpdate()
	{
	}

	private void SceneUnloaded(Scene scene)
	{
		Debug.Log("シーン破棄完了");
		Transition(nextState);
	}
}
