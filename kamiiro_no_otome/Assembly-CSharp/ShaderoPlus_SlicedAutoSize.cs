using UnityEngine;

[ExecuteInEditMode]
public class ShaderoPlus_SlicedAutoSize : MonoBehaviour
{
	public Material material;

	public float ScaleEverything = 1f;

	public string VariableNameForScaleX = "SlicedHorizontalBarUV_StretchSize_1";

	public string VariableNameForScaleY = "SlicedVerticalBarUV_StretchSize_1";

	private Transform t;

	private void Start()
	{
		t = GetComponent<Transform>();
	}

	private void Update()
	{
		Vector3 localScale = t.localScale;
		material.SetFloat(VariableNameForScaleX, localScale.x * ScaleEverything);
		material.SetFloat(VariableNameForScaleY, localScale.y * ScaleEverything);
	}
}
