using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class ChangeCategorySprite : StateBehaviour
{
	private StatusManager statusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		statusManager = GetComponent<StatusManager>();
	}

	public override void OnStateBegin()
	{
		statusManager.selectItemScrollContentIndex = 0;
		Image[] itemCategoryTabImageArray = statusManager.itemCategoryTabImageArray;
		for (int i = 0; i < itemCategoryTabImageArray.Length; i++)
		{
			itemCategoryTabImageArray[i].sprite = statusManager.categoryTabSpriteArray[0];
		}
		statusManager.itemCategoryTabImageArray[statusManager.selectItemCategoryNum].sprite = statusManager.categoryTabSpriteArray[1];
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
