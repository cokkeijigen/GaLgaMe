using System.Collections.Generic;
using UnityEngine;

public class RandomManager : MonoBehaviour
{
	public static int GetRandomValue(Dictionary<int, float> dictionary)
	{
		float num = 0f;
		foreach (KeyValuePair<int, float> item in dictionary)
		{
			num += item.Value;
		}
		float num2 = Random.value * num;
		foreach (KeyValuePair<int, float> item2 in dictionary)
		{
			if (num2 < item2.Value)
			{
				return item2.Key;
			}
			num2 -= item2.Value;
		}
		return 0;
	}
}
