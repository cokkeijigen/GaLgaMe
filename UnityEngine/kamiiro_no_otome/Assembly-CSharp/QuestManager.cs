using System.Collections.Generic;
using Arbor;
using I2.Loc;
using PathologicalGames;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : SerializedMonoBehaviour
{
	private LocalMapAccessManager localMapAccessManager;

	public QuestApplyManager questApplyManager;

	public ArborFSM questFSM;

	public GameObject exitButtonGo;

	public CanvasGroup questCanvasGroup;

	public GameObject[] questTypeTabGoArray;

	public Sprite[] questTypeTabSpriteArray;

	public GameObject newStoryQuestBalloonGo;

	public GameObject newStorySubQuestBalloonGo;

	public GameObject questScrollContentGo;

	public Transform questScrollPrefabGo;

	public Transform clearedQuestScrollPrefabGo;

	public Transform qustPoolParent;

	public Sprite[] questScrollContentSpriteArray;

	public Scrollbar questScrollBar;

	public GameObject questInfoFrameGo;

	public Localize questInfoTextLoc;

	public Scrollbar questInfoScrollBar;

	public GameObject questNotSelectFrameGo;

	public GameObject requirementFrameGo;

	public Image questRequireItemImage;

	public Image questRequireEnemyImage;

	public Localize questRequireNameTextLoc;

	public Localize questRequireTypeTextLoc;

	public Localize questRequireCurrentTextLoc;

	public Text questRequireNeedCountText;

	public Text questRequireCurrentCountText;

	public Dictionary<string, Sprite> questRequireImageDictionary = new Dictionary<string, Sprite>();

	public GameObject rewardFrameGo;

	public GameObject questRewardContentGo;

	public Transform questRewardPrefabGo;

	public GameObject questApplyButtonGo;

	public Localize questApplyButtonTextLoc;

	public Image questCharacterImage;

	public Localize questCharacterTextLoc;

	public Dictionary<string, Sprite> questCharacterImageDictionary = new Dictionary<string, Sprite>();

	public int selectTabTypeNum;

	public int selectScrollContentIndex;

	public int clickedQuestID;

	public bool isQuestClearApplyButton;

	public bool isQuestCleared;

	private void Awake()
	{
		if (PlayerDataManager.mapPlaceStatusNum != 3)
		{
			localMapAccessManager = GameObject.Find("LocalMap Access Manager").GetComponent<LocalMapAccessManager>();
		}
	}

	private void Start()
	{
		if (PlayerDataManager.mapPlaceStatusNum != 3)
		{
			localMapAccessManager.localMapExitFSM.gameObject.SetActive(value: false);
		}
	}

	public void PushQuestTypeTab(int index)
	{
		selectTabTypeNum = index;
		selectScrollContentIndex = 0;
		questFSM.SendTrigger("PushTypeTab");
	}

	public void ResetScrollViewContents(string contentType)
	{
		int num = 0;
		Transform transform = null;
		switch (contentType)
		{
		case "select":
			num = questScrollContentGo.transform.childCount;
			transform = questScrollContentGo.transform;
			break;
		case "reward":
			num = questRewardContentGo.transform.childCount;
			transform = questRewardContentGo.transform;
			break;
		case "clear":
			num = questApplyManager.rewardContents.childCount;
			transform = questApplyManager.rewardContents.transform;
			break;
		}
		if (num == 0)
		{
			return;
		}
		Transform[] array = new Transform[num];
		for (int i = 0; i < num; i++)
		{
			array[i] = transform.GetChild(i);
		}
		if (array.Length != 0 && array != null)
		{
			for (int j = 0; j < num; j++)
			{
				PoolManager.Pools["questPool"].Despawn(array[j], 0f, qustPoolParent);
			}
		}
	}
}
