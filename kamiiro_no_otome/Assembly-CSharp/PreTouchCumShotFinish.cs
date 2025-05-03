using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class PreTouchCumShotFinish : StateBehaviour
{
	private SexTouchManager sexTouchManager;

	private SexTouchStatusManager sexTouchStatusManager;

	private SexTouchHeroineDataManager sexTouchHeroineDataManager;

	public float fadeTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexTouchManager = GameObject.Find("Sex Touch Manager").GetComponent<SexTouchManager>();
		sexTouchStatusManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchStatusManager>();
		sexTouchHeroineDataManager = GameObject.Find("SexTouch Heroine Manager").GetComponent<SexTouchHeroineDataManager>();
	}

	public override void OnStateBegin()
	{
		sexTouchManager.touchHeroineBeforeSprite.sprite = sexTouchManager.touchHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite;
		sexTouchManager.touchHeroineBeforeSprite.color = Color.white;
		if (sexTouchStatusManager.isCumShotFace)
		{
			sexTouchManager.touchHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexTouchHeroineDataManager.sexTouchHeroineSpriteData.heroineCgFaceShotList[0];
		}
		else
		{
			sexTouchManager.touchHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexTouchHeroineDataManager.sexTouchHeroineSpriteData.heroineCgCumShotList[3];
		}
		sexTouchManager.touchHeroineBeforeSprite.DOFade(0f, fadeTime).OnComplete(InvokeMethod);
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

	private void InvokeMethod()
	{
		Transition(stateLink);
	}
}
