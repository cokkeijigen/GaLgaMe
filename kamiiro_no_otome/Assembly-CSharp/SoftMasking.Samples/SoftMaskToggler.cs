using UnityEngine;
using UnityEngine.UI;

namespace SoftMasking.Samples
{
	public class SoftMaskToggler : MonoBehaviour
	{
		public GameObject mask;

		public bool doNotTouchImage;

		public void Toggle(bool enabled)
		{
			if ((bool)mask)
			{
				mask.GetComponent<SoftMask>().enabled = enabled;
				mask.GetComponent<Mask>().enabled = !enabled;
				if (!doNotTouchImage)
				{
					mask.GetComponent<Image>().enabled = !enabled;
				}
			}
		}
	}
}
