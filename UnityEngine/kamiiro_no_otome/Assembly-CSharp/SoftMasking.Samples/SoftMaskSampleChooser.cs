using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SoftMasking.Samples
{
	public class SoftMaskSampleChooser : MonoBehaviour
	{
		public Dropdown dropdown;

		public Text fallbackLabel;

		public void Start()
		{
			string activeSceneName = SceneManager.GetActiveScene().name;
			int num = dropdown.options.FindIndex((Dropdown.OptionData x) => x.text == activeSceneName);
			if (num >= 0)
			{
				dropdown.value = num;
				dropdown.onValueChanged.AddListener(Choose);
			}
			else
			{
				Fallback(activeSceneName);
			}
		}

		private void Fallback(string activeSceneName)
		{
			dropdown.gameObject.SetActive(value: false);
			fallbackLabel.gameObject.SetActive(value: true);
			fallbackLabel.text = activeSceneName;
		}

		public void Choose(int sampleIndex)
		{
			SceneManager.LoadScene(dropdown.options[sampleIndex].text);
		}
	}
}
