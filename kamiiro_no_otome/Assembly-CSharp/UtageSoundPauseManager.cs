using DG.Tweening;
using UnityEngine;
using Utage;

public class UtageSoundPauseManager : MonoBehaviour
{
	private Tween delayedTween;

	public void Pause(AdvCommandSendMessageByName command)
	{
		float num = command.ParseCellOptional(AdvColumnName.Arg6, 0f);
		GameObject gameObject = base.transform.Find("Bgm").gameObject;
		SoundGroup component = gameObject.GetComponent<SoundGroup>();
		AudioSource audioSource = gameObject.GetComponentInChildren<AudioSource>();
		component.GroupVolumeFadeTime = num;
		component.GroupVolume = 0f;
		delayedTween = DOVirtual.DelayedCall(num, delegate
		{
			DelayPause(audioSource);
		});
	}

	public void UnPause(AdvCommandSendMessageByName command)
	{
		float num = command.ParseCellOptional(AdvColumnName.Arg6, 0f);
		GameObject gameObject = base.transform.Find("Bgm").gameObject;
		SoundGroup component = gameObject.GetComponent<SoundGroup>();
		AudioSource audioSource = gameObject.GetComponentInChildren<AudioSource>();
		component.GroupVolumeFadeTime = num;
		component.GroupVolume = 1f;
		if (delayedTween != null && delayedTween.IsActive())
		{
			Debug.Log("PauseのTweenをKILL");
			delayedTween.Kill();
		}
		else
		{
			Debug.Log("PauseのTweenは実行していない");
		}
		DOVirtual.DelayedCall(num, delegate
		{
			DelayUnPause(audioSource);
		});
	}

	public void PauseGroup(string groupName)
	{
		SoundGroup[] componentsInChildren = base.gameObject.GetComponentsInChildren<SoundGroup>();
		foreach (SoundGroup soundGroup in componentsInChildren)
		{
			if (soundGroup.GroupName == groupName)
			{
				AudioSource[] componentsInChildren2 = soundGroup.GetComponentsInChildren<AudioSource>();
				for (int j = 0; j < componentsInChildren2.Length; j++)
				{
					componentsInChildren2[j].Pause();
				}
			}
		}
	}

	private void DelayPause(AudioSource audioSource)
	{
		audioSource.Pause();
	}

	private void DelayUnPause(AudioSource audioSource)
	{
		audioSource.UnPause();
	}
}
