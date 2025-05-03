using System;
using System.Collections;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Audio)]
	[Tooltip("Stops playing the Audio Clip played by an Audio Source component on a Game Object.")]
	public class AudioStop : ComponentAction<AudioSource>
	{
		[RequiredField]
		[CheckForComponent(typeof(AudioSource))]
		[Tooltip("The GameObject with an AudioSource component.")]
		public FsmOwnerDefault gameObject;

		[Tooltip("Audio Stop can make a hard pop sound. A short fade out can fix this glitch.")]
		public FsmFloat fadeTime;

		private float volume;

		public override void Reset()
		{
			gameObject = null;
			fadeTime = null;
		}

		public override void OnEnter()
		{
			if (UpdateCache(base.Fsm.GetOwnerDefaultTarget(gameObject)))
			{
				volume = base.audio.volume;
				if (fadeTime.Value < 0.01f)
				{
					base.audio.Stop();
				}
				else
				{
					StartCoroutine(VolumeFade(base.audio, 0f, fadeTime.Value));
				}
			}
			Finish();
		}

		private IEnumerator VolumeFade(AudioSource audioSource, float endVolume, float fadeDuration)
		{
			float startTime = Time.time;
			while (Time.time < startTime + fadeDuration)
			{
				float num = (startTime + fadeDuration - Time.time) / fadeDuration;
				num *= num;
				audioSource.volume = num * volume + endVolume * (1f - num);
				yield return null;
			}
			if (Math.Abs(endVolume) < 0.01f)
			{
				audioSource.Stop();
			}
		}
	}
}
