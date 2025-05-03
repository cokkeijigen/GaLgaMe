using System.Linq;
using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class RefreshPlayerStatusView : StateBehaviour
{
	private DungeonMapStatusManager dungeonMapStatusManager;

	private ArborFSM statusFSM;

	public float tweenTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		dungeonMapStatusManager = GameObject.Find("Dungeon Map Status Manager").GetComponent<DungeonMapStatusManager>();
		statusFSM = dungeonMapStatusManager.transform.GetComponent<ArborFSM>();
	}

	public override void OnStateBegin()
	{
		statusFSM.SendTrigger("RefreshDungeonBuff");
		switch (dungeonMapStatusManager.playerStatusRefreshType)
		{
		case "getItem":
			Invoke("InvokeMethod", tweenTime);
			break;
		case "libido":
			dungeonMapStatusManager.isPlayerStatusViewSetUp.Add(item: false);
			dungeonMapStatusManager.playerLibidoText.DOCounter(dungeonMapStatusManager.beforePlayerStatusValue, PlayerDataManager.playerLibido, tweenTime).OnComplete(delegate
			{
				dungeonMapStatusManager.isPlayerStatusViewSetUp[0] = true;
			});
			break;
		case "hp":
			dungeonMapStatusManager.isPlayerStatusViewSetUp.Add(item: false);
			dungeonMapStatusManager.isPlayerStatusViewSetUp.Add(item: false);
			dungeonMapStatusManager.playerHpText.DOCounter(dungeonMapStatusManager.beforePlayerStatusValue, PlayerStatusDataManager.playerAllHp, tweenTime, addThousandsSeparator: false).OnComplete(delegate
			{
				dungeonMapStatusManager.isPlayerStatusViewSetUp[0] = true;
			});
			dungeonMapStatusManager.playerHpSlider.DOValue(PlayerStatusDataManager.playerAllHp, tweenTime).OnComplete(delegate
			{
				dungeonMapStatusManager.isPlayerStatusViewSetUp[1] = true;
			});
			break;
		case "sp":
			dungeonMapStatusManager.isPlayerStatusViewSetUp.Add(item: false);
			dungeonMapStatusManager.isPlayerStatusViewSetUp.Add(item: false);
			dungeonMapStatusManager.playerSpText.DOCounter(dungeonMapStatusManager.beforePlayerStatusValue, PlayerStatusDataManager.characterSp[PlayerDataManager.DungeonHeroineFollowNum], tweenTime).OnComplete(delegate
			{
				dungeonMapStatusManager.isPlayerStatusViewSetUp[0] = true;
			});
			dungeonMapStatusManager.playerSpSlider.DOValue(PlayerStatusDataManager.characterSp[PlayerDataManager.DungeonHeroineFollowNum], tweenTime).OnComplete(delegate
			{
				dungeonMapStatusManager.isPlayerStatusViewSetUp[1] = true;
			});
			break;
		default:
			Invoke("InvokeMethod", tweenTime);
			break;
		}
	}

	public override void OnStateEnd()
	{
		dungeonMapStatusManager.isPlayerStatusViewSetUp.Clear();
	}

	public override void OnStateUpdate()
	{
		if (dungeonMapStatusManager.isPlayerStatusViewSetUp.All((bool data) => data))
		{
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}

	private void InvokeMethod()
	{
		Transition(stateLink);
	}
}
