using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CloseInDoorCanvas : StateBehaviour
{
	private HeaderStatusManager headerStatusManager;

	private InDoorTalkManager inDoorTalkManager;

	private MapHeroineUnFollowManager mapHeroineUnFollowManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
	}

	public override void OnStateBegin()
	{
		PlayerNonSaveDataManager.inDoorAllTalkDictionary.Clear();
		for (int i = 0; i < inDoorTalkManager.positionFarImageArray.Length; i++)
		{
			inDoorTalkManager.positionFarImageArray[i].GetComponent<ParameterContainer>().GetGameObject("alertGroupGo").SetActive(value: false);
		}
		for (int j = 0; j < inDoorTalkManager.positionMiddleImageArray.Length; j++)
		{
			inDoorTalkManager.positionMiddleImageArray[j].GetComponent<ParameterContainer>().GetGameObject("alertGroupGo").SetActive(value: false);
		}
		for (int k = 0; k < inDoorTalkManager.positionNearImageArray.Length; k++)
		{
			inDoorTalkManager.positionNearImageArray[k].GetComponent<ParameterContainer>().GetGameObject("alertGroupGo").SetActive(value: false);
		}
		if (!PlayerNonSaveDataManager.isRequiedUtageResume)
		{
			headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
			headerStatusManager.placeTextLoc.Term = "area" + PlayerDataManager.currentAccessPointName;
			headerStatusManager.partyGroupParent.SetActive(value: true);
			headerStatusManager.menuCanvasGroup.gameObject.SetActive(value: true);
			headerStatusManager.clockCanvasGroup.gameObject.SetActive(value: true);
			mapHeroineUnFollowManager = GameObject.Find("Map Rest Manager").GetComponent<MapHeroineUnFollowManager>();
			mapHeroineUnFollowManager.RefreshUnFollowButtonVisible();
		}
		PlayerNonSaveDataManager.isUsedShopForScnearioCheck = false;
		inDoorTalkManager.clickSummaryGo.SetActive(value: true);
		inDoorTalkManager.inDoorCanvasGo.SetActive(value: false);
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
