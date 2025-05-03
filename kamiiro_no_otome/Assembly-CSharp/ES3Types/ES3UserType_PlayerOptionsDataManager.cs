using System;
using System.Collections;
using Sirenix.Serialization;
using UnityEngine.Scripting;

namespace ES3Types
{
	[Preserve]
	[ES3Properties(new string[]
	{
		"optionsBgmVolume", "optionsHBgmVolume", "optionsSeVolume", "optionsAmbienceVolume", "optionsVoice1Volume", "optionsVoice2Volume", "optionsVoice3Volume", "optionsVoice4Volume", "optionsVoice5Volume", "optionsTextSpeed",
		"optionsAutoTextSpeed", "optionsMouseWheelSend", "optionsMouseWheelBacklog", "optionsMouseWheelPower", "optionsVoiceStopTypeNext", "optionsVoiceStopTypeClick", "optionsFullScreenMode", "optionsWindowSize", "currentDisplayWidth", "currentDisplayHeight",
		"defaultBgmVolume", "defaultHBgmVolume", "defaultSeVolume", "defaultAmbienceVolume", "defaultVoice1Volume", "defaultVoice2Volume", "defaultVoice3Volume", "defaultVoice4Volume", "defaultVoice5Volume", "defaultTextSpeed",
		"defaultAutoTextSpeed", "defaultMouseWheelSend", "defaultMouseWheelBacklog", "defaultMouseWheelPower", "defaultVoiceStopTypeNext", "defaultVoiceStopTypeClick", "defaultFullScreenMode", "odefaultWindowSize", "serializationData"
	})]
	public class ES3UserType_PlayerOptionsDataManager : ES3ComponentType
	{
		public static ES3Type Instance;

