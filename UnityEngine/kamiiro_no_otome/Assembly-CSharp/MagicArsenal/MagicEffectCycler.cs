using System.Collections.Generic;
using UnityEngine;

namespace MagicArsenal
{
	public class MagicEffectCycler : MonoBehaviour
	{
		[SerializeField]
		private List<GameObject> listOfEffects;

		[Header("Loop length in seconds")]
		[SerializeField]
		private float loopTimeLength = 5f;

		private float timeOfLastInstantiate;

		private GameObject instantiatedEffect;

		private int effectIndex;

		private void Start()
		{
			instantiatedEffect = Object.Instantiate(listOfEffects[effectIndex], base.transform.position, base.transform.rotation);
			effectIndex++;
			timeOfLastInstantiate = Time.time;
		}

		private void Update()
		{
			if (Time.time >= timeOfLastInstantiate + loopTimeLength)
			{
				Object.Destroy(instantiatedEffect);
				instantiatedEffect = Object.Instantiate(listOfEffects[effectIndex], base.transform.position, base.transform.rotation);
				timeOfLastInstantiate = Time.time;
				if (effectIndex < listOfEffects.Count - 1)
				{
					effectIndex++;
				}
				else
				{
					effectIndex = 0;
				}
			}
		}
	}
}
