using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetHeaderStatusItemSize : StateBehaviour
{
	private HeaderStatusManager headerStatusManager;

	public bool viewHeaderStatusGroup;

	public bool viewPartyGroup;

	public bool viewMenuButtonGroup;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
	}

	public override void OnStateBegin()
	{
		headerStatusManager.statusCanvasGroup.gameObject.SetActive(viewHeaderStatusGroup);
		headerStatusManager.partyGroupParent.SetActive(viewHeaderStatusGroup);
		headerStatusManager.menuCanvasGroup.gameObject.SetActive(viewHeaderStatusGroup);
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
