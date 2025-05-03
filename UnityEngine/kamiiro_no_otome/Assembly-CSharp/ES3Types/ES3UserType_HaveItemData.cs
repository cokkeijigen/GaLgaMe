using System;
using System.Collections;
using UnityEngine.Scripting;

namespace ES3Types
{
	[Preserve]
	[ES3Properties(new string[] { "itemSortID", "itemID", "haveCountNum" })]
	public class ES3UserType_HaveItemData : ES3ObjectType
	{
		public static ES3Type Instance;

		public ES3UserType_HaveItemData()
			: base(typeof(HaveItemData))
		{
			Instance = this;
			priority = 1;
		}

		protected override void WriteObject(object obj, ES3Writer writer)
		{
			HaveItemData haveItemData = (HaveItemData)obj;
			writer.WriteProperty("itemSortID", haveItemData.itemSortID, ES3Type_int.Instance);
			writer.WriteProperty("itemID", haveItemData.itemID, ES3Type_int.Instance);
			writer.WriteProperty("haveCountNum", haveItemData.haveCountNum, ES3Type_int.Instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			HaveItemData haveItemData = (HaveItemData)obj;
			IEnumerator enumerator = reader.Properties.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					switch ((string)enumerator.Current)
					{
					case "itemSortID":
						haveItemData.itemSortID = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "itemID":
						haveItemData.itemID = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "haveCountNum":
						haveItemData.haveCountNum = reader.Read<int>(ES3Type_int.Instance);
						break;
					default:
						reader.Skip();
						break;
					}
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			HaveItemData haveItemData = new HaveItemData();
			ReadObject<T>(reader, haveItemData);
			return haveItemData;
		}
	}
}
