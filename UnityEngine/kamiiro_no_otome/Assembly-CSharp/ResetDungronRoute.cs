using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ResetDungronRoute : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private DungeonMapStatusManager dungeonMapStatusManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
		dungeonMapStatusManager = GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapStatusManager>();
	}

	public override void OnStateBegin()
	{
		for (int i = 0; i < dungeonMapManager.routeSelectFrameArray.Length; i++)
		{
			ParameterContainer component = dungeonMapManager.routeSelectFrameArray[i].GetComponent<ParameterContainer>();
			component.GetVariable<I2LocalizeComponent>("nameTextTerm").localize.Term = "empty";
			component.GetGameObject("numFrame").SetActive(value: false);
			component.GetGameObject("typeFrame").SetActive(value: false);
			component.GetGameObject("bonusTextGo").SetActive(value: false);
			component.GetVariable<TmpText>("modText").textMeshProUGUI.text = "+";
			component.GetVariable<TmpText>("numText").textMeshProUGUI.text = "0";
			component.GetVariable<UguiImage>("iconImageGo").image.sprite = GameDataManager.instance.itemCategoryDataBase.dungeonCardIconDictionary["noRoute"];
		}
		dungeonMapManager.selectCardList.Clear();
		dungeonMapManager.isBossRouteSelect = false;
		foreach (Transform item in dungeonMapManager.miniCardGroup.transform)
		{
			item.GetComponent<CanvasGroup>().interactable = true;
			item.GetComponent<CanvasGroup>().alpha = 1f;
		}
		dungeonMapManager.routeGroupArray[0].SetActive(value: true);
		dungeonMapManager.routeGroupArray[1].SetActive(value: false);
		dungeonMapStatusManager.ClearTotalNeedTp();
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
