namespace ES3Types
{
	public class ES3UserType_PlayerFlagDataManagerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_PlayerFlagDataManagerArray()
			: base(typeof(PlayerFlagDataManager[]), ES3UserType_PlayerFlagDataManager.Instance)
		{
			Instance = this;
		}
	}
}
