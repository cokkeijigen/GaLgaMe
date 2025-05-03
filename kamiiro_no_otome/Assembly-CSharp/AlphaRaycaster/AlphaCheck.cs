using UnityEngine;
using UnityEngine.UI;

namespace AlphaRaycaster
{
	[AddComponentMenu("Event/Alpha Check")]
	[ExecuteInEditMode]
	public class AlphaCheck : MonoBehaviour, ICanvasRaycastFilter
	{
		[Range(0f, 1f)]
		[Tooltip("Texture regions with opacity (alpha) lower than alpha threshold won't react to input events.")]
		public float AlphaThreshold = 0.9f;

		[Tooltip("Whether material tint color should affect alpha threshold.")]
		public bool IncludeMaterialAlpha;

		private GameObject gameObj;

		private Image checkedImage;

		private RawImage checkedRawImage;

		private Text checkedText;

		private bool isSetupValid;

		private void Awake()
		{
			gameObj = base.gameObject;
			checkedImage = GetComponent<Image>();
			checkedRawImage = GetComponent<RawImage>();
			checkedText = GetComponent<Text>();
			isSetupValid = (bool)checkedImage || (bool)checkedRawImage || (bool)checkedText;
		}

		public bool IsRaycastLocationValid(Vector2 screenPosition, Camera eventCamera)
		{
			if (!isSetupValid)
			{
				return true;
			}
			if ((bool)checkedImage)
			{
				return !AlphaRaycaster.AlphaCheckImage(gameObj, checkedImage, screenPosition, eventCamera, AlphaThreshold, IncludeMaterialAlpha);
			}
			if ((bool)checkedRawImage)
			{
				return !AlphaRaycaster.AlphaCheckRawImage(gameObj, checkedRawImage, screenPosition, eventCamera, AlphaThreshold, IncludeMaterialAlpha);
			}
			if ((bool)checkedText)
			{
				return !AlphaRaycaster.AlphaCheckText(gameObj, checkedText, screenPosition, eventCamera, AlphaThreshold, IncludeMaterialAlpha);
			}
			return true;
		}
	}
}
