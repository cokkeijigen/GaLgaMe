using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace EpicToonFX
{
	public class ETFXEffectController : MonoBehaviour
	{
		public GameObject[] effects;

		private int effectIndex;

		[Space(10f)]
		[Header("Spawn Settings")]
		public bool disableLights = true;

		public bool disableSound = true;

		public float startDelay = 0.2f;

		public float respawnDelay = 0.5f;

		public bool slideshowMode;

		public bool autoRotation;

		[Range(0.001f, 0.5f)]
		public float autoRotationSpeed = 0.1f;

		private GameObject currentEffect;

		private Text effectNameText;

		private Text effectIndexText;

		private ETFXMouseOrbit etfxMouseOrbit;

		private void Awake()
		{
			effectNameText = GameObject.Find("EffectName").GetComponent<Text>();
			effectIndexText = GameObject.Find("EffectIndex").GetComponent<Text>();
			etfxMouseOrbit = Camera.main.GetComponent<ETFXMouseOrbit>();
			etfxMouseOrbit.etfxEffectController = this;
		}

		private void Start()
		{
			etfxMouseOrbit = Camera.main.GetComponent<ETFXMouseOrbit>();
			etfxMouseOrbit.etfxEffectController = this;
			Invoke("InitializeLoop", startDelay);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
			{
				NextEffect();
			}
			if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
			{
				PreviousEffect();
			}
		}

		private void FixedUpdate()
		{
			if (autoRotation)
			{
				etfxMouseOrbit.SetAutoRotationSpeed(autoRotationSpeed);
				if (!etfxMouseOrbit.isAutoRotating)
				{
					etfxMouseOrbit.InitializeAutoRotation();
				}
			}
		}

		public void InitializeLoop()
		{
			StartCoroutine(EffectLoop());
		}

		public void NextEffect()
		{
			if (effectIndex < effects.Length - 1)
			{
				effectIndex++;
			}
			else
			{
				effectIndex = 0;
			}
			CleanCurrentEffect();
		}

		public void PreviousEffect()
		{
			if (effectIndex > 0)
			{
				effectIndex--;
			}
			else
			{
				effectIndex = effects.Length - 1;
			}
			CleanCurrentEffect();
		}

		private void CleanCurrentEffect()
		{
			StopAllCoroutines();
			if (currentEffect != null)
			{
				Object.Destroy(currentEffect);
			}
			StartCoroutine(EffectLoop());
		}

		private IEnumerator EffectLoop()
		{
			GameObject gameObject = (currentEffect = Object.Instantiate(effects[effectIndex], base.transform.position, Quaternion.identity));
			if (disableLights && (bool)gameObject.GetComponent<Light>())
			{
				gameObject.GetComponent<Light>().enabled = false;
			}
			if (disableSound && (bool)gameObject.GetComponent<AudioSource>())
			{
				gameObject.GetComponent<AudioSource>().enabled = false;
			}
			effectNameText.text = effects[effectIndex].name;
			effectIndexText.text = effectIndex + 1 + " of " + effects.Length;
			ParticleSystem particleSystem = gameObject.GetComponent<ParticleSystem>();
			while (true)
			{
				yield return new WaitForSeconds(particleSystem.main.duration + respawnDelay);
				if (!slideshowMode)
				{
					if (!particleSystem.main.loop)
					{
						currentEffect.SetActive(value: false);
						currentEffect.SetActive(value: true);
					}
				}
				else
				{
					if (particleSystem.main.loop)
					{
						yield return new WaitForSeconds(respawnDelay);
					}
					NextEffect();
				}
			}
		}
	}
}
