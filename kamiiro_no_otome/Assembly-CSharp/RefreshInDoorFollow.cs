using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;

[AddComponentMenu("")]
public class RefreshInDoorFollow : StateBehaviour
{
	private InDoorTalkManager inDoorTalkManager;

	private InDoorClickManager inDoorClickManager;

	private InDoorCommandManager inDoorCommandManager;

	private TotalMapAccessManager totalMapAccessManager;

	private ArborFSM headerStatusFSM;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		inDoorTalkManager = GameObject.Find("InDoor Talk Manager").GetComponent<InDoorTalkManager>();
		inDoorClickManager = GameObject.Find("InDoor Click Manager").GetComponent<InDoorClickManager>();
		inDoorCommandManager = GameObject.Find("InDoor Command Manager").GetComponent<InDoorCommandManager>();
	}

	public override void OnStateBegin()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		headerStatusFSM = GameObject.Find("Header Status Manager").GetComponent<ArborFSM>();
		inDoorCommandManager.inDoorDialogGroupGo.GetComponent<ParameterContainer>();
		inDoorTalkManager.inDoorCanvasGo.GetComponent<CanvasGroup>().interactable = true;
		inDoorTalkManager.commandButtonGroupGo.GetComponent<CanvasGroup>().interactable = true;
		inDoorTalkManager.commandButtonGroupGo.GetComponent<CanvasGroup>().alpha = 1f;
		PlayerNonSaveDataManager.isHeroineUnFollowRightClickBlock = false;
		HeaderStatusManager component = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		inDoorCommandManager.blackImageGo.SetActive(value: false);
		inDoorCommandManager.inDoorDialogGroupGo.SetActive(value: false);
		component.ResetHeaderUiBrightness();
		inDoorTalkManager.isFollowRequestApply = false;
		MasterAudio.PlaySound("SeMiniButton", 1f, null, 0f, null, null);
		headerStatusFSM.SendTrigger("HeaderStatusRefresh");
		SpriteRenderer component2 = totalMapAccessManager.localHeroineGo.GetComponent<SpriteRenderer>();
		if (PlayerDataManager.isDungeonHeroineFollow)
		{
			component2.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);
		}
		else
		{
			component2.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0);
		}
		if ((PlayerDataManager.currentPlaceName == "Inn" || PlayerDataManager.currentPlaceName == "InnStreet1") && inDoorTalkManager.positionTalkImageArray[0].activeInHierarchy)
		{
			inDoorClickManager.OpenInDoorCommandTalkBalloon();
		}
		PlayerEquipDataManager.CalcEquipPlayerHaveWeaponFactor(CallBackWeaponMethod, isAllCalc: true);
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

	private void CallBackWeaponMethod()
	{
		PlayerStatusDataManager.SetUpPlayerStatus(isSetUp: false, CallBackStatusMethod);
	}

	private void CallBackStatusMethod()
	{
		Transition(stateLink);
		Debug.Log("Equipデータの更新完了");
	}
}
