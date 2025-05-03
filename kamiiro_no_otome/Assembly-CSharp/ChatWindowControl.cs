using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Arbor;
using DG.Tweening;
using PathologicalGames;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Utage;

public class ChatWindowControl : SerializedMonoBehaviour
{
	public AdvEngine advEngine;

	public ArborFSM arborFSM;

	public Transform messageWindowManager;

	public GameObject chatPrefab;

	public GameObject chatMaster;

	public GameObject windowTopImageGO;

	public Text windowTopNameText;

	public RectTransform messageTexts;

	public Text chatText;

	public Text chatName;

	public GameObject poolParent;

	public Button fullBacklogButton;

	public GameObject backLogWindowGo;

	public Sprite frameSprite1;

	public Sprite frameSprite2;

	private Transform poolTransform;

	public bool isFullMode;

	public bool isPause;

	public bool isEndText;

	public bool isBackLogVisible;

	public bool isPushUtageButton;

	public bool isRecreate;

	public bool isChatCreating;

	public int enableTextCharCount;

	public int enableListCount;

	public int loopCount;

	private Text poolMessageText;

	public float tweenTime;

	public bool isPrefabVisibleSkip;

	public Tweener tweener;

	public List<GameObject> chatGoList = new List<GameObject>();

	public List<string> chatMessageList = new List<string>();

	public List<string> chatNameList = new List<string>();

	public List<Sprite> faceList = new List<Sprite>();

	public List<AdvBacklog> backLogList = new List<AdvBacklog>();

	public List<AdvBacklog> restoreBackLogList = new List<AdvBacklog>();

	public int backlogCount;

	private void Update()
	{
	}

	public void UtageToIsFull(AdvCommandSendMessageByName command)
	{
		isFullMode = command.ParseCellOptional(AdvColumnName.Arg3, defaultVal: false);
		Debug.Log("FULLmode：" + isFullMode);
	}

	public void UtageFullWaitStart()
	{
		Canvas.ForceUpdateCanvases();
		arborFSM.SendTrigger("ChatWaitStart");
		Debug.Log("ChatWaitStart");
	}

	private IEnumerator SendFullWaitStart()
	{
		yield return null;
		Canvas.ForceUpdateCanvases();
		arborFSM.SendTrigger("ChatWaitStart");
		Debug.Log("ChatWaitStart");
	}

	public void BackLogVisible(bool value)
	{
		isBackLogVisible = value;
	}

	public void UtageScrollCloseBackLog()
	{
		isBackLogVisible = false;
		isPushUtageButton = false;
	}

	public void AddBackLog()
	{
		int count = advEngine.BacklogManager.Backlogs.Count;
		restoreBackLogList.Add(advEngine.BacklogManager.Backlogs[count - 1]);
	}

	public void CreateFullModeBacklog()
	{
		backLogList.Clear();
		int count = advEngine.BacklogManager.Backlogs.Count;
		int num = chatMaster.transform.childCount - 1;
		int num2 = advEngine.BacklogManager.Backlogs.Count - num;
		Debug.Log("エンジンのログ総数は：" + count + "／テキストの数：" + num + "／差の数のIndex：" + num2);
		for (int i = 0; i < num; i++)
		{
			backLogList.Add(advEngine.BacklogManager.Backlogs[num2 + i]);
		}
	}

	public void RestoreBackLogReset()
	{
		while (restoreBackLogList.Count > 20)
		{
			restoreBackLogList.RemoveAt(0);
		}
		Debug.Log("リストア用のバックログをリセット");
	}

	public void SetMessageWindowImage()
	{
		if (windowTopNameText.text == "")
		{
			windowTopImageGO.GetComponent<Image>().sprite = frameSprite2;
		}
		else
		{
			windowTopImageGO.GetComponent<Image>().sprite = frameSprite1;
		}
	}

	public void CreateChatPrefabs()
	{
		poolTransform = PoolManager.Pools["ChatPool"].Spawn(chatPrefab, chatMaster.transform);
		poolTransform.localPosition = new Vector3(0f, 0f, 0f);
		poolTransform.localScale = new Vector3(1f, 1f, 1f);
		poolTransform.transform.SetAsLastSibling();
		messageTexts.transform.SetAsLastSibling();
		poolTransform.GetComponent<CanvasGroup>().alpha = 0f;
		poolTransform.transform.Find("Text Group/Header Panel/NameText").GetComponent<Text>().text = chatName.text;
		poolTransform.transform.Find("Text Group/Text Panel/MessageText").GetComponent<Text>().text = chatText.text;
		Debug.Log("チャットPrefab作成");
	}

	public void DestroyChatPrefabs()
	{
		GameObject[] array = GameObject.FindGameObjectsWithTag("ChatText");
		foreach (GameObject gameObject in array)
		{
			gameObject.GetComponent<CanvasGroup>().alpha = 0f;
			PoolManager.Pools["ChatPool"].Despawn(gameObject.transform, 0f, poolParent.transform);
		}
		Debug.Log("チャットPrefabを全て削除");
	}

