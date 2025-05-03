namespace ES3Types
{
	public class ES3UserType_PlayerInventoryDataManagerArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_PlayerInventoryDataManagerArray()
			: base(typeof(PlayerInventoryDataManager[]), ES3UserType_PlayerInventoryDataManager.Instance)
		{
			Instance = this;
		}
	}
}
