using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HutongGames.PlayMaker
{
	[AddComponentMenu("PlayMaker/UI/UI End Edit Event")]
	public class PlayMakerUiEndEditEvent : PlayMakerUiEventBase
	{
		public InputField inputField;

		public TMP_InputField tmpInputField;

		protected override void Initialize()
		{
			if (initialized)
			{
				return;
			}
			initialized = true;
			if (inputField == null)
			{
				inputField = GetComponent<InputField>();
			}
			if (inputField != null)
			{
				inputField.onEndEdit.AddListener(DoOnEndEdit);
			}
			if (!(inputField != null))
			{
				if (tmpInputField == null)
				{
					tmpInputField = GetComponent<TMP_InputField>();
				}
				if (tmpInputField != null)
				{
					tmpInputField.onEndEdit.AddListener(DoOnEndEdit);
				}
			}
		}

		protected void OnDisable()
		{
			initialized = false;
			if (inputField != null)
			{
				inputField.onEndEdit.RemoveListener(DoOnEndEdit);
			}
			if (tmpInputField != null)
			{
				tmpInputField.onEndEdit.RemoveListener(DoOnEndEdit);
			}
		}

		private void DoOnEndEdit(string value)
		{
			Fsm.EventData.StringData = value;
			SendEvent(FsmEvent.UiEndEdit);
		}
	}
}