	public void RecreateChatPrefabs(int count)
	{
		chatMaster.GetComponent<CanvasGroup>().alpha = 0f;
		Debug.Log("Recreate開始");
		for (int i = 0; i < count; i++)
		{
			Debug.Log("forの中：" + i);
			poolTransform = PoolManager.Pools["ChatPool"].Spawn(chatPrefab, chatMaster.transform);
			poolTransform.localPosition = new Vector3(0f, 0f, 0f);
			poolTransform.localScale = new Vector3(1f, 1f, 1f);
			poolTransform.transform.SetAsLastSibling();
			messageTexts.transform.SetAsLastSibling();
			poolTransform.transform.Find("Text Group/Header Panel/NameText").GetComponent<Text>().text = chatNameList[0];
			poolMessageText = poolTransform.transform.Find("Text Group/Text Panel/MessageText").GetComponent<Text>();
			poolMessageText.text = chatMessageList[0];
			tweenTime = 0f;
			if (i == 0)
			{
				poolTransform.transform.Find("Text Group/Header Panel/Button Group").gameObject.SetActive(value: true);
				poolTransform.transform.Find("Text Group/Text Panel/MessageText/IconRoot").gameObject.SetActive(value: false);
			}
			else if (i == count - 1)
			{
				poolTransform.transform.Find("Text Group/Header Panel/Button Group").gameObject.SetActive(value: false);
				poolTransform.transform.Find("Text Group/Text Panel/MessageText/IconRoot").gameObject.SetActive(value: true);
			}
			else
			{
				poolTransform.transform.Find("Text Group/Header Panel/Button Group").gameObject.SetActive(value: false);
				poolTransform.transform.Find("Text Group/Text Panel/MessageText/IconRoot").gameObject.SetActive(value: false);
			}
			Image component = poolTransform.transform.Find("Text Group").GetComponent<Image>();
			VerticalLayoutGroup component2 = poolTransform.transform.Find("Text Group").GetComponent<VerticalLayoutGroup>();
			if (chatNameList[0] == "")
			{
				component.sprite = frameSprite2;
				component.color = new Color(0f, 0f, 0f, 0.65f);
				if (i == 0)
				{
					component2.padding.top = 25;
					component2.spacing = 20f;
				}
				else
				{
					component2.padding.top = 0;
					component2.spacing = 0f;
				}
			}
			else
			{
				component.sprite = frameSprite1;
				component.color = new Color(0f, 0f, 0f, 0.65f);
				component2.padding.top = 25;
				component2.spacing = 20f;
			}
			chatMessageList.RemoveAt(0);
			chatNameList.RemoveAt(0);
			poolTransform.GetComponent<CanvasGroup>().alpha = 1f;
		}
		Debug.Log("チャット用のPrefabを生成");
	}

	private IEnumerator Recreate(int count, float alpha)
	{
		Debug.Log("Recreate開始");
		for (int i = 0; i < count; i++)
		{
			Debug.Log("forの中：" + i);
			poolTransform = PoolManager.Pools["ChatPool"].Spawn(chatPrefab, chatMaster.transform);
			poolTransform.localPosition = new Vector3(0f, 0f, 0f);
			poolTransform.localScale = new Vector3(1f, 1f, 1f);
			poolTransform.transform.SetAsLastSibling();
			messageTexts.transform.SetAsLastSibling();
			poolTransform.transform.Find("Text Group/Header Panel/NameText").GetComponent<Text>().text = chatNameList[0];
			poolMessageText = poolTransform.transform.Find("Text Group/Text Panel/MessageText").GetComponent<Text>();
			poolMessageText.text = chatMessageList[0];
			if (!isPrefabVisibleSkip)
			{
				string text = Regex.Replace(chatMessageList[0], "<[^>]*?>", string.Empty);
				tweenTime = (float)text.Length * 0.05f;
				poolMessageText.text = new string('\u3000', text.Length);
				tweener = poolMessageText.DOText(chatMessageList[0], tweenTime).SetEase(Ease.Linear);
			}
			else
			{
				tweenTime = 0f;
			}
			if (i == 0)
			{
				poolTransform.transform.Find("Text Group/Header Panel/Button Group").gameObject.SetActive(value: true);
				poolTransform.transform.Find("Text Group/Text Panel/MessageText/IconRoot").gameObject.SetActive(value: false);
			}
			else if (i == count - 1)
			{
				poolTransform.transform.Find("Text Group/Header Panel/Button Group").gameObject.SetActive(value: false);
				poolTransform.transform.Find("Text Group/Text Panel/MessageText/IconRoot").gameObject.SetActive(value: true);
			}
			else
			{
				poolTransform.transform.Find("Text Group/Header Panel/Button Group").gameObject.SetActive(value: false);
				poolTransform.transform.Find("Text Group/Text Panel/MessageText/IconRoot").gameObject.SetActive(value: false);
			}
			Image component = poolTransform.transform.Find("Text Group").GetComponent<Image>();
			VerticalLayoutGroup component2 = poolTransform.transform.Find("Text Group").GetComponent<VerticalLayoutGroup>();
			if (chatNameList[0] == "")
			{
				component.sprite = frameSprite2;
				component.color = new Color(0f, 0f, 0f, 0.65f);
				if (i == 0)
				{
					component2.padding.top = 25;
					component2.spacing = 20f;
				}
				else
				{
					component2.padding.top = 0;
					component2.spacing = 0f;
				}
			}
			else
			{
				component.sprite = frameSprite1;
				component.color = new Color(0f, 0f, 0f, 0.65f);
				component2.padding.top = 25;
				component2.spacing = 20f;
			}
			chatMessageList.RemoveAt(0);
			chatNameList.RemoveAt(0);
			if (alpha >= 1f && !isPrefabVisibleSkip)
			{
				poolTransform.GetComponent<CanvasGroup>().alpha = 1f;
				yield return new WaitForSeconds(tweenTime + 0.1f);
			}
			else if (alpha >= 1f && isPrefabVisibleSkip)
			{
				poolTransform.GetComponent<CanvasGroup>().alpha = 1f;
				yield return null;
			}
		}
		if (alpha >= 1f)
		{
			arborFSM.SendTrigger("RecreateEnd");
			Debug.Log("画面に収まるPrefabを生成");
			Debug.Log("Recreateトリガー送信");
		}
		else
		{
			Debug.Log("チャット用のPrefabを生成");
		}
	}
}
