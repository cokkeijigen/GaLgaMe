using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class PushBossButton : StateBehaviour
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
		if (!dungeonMapManager.isBossRouteSelect)
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
			foreach (Transform item in dungeonMapManager.miniCardGroup.transform)
			{
				item.GetComponent<CanvasGroup>().interactable = true;
				item.GetComponent<CanvasGroup>().alpha = 1f;
			}
			SetBossRoute();
			dungeonMapManager.isBossRouteSelect = true;
			dungeonMapStatusManager.CheckTotalNeedTp();
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

	private void SetBossRoute()
	{
		dungeonMapManager.routeGroupArray[0].SetActive(value: false);
		dungeonMapManager.routeGroupArray[1].SetActive(value: true);
		string key = "boss";
		string term = "dungeonCardBoss";
		ParameterContainer component = dungeonMapManager.routeSelectBigFrame.GetComponent<ParameterContainer>();
		component.GetVariable<I2LocalizeComponent>("nameTextTerm").localize.Term = term;
		Sprite sprite = GameDataManager.instance.itemCategoryDataBase.dungeonCardIconDictionary[key];
		component.GetVariable<UguiImage>("iconImageGo").image.sprite = sprite;
		component.GetGameObject("numFrame").SetActive(value: false);
		component.GetGameObject("typeFrame").SetActive(value: false);
	}
}
