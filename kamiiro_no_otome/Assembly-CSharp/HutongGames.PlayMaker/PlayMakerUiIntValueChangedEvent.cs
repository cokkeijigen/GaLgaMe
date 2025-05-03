using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker
{
	[AddComponentMenu("PlayMaker/UI/UI Int Value Changed Event")]
	public class PlayMakerUiIntValueChangedEvent : PlayMakerUiEventBase
	{
		public Dropdown dropdown;

		public TMP_Dropdown tmpDropdown;

		protected override void Initialize()
		{
			if (initialized)
			{
				return;
			}
			initialized = true;
			if (dropdown == null)
			{
				dropdown = GetComponent<Dropdown>();
			}
			if (dropdown != null)
			{
				dropdown.onValueChanged.AddListener(OnValueChanged);
			}
			if (!(dropdown != null))
			{
				if (tmpDropdown == null)
				{
					tmpDropdown = GetComponent<TMP_Dropdown>();
				}
				if (tmpDropdown != null)
				{
					tmpDropdown.onValueChanged.AddListener(OnValueChanged);
				}
			}
		}

		protected void OnDisable()
		{
			initialized = false;
			if (dropdown != null)
			{
				dropdown.onValueChanged.RemoveListener(OnValueChanged);
			}
			if (tmpDropdown != null)
			{
				tmpDropdown.onValueChanged.RemoveListener(OnValueChanged);
			}
		}

		private void OnValueChanged(int value)
		{
			Fsm.EventData.IntData = value;
			SendEvent(FsmEvent.UiIntValueChanged);
		}
	}
}
