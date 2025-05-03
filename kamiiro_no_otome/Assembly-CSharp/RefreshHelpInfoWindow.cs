using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class RefreshHelpInfoWindow : StateBehaviour
{
	private HelpDataManager helpDataManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		helpDataManager = GameObject.Find("Help Data Manager").GetComponent<HelpDataManager>();
	}

	public override void OnStateBegin()
	{
		if (helpDataManager.selectHelpData.isContentSpriteActive)
		{
			helpDataManager.helpInfoImage.gameObject.SetActive(value: true);
			helpDataManager.helpInfoImage.sprite = helpDataManager.selectHelpData.helpContentSprite;
		}
		else
		{
			helpDataManager.helpInfoImage.gameObject.SetActive(value: false);
		}
		helpDataManager.helpInfoHeaderTextLoc.Term = helpDataManager.selectHelpData.helpTermName;
		helpDataManager.helpInfoTextLoc.Term = helpDataManager.selectHelpData.helpTermName + "_summary";
		helpDataManager.helpInfoScrollBar.value = 1f;
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
