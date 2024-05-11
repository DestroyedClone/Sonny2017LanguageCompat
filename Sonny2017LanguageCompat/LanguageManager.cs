using System;
using System.Collections.Generic;
using System.Text;
using BepInEx;
using Sonny2017LanguageCompat.Language;
using UnityEngine;

namespace Sonny2017LanguageCompat
{
    public static class LanguageManager
    {
        public static LanguageManager.OnLanguageChanged onLanguageChanged;

        public delegate void OnLanguageChanged(string language);

        public static void Init()
        {
            currentLanguageSet = languages[Plugin.cfgCurrentLanguage.Value];
        }

        public static Dictionary<string, Dictionary<string, string>> languages = new Dictionary<string, Dictionary<string, string>>()
        {

        };

        public static Dictionary<string, string> currentLanguageSet = null;

        public static void SetLanguage(string languageName)
        {
            if (languages.TryGetValue(languageName, out var languageSet))
            {
                currentLanguageSet = languageSet;
                Plugin._logger.LogMessage($"Set language to {languageName}");
                LanguageManager.onLanguageChanged(languageName);
                return;
            }
            Plugin._logger.LogWarning($"Couldn't find language {languageName}, defaulting to english.");
            Plugin._logger.LogWarning($"Available Languages:");
            foreach (var language in languages)
            {
                Debug.Log(language.Key);
            }
            currentLanguageSet = English.languageEntries;
            LanguageManager.onLanguageChanged(languageName);
            //onLanguageChanged?.Invoke();
        }

        public static string GetString(string token)
        {
            if (currentLanguageSet.TryGetValue(token, out string value))
            {
                return value;
            }
            if (languages["english"].TryGetValue(token, out value))
            {
                Debug.LogWarning($"Couldn't find {token}, defaulting to english token");
                return value;
            }
            Debug.LogWarning($"Couldn't find {token}, returning as is");
            return token;
        }
        public static string GetString(string token, params object[] args)
        {
            var output = GetString(token);
            if (output == token)
            {
                Debug.LogWarning($"Can't format token because source is same as result! Args: {args}");
                return token;
            }
            return string.Format(GetString(token), args);
        }
        public static string RemoveText(string text, params string[] textToRemove)
        {
            if (textToRemove.Length == 0)
                return text;
            foreach (var t in textToRemove)
            {
                text.Replace(t, "");
            }
            return text;
        }


        public static string ReplaceStringWithToken(string baseText, string textToReplace, string requestedToken)
        {
            var tokenValue = LanguageManager.GetString(requestedToken);
            return baseText.Replace(textToReplace, tokenValue);
        }

        public static string UseStringAsInputForToken(string baseText, string requestedToken, params string[] textsToRemove)
        {
            foreach (var t in textsToRemove)
            {
                baseText = baseText.Replace(t, "");
            }
            var tokenValue = LanguageManager.GetString(requestedToken);
            return LanguageManager.GetString(tokenValue, baseText);
        }
    }
}
