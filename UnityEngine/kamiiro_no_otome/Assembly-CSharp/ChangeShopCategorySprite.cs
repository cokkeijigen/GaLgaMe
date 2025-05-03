using Arbor;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class ChangeShopCategorySprite : StateBehaviour
{
	private ShopManager shopManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		shopManager = GameObject.Find("Shop Manager").GetComponent<ShopManager>();
	}

	public override void OnStateBegin()
	{
		Image[] itemSelectCategoryTabImage = shopManager.itemSelectCategoryTabImage;
		for (int i = 0; i < itemSelectCategoryTabImage.Length; i++)
		{
			itemSelectCategoryTabImage[i].sprite = shopManager.itemSelectCategorySpriteArray[0];
		}
		shopManager.itemSelectCategoryTabImage[shopManager.selectTabCategoryNum].sprite = shopManager.itemSelectCategorySpriteArray[1];
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
