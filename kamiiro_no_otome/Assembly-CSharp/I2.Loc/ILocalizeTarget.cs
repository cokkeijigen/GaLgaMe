using UnityEngine;

namespace I2.Loc
{
	public abstract class ILocalizeTarget : ScriptableObject
	{
		public abstract bool IsValid(Localize cmp);

		public abstract void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm);

		public abstract void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation);

		public abstract bool CanUseSecondaryTerm();

		public abstract bool AllowMainTermToBeRTL();

		public abstract bool AllowSecondTermToBeRTL();

		public abstract eTermType GetPrimaryTermType(Localize cmp);

		public abstract eTermType GetSecondaryTermType(Localize cmp);
	}
}
