using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckCannotExecuteWhileFollowing : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private LocalMapAccessManager localMapAccessManager;

	private HeaderStatusManager headerStatusManager;

	private ParameterContainer parameterContainer;

	private PlayMakerFSM alertDialogFSM;

	public StateLink canNotFollowLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		localMapAccessManager = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>();
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
		parameterContainer = GetComponentInParent<ParameterContainer>();
		alertDialogFSM = totalMapAccessManager.mapAlertDialogGo.GetComponent<PlayMakerFSM>();
	}

	public override void OnStateBegin()
	{
		string @string = parameterContainer.GetString("scenarioName");
		int sortId = parameterContainer.GetInt("scenarioSortId");
		if (!string.IsNullOrEmpty(@string))
		{
			List<int> cannotExecuteWhileFollowingIdList = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Find((ScenarioFlagData data) => data.sortId == sortId).cannotExecuteWhileFollowingIdList;
			int needFollowHeroineNum = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Find((ScenarioFlagData data) => data.sortId == sortId).needFollowHeroineNum;
			if (PlayerDataManager.isDungeonHeroineFollow)
			{
				if (needFollowHeroineNum == 0)
				{
					totalMapAccessManager.alertTextLoc.Term = "dialogWorldMapDisable_RequiredSolo";
					Debug.Log("ソロオンリー");
					OpenMapAlertDialog();
					return;
				}
				if (cannotExecuteWhileFollowingIdList.Contains(PlayerDataManager.DungeonHeroineFollowNum) && cannotExecuteWhileFollowingIdList.Count > 0 && cannotExecuteWhileFollowingIdList != null)
				{
					totalMapAccessManager.alertTextLoc.Term = "dialogItemShopDisable";
					Debug.Log("同行中実行不可ヒロインがいる");
					OpenMapAlertDialog();
					return;
				}
				if (PlayerDataManager.DungeonHeroineFollowNum == needFollowHeroineNum)
				{
					Transition(stateLink);
					return;
				}
				switch (needFollowHeroineNum)
				{
				case 1:
					totalMapAccessManager.alertTextLoc.Term = "dialogWorldMapDisable_RequiredLucy";
					break;
				case 2:
					totalMapAccessManager.alertTextLoc.Term = "dialogWorldMapDisable_RequiredRina";
					break;
				case 3:
					totalMapAccessManager.alertTextLoc.Term = "dialogWorldMapDisable_RequiredShia";
					break;
				case 4:
					totalMapAccessManager.alertTextLoc.Term = "dialogWorldMapDisable_RequiredLevy";
					break;
				}
				Debug.Log("要同行ヒロインがいない");
				OpenMapAlertDialog();
			}
			else
			{
				switch (needFollowHeroineNum)
				{
				case 0:
				case 9:
					Transition(stateLink);
					return;
				case 1:
					totalMapAccessManager.alertTextLoc.Term = "dialogWorldMapDisable_RequiredLucy";
					break;
				case 2:
					totalMapAccessManager.alertTextLoc.Term = "dialogWorldMapDisable_RequiredRina";
					break;
				case 3:
					totalMapAccessManager.alertTextLoc.Term = "dialogWorldMapDisable_RequiredShia";
					break;
				case 4:
					totalMapAccessManager.alertTextLoc.Term = "dialogWorldMapDisable_RequiredLevy";
					break;
				}
				Debug.Log("要同行ヒロインがいない");
				OpenMapAlertDialog();
			}
		}
		else
		{
			Transition(stateLink);
		}
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

	private void OpenMapAlertDialog()
	{
		totalMapAccessManager.mapCanvasGroupArray[1].interactable = false;
		totalMapAccessManager.mapCanvasGroupArray[1].blocksRaycasts = false;
		headerStatusManager.menuCanvasGroup.alpha = 0.5f;
		headerStatusManager.exitButton.interactable = false;
		headerStatusManager.exitButton.blocksRaycasts = false;
		headerStatusManager.exitButton.alpha = 0.5f;
		localMapAccessManager.SetLocalMapExitEnable(isEnable: false);
		totalMapAccessManager.mapAlertDialogGo.SetActive(value: true);
		Debug.Log("入れないダイアログを表示する");
		alertDialogFSM.SendEvent("CloseMapAlertDialog");
		Transition(canNotFollowLink);
	}
}
