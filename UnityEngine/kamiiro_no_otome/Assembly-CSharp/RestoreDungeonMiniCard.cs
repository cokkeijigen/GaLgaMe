using Arbor;
using PathologicalGames;
using UnityEngine;

[AddComponentMenu("")]
public class RestoreDungeonMiniCard : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	private int cardPowerNum;

	private int randomEnemyCount;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponentInParent<DungeonMapManager>();
	}

	public override void OnStateBegin()
	{
		string cardName = "";
		DungeonCardData dungeonCardData = null;
		for (int i = 0; i < 4; i++)
		{
			Transform transform = PoolManager.Pools["DungeonObject"].Spawn(dungeonMapManager.miniCardGo, dungeonMapManager.miniCardGroup.transform);
			transform.localScale = new Vector3(1f, 1f, 1f);
			DungeonSelectCardData dungeonSelectCardData = PlayerNonSaveDataManager.backUpMiniCardList[i];
			cardName = dungeonSelectCardData.localizeTerm;
			dungeonCardData = GameDataManager.instance.dungeonMapCardDataBase.dungeonCardDataList.Find((DungeonCardData data) => data.cardLocalizeTerm == cardName);
			SetParameterContainer(dungeonCardData, transform, dungeonSelectCardData);
			transform.GetComponent<CanvasGroup>().interactable = true;
			transform.GetComponent<CanvasGroup>().alpha = 1f;
			DungeonSelectCardData dungeonSelectCardData2 = new DungeonSelectCardData();
			dungeonSelectCardData2.multiType = dungeonCardData.multiType.ToString();
			dungeonSelectCardData2.subTypeString = dungeonCardData.cardType.ToString();
			dungeonSelectCardData2.localizeTerm = dungeonCardData.cardLocalizeTerm;
			dungeonSelectCardData2.powerNum = dungeonSelectCardData.powerNum;
			dungeonSelectCardData2.enemyCountNum = dungeonSelectCardData.enemyCountNum;
			dungeonSelectCardData2.needSkipTp = dungeonSelectCardData.needSkipTp;
			dungeonSelectCardData2.sortID = dungeonCardData.sortID;
			dungeonSelectCardData2.gameObject = transform.gameObject;
			dungeonMapManager.miniCardList.Add(dungeonSelectCardData2);
		}
		dungeonMapManager.SortInPlayDungeonCard();
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

	private void SetParameterContainer(DungeonCardData cardData, Transform transform, DungeonSelectCardData dungeonSelectCardData)
	{
		cardPowerNum = dungeonSelectCardData.powerNum;
		GameDataManager.instance.dungeonMapDataBase.dungeonMapDataList.Find((DungeonMapData data) => data.dungeonName == PlayerDataManager.currentDungeonName);
		ParameterContainer component = transform.GetComponent<ParameterContainer>();
		component.GetVariable<TmpText>("nameText").textMeshProUGUI.text = cardData.cardName;
		component.GetVariable<UguiImage>("cardImage").image.sprite = cardData.cardSprite;
		component.SetInt("addLibidoNum", cardPowerNum);
		component.SetInt("enemyCountNum", dungeonSelectCardData.enemyCountNum);
		component.SetString("cardSubType", cardData.cardType.ToString());
		component.SetString("cardType", cardData.multiType.ToString());
		component.SetString("localizeTerm", cardData.cardLocalizeTerm);
		SetNumFrameSizeDelta(component, "default");
		if (cardData.multiType == DungeonCardData.Type.heroine)
		{
			component.GetGameObject("numFrame").SetActive(value: true);
			component.GetGameObject("typeFrame").SetActive(value: true);
			component.GetVariable<TmpText>("modText").textMeshProUGUI.text = "+";
			component.GetVariable<TmpText>("numText").textMeshProUGUI.text = cardPowerNum.ToString();
			SetTypeIconImage(cardData, component);
		}
		else if (cardData.multiType == DungeonCardData.Type.battle)
		{
			component.GetGameObject("numFrame").SetActive(value: true);
			component.GetGameObject("typeFrame").SetActive(value: true);
			SetNumFrameSizeDelta(component, "battle");
			component.GetVariable<TmpText>("modText").textMeshProUGUI.text = "×";
			component.GetVariable<TmpText>("numText").textMeshProUGUI.text = dungeonSelectCardData.enemyCountNum.ToString();
			component.GetVariable<TmpText>("skipTpNumText").textMeshProUGUI.text = dungeonSelectCardData.needSkipTp.ToString();
			SetTypeIconImage(cardData, component);
		}
		else if (cardData.multiType != 0)
		{
			component.GetGameObject("numFrame").SetActive(value: false);
			component.GetGameObject("typeFrame").SetActive(value: true);
			SetTypeIconImage(cardData, component);
		}
		else if (cardData.cardType == DungeonCardData.SubType.vigilant)
		{
			component.GetGameObject("numFrame").SetActive(value: true);
			component.GetGameObject("typeFrame").SetActive(value: false);
			SetNumFrameSizeDelta(component, "vigilant");
			component.GetVariable<TmpText>("modText").textMeshProUGUI.text = "-";
			component.GetVariable<TmpText>("numText").textMeshProUGUI.text = cardPowerNum.ToString();
		}
		else
		{
			component.GetGameObject("numFrame").SetActive(value: false);
			component.GetGameObject("typeFrame").SetActive(value: false);
		}
	}

	private void SetNumFrameSizeDelta(ParameterContainer parameterContainer, string type)
	{
		RectTransform component = parameterContainer.GetGameObject("numFrame").GetComponent<RectTransform>();
		RectTransform component2 = parameterContainer.GetGameObject("numGroup").GetComponent<RectTransform>();
		switch (type)
		{
		case "default":
			parameterContainer.GetGameObject("numTypeIconGo").SetActive(value: false);
			parameterContainer.GetGameObject("skipTpNumFrame").SetActive(value: false);
			component.sizeDelta = new Vector2(122f, 58f);
			component2.offsetMin = new Vector2(15f, 4f);
			component2.offsetMax = new Vector2(-15f, -4f);
			break;
		case "battle":
			parameterContainer.GetGameObject("numTypeIconGo").SetActive(value: false);
			parameterContainer.GetGameObject("skipTpNumFrame").SetActive(value: true);
			component.sizeDelta = new Vector2(122f, 58f);
			component2.offsetMin = new Vector2(20f, 4f);
			component2.offsetMax = new Vector2(-45f, -4f);
			break;
		case "vigilant":
			parameterContainer.GetGameObject("numTypeIconGo").SetActive(value: true);
			parameterContainer.GetGameObject("skipTpNumFrame").SetActive(value: false);
			component.sizeDelta = new Vector2(122f, 58f);
			component2.offsetMin = new Vector2(50f, 4f);
			component2.offsetMax = new Vector2(-15f, -4f);
			break;
		}
	}

	private void SetTypeIconImage(DungeonCardData cardData, ParameterContainer parameterContainer)
	{
		Sprite dungeonCardTypeSprite = dungeonMapManager.GetDungeonCardTypeSprite(cardData.multiType.ToString());
		parameterContainer.GetVariable<UguiImage>("typeImage").image.sprite = dungeonCardTypeSprite;
	}
}
