using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SoftMasking.Samples
{
	[RequireComponent(typeof(RectTransform))]
	public class RectManipulator : UIBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		[Flags]
		public enum ManipulationType
		{
			None = 0,
			Move = 1,
			ResizeLeft = 2,
			ResizeUp = 4,
			ResizeRight = 8,
			ResizeDown = 0x10,
			ResizeUpLeft = 6,
			ResizeUpRight = 0xC,
			ResizeDownLeft = 0x12,
			ResizeDownRight = 0x18,
			Rotate = 0x20
		}

		public RectTransform targetTransform;

		public ManipulationType manipulation;

		public ShowOnHover showOnHover;

		[Header("Limits")]
		public Vector2 minSize;

		[Header("Display")]
		public Graphic icon;

		public float normalAlpha = 0.2f;

		public float selectedAlpha = 1f;

		public float transitionDuration = 0.2f;

		private bool _isManipulatedNow;

		private Vector2 _startAnchoredPosition;

		private Vector2 _startSizeDelta;

		private float _startRotation;

		private RectTransform parentTransform => targetTransform.parent as RectTransform;

		public void OnPointerEnter(PointerEventData eventData)
		{
			HighlightIcon(highlight: true);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (!_isManipulatedNow)
			{
				HighlightIcon(highlight: false);
			}
		}

		private void HighlightIcon(bool highlight, bool instant = false)
		{
			if ((bool)icon)
			{
				float alpha = (highlight ? selectedAlpha : normalAlpha);
				float duration = (instant ? 0f : transitionDuration);
				icon.CrossFadeAlpha(alpha, duration, ignoreTimeScale: true);
			}
			if ((bool)showOnHover)
			{
				showOnHover.forcedVisible = highlight;
			}
		}

		protected override void Start()
		{
			base.Start();
			HighlightIcon(highlight: false, instant: true);
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			_isManipulatedNow = true;
			RememberStartTransform();
		}

		private void RememberStartTransform()
		{
			if ((bool)targetTransform)
			{
				_startAnchoredPosition = targetTransform.anchoredPosition;
				_startSizeDelta = targetTransform.sizeDelta;
				_startRotation = targetTransform.localRotation.eulerAngles.z;
			}
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (!(targetTransform == null) && !(parentTransform == null) && _isManipulatedNow)
			{
				Vector2 vector = ToParentSpace(eventData.pressPosition, eventData.pressEventCamera);
				Vector2 vector2 = ToParentSpace(eventData.position, eventData.pressEventCamera);
				DoRotate(vector, vector2);
				Vector2 parentSpaceMovement = vector2 - vector;
				DoMove(parentSpaceMovement);
				DoResize(parentSpaceMovement);
			}
		}

		private Vector2 ToParentSpace(Vector2 position, Camera eventCamera)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(parentTransform, position, eventCamera, out var localPoint);
			return localPoint;
		}

		private void DoMove(Vector2 parentSpaceMovement)
		{
			if (Is(ManipulationType.Move))
			{
				MoveTo(_startAnchoredPosition + parentSpaceMovement);
			}
		}

		private bool Is(ManipulationType expected)
		{
			return (manipulation & expected) == expected;
		}

		private void MoveTo(Vector2 desiredAnchoredPosition)
		{
			targetTransform.anchoredPosition = ClampPosition(desiredAnchoredPosition);
		}

		private Vector2 ClampPosition(Vector2 position)
		{
			Vector2 vector = parentTransform.rect.size / 2f;
			return new Vector2(Mathf.Clamp(position.x, 0f - vector.x, vector.x), Mathf.Clamp(position.y, 0f - vector.y, vector.y));
		}

		private void DoRotate(Vector2 startParentPoint, Vector2 targetParentPoint)
		{
			if (Is(ManipulationType.Rotate))
			{
				Vector2 startLever = startParentPoint - (Vector2)targetTransform.localPosition;
				Vector2 endLever = targetParentPoint - (Vector2)targetTransform.localPosition;
				float num = DeltaRotation(startLever, endLever);
				targetTransform.localRotation = Quaternion.AngleAxis(_startRotation + num, Vector3.forward);
			}
		}

		private float DeltaRotation(Vector2 startLever, Vector2 endLever)
		{
			float current = Mathf.Atan2(startLever.y, startLever.x) * 57.29578f;
			float target = Mathf.Atan2(endLever.y, endLever.x) * 57.29578f;
			return Mathf.DeltaAngle(current, target);
		}

		private void DoResize(Vector2 parentSpaceMovement)
		{
			Vector3 vector = Quaternion.Inverse(targetTransform.rotation) * parentSpaceMovement;
			Vector2 localOffset = ProjectResizeOffset(vector);
			if (localOffset.sqrMagnitude > 0f)
			{
				SetSizeDirected(localOffset, ResizeSign());
			}
		}

		private Vector2 ProjectResizeOffset(Vector2 localOffset)
		{
			bool num = Is(ManipulationType.ResizeLeft) || Is(ManipulationType.ResizeRight);
			return new Vector2(y: (Is(ManipulationType.ResizeUp) || Is(ManipulationType.ResizeDown)) ? localOffset.y : 0f, x: num ? localOffset.x : 0f);
		}

		private Vector2 ResizeSign()
		{
			return new Vector2(Is(ManipulationType.ResizeLeft) ? (-1f) : 1f, Is(ManipulationType.ResizeDown) ? (-1f) : 1f);
		}

		private void SetSizeDirected(Vector2 localOffset, Vector2 sizeSign)
		{
			Vector2 vector = ClampSize(_startSizeDelta + Vector2.Scale(localOffset, sizeSign));
			targetTransform.sizeDelta = vector;
			Vector2 vector2 = Vector2.Scale((vector - _startSizeDelta) / 2f, sizeSign);
			MoveTo(_startAnchoredPosition + (Vector2)targetTransform.TransformVector(vector2));
		}

		private Vector2 ClampSize(Vector2 size)
		{
			return new Vector2(Mathf.Max(size.x, minSize.x), Mathf.Max(size.y, minSize.y));
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			_isManipulatedNow = false;
			if (!eventData.hovered.Contains(base.gameObject))
			{
				HighlightIcon(highlight: false);
			}
		}
	}
}
