using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace I2.Loc
{
	[AddComponentMenu("I2/Localization/Localize Dropdown")]
	public class LocalizeDropdown : MonoBehaviour
	{
		public List<string> _Terms = new List<string>();

		public void Start()
		{
			LocalizationManager.OnLocalizeEvent += OnLocalize;
			OnLocalize();
		}

		public void OnDestroy()
		{
			LocalizationManager.OnLocalizeEvent -= OnLocalize;
		}

		private void OnEnable()
		{
			if (_Terms.Count == 0)
			{
				FillValues();
			}
			OnLocalize();
		}

		public void OnLocalize()
		{
			if (base.enabled && !(base.gameObject == null) && base.gameObject.activeInHierarchy && !string.IsNullOrEmpty(LocalizationManager.CurrentLanguage))
			{
				UpdateLocalization();
			}
		}

		private void FillValues()
		{
			Dropdown component = GetComponent<Dropdown>();
			if (component == null && I2Utils.IsPlaying())
			{
				FillValuesTMPro();
				return;
			}
			foreach (Dropdown.OptionData option in component.options)
			{
				_Terms.Add(option.text);
			}
		}

		public void UpdateLocalization()
		{
			Dropdown component = GetComponent<Dropdown>();
			if (component == null)
			{
				UpdateLocalizationTMPro();
				return;
			}
			component.options.Clear();
			foreach (string term in _Terms)
			{
				string translation = LocalizationManager.GetTranslation(term);
				component.options.Add(new Dropdown.OptionData(translation));
			}
			component.RefreshShownValue();
		}

		public void UpdateLocalizationTMPro()
		{
			TMP_Dropdown component = GetComponent<TMP_Dropdown>();
			if (component == null)
			{
				return;
			}
			component.options.Clear();
			foreach (string term in _Terms)
			{
				string translation = LocalizationManager.GetTranslation(term);
				component.options.Add(new TMP_Dropdown.OptionData(translation));
			}
			component.RefreshShownValue();
		}

		private void FillValuesTMPro()
		{
			TMP_Dropdown component = GetComponent<TMP_Dropdown>();
			if (component == null)
			{
				return;
			}
			foreach (TMP_Dropdown.OptionData option in component.options)
			{
				_Terms.Add(option.text);
			}
		}
	}
}
