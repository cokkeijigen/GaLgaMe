namespace I2.Loc
{
	public class LocalizeTargetDesc_Prefab : LocalizeTargetDesc<LocalizeTarget_UnityStandard_Prefab>
	{
		public override bool CanLocalize(Localize cmp)
		{
			return true;
		}
	}
}
