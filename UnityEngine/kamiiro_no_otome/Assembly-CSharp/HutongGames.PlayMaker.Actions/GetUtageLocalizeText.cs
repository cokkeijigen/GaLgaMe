using Utage;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Utage")]
	[Tooltip("")]
	public class GetUtageLocalizeText : FsmStateAction
	{
		[RequiredField]
		public FsmString localizekey;

		[RequiredField]
		public FsmString stringVariable;

		public override void Reset()
		{
			localizekey = null;
			stringVariable = null;
		}

		public override void OnEnter()
		{
			stringVariable.Value = LanguageManagerBase.Instance.LocalizeText(localizekey.Value);
			Finish();
		}
	}
}
