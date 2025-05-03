using UnityEngine;
using UnityEngine.UI;

public class SetScrollSensitivity : MonoBehaviour
{
	private void OnEnable()
	{
		GetComponent<ScrollRect>().scrollSensitivity = PlayerOptionsDataManager.optionsMouseWheelPower * 200f;
	}
}
