using System.Collections;
using System.Text.RegularExpressions;
using Arbor;
using DG.Tweening;
using PathologicalGames;
using UnityEngine;
using UnityEngine.UI;
using Utage;

[AddComponentMenu("")]
public class ChatEnablePrefabCreate : StateBehaviour
{
	private ChatWindowControl chatWindowControl;

	private ParameterContainer parameterContainer;

	private Transform poolTransform;

	private Text poolMessageText;

	public OutputSlotFloat outputTweenTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		chatWindowControl = GetComponent<ChatWindowControl>();
		parameterContainer = GetComponent<ParameterContainer>();
	}

	public override void OnStateBegin()
	{
		chatWindowControl.chatMaster.GetComponent<CanvasGroup>().alpha = 1f;
		Recreate();
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

	private void Recreate()
	{
		Debug.Log("Recreateループ開始");
		Debug.Log("ループの中：" + chatWindowControl.loopCount);
		poolTransform = PoolManager.Pools["ChatPool"].Spawn(chatWindowControl.chatPrefab, chatWindowControl.chatMaster.transform);
		poolTransform.localPosition = new Vector3(0f, 0f, 0f);
		poolTransform.localScale = new Vector3(1f, 1f, 1f);
		poolTransform.transform.SetAsLastSibling();
		chatWindowControl.messageTexts.transform.SetAsLastSibling();
		poolTransform.transform.Find("Text Group/Header Panel/NameText").GetComponent<Text>().text = chatWindowControl.chatNameList[0];
		poolMessageText = poolTransform.transform.Find("Text Group/Text Panel/MessageText").GetComponent<Text>();
		poolMessageText.text = chatWindowControl.chatMessageList[0];
		if (!chatWindowControl.isPrefabVisibleSkip)
		{
			string text = Regex.Replace(chatWindowControl.chatMessageList[0], "<[^>]*?>", string.Empty);
			chatWindowControl.enableTextCharCount += text.Length;
			float num = (float)text.Length * ((1f - PlayerOptionsDataManager.optionsTextSpeed) / 10f);
			Debug.Log("tweenTime：" + num);
			outputTweenTime.SetValue(num);
			poolMessageText.text = new string('\u3000', text.Length);
			Tweener tweener = poolMessageText.DOText(chatWindowControl.chatMessageList[0], num).SetEase(Ease.Linear);
			chatWindowControl.tweener = tweener;
		}
		if (chatWindowControl.loopCount == 0)
		{
			poolTransform.transform.Find("Text Group/Header Panel/Button Group").gameObject.SetActive(value: true);
			poolTransform.transform.Find("Text Group/Text Panel/MessageText/IconRoot").gameObject.SetActive(value: false);
			Button component = poolTransform.transform.Find("Text Group/Header Panel/Button Group/BackLogButton").GetComponent<Button>();
			component.interactable = false;
			chatWindowControl.fullBacklogButton = component;
		}
		else if (chatWindowControl.loopCount == chatWindowControl.enableListCount - 1)
		{
			poolTransform.transform.Find("Text Group/Header Panel/Button Group").gameObject.SetActive(value: false);
			poolTransform.transform.Find("Text Group/Text Panel/MessageText/IconRoot").gameObject.SetActive(value: true);
		}
		else
		{
			poolTransform.transform.Find("Text Group/Header Panel/Button Group").gameObject.SetActive(value: false);
			poolTransform.transform.Find("Text Group/Text Panel/MessageText/IconRoot").gameObject.SetActive(value: false);
		}
		Image component2 = poolTransform.transform.Find("Text Group").GetComponent<Image>();
		VerticalLayoutGroup component3 = poolTransform.transform.Find("Text Group").GetComponent<VerticalLayoutGroup>();
		if (chatWindowControl.chatNameList[0] == "")
		{
			component2.sprite = chatWindowControl.frameSprite2;
			component2.color = new Color(0f, 0f, 0f, 0.65f);
			if (chatWindowControl.loopCount == 0)
			{
				component3.padding.top = 24;
				component3.spacing = 20f;
			}
			else
			{
				component3.padding.top = 0;
				component3.spacing = 0f;
			}
		}
		else
		{
			component2.sprite = chatWindowControl.frameSprite1;
			component2.color = new Color(0f, 0f, 0f, 0.65f);
			component3.padding.top = 24;
			component3.spacing = 20f;
		}
		chatWindowControl.chatMessageList.RemoveAt(0);
		chatWindowControl.chatNameList.RemoveAt(0);
		poolTransform.GetComponent<CanvasGroup>().alpha = 1f;
		chatWindowControl.loopCount++;
		Transition(stateLink);
	}

	private IEnumerator AnimationChatText()
	{
		string[] array = chatWindowControl.chatMessageList[0].Split(' ');
		_ = Regex.Replace(chatWindowControl.chatMessageList[0], "<[^>]*?>", string.Empty).Length;
		_ = (1f - PlayerOptionsDataManager.optionsTextSpeed) / 10f;
		string[] array2 = array;
		foreach (string text in array2)
		{
			poolMessageText.text += text;
			poolMessageText.GetComponent<UguiNovelText>().SetAllDirty();
			yield return new WaitForSeconds(1f);
		}
	}

	private void SetChatText()
	{
		poolMessageText.GetComponent<UguiNovelText>().SetAllDirty();
	}
}
