using UnityEngine;

namespace I2.Loc
{
	public class LocalizeTarget_UnityStandard_TextMesh : LocalizeTarget<TextMesh>
	{
		private TextAlignment mAlignment_RTL = TextAlignment.Right;

		private TextAlignment mAlignment_LTR;

		private bool mAlignmentWasRTL;

		private bool mInitializeAlignment = true;

		static LocalizeTarget_UnityStandard_TextMesh()
		{
			AutoRegister();
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void AutoRegister()
		{
			LocalizationManager.RegisterTarget(new LocalizeTargetDesc_Type<TextMesh, LocalizeTarget_UnityStandard_TextMesh>
			{
				Name = "TextMesh",
				Priority = 100
			});
		}

		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			return eTermType.Text;
		}

		public override eTermType GetSecondaryTermType(Localize cmp)
		{
			return eTermType.Font;
		}

		public override bool CanUseSecondaryTerm()
		{
			return true;
		}

		public override bool AllowMainTermToBeRTL()
		{
			return true;
		}

		public override bool AllowSecondTermToBeRTL()
		{
			return false;
		}

		public override void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm)
		{
			primaryTerm = (mTarget ? mTarget.text : null);
			secondaryTerm = ((string.IsNullOrEmpty(Secondary) && mTarget.font != null) ? mTarget.font.name : null);
		}

		public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
		{
			Font secondaryTranslatedObj = cmp.GetSecondaryTranslatedObj<Font>(ref mainTranslation, ref secondaryTranslation);
			if (secondaryTranslatedObj != null && mTarget.font != secondaryTranslatedObj)
			{
				mTarget.font = secondaryTranslatedObj;
				mTarget.GetComponentInChildren<MeshRenderer>().material = secondaryTranslatedObj.material;
			}
			if (mInitializeAlignment)
			{
				mInitializeAlignment = false;
				mAlignment_LTR = (mAlignment_RTL = mTarget.alignment);
				if (LocalizationManager.IsRight2Left && mAlignment_RTL == TextAlignment.Right)
				{
					mAlignment_LTR = TextAlignment.Left;
				}
				if (!LocalizationManager.IsRight2Left && mAlignment_LTR == TextAlignment.Left)
				{
					mAlignment_RTL = TextAlignment.Right;
				}
			}
			if (mainTranslation != null && mTarget.text != mainTranslation)
			{
				if (cmp.CorrectAlignmentForRTL && mTarget.alignment != TextAlignment.Center)
				{
					mTarget.alignment = (LocalizationManager.IsRight2Left ? mAlignment_RTL : mAlignment_LTR);
				}
				mTarget.font.RequestCharactersInTexture(mainTranslation);
				mTarget.text = mainTranslation;
			}
		}
	}
}
