using UnityEngine;

namespace SciFiArsenal
{
	public class SciFiProjectileScript : MonoBehaviour
	{
		public GameObject impactParticle;

		public GameObject projectileParticle;

		public GameObject muzzleParticle;

		public GameObject[] trailParticles;

		[HideInInspector]
		public Vector3 impactNormal;

		private bool hasCollided;

		private void Start()
		{
			projectileParticle = Object.Instantiate(projectileParticle, base.transform.position, base.transform.rotation);
			projectileParticle.transform.parent = base.transform;
			if ((bool)muzzleParticle)
			{
				muzzleParticle = Object.Instantiate(muzzleParticle, base.transform.position, base.transform.rotation);
				muzzleParticle.transform.rotation = base.transform.rotation * Quaternion.Euler(180f, 0f, 0f);
				Object.Destroy(muzzleParticle, 1.5f);
			}
		}

		private void OnCollisionEnter(Collision hit)
		{
			if (hasCollided)
			{
				return;
			}
			hasCollided = true;
			impactParticle = Object.Instantiate(impactParticle, base.transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));
			if (hit.gameObject.tag == "Destructible")
			{
				Object.Destroy(hit.gameObject);
			}
			GameObject[] array = trailParticles;
			foreach (GameObject gameObject in array)
			{
				GameObject obj = base.transform.Find(projectileParticle.name + "/" + gameObject.name).gameObject;
				obj.transform.parent = null;
				Object.Destroy(obj, 3f);
			}
			Object.Destroy(projectileParticle, 3f);
			Object.Destroy(impactParticle, 5f);
			Object.Destroy(base.gameObject);
			ParticleSystem[] componentsInChildren = GetComponentsInChildren<ParticleSystem>();
			for (int j = 1; j < componentsInChildren.Length; j++)
			{
				ParticleSystem particleSystem = componentsInChildren[j];
				if (particleSystem.gameObject.name.Contains("Trail"))
				{
					particleSystem.transform.SetParent(null);
					Object.Destroy(particleSystem.gameObject, 2f);
				}
			}
		}
	}
}
