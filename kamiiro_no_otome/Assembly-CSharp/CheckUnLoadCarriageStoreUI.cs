using Arbor;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("")]
public class CheckUnLoadCarriageStoreUI : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private WorldMapAccessManager worldMapAccessManager;

	private LocalMapAccessManager localMapAccessManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		worldMapAccessManager = GameObject.Find("WorldMap Access Manager").GetComponent<WorldMapAccessManager>();
		localMapAccessManager = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>();
	}

	public override void OnStateBegin()
	{
		totalMapAccessManager.mapCanvasGroupArray[0].interactable = false;
		totalMapAccessManager.mapCanvasGroupArray[1].interactable = false;
		worldMapAccessManager.SetWorldMapCameraDragEnable(value: false);
		localMapAccessManager.localMapExitFSM.enabled = false;
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (!SceneIsLoaded("carriageStoreUI"))
		{
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}

	private bool SceneIsLoaded(string sceneName)
	{
		int sceneCount = SceneManager.sceneCount;
		for (int i = 0; i < sceneCount; i++)
		{
			Scene sceneAt = SceneManager.GetSceneAt(i);
			if (sceneAt.name == sceneName && sceneAt.isLoaded)
			{
				return true;
			}
		}
		return false;
	}
}
