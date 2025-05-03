using System.Collections.Generic;
using I2.Loc;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utage;

public class UtageMapSceneManager : MonoBehaviour
{
	private TotalMapAccessManager totalMapAccessManager;

	private LocalMapAccessManager localMapAccessManager;

	private HeaderStatusManager headerStatusManager;

	public CanvasGroup localMapCanvasGroup;

	public GameObject inDoorHomeCommandGroup;

	public List<GameObject> innGroupList;

	public List<GameObject> heroineGroupList;

	public List<GameObject> itemShopGroupList;

	public List<GameObject> hunterGuildGroupList;

	public List<GameObject> townGroupList;

	public List<GameObject> shopTownGroupList;

	public Localize followButtonTextLoc;

	public bool isFollowCancel;

	public GameObject talkSummaryWindow;

	public Localize talkSummaryTextLoc;

	public AdvEngine advEngine;

	private void Start()
	{
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		localMapAccessManager = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>();
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
	}

	public void ScenarioSceneLoad(AdvCommandSendMessageByName command)
	{
		PlayerNonSaveDataManager.selectScenarioName = command.ParseCellOptional(AdvColumnName.Arg3, "");
		PlayerNonSaveDataManager.loadSceneName = "scenario";
		PlayerNonSaveDataManager.currentSceneName = "main";
		GameObject.Find("Transition Manager").GetComponent<PlayMakerFSM>().SendEvent("StartFadeIn");
	}

	public void ResetLocalMapInPlaceFromUtage(AdvCommandSendMessageByName command)
	{
		int num = command.ParseCellOptional(AdvColumnName.Arg3, 0);
		totalMapAccessManager.ResetMapInPlace(num);
		Debug.Log("マップの数値を変更する：" + num);
	}

	public void OpenInDoorCommand(AdvCommandSendMessageByName command)
	{
		bool flag = command.ParseCellOptional(AdvColumnName.Arg3, defaultVal: false);
		string text = command.ParseCellOptional(AdvColumnName.Arg4, "");
		Debug.Log("インドアコマンドのアクティブ変更：" + flag);
		inDoorHomeCommandGroup.SetActive(flag);
		localMapCanvasGroup.interactable = !flag;
		foreach (GameObject innGroup in innGroupList)
		{
			innGroup.SetActive(value: false);
		}
		foreach (GameObject itemShopGroup in itemShopGroupList)
		{
			itemShopGroup.SetActive(value: false);
		}
		foreach (GameObject hunterGuildGroup in hunterGuildGroupList)
		{
			hunterGuildGroup.SetActive(value: false);
		}
		foreach (GameObject heroineGroup in heroineGroupList)
		{
			heroineGroup.SetActive(value: false);
		}
		foreach (GameObject townGroup in townGroupList)
		{
			townGroup.SetActive(value: false);
		}
		foreach (GameObject shopTownGroup in shopTownGroupList)
		{
			shopTownGroup.SetActive(value: false);
		}
		if (!flag)
		{
			return;
		}
		switch (text)
		{
		case "Inn":
		{
			foreach (GameObject innGroup2 in innGroupList)
			{
				innGroup2.SetActive(value: true);
			}
			break;
		}
		case "ItemShop":
		{
			foreach (GameObject itemShopGroup2 in itemShopGroupList)
			{
				itemShopGroup2.SetActive(value: true);
			}
			break;
		}
		case "HunterGuild":
		{
			foreach (GameObject hunterGuildGroup2 in hunterGuildGroupList)
			{
				hunterGuildGroup2.SetActive(value: true);
			}
			break;
		}
		case "Heroine":
			CheckCurrentHeroineIsDungeonFollow();
			{
				foreach (GameObject heroineGroup2 in heroineGroupList)
				{
					heroineGroup2.SetActive(value: true);
				}
				break;
			}
		case "Town":
		{
			foreach (GameObject townGroup2 in townGroupList)
			{
				townGroup2.SetActive(value: true);
			}
			break;
		}
		case "ShopTown":
		{
			foreach (GameObject shopTownGroup2 in shopTownGroupList)
			{
				shopTownGroup2.SetActive(value: true);
			}
			break;
		}
		}
	}

	private void CheckCurrentHeroineIsDungeonFollow()
	{
		if (PlayerDataManager.isDungeonHeroineFollow && PlayerNonSaveDataManager.inDoorHeroineNum == PlayerDataManager.DungeonHeroineFollowNum)
		{
			followButtonTextLoc.Term = "buttonFollowCancel";
			isFollowCancel = true;
		}
		else
		{
			followButtonTextLoc.Term = "buttonFollowRequest";
			isFollowCancel = false;
		}
		advEngine.Param.SetParameter("isFollowCancel", isFollowCancel);
	}

	public void LoadAdditiveScene(AdvCommandSendMessageByName command)
	{
		SceneManager.LoadSceneAsync(command.ParseCellOptional(AdvColumnName.Arg3, ""), LoadSceneMode.Additive);
		Debug.Log("シーン読み込み");
	}

	public void SetMenuButtonInteractable(AdvCommandSendMessageByName command)
	{
		bool flag = command.ParseCellOptional(AdvColumnName.Arg3, defaultVal: false);
		if (flag)
		{
			headerStatusManager.menuCanvasGroup.interactable = true;
			headerStatusManager.menuCanvasGroup.alpha = 1f;
		}
		else
		{
			headerStatusManager.menuCanvasGroup.interactable = false;
			headerStatusManager.menuCanvasGroup.alpha = 0.5f;
		}
		Debug.Log("メニューボタンのインタラクティブ：" + flag);
	}

	public void SetTalkUiSimple(AdvCommandSendMessageByName command)
	{
		bool active = command.ParseCellOptional(AdvColumnName.Arg3, defaultVal: false);
		headerStatusManager.menuCanvasGroup.gameObject.SetActive(active);
		headerStatusManager.partyGroupParent.gameObject.SetActive(active);
	}

	public void SetTalkSummaryVisible(AdvCommandSendMessageByName command)
	{
		bool active = command.ParseCellOptional(AdvColumnName.Arg3, defaultVal: false);
		string text = command.ParseCellOptional(AdvColumnName.Arg4, "");
		if (!(text == "scenario"))
		{
			if (text == "dungeon")
			{
				talkSummaryTextLoc.Term = "talkSummaryDungeon";
			}
		}
		else
		{
			talkSummaryTextLoc.Term = "talkSummaryScenario";
		}
		talkSummaryWindow.SetActive(active);
	}

	public void SetBlackImageVisible(AdvCommandSendMessageByName command)
	{
		bool active = command.ParseCellOptional(AdvColumnName.Arg3, defaultVal: false);
		totalMapAccessManager.blackImageGo.SetActive(active);
	}

	public void SetHeaderStatusCanvasAlpha(AdvCommandSendMessageByName command)
	{
		float alpha = command.ParseCellOptional(AdvColumnName.Arg3, 0f);
		headerStatusManager.headerStatusCanvasGroup.alpha = alpha;
	}
}
