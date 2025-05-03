using System.Collections.Generic;
using System.Linq;
using Arbor;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[AddComponentMenu("")]
public class AnimationSexBattleResultExp : StateBehaviour
{
	private SexBattleManager sexBattleManager;

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
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
		pointer = new PointerEventData(EventSystem.current);
	}

	public override void OnStateBegin()
	{
		checkCount = 0;
		endBool = false;
		sexBattleManager.expSliderList.Clear();
		sexBattleManager.lvTextList.Clear();
		tweenerList.Clear();
		checkBoolList.Clear();
		for (int i = 0; i < sexBattleManager.expPrefabSpawnGoList.Count; i++)
		{
			checkBoolList.Add(item: false);
		}
		foreach (Transform expPrefabSpawnGo in sexBattleManager.expPrefabSpawnGoList)
		{
			sexBattleManager.expSliderList.Add(expPrefabSpawnGo.Find("Exp Slider").GetComponent<Slider>());
			sexBattleManager.lvTextList.Add(expPrefabSpawnGo.Find("Lv Frame/Lv Text").GetComponent<TextMeshProUGUI>());
		}
		tweenerList.Add(sexBattleManager.expSliderList[0].DOValue(PlayerSexStatusDataManager.playerSexExp, animationTime).SetEase(Ease.Linear).OnComplete(delegate
		{
			checkCount++;
		}));
		tweenerList.Add(sexBattleManager.expSliderList[1].DOValue(PlayerSexStatusDataManager.heroineSexExp[PlayerNonSaveDataManager.selectSexBattleHeroineId - 1], animationTime).SetEase(Ease.Linear).OnComplete(delegate
		{
			checkCount++;
		}));
	}

	public override void OnStateEnd()
	{
		sexBattleManager.isResultAnimationEnd = true;
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
		else if (checkCount >= sexBattleManager.expPrefabSpawnGoList.Count && !endBool)
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
		sexBattleManager.expSliderList[0].value = PlayerSexStatusDataManager.playerSexExp;
		sexBattleManager.expSliderList[1].value = PlayerSexStatusDataManager.heroineSexExp[PlayerNonSaveDataManager.selectSexBattleHeroineId - 1];
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
