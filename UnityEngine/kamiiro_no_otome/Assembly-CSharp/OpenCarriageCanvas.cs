using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class OpenCarriageCanvas : StateBehaviour
{
	private CarriageManager carriageManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		carriageManager = GameObject.Find("Carriage Manager").GetComponent<CarriageManager>();
	}

	public override void OnStateBegin()
	{
		GameObject[] itemCategoryTabGoArray = carriageManager.itemCategoryTabGoArray;
		for (int i = 0; i < itemCategoryTabGoArray.Length; i++)
		{
			itemCategoryTabGoArray[i].GetComponent<Image>().sprite = carriageManager.selectTabSpriteArray[0];
		}
		carriageManager.itemCategoryTabGoArray[carriageManager.selectCategoryNum].GetComponent<Image>().sprite = carriageManager.selectTabSpriteArray[1];
		carriageManager.isScrollContentInitialize = false;
		carriageManager.sellClickedItemID = 0;
		carriageManager.selectScrollContentIndex = 0;
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
