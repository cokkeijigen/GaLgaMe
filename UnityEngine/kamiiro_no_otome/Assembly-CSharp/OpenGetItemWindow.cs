using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class OpenGetItemWindow : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCheckManager craftCheckManager;

	private CraftTalkManager craftTalkManager;

	private ParameterContainer parameterContainer;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GetComponentInParent<CraftManager>();
		craftCheckManager = GetComponent<CraftCheckManager>();
		craftTalkManager = GameObject.Find("Craft Talk Manager").GetComponent<CraftTalkManager>();
		parameterContainer = craftCheckManager.getFactorWindow.GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		craftCheckManager.getFactorWindow.SetActive(value: true);
		craftCheckManager.blackImageGo.transform.SetSiblingIndex(3);
		craftCheckManager.newFactorFrame.SetActive(value: false);
		craftCheckManager.newItemFrame.SetActive(value: true);
		switch (craftManager.selectCategoryNum)
		{
		case 2:
		{
			string text2 = GameDataManager.instance.itemCanMakeMaterialDataBase.itemCanMakeMaterialDataList.Find((ItemCanMakeMaterialData m) => m.itemID == craftManager.clickedItemID).category.ToString();
			parameterContainer.GetVariable<UguiImage>("iconImage_Simple").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[text2];
			parameterContainer.GetVariable<I2LocalizeComponent>("nameTextLoc_Simple").localize.Term = text2 + craftManager.clickedItemID;
			break;
		}
		case 3:
		{
			string text = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData m) => m.itemID == craftManager.clickedItemID).category.ToString();
			parameterContainer.GetVariable<UguiImage>("iconImage_Simple").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[text];
			parameterContainer.GetVariable<I2LocalizeComponent>("nameTextLoc_Simple").localize.Term = text + craftManager.clickedItemID;
			break;
		}
		case 4:
		{
			string key = GameDataManager.instance.itemCampItemDataBase.itemCampItemDataList.Find((ItemCampItemData m) => m.itemID == craftManager.clickedItemID).category.ToString();
			parameterContainer.GetVariable<UguiImage>("iconImage_Simple").image.sprite = GameDataManager.instance.itemCategoryDataBase.itemCategoryIconDictionary[key];
			parameterContainer.GetVariable<I2LocalizeComponent>("nameTextLoc_Simple").localize.Term = "campItem" + craftManager.clickedItemID;
			break;
		}
		}
		craftTalkManager.TalkBalloonItemCrafted();
		Transform transform = PoolManager.Pools["Craft Item Pool"].Spawn(craftCheckManager.craftedEffectPrefabGoArray[3], craftCheckManager.uIParticle.transform);
		craftCheckManager.craftedEffectSpawnGo = transform;
		transform.localPosition = new Vector3(0f, 0f, 0f);
		transform.localScale = new Vector3(1f, 1f, 1f);
		craftCheckManager.uIParticle.RefreshParticles();
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
