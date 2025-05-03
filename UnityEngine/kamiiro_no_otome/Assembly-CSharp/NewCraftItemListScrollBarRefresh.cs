using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class NewCraftItemListScrollBarRefresh : StateBehaviour
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
		switch (craftManager.selectCategoryNum)
		{
		case 0:
			if (PlayerFlagDataManager.recipeFlagDictionary["recipe3030"])
			{
				craftManager.itemSelectScrollBar.value = 0f;
				Debug.Log("ブレイブを解放済み／武器");
			}
			else
			{
				craftManager.itemSelectScrollBar.value = 1f;
				Debug.Log("ブレイブを未解放／武器");
			}
			break;
		case 1:
			if (PlayerFlagDataManager.recipeFlagDictionary["recipe3040"])
			{
				craftManager.itemSelectScrollBar.value = 0f;
				Debug.Log("ブレイブを解放済み／防具");
			}
			else
			{
				craftManager.itemSelectScrollBar.value = 1f;
				Debug.Log("ブレイブを未解放／防具");
			}
			break;
		default:
			craftManager.itemSelectScrollBar.value = 1f;
			break;
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