		public ES3UserType_PlayerOptionsDataManager()
			: base(typeof(PlayerOptionsDataManager))
		{
			Instance = this;
			priority = 1;
		}

		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			PlayerOptionsDataManager objectContainingField = (PlayerOptionsDataManager)obj;
			writer.WriteProperty("optionsBgmVolume", PlayerOptionsDataManager.optionsBgmVolume, ES3Type_float.Instance);
			writer.WriteProperty("optionsHBgmVolume", PlayerOptionsDataManager.optionsHBgmVolume, ES3Type_float.Instance);
			writer.WriteProperty("optionsSeVolume", PlayerOptionsDataManager.optionsSeVolume, ES3Type_float.Instance);
			writer.WriteProperty("optionsAmbienceVolume", PlayerOptionsDataManager.optionsAmbienceVolume, ES3Type_float.Instance);
			writer.WriteProperty("optionsVoice1Volume", PlayerOptionsDataManager.optionsVoice1Volume, ES3Type_float.Instance);
			writer.WriteProperty("optionsVoice2Volume", PlayerOptionsDataManager.optionsVoice2Volume, ES3Type_float.Instance);
			writer.WriteProperty("optionsVoice3Volume", PlayerOptionsDataManager.optionsVoice3Volume, ES3Type_float.Instance);
			writer.WriteProperty("optionsVoice4Volume", PlayerOptionsDataManager.optionsVoice4Volume, ES3Type_float.Instance);
			writer.WriteProperty("optionsVoice5Volume", PlayerOptionsDataManager.optionsVoice5Volume, ES3Type_float.Instance);
			writer.WriteProperty("optionsTextSpeed", PlayerOptionsDataManager.optionsTextSpeed, ES3Type_float.Instance);
			writer.WriteProperty("optionsAutoTextSpeed", PlayerOptionsDataManager.optionsAutoTextSpeed, ES3Type_float.Instance);
			writer.WriteProperty("optionsMouseWheelSend", PlayerOptionsDataManager.optionsMouseWheelSend, ES3Type_bool.Instance);
			writer.WriteProperty("optionsMouseWheelBacklog", PlayerOptionsDataManager.optionsMouseWheelBacklog, ES3Type_bool.Instance);
			writer.WriteProperty("optionsMouseWheelPower", PlayerOptionsDataManager.optionsMouseWheelPower, ES3Type_float.Instance);
			writer.WriteProperty("optionsVoiceStopTypeNext", PlayerOptionsDataManager.optionsVoiceStopTypeNext, ES3Type_bool.Instance);
			writer.WriteProperty("optionsVoiceStopTypeClick", PlayerOptionsDataManager.optionsVoiceStopTypeClick, ES3Type_bool.Instance);
			writer.WriteProperty("optionsFullScreenMode", PlayerOptionsDataManager.optionsFullScreenMode, ES3Type_bool.Instance);
			writer.WriteProperty("optionsWindowSize", PlayerOptionsDataManager.optionsWindowSize, ES3Type_int.Instance);
			writer.WriteProperty("currentDisplayWidth", PlayerOptionsDataManager.currentDisplayWidth, ES3Type_float.Instance);
			writer.WriteProperty("currentDisplayHeight", PlayerOptionsDataManager.currentDisplayHeight, ES3Type_float.Instance);
			writer.WriteProperty("defaultBgmVolume", PlayerOptionsDataManager.defaultBgmVolume, ES3Type_float.Instance);
			writer.WriteProperty("defaultHBgmVolume", PlayerOptionsDataManager.defaultHBgmVolume, ES3Type_float.Instance);
			writer.WriteProperty("defaultSeVolume", PlayerOptionsDataManager.defaultSeVolume, ES3Type_float.Instance);
			writer.WriteProperty("defaultAmbienceVolume", PlayerOptionsDataManager.defaultAmbienceVolume, ES3Type_float.Instance);
			writer.WriteProperty("defaultVoice1Volume", PlayerOptionsDataManager.defaultVoice1Volume, ES3Type_float.Instance);
			writer.WriteProperty("defaultVoice2Volume", PlayerOptionsDataManager.defaultVoice2Volume, ES3Type_float.Instance);
			writer.WriteProperty("defaultVoice3Volume", PlayerOptionsDataManager.defaultVoice3Volume, ES3Type_float.Instance);
			writer.WriteProperty("defaultVoice4Volume", PlayerOptionsDataManager.defaultVoice4Volume, ES3Type_float.Instance);
			writer.WriteProperty("defaultVoice5Volume", PlayerOptionsDataManager.defaultVoice5Volume, ES3Type_float.Instance);
			writer.WriteProperty("defaultTextSpeed", PlayerOptionsDataManager.defaultTextSpeed, ES3Type_float.Instance);
			writer.WriteProperty("defaultAutoTextSpeed", PlayerOptionsDataManager.defaultAutoTextSpeed, ES3Type_float.Instance);
			writer.WriteProperty("defaultMouseWheelSend", PlayerOptionsDataManager.defaultMouseWheelSend, ES3Type_bool.Instance);
			writer.WriteProperty("defaultMouseWheelBacklog", PlayerOptionsDataManager.defaultMouseWheelBacklog, ES3Type_bool.Instance);
			writer.WriteProperty("defaultMouseWheelPower", PlayerOptionsDataManager.defaultMouseWheelPower, ES3Type_float.Instance);
			writer.WriteProperty("defaultVoiceStopTypeNext", PlayerOptionsDataManager.defaultVoiceStopTypeNext, ES3Type_bool.Instance);
			writer.WriteProperty("defaultVoiceStopTypeClick", PlayerOptionsDataManager.defaultVoiceStopTypeClick, ES3Type_bool.Instance);
			writer.WriteProperty("defaultFullScreenMode", PlayerOptionsDataManager.defaultFullScreenMode, ES3Type_bool.Instance);
			writer.WriteProperty("odefaultWindowSize", PlayerOptionsDataManager.defaultWindowSize, ES3Type_int.Instance);
			writer.WritePrivateField("serializationData", objectContainingField);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			PlayerOptionsDataManager objectContainingField = (PlayerOptionsDataManager)obj;
			IEnumerator enumerator = reader.Properties.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					switch ((string)enumerator.Current)
					{
					case "optionsBgmVolume":
						PlayerOptionsDataManager.optionsBgmVolume = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "optionsHBgmVolume":
						PlayerOptionsDataManager.optionsHBgmVolume = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "optionsSeVolume":
						PlayerOptionsDataManager.optionsSeVolume = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "optionsAmbienceVolume":
						PlayerOptionsDataManager.optionsAmbienceVolume = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "optionsVoice1Volume":
						PlayerOptionsDataManager.optionsVoice1Volume = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "optionsVoice2Volume":
						PlayerOptionsDataManager.optionsVoice2Volume = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "optionsVoice3Volume":
						PlayerOptionsDataManager.optionsVoice3Volume = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "optionsVoice4Volume":
						PlayerOptionsDataManager.optionsVoice4Volume = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "optionsVoice5Volume":
						PlayerOptionsDataManager.optionsVoice5Volume = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "optionsTextSpeed":
						PlayerOptionsDataManager.optionsTextSpeed = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "optionsAutoTextSpeed":
						PlayerOptionsDataManager.optionsAutoTextSpeed = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "optionsMouseWheelSend":
						PlayerOptionsDataManager.optionsMouseWheelSend = reader.Read<bool>(ES3Type_bool.Instance);
						break;
					case "optionsMouseWheelBacklog":
						PlayerOptionsDataManager.optionsMouseWheelBacklog = reader.Read<bool>(ES3Type_bool.Instance);
						break;
					case "optionsMouseWheelPower":
						PlayerOptionsDataManager.optionsMouseWheelPower = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "optionsVoiceStopTypeNext":
						PlayerOptionsDataManager.optionsVoiceStopTypeNext = reader.Read<bool>(ES3Type_bool.Instance);
						break;
					case "optionsVoiceStopTypeClick":
						PlayerOptionsDataManager.optionsVoiceStopTypeClick = reader.Read<bool>(ES3Type_bool.Instance);
						break;
					case "optionsFullScreenMode":
						PlayerOptionsDataManager.optionsFullScreenMode = reader.Read<bool>(ES3Type_bool.Instance);
						break;
					case "optionsWindowSize":
						PlayerOptionsDataManager.optionsWindowSize = reader.Read<int>(ES3Type_int.Instance);
						break;
					case "currentDisplayWidth":
						PlayerOptionsDataManager.currentDisplayWidth = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "currentDisplayHeight":
						PlayerOptionsDataManager.currentDisplayHeight = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "defaultBgmVolume":
						PlayerOptionsDataManager.defaultBgmVolume = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "defaultHBgmVolume":
						PlayerOptionsDataManager.defaultHBgmVolume = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "defaultSeVolume":
						PlayerOptionsDataManager.defaultSeVolume = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "defaultAmbienceVolume":
						PlayerOptionsDataManager.defaultAmbienceVolume = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "defaultVoice1Volume":
						PlayerOptionsDataManager.defaultVoice1Volume = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "defaultVoice2Volume":
						PlayerOptionsDataManager.defaultVoice2Volume = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "defaultVoice3Volume":
						PlayerOptionsDataManager.defaultVoice3Volume = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "defaultVoice4Volume":
						PlayerOptionsDataManager.defaultVoice4Volume = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "defaultVoice5Volume":
						PlayerOptionsDataManager.defaultVoice5Volume = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "defaultTextSpeed":
						PlayerOptionsDataManager.defaultTextSpeed = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "defaultAutoTextSpeed":
						PlayerOptionsDataManager.defaultAutoTextSpeed = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "defaultMouseWheelSend":
						PlayerOptionsDataManager.defaultMouseWheelSend = reader.Read<bool>(ES3Type_bool.Instance);
						break;
					case "defaultMouseWheelBacklog":
						PlayerOptionsDataManager.defaultMouseWheelBacklog = reader.Read<bool>(ES3Type_bool.Instance);
						break;
					case "defaultMouseWheelPower":
						PlayerOptionsDataManager.defaultMouseWheelPower = reader.Read<float>(ES3Type_float.Instance);
						break;
					case "defaultVoiceStopTypeNext":
						PlayerOptionsDataManager.defaultVoiceStopTypeNext = reader.Read<bool>(ES3Type_bool.Instance);
						break;
					case "defaultVoiceStopTypeClick":
						PlayerOptionsDataManager.defaultVoiceStopTypeClick = reader.Read<bool>(ES3Type_bool.Instance);
						break;
					case "defaultFullScreenMode":
						PlayerOptionsDataManager.defaultFullScreenMode = reader.Read<bool>(ES3Type_bool.Instance);
						break;
					case "odefaultWindowSize":
						PlayerOptionsDataManager.defaultWindowSize = reader.Read<int>(ES3Type_int.Instance);
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
