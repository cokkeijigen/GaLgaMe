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
		"scenarioFlagDictionary", "tutorialFlagDictionary", "sceneGarellyFlagDictionary", "partyPowerUpFlagList", "heroineAllTimeFollowFlagList", "dungeonFlagDictionary", "recipeFlagDictionary", "questClearFlagList", "keyItemFlagDictionary", "eventStartingDayDictionary",
		"serializationData"
	})]
	public class ES3UserType_PlayerFlagDataManager : ES3ComponentType
	{
		public static ES3Type Instance;

		public ES3UserType_PlayerFlagDataManager()
			: base(typeof(PlayerFlagDataManager))
		{
			Instance = this;
			priority = 1;
		}

		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			PlayerFlagDataManager objectContainingField = (PlayerFlagDataManager)obj;
			writer.WriteProperty("scenarioFlagDictionary", PlayerFlagDataManager.scenarioFlagDictionary);
			writer.WriteProperty("tutorialFlagDictionary", PlayerFlagDataManager.tutorialFlagDictionary);
			writer.WriteProperty("sceneGarellyFlagDictionary", PlayerFlagDataManager.sceneGarellyFlagDictionary);
			writer.WriteProperty("partyPowerUpFlagList", PlayerFlagDataManager.partyPowerUpFlagList);
			writer.WriteProperty("heroineAllTimeFollowFlagList", PlayerFlagDataManager.heroineAllTimeFollowFlagList);
			writer.WriteProperty("dungeonFlagDictionary", PlayerFlagDataManager.dungeonFlagDictionary);
			writer.WriteProperty("recipeFlagDictionary", PlayerFlagDataManager.recipeFlagDictionary);
			writer.WriteProperty("questClearFlagList", PlayerFlagDataManager.questClearFlagList);
			writer.WriteProperty("keyItemFlagDictionary", PlayerFlagDataManager.keyItemFlagDictionary);
			writer.WriteProperty("eventStartingDayDictionary", PlayerFlagDataManager.eventStartingDayDictionary);
			writer.WritePrivateField("serializationData", objectContainingField);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			PlayerFlagDataManager objectContainingField = (PlayerFlagDataManager)obj;
			IEnumerator enumerator = reader.Properties.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					switch ((string)enumerator.Current)
					{
					case "scenarioFlagDictionary":
						PlayerFlagDataManager.scenarioFlagDictionary = reader.Read<Dictionary<string, bool>>();
						break;
					case "tutorialFlagDictionary":
						PlayerFlagDataManager.tutorialFlagDictionary = reader.Read<Dictionary<string, bool>>();
						break;
					case "sceneGarellyFlagDictionary":
						PlayerFlagDataManager.sceneGarellyFlagDictionary = reader.Read<Dictionary<string, bool>>();
						break;
					case "partyPowerUpFlagList":
						PlayerFlagDataManager.partyPowerUpFlagList = reader.Read<List<bool>>();
						break;
					case "heroineAllTimeFollowFlagList":
						PlayerFlagDataManager.heroineAllTimeFollowFlagList = reader.Read<List<bool>>();
						break;
					case "dungeonFlagDictionary":
						PlayerFlagDataManager.dungeonFlagDictionary = reader.Read<Dictionary<string, bool>>();
						break;
					case "recipeFlagDictionary":
						PlayerFlagDataManager.recipeFlagDictionary = reader.Read<Dictionary<string, bool>>();
						break;
					case "questClearFlagList":
						PlayerFlagDataManager.questClearFlagList = reader.Read<List<QuestClearData>>();
						break;
					case "keyItemFlagDictionary":
						PlayerFlagDataManager.keyItemFlagDictionary = reader.Read<Dictionary<string, bool>>();
						break;
					case "eventStartingDayDictionary":
						PlayerFlagDataManager.eventStartingDayDictionary = reader.Read<Dictionary<string, int>>();
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
