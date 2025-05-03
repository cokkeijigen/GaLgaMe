using UnityEngine;

namespace I2.Loc
{
	public class LocalizeTarget_UnityStandard_Child : LocalizeTarget<GameObject>
	{
		static LocalizeTarget_UnityStandard_Child()
		{
			AutoRegister();
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void AutoRegister()
		{
			LocalizationManager.RegisterTarget(new LocalizeTargetDesc_Child
			{
				Name = "Child",
				Priority = 200
			});
		}

		public override bool IsValid(Localize cmp)
		{
			return cmp.transform.childCount > 1;
		}

		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			return eTermType.GameObject;
		}

		public override eTermType GetSecondaryTermType(Localize cmp)
		{
			return eTermType.Text;
		}

		public override bool CanUseSecondaryTerm()
		{
			return false;
		}

		public override bool AllowMainTermToBeRTL()
		{
			return false;
		}

		public override bool AllowSecondTermToBeRTL()
		{
			return false;
		}

		public override void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm)
		{
			primaryTerm = cmp.name;
			secondaryTerm = null;
		}

		public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
		{
			if (!string.IsNullOrEmpty(mainTranslation))
			{
				Transform transform = cmp.transform;
				string text = mainTranslation;
				int num = mainTranslation.LastIndexOfAny(LanguageSourceData.CategorySeparators);
				if (num >= 0)
				{
					text = text.Substring(num + 1);
				}
				for (int i = 0; i < transform.childCount; i++)
				{
					Transform child = transform.GetChild(i);
					child.gameObject.SetActive(child.name == text);
				}
			}
		}
	}
}
