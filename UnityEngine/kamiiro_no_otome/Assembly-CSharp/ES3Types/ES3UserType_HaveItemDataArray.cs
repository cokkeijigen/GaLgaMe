namespace ES3Types
{
	public class ES3UserType_HaveItemDataArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_HaveItemDataArray()
			: base(typeof(HaveItemData[]), ES3UserType_HaveItemData.Instance)
		{
			Instance = this;
		}
	}
}
