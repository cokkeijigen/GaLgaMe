namespace I2.Loc
{
	public class LocalizeTargetDesc_Child : LocalizeTargetDesc<LocalizeTarget_UnityStandard_Child>
	{
		public override bool CanLocalize(Localize cmp)
		{
			return cmp.transform.childCount > 1;
		}
	}
}
