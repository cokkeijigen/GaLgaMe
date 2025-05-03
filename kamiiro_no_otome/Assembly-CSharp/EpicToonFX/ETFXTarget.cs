using System.Collections;
using UnityEngine;

namespace EpicToonFX
{
	public class ETFXTarget : MonoBehaviour
	{
		[Header("Effect shown on target hit")]
		public GameObject hitParticle;

		[Header("Effect shown on target respawn")]
		public GameObject respawnParticle;

		private Renderer targetRenderer;

		private Collider targetCollider;

		private void Start()
		{
			targetRenderer = GetComponent<Renderer>();
			targetCollider = GetComponent<Collider>();
		}

		private void SpawnTarget()
		{
			targetRenderer.enabled = true;
			targetCollider.enabled = true;
			Object.Destroy(Object.Instantiate(respawnParticle, base.transform.position, base.transform.rotation), 3.5f);
		}

		private void OnTriggerEnter(Collider col)
		{
			if (col.tag == "Missile" && (bool)hitParticle)
			{
				Object.Destroy(Object.Instantiate(hitParticle, base.transform.position, base.transform.rotation), 2f);
				targetRenderer.enabled = false;
				targetCollider.enabled = false;
				StartCoroutine(Respawn());
			}
		}

		private IEnumerator Respawn()
		{
			yield return new WaitForSeconds(3f);
			SpawnTarget();
		}
	}
}
