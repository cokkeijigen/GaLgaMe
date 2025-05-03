using UnityEngine;
using UnityEngine.SceneManagement;

namespace MagicArsenal
{
	public class MagicLoadSceneOnClick : MonoBehaviour
	{
		public bool GUIHide;

		public bool GUIHide2;

		public bool GUIHide3;

		public void LoadSceneProjectiles()
		{
			SceneManager.LoadScene("magic_projectiles");
		}

		public void LoadSceneSprays()
		{
			SceneManager.LoadScene("magic_sprays");
		}

		public void LoadSceneAura()
		{
			SceneManager.LoadScene("magic_aura");
		}

		public void LoadSceneModular()
		{
			SceneManager.LoadScene("magic_modular");
		}

		public void LoadSceneShields2()
		{
			SceneManager.LoadScene("magic_domes");
		}

		public void LoadSceneShields()
		{
			SceneManager.LoadScene("magic_shields");
		}

		public void LoadSceneSphereBlast()
		{
			SceneManager.LoadScene("magic_sphereblast");
		}

		public void LoadSceneEnchant()
		{
			SceneManager.LoadScene("magic_enchant");
		}

		public void LoadSceneSlash()
		{
			SceneManager.LoadScene("magic_slash");
		}

		public void LoadSceneCharge()
		{
			SceneManager.LoadScene("magic_charge");
		}

		public void LoadSceneCleave()
		{
			SceneManager.LoadScene("magic_cleave");
		}

		public void LoadSceneAura2()
		{
			SceneManager.LoadScene("magic_aura2");
		}

		public void LoadSceneWalls()
		{
			SceneManager.LoadScene("magic_walls");
		}

		public void LoadSceneBeams()
		{
			SceneManager.LoadScene("magic_beams");
		}

		public void LoadSceneMeshGlow()
		{
			SceneManager.LoadScene("magic_meshglow");
		}

		public void LoadScenePillarBlast()
		{
			SceneManager.LoadScene("magic_pillarblast");
		}

		public void LoadSceneAura3()
		{
			SceneManager.LoadScene("magic_aura3");
		}

		public void LoadSceneAuraCast()
		{
			SceneManager.LoadScene("magic_auracast");
		}

		public void LoadSceneRain()
		{
			SceneManager.LoadScene("magic_rain");
		}

		public void LoadSceneAOE()
		{
			SceneManager.LoadScene("magic_aoe");
		}

		public void LoadSceneNova()
		{
			SceneManager.LoadScene("magic_nova");
		}

		public void LoadSceneFlame()
		{
			SceneManager.LoadScene("magic_flame");
		}

		public void LoadSceneAuraCast2()
		{
			SceneManager.LoadScene("magic_auracast2");
		}

		public void LoadSceneCurse()
		{
			SceneManager.LoadScene("magic_curse");
		}

		public void LoadSceneBeamBlast()
		{
			SceneManager.LoadScene("magic_beamblast");
		}

		public void LoadSceneOrbitSphere()
		{
			SceneManager.LoadScene("magic_orbitsphere");
		}

		public void LoadSceneDot()
		{
			SceneManager.LoadScene("magic_dot");
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.J))
			{
				GUIHide = !GUIHide;
				if (GUIHide)
				{
					GameObject.Find("CanvasSceneSelect").GetComponent<Canvas>().enabled = false;
				}
				else
				{
					GameObject.Find("CanvasSceneSelect").GetComponent<Canvas>().enabled = true;
				}
			}
			if (Input.GetKeyDown(KeyCode.K))
			{
				GUIHide2 = !GUIHide2;
				if (GUIHide2)
				{
					GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
				}
				else
				{
					GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
				}
			}
			if (Input.GetKeyDown(KeyCode.L))
			{
				GUIHide3 = !GUIHide3;
				if (GUIHide3)
				{
					GameObject.Find("CanvasTips").GetComponent<Canvas>().enabled = false;
				}
				else
				{
					GameObject.Find("CanvasTips").GetComponent<Canvas>().enabled = true;
				}
			}
		}
	}
}
