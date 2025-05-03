using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace I2.Loc
{
	public class TranslationJob_POST : TranslationJob_WWW
	{
		private Dictionary<string, TranslationQuery> _requests;

		private GoogleTranslation.fnOnTranslationReady _OnTranslationReady;

		public TranslationJob_POST(Dictionary<string, TranslationQuery> requests, GoogleTranslation.fnOnTranslationReady OnTranslationReady)
		{
			_requests = requests;
			_OnTranslationReady = OnTranslationReady;
			List<string> list = GoogleTranslation.ConvertTranslationRequest(requests, encodeGET: false);
			WWWForm wWWForm = new WWWForm();
			wWWForm.AddField("action", "Translate");
			wWWForm.AddField("list", list[0]);
			www = UnityWebRequest.Post(LocalizationManager.GetWebServiceURL(), wWWForm);
			I2Utils.SendWebRequest(www);
		}

		public override eJobState GetState()
		{
			if (www != null && www.isDone)
			{
				ProcessResult(www.downloadHandler.data, www.error);
				www.Dispose();
				www = null;
			}
			return mJobState;
		}

		public void ProcessResult(byte[] bytes, string errorMsg)
		{
			if (!string.IsNullOrEmpty(errorMsg))
			{
				mJobState = eJobState.Failed;
				return;
			}
			errorMsg = GoogleTranslation.ParseTranslationResult(Encoding.UTF8.GetString(bytes, 0, bytes.Length), _requests);
			if (_OnTranslationReady != null)
			{
				_OnTranslationReady(_requests, errorMsg);
			}
			mJobState = eJobState.Succeeded;
		}
	}
}
