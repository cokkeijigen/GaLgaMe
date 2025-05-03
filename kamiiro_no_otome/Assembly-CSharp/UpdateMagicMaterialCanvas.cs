using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class UpdateMagicMaterialCanvas : StateBehaviour
{
	private CraftAddOnManager craftAddOnManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftAddOnManager = GameObject.Find("Craft AddOn Manager").GetComponent<CraftAddOnManager>();
	}

	public override void OnStateBegin()
	{
		craftAddOnManager.overlayCanvas.SetActive(value: false);
		craftAddOnManager.MagicMaterialListRefresh();
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
