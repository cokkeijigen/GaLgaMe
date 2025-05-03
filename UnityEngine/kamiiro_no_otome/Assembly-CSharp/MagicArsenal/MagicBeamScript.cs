using UnityEngine;
using UnityEngine.UI;

namespace MagicArsenal
{
	public class MagicBeamScript : MonoBehaviour
	{
		[Header("Prefabs")]
		public GameObject[] beamLineRendererPrefab;

		public GameObject[] beamStartPrefab;

		public GameObject[] beamEndPrefab;

		private int currentBeam;

		private GameObject beamStart;

		private GameObject beamEnd;

		private GameObject beam;

		private LineRenderer line;

		[Header("Adjustable Variables")]
		public float beamEndOffset = 1f;

		public float textureScrollSpeed = 8f;

		public float textureLengthScale = 3f;

		[Header("Put Sliders here (Optional)")]
		public Slider endOffSetSlider;

		public Slider scrollSpeedSlider;

		[Header("Put UI Text object here to show beam name")]
		public Text textBeamName;

		private void Start()
		{
			if ((bool)textBeamName)
			{
				textBeamName.text = beamLineRendererPrefab[currentBeam].name;
			}
			if ((bool)endOffSetSlider)
			{
				endOffSetSlider.value = beamEndOffset;
			}
			if ((bool)scrollSpeedSlider)
			{
				scrollSpeedSlider.value = textureScrollSpeed;
			}
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				Application.Quit();
			}
			if (Input.GetMouseButtonDown(0))
			{
				beamStart = Object.Instantiate(beamStartPrefab[currentBeam], new Vector3(0f, 0f, 0f), Quaternion.identity);
				beamEnd = Object.Instantiate(beamEndPrefab[currentBeam], new Vector3(0f, 0f, 0f), Quaternion.identity);
				beam = Object.Instantiate(beamLineRendererPrefab[currentBeam], new Vector3(0f, 0f, 0f), Quaternion.identity);
				line = beam.GetComponent<LineRenderer>();
			}
			if (Input.GetMouseButtonUp(0))
			{
				Object.Destroy(beamStart);
				Object.Destroy(beamEnd);
				Object.Destroy(beam);
			}
			if (Input.GetMouseButton(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray.origin, ray.direction, out var hitInfo))
				{
					Vector3 dir = hitInfo.point - base.transform.position;
					ShootBeamInDir(base.transform.position, dir);
				}
			}
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				nextBeam();
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
				nextBeam();
			}
			if (Input.GetKeyDown(KeyCode.A))
			{
				previousBeam();
			}
			else if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				previousBeam();
			}
		}

		public void nextBeam()
		{
			if (currentBeam < beamLineRendererPrefab.Length - 1)
			{
				currentBeam++;
			}
			else
			{
				currentBeam = 0;
			}
			if ((bool)textBeamName)
			{
				textBeamName.text = beamLineRendererPrefab[currentBeam].name;
			}
		}

		public void previousBeam()
		{
			if (currentBeam > 0)
			{
				currentBeam--;
			}
			else
			{
				currentBeam = beamLineRendererPrefab.Length - 1;
			}
			if ((bool)textBeamName)
			{
				textBeamName.text = beamLineRendererPrefab[currentBeam].name;
			}
		}

		public void UpdateEndOffset()
		{
			beamEndOffset = endOffSetSlider.value;
		}

		public void UpdateScrollSpeed()
		{
			textureScrollSpeed = scrollSpeedSlider.value;
		}

		private void ShootBeamInDir(Vector3 start, Vector3 dir)
		{
			line.positionCount = 2;
			line.SetPosition(0, start);
			beamStart.transform.position = start;
			Vector3 zero = Vector3.zero;
			zero = ((!Physics.Raycast(start, dir, out var hitInfo)) ? (base.transform.position + dir * 100f) : (hitInfo.point - dir.normalized * beamEndOffset));
			beamEnd.transform.position = zero;
			line.SetPosition(1, zero);
			beamStart.transform.LookAt(beamEnd.transform.position);
			beamEnd.transform.LookAt(beamStart.transform.position);
			float num = Vector3.Distance(start, zero);
			line.sharedMaterial.mainTextureScale = new Vector2(num / textureLengthScale, 1f);
			line.sharedMaterial.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0f);
		}
	}
}
