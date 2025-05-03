using UnityEngine;
using UnityEngine.EventSystems;

namespace SoftMasking.Samples
{
	public class Tooltip : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		public RectTransform tooltip;

		public void LateUpdate()
		{
			if (tooltip.gameObject.activeInHierarchy && RectTransformUtility.ScreenPointToLocalPointInRectangle(tooltip.parent.GetComponent<RectTransform>(), Input.mousePosition, null, out var localPoint))
			{
				tooltip.anchoredPosition = localPoint + new Vector2(10f, -20f);
			}
		}

		void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
		{
			tooltip.gameObject.SetActive(value: true);
		}

		void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
		{
			tooltip.gameObject.SetActive(value: false);
		}
	}
}
