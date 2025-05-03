namespace ES3Types
{
	public class ES3UserType_PlayerOptionsDataManagerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_PlayerOptionsDataManagerArray()
			: base(typeof(PlayerOptionsDataManager[]), ES3UserType_PlayerOptionsDataManager.Instance)
		{
			Instance = this;
		}
	}
}
