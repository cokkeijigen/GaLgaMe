using System;
using System.Collections.Generic;
using UnityEngine;

namespace I2.Loc
{
	[Serializable]
	public class TermData
	{
		public string Term = string.Empty;

		public eTermType TermType;

		[NonSerialized]
		public string Description;

		public string[] Languages = new string[0];

		public byte[] Flags = new byte[0];

		[SerializeField]
		private string[] Languages_Touch;

		public string GetTranslation(int idx, string specialization = null, bool editMode = false)
		{
			string text = Languages[idx];
			if (text != null)
			{
				text = SpecializationManager.GetSpecializedText(text, specialization);
				if (!editMode)
				{
					text = text.Replace("[i2nt]", "").Replace("[/i2nt]", "");
				}
			}
			return text;
		}

		public void SetTranslation(int idx, string translation, string specialization = null)
		{
			Languages[idx] = SpecializationManager.SetSpecializedText(Languages[idx], translation, specialization);
		}

		public void RemoveSpecialization(string specialization)
		{
			for (int i = 0; i < Languages.Length; i++)
			{
				RemoveSpecialization(i, specialization);
			}
		}

		public void RemoveSpecialization(int idx, string specialization)
		{
			string text = Languages[idx];
			if (!(specialization == "Any") && text.Contains("[i2s_" + specialization + "]"))
			{
				Dictionary<string, string> specializations = SpecializationManager.GetSpecializations(text);
				specializations.Remove(specialization);
				Languages[idx] = SpecializationManager.SetSpecializedText(specializations);
			}
		}

		public bool IsAutoTranslated(int idx, bool IsTouch)
		{
			return (Flags[idx] & 2) > 0;
		}

		public void Validate()
		{
			int num = Mathf.Max(Languages.Length, Flags.Length);
			if (Languages.Length != num)
			{
				Array.Resize(ref Languages, num);
			}
			if (Flags.Length != num)
			{
				Array.Resize(ref Flags, num);
			}
			if (Languages_Touch == null)
			{
				return;
			}
			for (int i = 0; i < Mathf.Min(Languages_Touch.Length, num); i++)
			{
				if (string.IsNullOrEmpty(Languages[i]) && !string.IsNullOrEmpty(Languages_Touch[i]))
				{
					Languages[i] = Languages_Touch[i];
					Languages_Touch[i] = null;
				}
			}
			Languages_Touch = null;
		}

		public bool IsTerm(string name, bool allowCategoryMistmatch)
		{
			if (!allowCategoryMistmatch)
			{
				return name == Term;
			}
			return name == LanguageSourceData.GetKeyFromFullTerm(Term);
		}

		public bool HasSpecializations()
		{
			for (int i = 0; i < Languages.Length; i++)
			{
				if (!string.IsNullOrEmpty(Languages[i]) && Languages[i].Contains("[i2s_"))
				{
					return true;
				}
			}
			return false;
		}

		public List<string> GetAllSpecializations()
		{
			List<string> list = new List<string>();
			for (int i = 0; i < Languages.Length; i++)
			{
				SpecializationManager.AppendSpecializations(Languages[i], list);
			}
			return list;
		}
	}
}
