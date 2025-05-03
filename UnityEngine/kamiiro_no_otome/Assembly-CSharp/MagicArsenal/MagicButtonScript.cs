using UnityEngine;
using UnityEngine.UI;

namespace MagicArsenal
{
	public class MagicButtonScript : MonoBehaviour
	{
		public GameObject Button;

		private Text MyButtonText;

		private string projectileParticleName;

		private MagicFireProjectile effectScript;

		private MagicProjectileScript projectileScript;

		public float buttonsX;

		public float buttonsY;

		public float buttonsSizeX;

		public float buttonsSizeY;

		public float buttonsDistance;

		private void Start()
		{
			effectScript = GameObject.Find("MagicFireProjectile").GetComponent<MagicFireProjectile>();
			getProjectileNames();
			MyButtonText = Button.transform.Find("Text").GetComponent<Text>();
			MyButtonText.text = projectileParticleName;
		}

		private void Update()
		{
			MyButtonText.text = projectileParticleName;
		}

		public void getProjectileNames()
		{
			projectileScript = effectScript.projectiles[effectScript.currentProjectile].GetComponent<MagicProjectileScript>();
			projectileParticleName = projectileScript.projectileParticle.name;
		}

		public bool overButton()
		{
			Rect rect = new Rect(buttonsX, buttonsY, buttonsSizeX, buttonsSizeY);
			Rect rect2 = new Rect(buttonsX + buttonsDistance, buttonsY, buttonsSizeX, buttonsSizeY);
			if (rect.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)) || rect2.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y)))
			{
				return true;
			}
			return false;
		}
	}
}
