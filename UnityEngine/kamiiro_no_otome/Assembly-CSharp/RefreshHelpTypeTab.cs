using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class RefreshHelpTypeTab : StateBehaviour
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
		GameObject[] helpTypeTabGoArray = helpDataManager.helpTypeTabGoArray;
		for (int i = 0; i < helpTypeTabGoArray.Length; i++)
		{
			helpTypeTabGoArray[i].GetComponent<Image>().sprite = helpDataManager.helpTypeTabSpriteArray[0];
		}
		helpDataManager.helpTypeTabGoArray[helpDataManager.selectTabTypeNum].GetComponent<Image>().sprite = helpDataManager.helpTypeTabSpriteArray[1];
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
