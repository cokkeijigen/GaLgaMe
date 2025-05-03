using UnityEngine;

public class SciFiBeamStatic : MonoBehaviour
{
	[Header("Prefabs")]
	public GameObject beamLineRendererPrefab;

	public GameObject beamStartPrefab;

	public GameObject beamEndPrefab;

	private GameObject beamStart;

	private GameObject beamEnd;

	private GameObject beam;

	private LineRenderer line;

	[Header("Beam Options")]
	public bool alwaysOn = true;

	public bool beamCollides = true;

	public float beamLength = 100f;

	public float beamEndOffset;

	public float textureScrollSpeed;

	public float textureLengthScale = 1f;

	private void Start()
	{
	}

	private void OnEnable()
	{
		if (alwaysOn)
		{
			SpawnBeam();
		}
	}

	private void OnDisable()
	{
		RemoveBeam();
	}

	private void FixedUpdate()
	{
		if ((bool)beam)
		{
			line.SetPosition(0, base.transform.position);
			RaycastHit hitInfo;
			Vector3 vector = ((!beamCollides || !Physics.Raycast(base.transform.position, base.transform.forward, out hitInfo)) ? (base.transform.position + base.transform.forward * beamLength) : (hitInfo.point - base.transform.forward * beamEndOffset));
			line.SetPosition(1, vector);
			if ((bool)beamStart)
			{
				beamStart.transform.position = base.transform.position;
				beamStart.transform.LookAt(vector);
			}
			if ((bool)beamEnd)
			{
				beamEnd.transform.position = vector;
				beamEnd.transform.LookAt(beamStart.transform.position);
			}
			float num = Vector3.Distance(base.transform.position, vector);
			line.material.mainTextureScale = new Vector2(num / textureLengthScale, 1f);
			line.material.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0f);
		}
	}

	public void SpawnBeam()
	{
		if ((bool)beamLineRendererPrefab)
		{
			if ((bool)beamStartPrefab)
			{
				beamStart = Object.Instantiate(beamStartPrefab);
			}
			if ((bool)beamEndPrefab)
			{
				beamEnd = Object.Instantiate(beamEndPrefab);
			}
			beam = Object.Instantiate(beamLineRendererPrefab);
			beam.transform.position = base.transform.position;
			beam.transform.parent = base.transform;
			beam.transform.rotation = base.transform.rotation;
			line = beam.GetComponent<LineRenderer>();
			line.useWorldSpace = true;
			line.positionCount = 2;
		}
		else
		{
			MonoBehaviour.print("Add a hecking prefab with a line renderer to the SciFiBeamStatic script on " + base.gameObject.name + "! Heck!");
		}
	}

	public void RemoveBeam()
	{
		if ((bool)beam)
		{
			Object.Destroy(beam);
		}
		if ((bool)beamStart)
		{
			Object.Destroy(beamStart);
		}
		if ((bool)beamEnd)
		{
			Object.Destroy(beamEnd);
		}
	}
}
