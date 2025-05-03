using UnityEngine;

namespace CartoonCoffee
{
	public class DemoTarget : MonoBehaviour
	{
		private SpriteRenderer sr;

		private Color originalColor;

		private void Start()
		{
			sr = GetComponent<SpriteRenderer>();
			originalColor = sr.color;
		}

		public void Impact()
		{
			sr.color = Color.white;
			base.transform.localScale = new Vector3(0.65f, 0.65f, 1f);
		}

		private void Update()
		{
			sr.color = Color.Lerp(sr.color, originalColor, Time.deltaTime * 5f);
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, Vector3.one, Time.deltaTime * 8f);
		}
	}
}
