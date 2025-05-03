using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class ClickDungeonCard : StateBehaviour
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
		if (dungeonMapManager.selectCardList.Count < 3 || dungeonMapManager.isBossRouteSelect)
		{
			ParameterContainer component = GetComponent<ParameterContainer>();
			string @string = component.GetString("cardSubType");
			string string2 = component.GetString("localizeTerm");
			DungeonSelectCardData item = dungeonMapManager.miniCardList.Find((DungeonSelectCardData data) => data.gameObject == base.gameObject);
			dungeonMapManager.selectCardList.Add(item);
			int count = dungeonMapManager.selectCardList.Count;
			ParameterContainer component2 = dungeonMapManager.routeSelectFrameArray[count - 1].GetComponent<ParameterContainer>();
			component2.GetVariable<I2LocalizeComponent>("nameTextTerm").localize.Term = string2;
			Sprite sprite = GameDataManager.instance.itemCategoryDataBase.dungeonCardIconDictionary[@string];
			component2.GetVariable<UguiImage>("iconImageGo").image.sprite = sprite;
			int @int = component.GetInt("addLibidoNum");
			int int2 = component.GetInt("enemyCountNum");
			string string3 = component.GetString("cardType");
			if (string3 == "heroine")
			{
				component2.GetGameObject("numFrame").SetActive(value: true);
				component2.GetGameObject("typeFrame").SetActive(value: true);
				component2.GetVariable<TmpText>("modText").textMeshProUGUI.text = "+";
				component2.GetVariable<TmpText>("numText").textMeshProUGUI.text = @int.ToString();
			}
			else if (string3 == "battle")
			{
				component2.GetGameObject("numFrame").SetActive(value: true);
				component2.GetGameObject("typeFrame").SetActive(value: true);
				component2.GetVariable<TmpText>("modText").textMeshProUGUI.text = "×";
				component2.GetVariable<TmpText>("numText").textMeshProUGUI.text = int2.ToString();
			}
			else if (string3 != "none")
			{
				component2.GetGameObject("numFrame").SetActive(value: false);
				component2.GetGameObject("typeFrame").SetActive(value: true);
			}
			else
			{
				component2.GetGameObject("numFrame").SetActive(value: false);
				component2.GetGameObject("typeFrame").SetActive(value: false);
			}
			component2.GetVariable<UguiImage>("typeImage").image.sprite = GameDataManager.instance.itemCategoryDataBase.dungeonCardTypeIconDictionary[string3];
			GetComponent<CanvasGroup>().interactable = false;
			GetComponent<CanvasGroup>().alpha = 0.5f;
			dungeonMapStatusManager.CheckTotalNeedTp();
			MasterAudio.PlaySound("SeMiniButton", 1f, null, 0f, null, null);
		}
		else
		{
			MasterAudio.PlaySound("SeError", 1f, null, 0f, null, null);
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
