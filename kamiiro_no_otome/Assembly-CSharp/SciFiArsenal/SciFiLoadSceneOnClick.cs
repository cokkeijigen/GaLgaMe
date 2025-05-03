using UnityEngine;
using UnityEngine.SceneManagement;

namespace SciFiArsenal
{
	public class SciFiLoadSceneOnClick : MonoBehaviour
	{
		public bool GUIHide;

		public bool GUIHide2;

		public bool GUIHide3;

		public bool GUIHide4;

		public void LoadSceneSciFiProjectiles()
		{
			SceneManager.LoadScene("scifi_projectiles");
		}

		public void LoadSceneSciFiBeamup()
		{
			SceneManager.LoadScene("scifi_beamup");
		}

		public void LoadSceneSciFiBuff()
		{
			SceneManager.LoadScene("scifi_buff");
		}

		public void LoadSceneSciFiFlamethrowers2()
		{
			SceneManager.LoadScene("scifi_flamethrowers");
		}

		public void LoadSceneSciFiQuestZone()
		{
			SceneManager.LoadScene("scifi_hexagonzone");
		}

		public void LoadSceneSciFiLightjump()
		{
			SceneManager.LoadScene("scifi_lightjump");
		}

		public void LoadSceneSciFiLoot()
		{
			SceneManager.LoadScene("scifi_loot");
		}

		public void LoadSceneSciFiBeams()
		{
			SceneManager.LoadScene("scifi_beams");
		}

		public void LoadSceneSciFiPortals()
		{
			SceneManager.LoadScene("scifi_portals");
		}

		public void LoadSceneSciFiRegenerate()
		{
			SceneManager.LoadScene("scifi_regenerate");
		}

		public void LoadSceneSciFiShields()
		{
			SceneManager.LoadScene("scifi_shields");
		}

		public void LoadSceneSciFiSwirlyAura()
		{
			SceneManager.LoadScene("scifi_swirlyaura");
		}

		public void LoadSceneSciFiWarpgates()
		{
			SceneManager.LoadScene("scifi_warpgates");
		}

		public void LoadSceneSciFiJetflame()
		{
			SceneManager.LoadScene("scifi_jetflame");
		}

		public void LoadSceneSciFiUltimateNova()
		{
			SceneManager.LoadScene("scifi_ultimatenova");
		}

		public void LoadSceneSciFiFire()
		{
			SceneManager.LoadScene("scifi_fire");
		}

		public void LoadSceneSciFiUpdate6()
		{
			SceneManager.LoadScene("update_scifi_6");
		}

		public void LoadSceneSciFiUpdate7()
		{
			SceneManager.LoadScene("update_scifi_7");
		}

		public void LoadSceneSciFiUpdate8()
		{
			SceneManager.LoadScene("update_scifi_8");
		}

		public void LoadSceneSciFiUpdate9()
		{
			SceneManager.LoadScene("update_scifi_9");
		}

		public void LoadSceneSciFiUpdate10()
		{
			SceneManager.LoadScene("update_scifi_10");
		}

		public void LoadSceneSciFiUpdate11()
		{
			SceneManager.LoadScene("update_scifi_11");
		}

		public void LoadSceneSciFiUpdate12()
		{
			SceneManager.LoadScene("update_scifi_12");
		}

		public void LoadSceneSciFiBlood()
		{
			SceneManager.LoadScene("scifi_blood");
		}

		public void LoadSceneSciFiRoundZone()
		{
			SceneManager.LoadScene("scifi_roundzone");
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.L))
			{
				GUIHide = !GUIHide;
				if (GUIHide)
				{
					GameObject.Find("SciFiSceneSelectNew").GetComponent<Canvas>().enabled = false;
				}
				else
				{
					GameObject.Find("SciFiSceneSelectNew").GetComponent<Canvas>().enabled = true;
				}
			}
			if (Input.GetKeyDown(KeyCode.J))
			{
				GUIHide2 = !GUIHide2;
				if (GUIHide2)
				{
					GameObject.Find("SciFiProjectileCanvas").GetComponent<Canvas>().enabled = false;
				}
				else
				{
					GameObject.Find("SciFiProjectileCanvas").GetComponent<Canvas>().enabled = true;
				}
			}
			if (Input.GetKeyDown(KeyCode.H))
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
			if (Input.GetKeyDown(KeyCode.K))
			{
				GUIHide4 = !GUIHide4;
				if (GUIHide3)
				{
					GameObject.Find("SciFiBeamCanvas").GetComponent<Canvas>().enabled = false;
				}
				else
				{
					GameObject.Find("SciFiBeamCanvas").GetComponent<Canvas>().enabled = true;
				}
			}
		}
	}
}
