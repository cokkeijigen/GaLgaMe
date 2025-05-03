using System;
using System.Collections.Generic;
using UnityEngine;

namespace I2.Loc
{
	public class LocalizationParamsManager : MonoBehaviour, ILocalizationParamsManager
	{
		[Serializable]
		public struct ParamValue
		{
			public string Name;

			public string Value;
		}

		[SerializeField]
		public List<ParamValue> _Params = new List<ParamValue>();

		public bool _IsGlobalManager;

		public string GetParameterValue(string ParamName)
		{
			if (_Params != null)
			{
				int i = 0;
				for (int count = _Params.Count; i < count; i++)
				{
					if (_Params[i].Name == ParamName)
					{
						return _Params[i].Value;
					}
				}
			}
			return null;
		}

		public void SetParameterValue(string ParamName, string ParamValue, bool localize = true)
		{
			bool flag = false;
			int i = 0;
			for (int count = _Params.Count; i < count; i++)
			{
				if (_Params[i].Name == ParamName)
				{
					ParamValue value = _Params[i];
					value.Value = ParamValue;
					_Params[i] = value;
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				_Params.Add(new ParamValue
				{
					Name = ParamName,
					Value = ParamValue
				});
			}
			if (localize)
			{
				OnLocalize();
			}
		}

		public void OnLocalize()
		{
			Localize component = GetComponent<Localize>();
			if (component != null)
			{
				component.OnLocalize(Force: true);
			}
		}

		public virtual void OnEnable()
		{
			if (_IsGlobalManager)
			{
				DoAutoRegister();
			}
		}

		public void DoAutoRegister()
		{
			if (!LocalizationManager.ParamManagers.Contains(this))
			{
				LocalizationManager.ParamManagers.Add(this);
				LocalizationManager.LocalizeAll(Force: true);
			}
		}

		public void OnDisable()
		{
			LocalizationManager.ParamManagers.Remove(this);
		}
	}
}
