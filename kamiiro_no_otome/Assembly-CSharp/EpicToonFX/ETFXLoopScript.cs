using System.Collections;
using UnityEngine;

namespace EpicToonFX
{
	public class ETFXLoopScript : MonoBehaviour
	{
		public GameObject chosenEffect;

		public float loopTimeLimit = 2f;

		[Header("Spawn without")]
		public bool disableLights = true;

		public bool disableSound = true;

		private void Start()
		{
			PlayEffect();
		}

		public void PlayEffect()
		{
			StartCoroutine("EffectLoop");
		}

		private IEnumerator EffectLoop()
		{
			GameObject effectPlayer = Object.Instantiate(chosenEffect, base.transform.position, base.transform.rotation);
			if (disableLights && (bool)effectPlayer.GetComponent<Light>())
			{
				effectPlayer.GetComponent<Light>().enabled = false;
			}
			if (disableSound && (bool)effectPlayer.GetComponent<AudioSource>())
			{
				effectPlayer.GetComponent<AudioSource>().enabled = false;
			}
			yield return new WaitForSeconds(loopTimeLimit);
			Object.Destroy(effectPlayer);
			PlayEffect();
		}
	}
}
