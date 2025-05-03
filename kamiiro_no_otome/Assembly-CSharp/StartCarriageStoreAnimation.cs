using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class StartCarriageStoreAnimation : StateBehaviour
{
	public enum AnimationType
	{
		storeTending,
		storeNotice
	}

	private CarriageStoreNoticeManager carriageStoreNoticeManager;

	public AnimationType animationType;

	public AudioSource audioSource;

	private bool isSkip;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		carriageStoreNoticeManager = GameObject.Find("Store Tending Manager").GetComponent<CarriageStoreNoticeManager>();
	}

	public override void OnStateBegin()
	{
		audioSource.volume = PlayerOptionsDataManager.optionsSeVolume;
		isSkip = false;
		switch (animationType)
		{
		case AnimationType.storeTending:
			carriageStoreNoticeManager.storeDirector.playableAsset = carriageStoreNoticeManager.storeTendingTimelineArray[0];
			switch (PlayerDataManager.currentTimeZone)
			{
			case 0:
			case 1:
				carriageStoreNoticeManager.storeTendingBgImage.sprite = carriageStoreNoticeManager.storeTendingBgSpriteDictionary["tendingBg_m"];
				break;
			case 2:
				carriageStoreNoticeManager.storeTendingBgImage.sprite = carriageStoreNoticeManager.storeTendingBgSpriteDictionary["tendingBg_y"];
				break;
			case 3:
				carriageStoreNoticeManager.storeTendingBgImage.sprite = carriageStoreNoticeManager.storeTendingBgSpriteDictionary["tendingBg_n"];
				break;
			}
			PlayerNonSaveDataManager.storeTendingRemainTime = 1;
			break;
		case AnimationType.storeNotice:
			carriageStoreNoticeManager.storeDirector.playableAsset = carriageStoreNoticeManager.storeTendingTimelineArray[1];
			switch (PlayerDataManager.currentTimeZone)
			{
			case 0:
			case 1:
				carriageStoreNoticeManager.storeNoticeBgImage.sprite = carriageStoreNoticeManager.storeNoticeBgSpriteDictionary["noticeBg_m"];
				break;
			case 2:
				carriageStoreNoticeManager.storeNoticeBgImage.sprite = carriageStoreNoticeManager.storeNoticeBgSpriteDictionary["noticeBg_y"];
				break;
			case 3:
				carriageStoreNoticeManager.storeNoticeBgImage.sprite = carriageStoreNoticeManager.storeNoticeBgSpriteDictionary["noticeBg_n"];
				break;
			}
			break;
		}
		carriageStoreNoticeManager.storeDirector.time = 0.0;
		carriageStoreNoticeManager.storeDirector.Play();
		Debug.Log("陳列販売アニメを再生");
	}

	public override void OnStateEnd()
	{
		carriageStoreNoticeManager.storeDirector.time = carriageStoreNoticeManager.storeDirector.duration;
	}

	public override void OnStateUpdate()
	{
		if (Input.GetButtonDown("Fire1") && !isSkip)
		{
			isSkip = true;
			carriageStoreNoticeManager.storeDirector.time = carriageStoreNoticeManager.storeDirector.duration - 0.10000000149011612;
		}
		if (carriageStoreNoticeManager.storeDirector.time == carriageStoreNoticeManager.storeDirector.duration)
		{
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
