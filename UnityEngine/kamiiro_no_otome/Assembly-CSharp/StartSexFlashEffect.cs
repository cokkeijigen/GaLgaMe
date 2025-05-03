using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class StartSexFlashEffect : StateBehaviour
{
	private SexBattleManager sexBattleManager;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		sexBattleManager = GameObject.Find("Sex Battle Manager").GetComponent<SexBattleManager>();
	}

	public override void OnStateBegin()
	{
		sexBattleManager.whiteImageCanvasGroup.DOFade(1f, 0.1f).SetEase(Ease.Linear).OnComplete(delegate
		{
			InvokeMethod();
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

	private void InvokeMethod()
	{
		sexBattleManager.whiteImageCanvasGroup.DOFade(0f, 0.1f).SetEase(Ease.Linear).OnComplete(delegate
		{
			InvokeMethod2();
		});
	}

	private void InvokeMethod2()
	{
		Transition(stateLink);
	}
}
