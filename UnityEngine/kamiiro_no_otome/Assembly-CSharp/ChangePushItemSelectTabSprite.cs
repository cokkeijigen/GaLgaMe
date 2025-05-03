using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class ChangePushItemSelectTabSprite : StateBehaviour
{
	private CraftManager craftManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
	}

	public override void OnStateBegin()
	{
		GameObject[] itemCategoryTabGoArray = craftManager.itemCategoryTabGoArray;
		for (int i = 0; i < itemCategoryTabGoArray.Length; i++)
		{
			itemCategoryTabGoArray[i].GetComponent<Image>().sprite = craftManager.selectTabSpriteArray[0];
		}
		craftManager.itemCategoryTabGoArray[craftManager.selectCategoryNum].GetComponent<Image>().sprite = craftManager.selectTabSpriteArray[1];
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
