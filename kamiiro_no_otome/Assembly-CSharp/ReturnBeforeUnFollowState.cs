using System.Linq;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class ReturnBeforeUnFollowState : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
	}

	public override void OnStateBegin()
	{
		PlayerDataManager.isDungeonHeroineFollow = false;
		PlayerNonSaveDataManager.isHeroineUnFollowReserveAtLocalMap = false;
		PlayerNonSaveDataManager.isHeroineUnFollowRightClickBlock = false;
		GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>().localHeroineGo.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0f);
		GameObject.Find("Header Status Manager").GetComponent<ArborFSM>().SendTrigger("HeaderStatusRefresh");
		switch (PlayerNonSaveDataManager.heroineUnFollowBeforeStateName)
		{
		case "worldMap":
			GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>().AfterWorldMapHeroineUnFollow();
			break;
		case "camp":
			GameObject.Find("Map Camp Manager").GetComponent<MapCampManager>().AfterWorldMapCampHeroineUnFollow();
			break;
		case "worldToInDoor":
			GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>().AfterReserveHeroineUnFollow();
			break;
		case "localMap":
			GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>().AfterReserveHeroineUnFollow();
			break;
		case "craft":
			GameObject.Find("Craft Check Manager").GetComponent<CraftCheckManager>().PushConfirmDialogOkButton();
			break;
		case "extension":
			GameObject.Find("Extension Dialog Manager").GetComponent<ExtensionDialogManager>().StartExtensionAnimation();
			break;
		case "carriageStore":
			GameObject.Find("Carriage Manager").GetComponent<CarriageManager>().StartStoreTending();
			foreach (GameObject item in totalMapAccessManager.localAreaParentGo.transform.Find(PlayerDataManager.currentAccessPointName).gameObject.GetComponent<ParameterContainer>().GetGameObjectList("localMapButtonList").ToList())
			{
				item.GetComponent<ArborFSM>().SendTrigger("ResetLocalMapPoint");
			}
			break;
		case "shop":
		{
			GameObject.Find("Shop Manager").GetComponent<ShopManager>().PushShopApplyButton();
			InDoorTalkManager component = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
			component.isFollowRequest = false;
			component.positionTalkImageArray[1].SetActive(value: false);
			break;
		}
		case "rest":
			GameObject.Find("InDoor Talk Manager").GetComponent<ArborFSM>().SendTrigger("RefreshInDoorPrivateLocation");
			GameObject.Find("InDoor Rest Manager").GetComponent<ArborFSM>().SendTrigger("StartInnRest");
			break;
		case "mapRest":
			GameObject.Find("Map Rest Manager").GetComponent<MapRestManager>().AfterWorldMapRestHeroineUnFollow();
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
