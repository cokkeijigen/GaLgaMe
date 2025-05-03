using UnityEngine;

namespace CartoonCoffee
{
	public class DemoProjectile : MonoBehaviour
	{
		public Vector3 velocity;

		public float deathDelay;

		public GameObject impactParticle;

		public float curveFactor;

		public Vector3 handUp;

		public bool alignWithDirection;

		private bool isDead;

		private Vector3 mousePosition;

		private float baseDistance;

		private Vector3 realPosition;

		private Vector3 spawnPosition;

		private void Start()
		{
			isDead = false;
			mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePosition.z = 0f;
			baseDistance = Vector2.Distance(base.transform.position, mousePosition);
			realPosition = base.transform.position;
			spawnPosition = base.transform.position;
			if (baseDistance < 4f)
			{
				curveFactor = 0f;
			}
		}

		private void Update()
		{
			realPosition += velocity * Time.deltaTime * 4f;
			float num = Vector2.Distance(realPosition, mousePosition);
			if (Vector2.Distance(spawnPosition, base.transform.position) >= baseDistance)
			{
				curveFactor = Mathf.Lerp(curveFactor, 0f, Time.deltaTime * 10f);
			}
			if (alignWithDirection)
			{
				base.transform.eulerAngles = new Vector3(0f, 0f, Vector2.SignedAngle(Vector2.right, velocity));
			}
			base.transform.position = realPosition + handUp * Mathf.Pow(Mathf.Abs((baseDistance * 0.5f - Mathf.Abs(baseDistance * 0.5f - num)) / baseDistance), 0.3f) * curveFactor;
			if (isDead)
			{
				velocity = Vector3.Lerp(velocity, Vector3.zero, Time.time * 10f);
				base.transform.localScale = Vector3.Lerp(base.transform.localScale, Vector3.zero, Time.deltaTime * 10f);
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (!isDead && !collision.isTrigger)
			{
				isDead = true;
				GetComponent<ParticleSystem>().Stop(withChildren: true, ParticleSystemStopBehavior.StopEmitting);
				Object.Destroy(base.gameObject, deathDelay);
				DemoTarget component = collision.GetComponent<DemoTarget>();
				if (component != null)
				{
					component.Impact();
				}
				if (impactParticle != null)
				{
					GameObject obj = Object.Instantiate(impactParticle);
					obj.transform.position = base.transform.position;
					Object.Destroy(obj, 2f);
				}
			}
		}
	}
}
