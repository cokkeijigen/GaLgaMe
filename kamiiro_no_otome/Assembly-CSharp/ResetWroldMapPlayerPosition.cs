using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class ResetWroldMapPlayerPosition : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private WorldMapAccessManager worldMapAccessManager;

	public GameObject worldMapCameraGo;

	public CanvasGroup worldMapUiCanvas;

	private GameObject currentPointGo;

	private GameObject worldAreaParentGo;

	public float time;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		worldMapAccessManager = GameObject.Find("WorldMap Access Manager").GetComponent<WorldMapAccessManager>();
		worldAreaParentGo = GameObject.Find("World Area");
	}

	public override void OnStateBegin()
	{
		PlayerDataManager.worldMapInputBlock = true;
		totalMapAccessManager.mapCanvasGroupArray[2].interactable = false;
		totalMapAccessManager.mapCanvasGroupArray[2].alpha = 0.5f;
		currentPointGo = worldAreaParentGo.transform.Find(PlayerDataManager.currentAccessPointName).gameObject;
		Vector3 position = new Vector3(currentPointGo.transform.position.x, currentPointGo.transform.position.y + 0.35f, 0f);
		totalMapAccessManager.worldPlayerGo.transform.position = position;
		if (PlayerNonSaveDataManager.isRefreshWorldMap)
		{
			Transition(stateLink);
			return;
		}
		float x = currentPointGo.transform.position.x;
		float y = currentPointGo.transform.position.y;
		x = Mathf.Clamp(x, worldMapAccessManager.moveLimitX * -1f, worldMapAccessManager.moveLimitX);
		y = Mathf.Clamp(y, worldMapAccessManager.moveLimitY * -1f, worldMapAccessManager.moveLimitY);
		ShortcutExtensions.DOMove(endValue: new Vector3(x, y, -100f), target: worldMapAccessManager.worldMapCameraGo.transform, duration: time).OnComplete(delegate
		{
			worldMapUiCanvas.interactable = true;
			worldMapUiCanvas.alpha = 1f;
			Transition(stateLink);
		});
	}

	public override void OnStateEnd()
	{
		totalMapAccessManager.mapCanvasGroupArray[0].alpha = 1f;
		totalMapAccessManager.localPlayerGo.SetActive(value: true);
		totalMapAccessManager.localHeroineGo.SetActive(value: true);
	}

	public override void OnStateUpdate()
	{
	}

	public override void OnStateLateUpdate()
	{
	}
}
