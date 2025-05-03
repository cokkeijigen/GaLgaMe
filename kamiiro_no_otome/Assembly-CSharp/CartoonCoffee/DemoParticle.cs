using UnityEngine;

namespace CartoonCoffee
{
	public class DemoParticle : MonoBehaviour
	{
		public bool alignWithDirection = true;

		public bool hasCharging = true;

		public float particleSpeed = 8f;

		public float deathDelay;

		[Header("Spray Pattern:")]
		public float spreadAngle;

		public float curveFactor = 2.5f;

		public float spamDelay = 0.05f;
	}
}
