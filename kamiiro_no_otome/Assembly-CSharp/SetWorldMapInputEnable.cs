using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetWorldMapInputEnable : StateBehaviour
{
	public CanvasGroup worldCanvas;

	public bool setValue;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
	}

	public override void OnStateBegin()
	{
		if (worldCanvas == null)
		{
			worldCanvas = GameObject.Find("World Canvas").GetComponent<CanvasGroup>();
		}
		PlayerNonSaveDataManager.isRefreshWorldMap = false;
		if (setValue)
		{
			PlayerDataManager.worldMapInputBlock = false;
			worldCanvas.interactable = true;
			worldCanvas.blocksRaycasts = true;
		}
		else
		{
			PlayerDataManager.worldMapInputBlock = true;
			worldCanvas.interactable = false;
			worldCanvas.blocksRaycasts = false;
		}
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
