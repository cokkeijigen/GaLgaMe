using UnityEngine;
using UnityEngine.UI;

namespace ETFXPEL
{
	public class UICanvasManager : MonoBehaviour
	{
		public static UICanvasManager GlobalAccess;

		public bool MouseOverButton;

		public Text PENameText;

		public Text ToolTipText;

		private RaycastHit rayHit;

		private void Awake()
		{
			GlobalAccess = this;
		}

		private void Start()
		{
			if (PENameText != null)
			{
				PENameText.text = ParticleEffectsLibrary.GlobalAccess.GetCurrentPENameString();
			}
		}

		private void Update()
		{
			if (!MouseOverButton && Input.GetMouseButtonUp(0))
			{
				SpawnCurrentParticleEffect();
			}
			if (Input.GetKeyUp(KeyCode.A))
			{
				SelectPreviousPE();
			}
			if (Input.GetKeyUp(KeyCode.D))
			{
				SelectNextPE();
			}
		}

		public void UpdateToolTip(ButtonTypes toolTipType)
		{
			if (ToolTipText != null)
			{
				switch (toolTipType)
				{
				case ButtonTypes.Previous:
					ToolTipText.text = "Select Previous Particle Effect";
					break;
				case ButtonTypes.Next:
					ToolTipText.text = "Select Next Particle Effect";
					break;
				}
			}
		}

		public void ClearToolTip()
		{
			if (ToolTipText != null)
			{
				ToolTipText.text = "";
			}
		}

		private void SelectPreviousPE()
		{
			ParticleEffectsLibrary.GlobalAccess.PreviousParticleEffect();
			if (PENameText != null)
			{
				PENameText.text = ParticleEffectsLibrary.GlobalAccess.GetCurrentPENameString();
			}
		}

		private void SelectNextPE()
		{
			ParticleEffectsLibrary.GlobalAccess.NextParticleEffect();
			if (PENameText != null)
			{
				PENameText.text = ParticleEffectsLibrary.GlobalAccess.GetCurrentPENameString();
			}
		}

		private void SpawnCurrentParticleEffect()
		{
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit))
			{
				ParticleEffectsLibrary.GlobalAccess.SpawnParticleEffect(rayHit.point);
			}
		}

		public void UIButtonClick(ButtonTypes buttonTypeClicked)
		{
			switch (buttonTypeClicked)
			{
			case ButtonTypes.Previous:
				SelectPreviousPE();
				break;
			case ButtonTypes.Next:
				SelectNextPE();
				break;
			}
		}
	}
}
