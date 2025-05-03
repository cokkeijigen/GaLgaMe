namespace ES3Types
{
	public class ES3UserType_PlayerDataManagerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_PlayerDataManagerArray()
			: base(typeof(PlayerDataManager[]), ES3UserType_PlayerDataManager.Instance)
		{
			Instance = this;
		}
	}
}
