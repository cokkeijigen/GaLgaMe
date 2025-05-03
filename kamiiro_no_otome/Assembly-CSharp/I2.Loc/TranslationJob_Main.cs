using System.Collections.Generic;

namespace I2.Loc
{
	public class TranslationJob_Main : TranslationJob
	{
		private TranslationJob_WEB mWeb;

		private TranslationJob_POST mPost;

		private TranslationJob_GET mGet;

		private Dictionary<string, TranslationQuery> _requests;

		private GoogleTranslation.fnOnTranslationReady _OnTranslationReady;

		public string mErrorMessage;

		public TranslationJob_Main(Dictionary<string, TranslationQuery> requests, GoogleTranslation.fnOnTranslationReady OnTranslationReady)
		{
			_requests = requests;
			_OnTranslationReady = OnTranslationReady;
			mPost = new TranslationJob_POST(requests, OnTranslationReady);
		}

		public override eJobState GetState()
		{
			if (mWeb != null)
			{
				switch (mWeb.GetState())
				{
				case eJobState.Running:
					return eJobState.Running;
				case eJobState.Succeeded:
					mJobState = eJobState.Succeeded;
					break;
				case eJobState.Failed:
					mWeb.Dispose();
					mWeb = null;
					mPost = new TranslationJob_POST(_requests, _OnTranslationReady);
					break;
				}
			}
			if (mPost != null)
			{
				switch (mPost.GetState())
				{
				case eJobState.Running:
					return eJobState.Running;
				case eJobState.Succeeded:
					mJobState = eJobState.Succeeded;
					break;
				case eJobState.Failed:
					mPost.Dispose();
					mPost = null;
					mGet = new TranslationJob_GET(_requests, _OnTranslationReady);
					break;
				}
			}
			if (mGet != null)
			{
				switch (mGet.GetState())
				{
				case eJobState.Running:
					return eJobState.Running;
				case eJobState.Succeeded:
					mJobState = eJobState.Succeeded;
					break;
				case eJobState.Failed:
					mErrorMessage = mGet.mErrorMessage;
					if (_OnTranslationReady != null)
					{
						_OnTranslationReady(_requests, mErrorMessage);
					}
					mGet.Dispose();
					mGet = null;
					break;
				}
			}
			return mJobState;
		}

		public override void Dispose()
		{
			if (mPost != null)
			{
				mPost.Dispose();
			}
			if (mGet != null)
			{
				mGet.Dispose();
			}
			mPost = null;
			mGet = null;
		}
	}
}
