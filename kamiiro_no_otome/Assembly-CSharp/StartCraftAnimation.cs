using Arbor;
using UnityEngine;
using UnityEngine.Playables;

[AddComponentMenu("")]
public class StartCraftAnimation : StateBehaviour
{
	private CraftManager craftManager;

	private CraftCanvasManager craftCanvasManager;

	private CraftCheckManager craftCheckManager;

	public AudioSource audioSource;

	public PlayableDirector playableDirector;

	private bool isSkip;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		craftManager = GameObject.Find("Craft Manager").GetComponent<CraftManager>();
		craftCanvasManager = GameObject.Find("Craft And Merge Manager").GetComponent<CraftCanvasManager>();
		craftCheckManager = GetComponent<CraftCheckManager>();
	}

	public override void OnStateBegin()
	{
		audioSource.volume = PlayerOptionsDataManager.optionsSeVolume;
		isSkip = false;
		craftCheckManager.animationFrame.SetActive(value: true);
		if (craftCheckManager.isAnimationCraft)
		{
			playableDirector.playableAsset = craftCheckManager.animationAssetArray[0];
		}
		else
		{
			playableDirector.playableAsset = craftCheckManager.animationAssetArray[1];
		}
		playableDirector.time = 0.0;
		playableDirector.Play();
	}

	public override void OnStateEnd()
	{
	}

	public override void OnStateUpdate()
	{
		if (Input.GetButtonDown("Fire1") && !isSkip)
		{
			isSkip = true;
			playableDirector.time = playableDirector.duration - 0.10000000149011612;
		}
		if (playableDirector.time == playableDirector.duration)
		{
			Transition(stateLink);
		}
	}

	public override void OnStateLateUpdate()
	{
	}
}
