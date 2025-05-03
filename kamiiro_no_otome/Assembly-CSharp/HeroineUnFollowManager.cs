using System.Collections.Generic;
using I2.Loc;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class HeroineUnFollowManager : SerializedMonoBehaviour
{
	public GameObject unFollowCanvasGo;

	public GameObject unFollowDialogGo;

	public Image unFollowCanbasBlackImage;

	public PlayableDirector unFollowDirector;

	public PlayableAsset[] unFollowTimelineAssetArray;

	public Image unFollowTimelineBgImage;

	public Dictionary<string, Sprite> unFollowTimelineBgDictionary = new Dictionary<string, Sprite>();

	public Localize unFollowBallonTextLoc;

	public Localize[] unFollowMessageTextLocArray;

	public GameObject skipFlameGo;

	public AudioSource unFollowAudioSource;

	private void Awake()
	{
		unFollowCanbasBlackImage.color = new Color(0f, 0f, 0f, PlayerNonSaveDataManager.heroineUnFollowBlackImageAlpha);
		unFollowDialogGo.SetActive(value: false);
	}
}
