using System;
using UnityEngine;

public class SciFiLightFlicker : MonoBehaviour
{
	public string waveFunction = "sin";

	public float startValue;

	public float amplitude = 1f;

	public float phase;

	public float frequency = 0.5f;

	private Color originalColor;

	private void Start()
	{
		originalColor = GetComponent<Light>().color;
	}

	private void Update()
	{
		GetComponent<Light>().color = originalColor * EvalWave();
	}

	private float EvalWave()
	{
		float num = (Time.time + phase) * frequency;
		num -= Mathf.Floor(num);
		float num2 = ((waveFunction == "sin") ? Mathf.Sin(num * 2f * MathF.PI) : ((waveFunction == "tri") ? ((!(num < 0.5f)) ? (-4f * num + 3f) : (4f * num - 1f)) : ((waveFunction == "sqr") ? ((!(num < 0.5f)) ? (-1f) : 1f) : ((waveFunction == "saw") ? num : ((waveFunction == "inv") ? (1f - num) : ((!(waveFunction == "noise")) ? 1f : (1f - UnityEngine.Random.value * 2f)))))));
		return num2 * amplitude + startValue;
	}
}
