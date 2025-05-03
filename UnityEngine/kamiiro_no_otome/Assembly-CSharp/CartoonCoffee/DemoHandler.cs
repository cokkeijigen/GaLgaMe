using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CartoonCoffee
{
	public class DemoHandler : MonoBehaviour
	{
		public static DemoHandler c;

		private Text currentText;

		private Text hintText;

		private Transform shootPosition;

		private List<Transform> particles;

		private float chargeStart;

		private int currentIndex;

		private ParticleSystem currentChargingParticle;

		private float lastShot;

		private void Awake()
		{
			c = this;
			Transform transform = base.transform.Find("Particles");
			particles = new List<Transform>();
			for (int i = 0; i < transform.childCount; i++)
			{
				GameObject gameObject = transform.GetChild(i).gameObject;
				gameObject.SetActive(value: false);
				particles.Add(gameObject.transform);
			}
			shootPosition = base.transform.Find("Aimer/Arm/ShootPosition");
			currentIndex = 0;
			currentText = base.transform.Find("Canvas/CurrentText").GetComponent<Text>();
			hintText = base.transform.Find("Canvas/Hint").GetComponent<Text>();
		}

		private void Start()
		{
			RefreshText();
		}

		private void Update()
		{
			HandleHintFlashing();
			HandleScrolling();
			HandleShooting();
		}

		public string GetProjectile()
		{
			return particles[currentIndex].name;
		}

		public string GetIndexString()
		{
			return currentIndex + 1 + "/" + particles.Count;
		}

		private void HandleShooting()
		{
			if (currentChargingParticle == null)
			{
				DemoParticle component = particles[currentIndex].GetComponent<DemoParticle>();
				if (Input.mousePosition.x / (float)Screen.width > 0.175f && (Input.GetMouseButtonDown(0) || (Input.GetMouseButton(1) && Time.time > lastShot + component.spamDelay)))
				{
					lastShot = Time.time;
					if (component.hasCharging)
					{
						GameObject gameObject = Object.Instantiate(particles[currentIndex].GetChild(0).GetChild(0).gameObject);
						gameObject.transform.SetParent(shootPosition);
						gameObject.transform.localPosition = Vector3.zero;
						currentChargingParticle = gameObject.GetComponent<ParticleSystem>();
						gameObject.SetActive(value: false);
						chargeStart = Time.time;
					}
					else
					{
						int level = 0;
						SpawnProjectile(level, component, 0);
					}
				}
			}
			else if (Input.mousePosition.x / (float)Screen.width < 0.175f || !Input.GetMouseButton(0))
			{
				Object.Destroy(currentChargingParticle.gameObject);
				currentChargingParticle = null;
				DemoParticle component2 = particles[currentIndex].GetComponent<DemoParticle>();
				int level2 = 0;
				if (Time.time - chargeStart > 0.2f)
				{
					level2 = ((!(Time.time - chargeStart > 1.1f)) ? 1 : 2);
				}
				SpawnProjectile(level2, component2, 1);
			}
			else if (Time.time - chargeStart > 0.2f && !currentChargingParticle.gameObject.activeInHierarchy)
			{
				currentChargingParticle.gameObject.SetActive(value: true);
			}
		}

		private void SpawnProjectile(int level, DemoParticle settings, int burstIndex)
		{
			GameObject obj = Object.Instantiate(particles[currentIndex].GetChild(0).GetChild(burstIndex).gameObject);
			obj.transform.position = shootPosition.position;
			obj.transform.eulerAngles = shootPosition.eulerAngles;
			Object.Destroy(obj, 1f);
			GameObject obj2 = Object.Instantiate(particles[currentIndex].GetChild(1).GetChild(level).gameObject);
			obj2.transform.position = shootPosition.position;
			DemoProjectile demoProjectile = obj2.AddComponent<DemoProjectile>();
			demoProjectile.velocity = Quaternion.Euler(0f, 0f, (Random.value - 0.5f) * settings.spreadAngle) * shootPosition.right * settings.particleSpeed;
			demoProjectile.deathDelay = settings.deathDelay;
			demoProjectile.impactParticle = particles[currentIndex].GetChild(2).GetChild(level).gameObject;
			demoProjectile.curveFactor = ((level > 0) ? 0f : ((Random.value * 2f - 1f) * settings.curveFactor));
			demoProjectile.handUp = shootPosition.up;
			demoProjectile.alignWithDirection = settings.alignWithDirection;
			obj2.AddComponent<Rigidbody2D>().isKinematic = true;
			CircleCollider2D circleCollider2D = obj2.AddComponent<CircleCollider2D>();
			circleCollider2D.isTrigger = true;
			circleCollider2D.radius = 0.2f + (float)level * 0.2f;
		}

		private void HandleScrolling()
		{
			if (!(currentChargingParticle != null) && Input.mouseScrollDelta.y != 0f)
			{
				if (Input.mouseScrollDelta.y < 0f)
				{
					Next();
				}
				else
				{
					Previous();
				}
				RefreshText();
			}
		}

		public void Next()
		{
			currentIndex++;
			if (currentIndex > particles.Count - 1)
			{
				currentIndex = 0;
			}
			if (DemoPreview.c != null)
			{
				DemoPreview.c.UpdateText();
			}
		}

		public void Previous()
		{
			currentIndex--;
			if (currentIndex < 0)
			{
				currentIndex = particles.Count - 1;
			}
			if (DemoPreview.c != null)
			{
				DemoPreview.c.UpdateText();
			}
		}

		public void SwitchToPreview()
		{
			SceneManager.LoadScene("Gallery");
		}

		private void HandleHintFlashing()
		{
			if (hintText != null)
			{
				Color color = hintText.color;
				color.a = Mathf.Sin(Time.time * 3f) * 0.2f + 0.8f;
				hintText.color = color;
			}
		}

		private void RefreshText()
		{
			if (currentText != null)
			{
				currentText.text = "<color=#FFFFFF99>Projectile:</color> " + particles[currentIndex].name;
			}
			if (DemoPreview.c != null)
			{
				DemoPreview.c.UpdateText();
			}
		}
	}
}
