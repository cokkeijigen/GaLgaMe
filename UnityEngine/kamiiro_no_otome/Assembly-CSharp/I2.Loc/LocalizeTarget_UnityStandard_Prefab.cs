using UnityEngine;

namespace I2.Loc
{
	public class LocalizeTarget_UnityStandard_Prefab : LocalizeTarget<GameObject>
	{
		static LocalizeTarget_UnityStandard_Prefab()
		{
			AutoRegister();
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void AutoRegister()
		{
			LocalizationManager.RegisterTarget(new LocalizeTargetDesc_Prefab
			{
				Name = "Prefab",
				Priority = 250
			});
		}

		public override bool IsValid(Localize cmp)
		{
			return true;
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
			if (string.IsNullOrEmpty(mainTranslation) || ((bool)mTarget && mTarget.name == mainTranslation))
			{
				return;
			}
			Transform transform = cmp.transform;
			string text = mainTranslation;
			int num = mainTranslation.LastIndexOfAny(LanguageSourceData.CategorySeparators);
			if (num >= 0)
			{
				text = text.Substring(num + 1);
			}
			Transform transform2 = InstantiateNewPrefab(cmp, mainTranslation);
			if (transform2 == null)
			{
				return;
			}
			transform2.name = text;
			for (int num2 = transform.childCount - 1; num2 >= 0; num2--)
			{
				Transform child = transform.GetChild(num2);
				if (child != transform2)
				{
					Object.Destroy(child.gameObject);
				}
			}
		}

		private Transform InstantiateNewPrefab(Localize cmp, string mainTranslation)
		{
			GameObject gameObject = cmp.FindTranslatedObject<GameObject>(mainTranslation);
			if (gameObject == null)
			{
				return null;
			}
			GameObject gameObject2 = mTarget;
			mTarget = Object.Instantiate(gameObject);
			if (mTarget == null)
			{
				return null;
			}
			Transform transform = cmp.transform;
			Transform transform2 = mTarget.transform;
			transform2.SetParent(transform);
			Transform transform3 = (gameObject2 ? gameObject2.transform : transform);
			transform2.rotation = transform3.rotation;
			transform2.position = transform3.position;
			return transform2;
		}
	}
}
