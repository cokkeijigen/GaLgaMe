using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ChangeShopScrollContentSprite : StateBehaviour
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
		if (shopManager.itemSelectScrollContentGo.childCount > 0)
		{
			foreach (Transform item in shopManager.itemSelectScrollContentGo)
			{
				item.GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage").image.sprite = shopManager.selectScrollContentSpriteArray[0];
			}
			shopManager.itemSelectScrollContentGo.GetChild(shopManager.clickedItemIndex).GetComponent<ParameterContainer>().GetVariable<UguiImage>("itemImage")
				.image.sprite = shopManager.selectScrollContentSpriteArray[1];
		}
		if (shopManager.clickedItemID >= 3000)
		{
			shopManager.isEquipItem = false;
			if (PlayerInventoryDataManager.haveAccessoryList.Any())
			{
				HaveAccessoryData haveAccessoryData = PlayerInventoryDataManager.haveAccessoryList.Find((HaveAccessoryData data) => data.itemID == shopManager.clickedItemID);
				if (haveAccessoryData != null && haveAccessoryData.equipCharacter != 9)
				{
					shopManager.isEquipItem = true;
				}
			}
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
