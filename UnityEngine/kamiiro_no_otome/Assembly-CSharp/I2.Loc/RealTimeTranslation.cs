using System.Collections.Generic;
using UnityEngine;

namespace I2.Loc
{
	public class RealTimeTranslation : MonoBehaviour
	{
		private string OriginalText = "This is an example showing how to use the google translator to translate chat messages within the game.\nIt also supports multiline translations.";

		private string TranslatedText = string.Empty;

		private bool IsTranslating;

		public void OnGUI()
		{
			GUILayout.Label("Translate:");
			OriginalText = GUILayout.TextArea(OriginalText, GUILayout.Width(Screen.width));
			GUILayout.Space(10f);
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("English -> Español", GUILayout.Height(100f)))
			{
				StartTranslating("en", "es");
			}
			if (GUILayout.Button("Español -> English", GUILayout.Height(100f)))
			{
				StartTranslating("es", "en");
			}
			GUILayout.EndHorizontal();
			GUILayout.Space(10f);
			GUILayout.BeginHorizontal();
			GUILayout.TextArea("Multiple Translation with 1 call:\n'This is an example' -> en,zh\n'Hola' -> en");
			if (GUILayout.Button("Multi Translate", GUILayout.ExpandHeight(expand: true)))
			{
				ExampleMultiTranslations_Async();
			}
			GUILayout.EndHorizontal();
			GUILayout.TextArea(TranslatedText, GUILayout.Width(Screen.width));
			GUILayout.Space(10f);
			if (IsTranslating)
			{
				GUILayout.Label("Contacting Google....");
			}
		}

		public void StartTranslating(string fromCode, string toCode)
		{
			IsTranslating = true;
			GoogleTranslation.Translate(OriginalText, fromCode, toCode, OnTranslationReady);
		}

		private void OnTranslationReady(string Translation, string errorMsg)
		{
			IsTranslating = false;
			if (errorMsg != null)
			{
				Debug.LogError(errorMsg);
			}
			else
			{
				TranslatedText = Translation;
			}
		}

		public void ExampleMultiTranslations_Blocking()
		{
			Dictionary<string, TranslationQuery> dictionary = new Dictionary<string, TranslationQuery>();
			GoogleTranslation.AddQuery("This is an example", "en", "es", dictionary);
			GoogleTranslation.AddQuery("This is an example", "auto", "zh", dictionary);
			GoogleTranslation.AddQuery("Hola", "es", "en", dictionary);
			if (GoogleTranslation.ForceTranslate(dictionary))
			{
				Debug.Log(GoogleTranslation.GetQueryResult("This is an example", "en", dictionary));
				Debug.Log(GoogleTranslation.GetQueryResult("This is an example", "zh", dictionary));
				Debug.Log(GoogleTranslation.GetQueryResult("This is an example", "", dictionary));
				Debug.Log(dictionary["Hola"].Results[0]);
			}
		}

		public void ExampleMultiTranslations_Async()
		{
			IsTranslating = true;
			Dictionary<string, TranslationQuery> dictionary = new Dictionary<string, TranslationQuery>();
			GoogleTranslation.AddQuery("This is an example", "en", "es", dictionary);
			GoogleTranslation.AddQuery("This is an example", "auto", "zh", dictionary);
			GoogleTranslation.AddQuery("Hola", "es", "en", dictionary);
			GoogleTranslation.Translate(dictionary, OnMultitranslationReady);
		}

		private void OnMultitranslationReady(Dictionary<string, TranslationQuery> dict, string errorMsg)
		{
			if (!string.IsNullOrEmpty(errorMsg))
			{
				Debug.LogError(errorMsg);
				return;
			}
			IsTranslating = false;
			TranslatedText = "";
			TranslatedText = TranslatedText + GoogleTranslation.GetQueryResult("This is an example", "es", dict) + "\n";
			TranslatedText = TranslatedText + GoogleTranslation.GetQueryResult("This is an example", "zh", dict) + "\n";
			TranslatedText = TranslatedText + GoogleTranslation.GetQueryResult("This is an example", "", dict) + "\n";
			TranslatedText += dict["Hola"].Results[0];
		}

		public bool IsWaitingForTranslation()
		{
			return IsTranslating;
		}

		public string GetTranslatedText()
		{
			return TranslatedText;
		}

		public void SetOriginalText(string text)
		{
			OriginalText = text;
		}
	}
}
