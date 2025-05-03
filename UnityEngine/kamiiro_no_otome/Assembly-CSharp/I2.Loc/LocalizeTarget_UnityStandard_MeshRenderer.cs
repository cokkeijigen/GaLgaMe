using UnityEngine;

namespace I2.Loc
{
	public class LocalizeTarget_UnityStandard_MeshRenderer : LocalizeTarget<MeshRenderer>
	{
		static LocalizeTarget_UnityStandard_MeshRenderer()
		{
			AutoRegister();
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void AutoRegister()
		{
			LocalizationManager.RegisterTarget(new LocalizeTargetDesc_Type<MeshRenderer, LocalizeTarget_UnityStandard_MeshRenderer>
			{
				Name = "MeshRenderer",
				Priority = 800
			});
		}

		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			return eTermType.Mesh;
		}

		public override eTermType GetSecondaryTermType(Localize cmp)
		{
			return eTermType.Material;
		}

		public override bool CanUseSecondaryTerm()
		{
			return true;
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
			if (mTarget == null)
			{
				primaryTerm = (secondaryTerm = null);
			}
			else
			{
				MeshFilter component = mTarget.GetComponent<MeshFilter>();
				if (component == null || component.sharedMesh == null)
				{
					primaryTerm = null;
				}
				else
				{
					primaryTerm = component.sharedMesh.name;
				}
			}
			if (mTarget == null || mTarget.sharedMaterial == null)
			{
				secondaryTerm = null;
			}
			else
			{
				secondaryTerm = mTarget.sharedMaterial.name;
			}
		}

		public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
		{
			Material secondaryTranslatedObj = cmp.GetSecondaryTranslatedObj<Material>(ref mainTranslation, ref secondaryTranslation);
			if (secondaryTranslatedObj != null && mTarget.sharedMaterial != secondaryTranslatedObj)
			{
				mTarget.material = secondaryTranslatedObj;
			}
			Mesh mesh = cmp.FindTranslatedObject<Mesh>(mainTranslation);
			MeshFilter component = mTarget.GetComponent<MeshFilter>();
			if (mesh != null && component.sharedMesh != mesh)
			{
				component.mesh = mesh;
			}
		}
	}
}
