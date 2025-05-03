using DarkTonic.MasterAudio;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;
using Utage;

public class ScenarioNoticeManager : MonoBehaviour
{
	public UtageAddSceneManager utageAddSceneManager;

	public GameObject scenarioNoticeCanvas;

	public GameObject craftTextGroup;

	public GameObject eventItemTextGroup;

	public Image eventItemIconImage;

	public Localize scenarioNoticeTextLoc1;

	public Localize scenarioNoticeTextLoc2;

	public void OpenScenarioNotice()
	{
		MasterAudio.PlaySound("SeNotice", 1f, null, 0f, null, null);
		craftTextGroup.SetActive(value: true);
		eventItemTextGroup.SetActive(value: false);
		scenarioNoticeCanvas.SetActive(value: true);
	}

	public void OpenScenarioNoticeWithEventItem(AdvCommandSendMessageByName command)
	{
		int itemId = command.ParseCellOptional(AdvColumnName.Arg3, 0);
		scenarioNoticeTextLoc1.Term = "eventItem" + itemId;
		Sprite itemSprite = GameDataManager.instance.itemEventItemDataBase.itemEventItemDataList.Find((ItemEventItemData data) => data.itemID == itemId).itemSprite;
		eventItemIconImage.sprite = itemSprite;
		MasterAudio.PlaySound("SeNotice", 1f, null, 0f, null, null);
		craftTextGroup.SetActive(value: false);
		eventItemTextGroup.SetActive(value: true);
		scenarioNoticeCanvas.SetActive(value: true);
	}

	public void CloseScenarioNotice()
	{
		scenarioNoticeCanvas.SetActive(value: false);
		utageAddSceneManager.advEngine.ResumeScenario();
	}
}
