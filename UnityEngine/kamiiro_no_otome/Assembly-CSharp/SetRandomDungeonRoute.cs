using System.Collections.Generic;
using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class SetRandomDungeonRoute : StateBehaviour
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
		int num = 0;
		GameObject autoRoute;
		if (dungeonMapManager.selectCardList.Count > 0 && dungeonMapManager.selectCardList != null)
		{
			Debug.Log("選択カードは１枚以上ある");
			IEnumerable<GameObject> enumerable = dungeonMapManager.selectCardList.Select((DungeonSelectCardData data) => data.gameObject);
			foreach (GameObject item in enumerable)
			{
				Debug.Log("選択中のカード名：" + item.name);
			}
			IEnumerable<GameObject> enumerable2 = dungeonMapManager.miniCardList.Select((DungeonSelectCardData data) => data.gameObject);
			foreach (GameObject item2 in enumerable2)
			{
				Debug.Log("場に出ているカード名：" + item2.name);
			}
			IEnumerable<GameObject> enumerable3 = enumerable2.Except(enumerable);
			foreach (GameObject item3 in enumerable3)
			{
				Debug.Log("差分の名前：" + item3.name);
			}
			num = Random.Range(0, enumerable3.Count());
			autoRoute = enumerable3.ElementAt(num);
		}
		else
		{
			Debug.Log("選択カードは１枚もない");
			num = Random.Range(0, 4);
			autoRoute = dungeonMapManager.miniCardList[num].gameObject;
		}
		SetAutoRoute(autoRoute);
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

	private void SetAutoRoute(GameObject randomGo)
	{
		GameObject selectGo = dungeonMapManager.miniCardList.Find((DungeonSelectCardData data) => data.gameObject == randomGo).gameObject;
		ParameterContainer component = selectGo.GetComponent<ParameterContainer>();
		string @string = component.GetString("cardSubType");
		string string2 = component.GetString("localizeTerm");
		DungeonSelectCardData item = dungeonMapManager.miniCardList.Find((DungeonSelectCardData data) => data.gameObject == selectGo);
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
		selectGo.GetComponent<CanvasGroup>().interactable = false;
		selectGo.GetComponent<CanvasGroup>().alpha = 0.5f;
		dungeonMapStatusManager.CheckTotalNeedTp();
	}
}
