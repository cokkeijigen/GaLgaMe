using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SoftMasking.Samples
{
	[RequireComponent(typeof(RectTransform))]
	public class MaskPainter : UIBehaviour, IPointerDownHandler, IEventSystemHandler, IDragHandler
	{
		public Graphic spot;

		public RectTransform stroke;

		private RectTransform _rectTransform;

		protected override void Awake()
		{
			base.Awake();
			_rectTransform = GetComponent<RectTransform>();
		}

		protected override void Start()
		{
			base.Start();
			spot.enabled = false;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			UpdateStrokeByEvent(eventData);
		}

		public void OnDrag(PointerEventData eventData)
		{
			UpdateStrokeByEvent(eventData, drawTrail: true);
		}

		private void UpdateStrokeByEvent(PointerEventData eventData, bool drawTrail = false)
		{
			UpdateStrokePosition(eventData.position, drawTrail);
			UpdateStrokeColor(eventData.button);
		}

		private void UpdateStrokePosition(Vector2 screenPosition, bool drawTrail = false)
		{
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, screenPosition, null, out var localPoint))
			{
				Vector2 anchoredPosition = stroke.anchoredPosition;
				stroke.anchoredPosition = localPoint;
				if (drawTrail)
				{
					SetUpTrail(anchoredPosition);
				}
				spot.enabled = true;
			}
		}

		private void SetUpTrail(Vector2 prevPosition)
		{
			Vector2 vector = stroke.anchoredPosition - prevPosition;
			stroke.localRotation = Quaternion.AngleAxis(Mathf.Atan2(vector.y, vector.x) * 57.29578f, Vector3.forward);
			spot.rectTransform.offsetMin = new Vector2(0f - vector.magnitude, 0f);
		}

		private void UpdateStrokeColor(PointerEventData.InputButton pressedButton)
		{
			spot.materialForRendering.SetInt("_BlendOp", (pressedButton != 0) ? 2 : 0);
		}
	}
}
