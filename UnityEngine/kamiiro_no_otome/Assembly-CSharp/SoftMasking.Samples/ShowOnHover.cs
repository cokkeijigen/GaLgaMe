using UnityEngine;
using UnityEngine.EventSystems;

namespace SoftMasking.Samples
{
	[RequireComponent(typeof(RectTransform))]
	public class ShowOnHover : UIBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		public CanvasGroup targetGroup;

		private bool _forcedVisible;

		private bool _isPointerOver;

		public bool forcedVisible
		{
			get
			{
				return _forcedVisible;
			}
			set
			{
				if (_forcedVisible != value)
				{
					_forcedVisible = value;
					UpdateVisibility();
				}
			}
		}

		protected override void Start()
		{
			base.Start();
			UpdateVisibility();
		}

		private void UpdateVisibility()
		{
			SetVisible(ShouldBeVisible());
		}

		private bool ShouldBeVisible()
		{
			if (!_forcedVisible)
			{
				return _isPointerOver;
			}
			return true;
		}

		private void SetVisible(bool visible)
		{
			if ((bool)targetGroup)
			{
				targetGroup.alpha = (visible ? 1f : 0f);
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			_isPointerOver = true;
			UpdateVisibility();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			_isPointerOver = false;
			UpdateVisibility();
		}
	}
}
