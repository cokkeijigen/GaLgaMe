using System.Collections;
using UnityEngine;

namespace SciFiArsenal
{
	public class SciFiLoopScript : MonoBehaviour
	{
		public GameObject chosenEffect;

		public float loopTimeLimit = 2f;

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
			yield return new WaitForSeconds(loopTimeLimit);
			Object.Destroy(effectPlayer);
			PlayEffect();
		}
	}
}
