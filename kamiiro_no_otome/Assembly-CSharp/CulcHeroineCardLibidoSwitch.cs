using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CulcHeroineCardLibidoSwitch : StateBehaviour
{
	private DungeonMapManager dungeonMapManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapManager = GameObject.Find("Dungeon Map Manager").GetComponent<DungeonMapManager>();
	}

	public override void OnStateBegin()
	{
		ParameterContainer component = GetComponent<ParameterContainer>();
		string text = ((PlayerDataManager.playerLibido >= 50) ? "dungeonCardHeroineTouch" : ((PlayerDataManager.playerLibido < 25) ? "dungeonCardHeroineTalk" : "dungeonCardHeroineWatch"));
		component.SetString("cardSubType", text);
		CulcHeroineCard(text);
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

	private void CulcHeroineCard(string cardSubType)
	{
		DungeonCardData dungeonCardData = GameDataManager.instance.dungeonMapCardDataBase.dungeonCardDataList.Find((DungeonCardData data) => data.cardLocalizeTerm == cardSubType);
		int minInclusive = dungeonCardData.cardPower / 2;
		int cardPower = dungeonCardData.cardPower;
		int num = Random.Range(minInclusive, cardPower);
		ParameterContainer component = GetComponent<ParameterContainer>();
		component.GetVariable<TmpText>("nameText").textMeshProUGUI.text = dungeonCardData.cardName;
		component.GetVariable<UguiImage>("cardImage").image.sprite = dungeonCardData.cardSprite;
		component.SetInt("addLibidoNum", num);
		component.SetString("cardSubType", dungeonCardData.cardLocalizeTerm);
		component.GetVariable<TmpText>("addLibidoNumWithTypeText").textMeshProUGUI.text = num.ToString();
		DungeonSelectCardData dungeonSelectCardData = new DungeonSelectCardData();
		dungeonSelectCardData.subTypeString = dungeonCardData.cardType.ToString();
		dungeonSelectCardData.powerNum = num;
		dungeonSelectCardData.sortID = dungeonCardData.sortID;
		dungeonSelectCardData.gameObject = base.gameObject;
		int siblingIndex = base.gameObject.transform.GetSiblingIndex();
		dungeonMapManager.miniCardList[siblingIndex] = dungeonSelectCardData;
	}
}
