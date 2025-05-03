using UnityEngine;
using UnityEngine.EventSystems;

namespace MagicArsenal
{
	public class MagicFireProjectile : MonoBehaviour
	{
		private RaycastHit hit;

		public GameObject[] projectiles;

		public Transform spawnPosition;

		[HideInInspector]
		public int currentProjectile;

		public float speed = 1000f;

		private MagicButtonScript selectedProjectileButton;

		private void Start()
		{
			selectedProjectileButton = GameObject.Find("Button").GetComponent<MagicButtonScript>();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				nextEffect();
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
				nextEffect();
			}
			if (Input.GetKeyDown(KeyCode.A))
			{
				previousEffect();
			}
			else if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				previousEffect();
			}
			if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f))
			{
				GameObject gameObject = Object.Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity);
				gameObject.transform.LookAt(hit.point);
				gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * speed);
				gameObject.GetComponent<MagicProjectileScript>().impactNormal = hit.normal;
			}
			Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 100f, Color.yellow);
		}

		public void nextEffect()
		{
			if (currentProjectile < projectiles.Length - 1)
			{
				currentProjectile++;
			}
			else
			{
				currentProjectile = 0;
			}
			selectedProjectileButton.getProjectileNames();
		}

		public void previousEffect()
		{
			if (currentProjectile > 0)
			{
				currentProjectile--;
			}
			else
			{
				currentProjectile = projectiles.Length - 1;
			}
			selectedProjectileButton.getProjectileNames();
		}

		public void AdjustSpeed(float newSpeed)
		{
			speed = newSpeed;
		}
	}
}
