using UnityEngine;

namespace I2.Loc
{
	public class TermsPopup : PropertyAttribute
	{
		public string Filter { get; private set; }

		public TermsPopup(string filter = "")
		{
			Filter = filter;
		}
	}
}
