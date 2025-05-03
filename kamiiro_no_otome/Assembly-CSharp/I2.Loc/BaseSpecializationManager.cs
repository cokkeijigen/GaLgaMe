using System.Collections.Generic;

namespace I2.Loc
{
	public class BaseSpecializationManager
	{
		public string[] mSpecializations;

		public Dictionary<string, string> mSpecializationsFallbacks;

		public virtual void InitializeSpecializations()
		{
			mSpecializations = new string[12]
			{
				"Any", "PC", "Touch", "Controller", "VR", "XBox", "PS4", "OculusVR", "ViveVR", "GearVR",
				"Android", "IOS"
			};
			mSpecializationsFallbacks = new Dictionary<string, string>
			{
				{ "XBox", "Controller" },
				{ "PS4", "Controller" },
				{ "OculusVR", "VR" },
				{ "ViveVR", "VR" },
				{ "GearVR", "VR" },
				{ "Android", "Touch" },
				{ "IOS", "Touch" }
			};
		}

		public virtual string GetCurrentSpecialization()
		{
			if (mSpecializations == null)
			{
				InitializeSpecializations();
			}
			return "PC";
		}

		public virtual string GetFallbackSpecialization(string specialization)
		{
			if (mSpecializationsFallbacks == null)
			{
				InitializeSpecializations();
			}
			if (mSpecializationsFallbacks.TryGetValue(specialization, out var value))
			{
				return value;
			}
			return "Any";
		}
	}
}
