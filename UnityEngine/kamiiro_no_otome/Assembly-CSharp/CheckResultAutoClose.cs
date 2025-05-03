using Arbor;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class CheckResultAutoClose : StateBehaviour
{
	private ResultDialogManager resultDialogManager;

	private Tweener tweener;

	public StateLink autoLink;

	public StateLink normalLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		resultDialogManager = GetComponent<ResultDialogManager>();
	}

	public override void OnStateBegin()
	{
		if (PlayerDataManager.isResultAutoClose)
		{
			SetResultAutoClose();
		}
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (!PlayerDataManager.isResultAutoClose)
		{
			tweener.Kill();
			resultDialogManager.autoCloseGroupGo.SetActive(value: false);
			Transition(normalLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}

	private void SetResultAutoClose()
	{
		float num = 1f;
		switch (PlayerDataManager.dungeonBattleSpeed)
		{
		case 1:
			num = 1f;
			break;
		case 2:
			num = 0.9f;
			break;
		case 4:
			num = 0.8f;
			break;
		default:
			num = 1f;
			break;
		}
		resultDialogManager.autoCloseGroupGo.SetActive(value: true);
		resultDialogManager.autoCloseImageFill.fillAmount = 0f;
		tweener = resultDialogManager.autoCloseImageFill.DOFillAmount(1f, num).SetEase(Ease.Linear).OnComplete(delegate
		{
			resultDialogManager.autoCloseGroupGo.SetActive(value: false);
			Transition(autoLink);
		});
	}
}
