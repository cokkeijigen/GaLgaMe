using System.Collections.Generic;
using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class CheckWorldCannotExecuteWhileFollowing : StateBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private ParameterContainer parameterContainer;

	public StateLink canNotFollowLink;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		parameterContainer = GetComponentInParent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		string scenarioName = parameterContainer.GetString("scenarioName");
		int childCount = totalMapAccessManager.worldAreaParentGo.transform.childCount;
		List<string> list = new List<string>();
		for (int i = 0; i < childCount; i++)
		{
			list.Add(totalMapAccessManager.worldAreaParentGo.transform.GetChild(i).gameObject.name);
		}
		if (!list.Contains(scenarioName))
		{
			List<int> cannotExecuteWhileFollowingIdList = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Find((ScenarioFlagData data) => data.scenarioName == scenarioName).cannotExecuteWhileFollowingIdList;
			int needFollowHeroineNum = GameDataManager.instance.sceneGarellyDataBase.sceneFlagNameData.scenarioFlagDataList.Find((ScenarioFlagData data) => data.scenarioName == scenarioName).needFollowHeroineNum;
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
				if (PlayerDataManager.DungeonHeroineFollowNum != needFollowHeroineNum)
				{
					switch (needFollowHeroineNum)
					{
					case 9:
						break;
					case 1:
						PlayerNonSaveDataManager.selectDisableMapPointTerm = "dialogWorldMapDisable_RequiredLucy";
						goto default;
					case 2:
						PlayerNonSaveDataManager.selectDisableMapPointTerm = "dialogWorldMapDisable_RequiredRina";
						goto default;
					case 3:
						PlayerNonSaveDataManager.selectDisableMapPointTerm = "dialogWorldMapDisable_RequiredShia";
						goto default;
					case 4:
						PlayerNonSaveDataManager.selectDisableMapPointTerm = "dialogWorldMapDisable_RequiredLevy";
						goto default;
					default:
						Debug.Log("要同行ヒロインがいない");
						OpenMapAlertDialog();
						return;
					}
				}
				Transition(stateLink);
			}
			else if (needFollowHeroineNum == 0 || needFollowHeroineNum == 9)
			{
				Debug.Log("ソロオンリー");
				Transition(stateLink);
			}
			else
			{
				PlayerNonSaveDataManager.selectDisableMapPointTerm = parameterContainer.GetString("disablePointTerm");
				Debug.Log("要同行ヒロインがいない");
				OpenMapAlertDialog();
			}
		}
		else if (parameterContainer.GetBool("isWorldMapPointDisable"))
		{
			PlayerNonSaveDataManager.selectDisableMapPointTerm = parameterContainer.GetString("disablePointTerm");
			Debug.Log("行けないポイント");
			OpenMapAlertDialog();
		}
		else
		{
			Debug.Log("シナリオなし");
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
		totalMapAccessManager.worldMapFSM.SendTrigger("WorldMapPointClickDisable");
		Transition(canNotFollowLink);
	}
}
