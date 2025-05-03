using UnityEngine;

namespace EpicToonFX
{
	public class ETFXLightFade : MonoBehaviour
	{
		[Header("Seconds to dim the light")]
		public float life = 0.2f;

		public bool killAfterLife = true;

		private Light li;

		private float initIntensity;

		private void Start()
		{
			if ((bool)base.gameObject.GetComponent<Light>())
			{
				li = base.gameObject.GetComponent<Light>();
				initIntensity = li.intensity;
			}
			else
			{
				MonoBehaviour.print("No light object found on " + base.gameObject.name);
			}
		}

		private void Update()
		{
			if ((bool)base.gameObject.GetComponent<Light>())
			{
				li.intensity -= initIntensity * (Time.deltaTime / life);
				if (killAfterLife && li.intensity <= 0f)
				{
					Object.Destroy(base.gameObject.GetComponent<Light>());
				}
			}
		}
	}
}
