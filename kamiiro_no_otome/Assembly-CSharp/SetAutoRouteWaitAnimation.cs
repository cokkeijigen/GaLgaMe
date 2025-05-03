using Arbor;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("")]
public class SetAutoRouteWaitAnimation : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private ParameterContainer parameterContainer;

	private Tweener tweener;

	public float animationTime;

	public StateLink routeCompleteLink;

	public StateLink autoLink;

	public StateLink normalLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
	}

	public override void OnStateBegin()
	{
		CanvasGroup[] mapCanvasGroupArray = dungeonMapManager.mapCanvasGroupArray;
		foreach (CanvasGroup obj in mapCanvasGroupArray)
		{
			obj.interactable = false;
			obj.blocksRaycasts = false;
			obj.alpha = 0.5f;
		}
		dungeonMapManager.routeButtonGroup.SetActive(value: false);
		dungeonMapManager.autoAlertGroup.SetActive(value: true);
		dungeonMapManager.miniCardGroupCanvasGroup.interactable = false;
		dungeonMapManager.miniCardGroupCanvasGroup.blocksRaycasts = false;
		dungeonMapManager.miniCardGroupCanvasGroup.alpha = 0.7f;
		CheckBossRouteSelect();
		Debug.Log("カードの選択数：" + dungeonMapManager.selectCardList.Count);
		float num = 0f;
		switch (PlayerDataManager.dungeonMoveSpeed)
		{
		case 1:
			num = 0.8f;
			break;
		case 2:
			num = 0.6f;
			break;
		case 4:
			num = 0.4f;
			break;
		default:
			num = 0.8f;
			break;
		}
		if (dungeonMapManager.selectCardList.Count == 3)
		{
			AutoRouteComplete();
			return;
		}
		parameterContainer = dungeonMapManager.routeSelectFrameArray[dungeonMapManager.selectCardList.Count].gameObject.GetComponent<ParameterContainer>();
		parameterContainer.GetGameObject("autoGroupGo").SetActive(value: true);
		Image image = parameterContainer.GetVariable<UguiImage>("autoFillImage").image;
		image.fillAmount = 0f;
		tweener = image.DOFillAmount(1f, num).SetEase(Ease.Linear).OnComplete(delegate
		{
			parameterContainer.GetGameObject("autoGroupGo").SetActive(value: false);
			Transition(autoLink);
		});
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (!PlayerDataManager.isDungeonMapAuto)
		{
			tweener.Kill();
			parameterContainer.GetGameObject("autoGroupGo").SetActive(value: false);
			Transition(normalLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}

	private void CheckBossRouteSelect()
	{
		if (!dungeonMapManager.isBossRouteSelect)
		{
			return;
		}
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
		dungeonMapManager.isBossRouteSelect = false;
	}

	private void AutoRouteComplete()
	{
		Transition(routeCompleteLink);
	}
}
