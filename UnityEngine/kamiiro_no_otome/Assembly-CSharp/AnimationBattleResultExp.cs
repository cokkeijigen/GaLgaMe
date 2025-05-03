using System.Collections.Generic;
using System.Linq;
using Arbor;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[AddComponentMenu("")]
public class AnimationBattleResultExp : StateBehaviour
{
	private ResultDialogManager resultDialogManager;

	private PointerEventData pointer;

	public float animationTime;

	[SerializeField]
	private bool endBool;

	public List<bool> checkBoolList;

	public int checkCount;

	private List<Tweener> tweenerList = new List<Tweener>();

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		resultDialogManager = GetComponent<ResultDialogManager>();
		pointer = new PointerEventData(EventSystem.current);
	}

	public override void OnStateBegin()
	{
		checkCount = 0;
		endBool = false;
		resultDialogManager.expSliderList.Clear();
		resultDialogManager.lvTextList.Clear();
		tweenerList.Clear();
		checkBoolList.Clear();
		for (int i = 0; i < resultDialogManager.characterImageSpawnGoList.Count; i++)
		{
			checkBoolList.Add(item: false);
		}
		foreach (GameObject characterImageSpawnGo in resultDialogManager.characterImageSpawnGoList)
		{
			resultDialogManager.expSliderList.Add(characterImageSpawnGo.transform.Find("Exp Slider").GetComponent<Slider>());
			resultDialogManager.lvTextList.Add(characterImageSpawnGo.transform.Find("Lv Frame/Lv Text").GetComponent<TextMeshProUGUI>());
		}
		float duration = animationTime;
		if (PlayerNonSaveDataManager.battleResultDialogType == "dungeonBattle")
		{
			switch (PlayerDataManager.dungeonBattleSpeed)
			{
			case 1:
				duration = 0.75f;
				break;
			case 2:
				duration = 0.25f;
				break;
			case 4:
				duration = 0.5f;
				break;
			default:
				duration = 0.75f;
				break;
			}
		}
		for (int j = 0; j < resultDialogManager.expSliderList.Count; j++)
		{
			tweenerList.Add(resultDialogManager.expSliderList[j].DOValue(PlayerStatusDataManager.characterExp[PlayerStatusDataManager.playerPartyMember[j]], duration).SetEase(Ease.Linear).OnComplete(delegate
			{
				checkCount++;
			}));
		}
	}

	public override void OnStateEnd()
	{
		resultDialogManager.isResultAnimationEnd = true;
	}

	public override void OnStateUpdate()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			List<RaycastResult> list = new List<RaycastResult>();
			pointer.position = Input.mousePosition;
			EventSystem.current.RaycastAll(pointer, list);
			bool flag = CheckClickIsToggle(list);
			Debug.Log("ボタンをクリックした：" + flag);
			if (!flag && !endBool)
			{
				endBool = true;
				EndAnimation();
			}
		}
		else if (checkCount >= PlayerStatusDataManager.playerPartyMember.Length && !endBool)
		{
			endBool = true;
			EndAnimation();
		}
	}

	public override void OnStateLateUpdate()
	{
	}

	private void EndAnimation()
	{
		Debug.Log("アニメ終了");
		foreach (Tweener tweener in tweenerList)
		{
			tweener.Complete();
		}
		for (int i = 0; i < resultDialogManager.expSliderList.Count; i++)
		{
			resultDialogManager.expSliderList[i].value = PlayerStatusDataManager.characterExp[PlayerStatusDataManager.playerPartyMember[i]];
		}
		Transition(stateLink);
	}

	private bool CheckClickIsToggle(List<RaycastResult> results)
	{
		bool result = false;
		for (int i = 0; i < results.Count(); i++)
		{
			if (results[i].gameObject.GetComponent<Toggle>() != null)
			{
				result = true;
				break;
			}
		}
		return result;
	}
}
