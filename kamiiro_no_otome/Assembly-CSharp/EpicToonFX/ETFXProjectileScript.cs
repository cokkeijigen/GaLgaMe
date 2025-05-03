using UnityEngine;

namespace EpicToonFX
{
	public class ETFXProjectileScript : MonoBehaviour
	{
		public GameObject impactParticle;

		public GameObject projectileParticle;

		public GameObject muzzleParticle;

		[Header("Adjust if not using Sphere Collider")]
		public float colliderRadius = 1f;

		[Range(0f, 1f)]
		public float collideOffset = 0.15f;

		private void Start()
		{
			projectileParticle = Object.Instantiate(projectileParticle, base.transform.position, base.transform.rotation);
			projectileParticle.transform.parent = base.transform;
			if ((bool)muzzleParticle)
			{
				muzzleParticle = Object.Instantiate(muzzleParticle, base.transform.position, base.transform.rotation);
				Object.Destroy(muzzleParticle, 1.5f);
			}
		}

		private void FixedUpdate()
		{
			if (GetComponent<Rigidbody>().velocity.magnitude != 0f)
			{
				base.transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
			}
			float radius = ((!base.transform.GetComponent<SphereCollider>()) ? colliderRadius : base.transform.GetComponent<SphereCollider>().radius);
			Vector3 velocity = base.transform.GetComponent<Rigidbody>().velocity;
			if (base.transform.GetComponent<Rigidbody>().useGravity)
			{
				velocity += Physics.gravity * Time.deltaTime;
			}
			velocity = velocity.normalized;
			float maxDistance = base.transform.GetComponent<Rigidbody>().velocity.magnitude * Time.deltaTime;
			if (!Physics.SphereCast(base.transform.position, radius, velocity, out var hitInfo, maxDistance))
			{
				return;
			}
			base.transform.position = hitInfo.point + hitInfo.normal * collideOffset;
			GameObject obj = Object.Instantiate(impactParticle, base.transform.position, Quaternion.FromToRotation(Vector3.up, hitInfo.normal));
			ParticleSystem[] componentsInChildren = GetComponentsInChildren<ParticleSystem>();
			for (int i = 1; i < componentsInChildren.Length; i++)
			{
				ParticleSystem particleSystem = componentsInChildren[i];
				if (particleSystem.gameObject.name.Contains("Trail"))
				{
					particleSystem.transform.SetParent(null);
					Object.Destroy(particleSystem.gameObject, 2f);
				}
			}
			Object.Destroy(projectileParticle, 3f);
			Object.Destroy(obj, 3.5f);
			Object.Destroy(base.gameObject);
		}
	}
}
