using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine.Scripting;

namespace ES3Types
{
	[Preserve]
	[ES3Properties(new string[]
	{
		"haveItemList", "haveItemMaterialList", "haveItemCanMakeMaterialList", "haveItemCampItemlList", "haveItemMagicMaterialList", "haveCashableItemList", "haveWeaponList", "haveArmorList", "haveAccessoryList", "haveEventItemList",
		"havePartyWeaponList", "havePartyArmorList", "itemRarityList", "playerLearnedSkillList", "serializationData"
	})]
	public class ES3UserType_PlayerInventoryDataManager : ES3ComponentType
	{
		public static ES3Type Instance;

		public ES3UserType_PlayerInventoryDataManager()
			: base(typeof(PlayerInventoryDataManager))
		{
			Instance = this;
			priority = 1;
		}

		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			PlayerInventoryDataManager objectContainingField = (PlayerInventoryDataManager)obj;
			writer.WriteProperty("haveItemList", PlayerInventoryDataManager.haveItemList);
			writer.WriteProperty("haveItemMaterialList", PlayerInventoryDataManager.haveItemMaterialList);
			writer.WriteProperty("haveItemCanMakeMaterialList", PlayerInventoryDataManager.haveItemCanMakeMaterialList);
			writer.WriteProperty("haveItemCampItemlList", PlayerInventoryDataManager.haveItemCampItemList);
			writer.WriteProperty("haveItemMagicMaterialList", PlayerInventoryDataManager.haveItemMagicMaterialList);
			writer.WriteProperty("haveCashableItemList", PlayerInventoryDataManager.haveCashableItemList);
			writer.WriteProperty("haveWeaponList", PlayerInventoryDataManager.haveWeaponList);
			writer.WriteProperty("haveArmorList", PlayerInventoryDataManager.haveArmorList);
			writer.WriteProperty("haveAccessoryList", PlayerInventoryDataManager.haveAccessoryList);
			writer.WriteProperty("haveEventItemList", PlayerInventoryDataManager.haveEventItemList);
			writer.WriteProperty("havePartyWeaponList", PlayerInventoryDataManager.havePartyWeaponList);
			writer.WriteProperty("havePartyArmorList", PlayerInventoryDataManager.havePartyArmorList);
			writer.WriteProperty("playerLearnedSkillList", PlayerInventoryDataManager.playerLearnedSkillList);
			writer.WritePrivateField("serializationData", objectContainingField);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			PlayerInventoryDataManager objectContainingField = (PlayerInventoryDataManager)obj;
			IEnumerator enumerator = reader.Properties.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					switch ((string)enumerator.Current)
					{
					case "haveItemList":
						PlayerInventoryDataManager.haveItemList = reader.Read<List<HaveItemData>>();
						break;
					case "haveItemMaterialList":
						PlayerInventoryDataManager.haveItemMaterialList = reader.Read<List<HaveItemData>>();
						break;
					case "haveItemCanMakeMaterialList":
						PlayerInventoryDataManager.haveItemCanMakeMaterialList = reader.Read<List<HaveItemData>>();
						break;
					case "haveItemCampItemlList":
						PlayerInventoryDataManager.haveItemCampItemList = reader.Read<List<HaveCampItemData>>();
						break;
					case "haveItemMagicMaterialList":
						PlayerInventoryDataManager.haveItemMagicMaterialList = reader.Read<List<HaveItemData>>();
						break;
					case "haveCashableItemList":
						PlayerInventoryDataManager.haveCashableItemList = reader.Read<List<HaveItemData>>();
						break;
					case "haveWeaponList":
						PlayerInventoryDataManager.haveWeaponList = reader.Read<List<HaveWeaponData>>();
						break;
					case "haveArmorList":
						PlayerInventoryDataManager.haveArmorList = reader.Read<List<HaveArmorData>>();
						break;
					case "haveAccessoryList":
						PlayerInventoryDataManager.haveAccessoryList = reader.Read<List<HaveAccessoryData>>();
						break;
					case "haveEventItemList":
						PlayerInventoryDataManager.haveEventItemList = reader.Read<List<HaveEventItemData>>();
						break;
					case "havePartyWeaponList":
						PlayerInventoryDataManager.havePartyWeaponList = reader.Read<List<HavePartyWeaponData>>();
						break;
					case "havePartyArmorList":
						PlayerInventoryDataManager.havePartyArmorList = reader.Read<List<HavePartyArmorData>>();
						break;
					case "playerLearnedSkillList":
						PlayerInventoryDataManager.playerLearnedSkillList = reader.Read<List<LearnedSkillData>>();
						break;
					case "serializationData":
						reader.SetPrivateField("serializationData", reader.Read<SerializationData>(), objectContainingField);
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
	}
}
