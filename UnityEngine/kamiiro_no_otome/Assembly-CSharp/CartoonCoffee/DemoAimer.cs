using UnityEngine;

namespace CartoonCoffee
{
	public class DemoAimer : MonoBehaviour
	{
		private Transform arm;

		private void Start()
		{
			arm = base.transform.Find("Arm");
		}

		private void LateUpdate()
		{
			HandleAiming();
		}

		private void HandleAiming()
		{
			Vector3 vector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			vector.z = arm.position.z;
			arm.eulerAngles = new Vector3(0f, 0f, Vector2.SignedAngle(Vector2.right, vector - base.transform.position));
		}
	}
}
