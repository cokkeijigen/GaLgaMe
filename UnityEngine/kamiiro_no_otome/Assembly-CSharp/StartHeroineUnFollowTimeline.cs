using Arbor;
using UnityEngine;

[AddComponentMenu("")]
public class StartHeroineUnFollowTimeline : StateBehaviour
{
	private HeroineUnFollowManager heroineUnFollowManager;

	private bool isSkip;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		heroineUnFollowManager = GameObject.Find("Heroine UnFollow Manager").GetComponent<HeroineUnFollowManager>();
	}

	public override void OnStateBegin()
	{
		if (PlayerDataManager.mapPlaceStatusNum == 0)
		{
			string key = "world" + PlayerDataManager.currentTimeZone;
			heroineUnFollowManager.unFollowTimelineBgImage.sprite = heroineUnFollowManager.unFollowTimelineBgDictionary[key];
			heroineUnFollowManager.unFollowTimelineBgImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
		}
		else
		{
			string key2 = "town" + PlayerDataManager.currentTimeZone;
			heroineUnFollowManager.unFollowTimelineBgImage.sprite = heroineUnFollowManager.unFollowTimelineBgDictionary[key2];
			heroineUnFollowManager.unFollowTimelineBgImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, -50f);
		}
		heroineUnFollowManager.unFollowAudioSource.volume = PlayerOptionsDataManager.optionsSeVolume;
		isSkip = false;
		heroineUnFollowManager.unFollowMessageTextLocArray[0].Term = "character" + PlayerDataManager.DungeonHeroineFollowNum;
		int num = PlayerDataManager.DungeonHeroineFollowNum - 1;
		heroineUnFollowManager.unFollowDirector.playableAsset = heroineUnFollowManager.unFollowTimelineAssetArray[num];
		if (PlayerNonSaveDataManager.isHeroineUnFollowReserveAtLocalMap)
		{
			heroineUnFollowManager.unFollowBallonTextLoc.Term = "talk_UnFollowAnimation_" + PlayerDataManager.DungeonHeroineFollowNum + "1";
		}
		else
		{
			heroineUnFollowManager.unFollowBallonTextLoc.Term = "talk_UnFollowAnimation_" + PlayerDataManager.DungeonHeroineFollowNum + "0";
		}
		heroineUnFollowManager.unFollowDirector.time = 0.0;
		heroineUnFollowManager.unFollowDirector.Play();
		Debug.Log("ヒロイン帰宅アニメを再生");
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (Input.GetButtonDown("Fire1") && !isSkip)
		{
			isSkip = true;
			heroineUnFollowManager.unFollowDirector.time = heroineUnFollowManager.unFollowDirector.duration - 0.10000000149011612;
		}
		if (heroineUnFollowManager.unFollowDirector.time == heroineUnFollowManager.unFollowDirector.duration)
		{
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
