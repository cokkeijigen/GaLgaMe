using System.Collections.Generic;
using System.Linq;
using Arbor;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[AddComponentMenu("")]
public class AnimationSexTouchResultExp : StateBehaviour
{
	private SexTouchManager sexTouchManager;

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
		sexTouchManager = GameObject.Find("Sex Touch Manager").GetComponent<SexTouchManager>();
		pointer = new PointerEventData(EventSystem.current);
	}

	public override void OnStateBegin()
	{
		checkCount = 0;
		endBool = false;
		sexTouchManager.touchExpSliderList.Clear();
		sexTouchManager.touchLvTextList.Clear();
		tweenerList.Clear();
		checkBoolList.Clear();
		for (int i = 0; i < sexTouchManager.touchExpPrefabSpawnGoList.Count; i++)
		{
			checkBoolList.Add(item: false);
		}
		foreach (Transform touchExpPrefabSpawnGo in sexTouchManager.touchExpPrefabSpawnGoList)
		{
			sexTouchManager.touchExpSliderList.Add(touchExpPrefabSpawnGo.Find("Exp Slider").GetComponent<Slider>());
			sexTouchManager.touchLvTextList.Add(touchExpPrefabSpawnGo.Find("Lv Frame/Lv Text").GetComponent<TextMeshProUGUI>());
		}
		tweenerList.Add(sexTouchManager.touchExpSliderList[0].DOValue(PlayerSexStatusDataManager.playerSexExp, animationTime).SetEase(Ease.Linear).OnComplete(delegate
		{
			checkCount++;
		}));
		tweenerList.Add(sexTouchManager.touchExpSliderList[1].DOValue(PlayerSexStatusDataManager.heroineSexExp[PlayerNonSaveDataManager.selectSexBattleHeroineId - 1], animationTime).SetEase(Ease.Linear).OnComplete(delegate
		{
			checkCount++;
		}));
	}

	public override void OnStateEnd()
	{
		sexTouchManager.isTouchResultAnimationEnd = true;
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
		else if (checkCount >= sexTouchManager.touchExpPrefabSpawnGoList.Count && !endBool)
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
		sexTouchManager.touchExpSliderList[0].value = PlayerSexStatusDataManager.playerSexExp;
		sexTouchManager.touchExpSliderList[1].value = PlayerSexStatusDataManager.heroineSexExp[PlayerNonSaveDataManager.selectSexBattleHeroineId - 1];
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
