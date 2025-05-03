using Arbor;
using DarkTonic.MasterAudio;
using UnityEngine;
using UnityEngine.UI;
using Utage;

[AddComponentMenu("")]
public class TestPreBgmVolume : StateBehaviour
{
	public enum Type
	{
		testApply,
		testRevert
	}

	public ParameterContainer parameterContainer;

	private Slider slider;

	private AdvConfig advConfig;

	public Type type;

	public bool isSexBgmVolume;

	public StateLink stateLink;

	private void Start()
	{
	}

	public override void OnStateAwake()
	{
		slider = GetComponent<Slider>();
	}

	public override void OnStateBegin()
	{
		GameObject gameObject = GameObject.Find("AdvEngine");
		if (gameObject != null)
		{
			advConfig = gameObject.GetComponent<AdvConfig>();
		}
		PlaylistController component = GameObject.Find("PlaylistController").GetComponent<PlaylistController>();
		if (isSexBgmVolume)
		{
			switch (type)
			{
			case Type.testApply:
				if (advConfig != null)
				{
					advConfig.BgmVolume = slider.value / 10f;
				}
				component.PlaylistVolume = slider.value / 10f;
				break;
			case Type.testRevert:
				if (advConfig != null)
				{
					if (PlayerNonSaveDataManager.isUtageHmode)
					{
						advConfig.BgmVolume = PlayerOptionsDataManager.optionsHBgmVolume;
					}
					else
					{
						advConfig.BgmVolume = PlayerOptionsDataManager.optionsBgmVolume;
					}
				}
				component.PlaylistVolume = 1f;
				break;
			}
		}
		else
		{
			switch (type)
			{
			case Type.testApply:
				if (advConfig != null)
				{
					advConfig.BgmVolume = slider.value / 10f;
				}
				component.PlaylistVolume = slider.value / 10f;
				break;
			case Type.testRevert:
				if (advConfig != null)
				{
					advConfig.BgmVolume = PlayerOptionsDataManager.optionsBgmVolume;
				}
				component.PlaylistVolume = 1f;
				break;
			}
		}
		Transition(stateLink);
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
}
