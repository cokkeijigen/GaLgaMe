using System.Collections.Generic;
using Arbor;
using UnityEngine;
using UnityEngine.UI;
using Utage;

[AddComponentMenu("")]
public class ChatMaxHeightCheck : StateBehaviour
{
	private ChatWindowControl chatWindowControl;

	private AdvEngine advEngine;

	private GameObject chatMaster;

	public float masterHeight;

	public float checkHeight;

	public float limitHeight;

	public int enableListCount;

	public int textCount;

	public float doFadeTime;

	private List<AdvBacklog> backlogList = new List<AdvBacklog>();

	private List<AdvBacklog> restoreBacklogList = new List<AdvBacklog>();

	public int backlogCount;

	public StateLink stateLink;

	public override void OnStateAwake()
	{
		chatWindowControl = GetComponent<ChatWindowControl>();
		advEngine = chatWindowControl.advEngine;
		chatMaster = chatWindowControl.chatMaster;
		masterHeight = chatMaster.GetComponent<RectTransform>().sizeDelta.y;
		backlogList = chatWindowControl.backLogList;
		restoreBacklogList = chatWindowControl.restoreBackLogList;
	}

	public override void OnStateBegin()
	{
		checkHeight = 0f;
		enableListCount = 0;
		textCount = chatMaster.transform.childCount - 1;
		chatWindowControl.enableTextCharCount = 0;
		for (int i = 0; i < textCount; i++)
		{
			GameObject gameObject = chatMaster.transform.GetChild(i).gameObject;
			checkHeight = gameObject.GetComponent<RectTransform>().anchoredPosition.y - gameObject.GetComponent<RectTransform>().sizeDelta.y;
			chatWindowControl.chatGoList.Add(gameObject);
			chatWindowControl.chatMessageList.Add(gameObject.transform.Find("Text Group/Text Panel/MessageText").GetComponent<Text>().text);
			chatWindowControl.chatNameList.Add(gameObject.transform.Find("Text Group/Header Panel/NameText").GetComponent<Text>().text);
			if (checkHeight >= limitHeight)
			{
				enableListCount++;
			}
		}
		if (!chatWindowControl.isRecreate)
		{
			FirstBacklogRecreate();
		}
		chatWindowControl.DestroyChatPrefabs();
		BacklogRecreate();
		chatWindowControl.enableListCount = enableListCount;
		Debug.Log("表示テキスト数：" + enableListCount);
		chatWindowControl.loopCount = 0;
		chatWindowControl.isPrefabVisibleSkip = false;
		Transition(stateLink);
	}

	private void FirstBacklogRecreate()
	{
		advEngine.BacklogManager.Backlogs.Clear();
		backlogCount = restoreBacklogList.Count - textCount;
		List<AdvBacklog> list = new List<AdvBacklog>(restoreBacklogList);
		list.RemoveRange(backlogCount, textCount);
		for (int i = 0; i < list.Count; i++)
		{
			advEngine.BacklogManager.Backlogs.Add(list[i]);
		}
		chatWindowControl.isRecreate = true;
		Debug.Log("初回のバックログ整形作業終了");
	}

	private void BacklogRecreate()
	{
		Debug.Log("表示テキスト数" + enableListCount + "／元バックログ数" + backlogList.Count);
		for (int i = 0; i < enableListCount; i++)
		{
			advEngine.BacklogManager.Backlogs.Add(backlogList[0]);
			backlogList.RemoveAt(0);
		}
		Debug.Log("画面内のバックログを再生成");
		while (advEngine.BacklogManager.Backlogs.Count > 20)
		{
			advEngine.BacklogManager.Backlogs.RemoveAt(0);
		}
		Debug.Log("バックログを20個まで減らす");
	}
}
