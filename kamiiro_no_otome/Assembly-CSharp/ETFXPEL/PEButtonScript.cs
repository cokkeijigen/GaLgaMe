using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ETFXPEL
{
	public class PEButtonScript : MonoBehaviour, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
	{
		private Button myButton;

		public ButtonTypes ButtonType;

		private void Start()
		{
			myButton = base.gameObject.GetComponent<Button>();
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			UICanvasManager.GlobalAccess.MouseOverButton = true;
			UICanvasManager.GlobalAccess.UpdateToolTip(ButtonType);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			UICanvasManager.GlobalAccess.MouseOverButton = false;
			UICanvasManager.GlobalAccess.ClearToolTip();
		}

		public void OnButtonClicked()
		{
			UICanvasManager.GlobalAccess.UIButtonClick(ButtonType);
		}
	}
}
