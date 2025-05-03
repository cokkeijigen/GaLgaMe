using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EpicToonFX
{
	public class ETFXEffectCycler : MonoBehaviour
	{
		public List<GameObject> listOfEffects;

		private int effectIndex;

		[Header("Spawn Settings")]
		[SerializeField]
		[Space(10f)]
		public float loopLength = 1f;

		public float startDelay = 1f;

		public bool disableLights = true;

		public bool disableSound = true;

		private void Start()
		{
			Invoke("PlayEffect", startDelay);
		}

		public void PlayEffect()
		{
			StartCoroutine("EffectLoop");
			if (effectIndex < listOfEffects.Count - 1)
			{
				effectIndex++;
			}
			else
			{
				effectIndex = 0;
			}
		}

		private IEnumerator EffectLoop()
		{
			GameObject instantiatedEffect = Object.Instantiate(listOfEffects[effectIndex], base.transform.position, base.transform.rotation * Quaternion.Euler(0f, 0f, 0f));
			if (disableLights && (bool)instantiatedEffect.GetComponent<Light>())
			{
				instantiatedEffect.GetComponent<Light>().enabled = false;
			}
			if (disableSound && (bool)instantiatedEffect.GetComponent<AudioSource>())
			{
				instantiatedEffect.GetComponent<AudioSource>().enabled = false;
			}
			yield return new WaitForSeconds(loopLength);
			Object.Destroy(instantiatedEffect);
			PlayEffect();
		}
	}
}
