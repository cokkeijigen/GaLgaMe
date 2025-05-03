using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class FlashTouchMouthCumShot : StateBehaviour
{
	private SexTouchManager sexTouchManager;

	private SexTouchStatusManager sexTouchStatusManager;

	private SexTouchHeroineDataManager sexTouchHeroineDataManager;

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
		sexTouchManager.touchWhiteImageCanvasGroup.DOFade(1f, 0.1f).OnComplete(delegate
		{
			FlashFadeOut();
		});
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

	private void FlashFadeOut()
	{
		sexTouchManager.touchHeroineSpriteGo.GetComponent<SpriteRenderer>().sprite = sexTouchHeroineDataManager.sexTouchHeroineSpriteData.heroineCgCumShotList[0];
		MasterAudio.PlaySound("SexBattle_CumShot_Inside", 1f, null, 0f, null, null);
		sexTouchManager.touchWhiteImageCanvasGroup.DOFade(0f, 0.1f);
		Transition(stateLink);
	}
}
