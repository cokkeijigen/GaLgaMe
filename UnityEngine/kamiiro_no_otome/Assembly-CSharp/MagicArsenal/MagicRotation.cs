using UnityEngine;

namespace MagicArsenal
{
	public class MagicRotation : MonoBehaviour
	{
		public enum spaceEnum
		{
			Local,
			World
		}

		[Header("Rotate axises by degrees per second")]
		public Vector3 rotateVector = Vector3.zero;

		public spaceEnum rotateSpace;

		private void Start()
		{
		}

		private void Update()
		{
			if (rotateSpace == spaceEnum.Local)
			{
				base.transform.Rotate(rotateVector * Time.deltaTime);
			}
			if (rotateSpace == spaceEnum.World)
			{
				base.transform.Rotate(rotateVector * Time.deltaTime, Space.World);
			}
		}
	}
}
