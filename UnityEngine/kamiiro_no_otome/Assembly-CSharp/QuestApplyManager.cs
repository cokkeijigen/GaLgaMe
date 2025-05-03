using Arbor;
using Coffee.UIExtensions;
using DarkTonic.MasterAudio;
using I2.Loc;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class QuestApplyManager : SerializedMonoBehaviour
{
	public QuestManager questManager;

	public ArborFSM applyFSM;

	public GameObject dialogCanvasGo;

	public Image questTypeImage;

	public Transform rewardContents;

	public GameObject levelUpFrameGo;

	public Localize levelUpTypeTextLoc;

	public Text beforeLvNumText;

	public Text afterLvNumText;

	public Transform questPoolParent;

	public UIParticle uIParticle;

	public Transform questClearPrefabGo;

	public Transform questClearSpawnGo;

	public UIParticle uIParticle_levelUp;

	public Transform questLevelUpPrefabGo;

	public Transform questLevelUpSpawnGo;

	public GameObject questNoticeCanvasGo;

	public GameObject mapNoticeGroup;

	public GameObject itemNoticeGroup;

	public GameObject rareItemNoticeGroup;

	public Localize dungeonNameTextLoc;

	public bool isRewardRareItem;

	private void Awake()
	{
		dialogCanvasGo.SetActive(value: false);
	}

	public void PushQuestDialogOkButton()
	{
		applyFSM.SendTrigger("PushQuestDialogOkButton");
	}

	public void PushNoticeDialogOkButton()
	{
		questNoticeCanvasGo.SetActive(value: false);
		applyFSM.SendTrigger("PushNoticeDialogOkButton");
	}

	public void OpenQuestNoticeCanvas(string type, string dungeonName)
	{
		switch (type)
		{
		case "dungeon":
			dungeonNameTextLoc.Term = dungeonName;
			mapNoticeGroup.SetActive(value: true);
			itemNoticeGroup.SetActive(value: false);
			rareItemNoticeGroup.SetActive(value: false);
			break;
		case "recipe":
			mapNoticeGroup.SetActive(value: false);
			itemNoticeGroup.SetActive(value: true);
			rareItemNoticeGroup.SetActive(value: false);
			break;
		case "rare":
			mapNoticeGroup.SetActive(value: false);
			itemNoticeGroup.SetActive(value: false);
			rareItemNoticeGroup.SetActive(value: true);
			break;
		}
		MasterAudio.PlaySound("SeNotice", 1f, null, 0f, null, null);
		questNoticeCanvasGo.SetActive(value: true);
	}
}
