using Arbor;
using I2.Loc;
using PathologicalGames;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class HelpDataManager : SerializedMonoBehaviour
{
	public ArborFSM helpFSM;

	public GameObject[] helpTypeTabGoArray;

	public Sprite[] helpTypeTabSpriteArray;

	public GameObject helpScrollContentGo;

	public Transform helpScrollPrefabGo;

	public Transform helpPoolParent;

	public Sprite[] helpScrollContentSpriteArray;

	public Scrollbar helpScrollBar;

	public Image helpInfoImage;

	public Localize helpInfoHeaderTextLoc;

	public Localize helpInfoTextLoc;

	public Scrollbar helpInfoScrollBar;

	public int selectTabTypeNum;

	public int selectScrollContentIndex;

	public int clickedHelpID;

	public HelpData selectHelpData;

	public void DespawnHelpScrollContent()
	{
		if (helpScrollContentGo.transform.childCount > 0)
		{
			Transform[] array = new Transform[helpScrollContentGo.transform.childCount];
			for (int i = 0; i < helpScrollContentGo.transform.childCount; i++)
			{
				array[i] = helpScrollContentGo.transform.GetChild(i);
			}
			Transform[] array2 = array;
			foreach (Transform instance in array2)
			{
				PoolManager.Pools["helpPool"].Despawn(instance, 0f, helpPoolParent);
			}
		}
	}

	public void PushTypeTab(int index)
	{
		selectScrollContentIndex = 0;
		selectTabTypeNum = index;
		helpFSM.SendTrigger("PushTypeTab");
	}

	public void SetSelectHelpData(int sortID)
	{
		clickedHelpID = sortID;
		switch (selectTabTypeNum)
		{
		case 0:
			selectHelpData = GameDataManager.instance.helpDataBase.helpCarriageList.Find((HelpData data) => data.sortID == clickedHelpID);
			break;
		case 1:
			selectHelpData = GameDataManager.instance.helpDataBase.helpCommandBattleList.Find((HelpData data) => data.sortID == clickedHelpID);
			break;
		case 2:
			selectHelpData = GameDataManager.instance.helpDataBase.helpDungeonList.Find((HelpData data) => data.sortID == clickedHelpID);
			break;
		case 3:
			selectHelpData = GameDataManager.instance.helpDataBase.helpSurveyList.Find((HelpData data) => data.sortID == clickedHelpID);
			break;
		case 4:
			selectHelpData = GameDataManager.instance.helpDataBase.helpSexBattleList.Find((HelpData data) => data.sortID == clickedHelpID);
			break;
		case 5:
			selectHelpData = GameDataManager.instance.helpDataBase.helpMapList.Find((HelpData data) => data.sortID == clickedHelpID);
			break;
		case 6:
			selectHelpData = GameDataManager.instance.helpDataBase.helpStatusList.Find((HelpData data) => data.sortID == clickedHelpID);
			break;
		}
		Debug.Log("ヘルプデータを代入");
	}
}
