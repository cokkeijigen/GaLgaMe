using UnityEngine;
using UnityEngine.UI;

namespace I2.Loc
{
	public class LocalizeTarget_UnityUI_Image : LocalizeTarget<Image>
	{
		static LocalizeTarget_UnityUI_Image()
		{
			AutoRegister();
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void AutoRegister()
		{
			LocalizationManager.RegisterTarget(new LocalizeTargetDesc_Type<Image, LocalizeTarget_UnityUI_Image>
			{
				Name = "Image",
				Priority = 100
			});
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

		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			if (!(mTarget.sprite == null))
			{
				return eTermType.Sprite;
			}
			return eTermType.Texture;
		}

		public override eTermType GetSecondaryTermType(Localize cmp)
		{
			return eTermType.Text;
		}

		public override void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm)
		{
			primaryTerm = (mTarget.mainTexture ? mTarget.mainTexture.name : "");
			if (mTarget.sprite != null && mTarget.sprite.name != primaryTerm)
			{
				primaryTerm = primaryTerm + "." + mTarget.sprite.name;
			}
			secondaryTerm = null;
		}

		public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
		{
			Sprite sprite = mTarget.sprite;
			if (sprite == null || sprite.name != mainTranslation)
			{
				mTarget.sprite = cmp.FindTranslatedObject<Sprite>(mainTranslation);
			}
		}
	}
}
