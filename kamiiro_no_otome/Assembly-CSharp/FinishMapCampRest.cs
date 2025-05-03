using Arbor;
using DarkTonic.MasterAudio;
using DG.Tweening;
using UnityEngine;

[AddComponentMenu("")]
public class FinishMapCampRest : StateBehaviour
{
	private MapCampManager mapCampManager;

	private TotalMapAccessManager totalMapAccessManager;

	private HeaderStatusManager headerStatusManager;

	public float fadeTime;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		mapCampManager = GameObject.Find("Map Camp Manager").GetComponent<MapCampManager>();
		totalMapAccessManager = GameObject.Find("TotalMap Access Manager").GetComponent<TotalMapAccessManager>();
		headerStatusManager = GameObject.Find("Header Status Manager").GetComponent<HeaderStatusManager>();
	}

	public override void OnStateBegin()
	{
		MasterAudio.FadeAllPlaylistsToVolume(PlayerOptionsDataManager.optionsBgmVolume, 0.3f);
		totalMapAccessManager.totalMapFSM.SendTrigger("CloseWorldDialog");
		mapCampManager.campBlackImage.DOFade(0f, fadeTime).OnComplete(FadeToClearEnd);
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

	private void FadeToClearEnd()
	{
		headerStatusManager.menuCanvasGroup.interactable = true;
		Transition(stateLink);
	}
}
