using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

namespace Sonny2017LanguageCompat
{
    internal static class Common
    {
        public const string VersionToken = "COMMON_VERSION";
        public static string VersionCurrentLanguage
        {
            get
            {
                return LanguageManager.GetString("COMMON_VERSION", VersionNumber);
            }
        }

        public const string VersionNumber = "1.6.7";

        public static bool TryGetComponent<TComponent>(this GameObject gameObject, out TComponent component) where TComponent : Component
        {
            component = gameObject.GetComponent<TComponent>();
            return !(component is null);
        }

        public static TokenTranslator AddTokenizer(this GameObject gameObject, Text textComponent, string token, params object[] parameters)
        {
            Debug.Log($"== Adding tokenizer with token {token}");
            if (!gameObject.TryGetComponent(out TokenTranslator component))
            {
                component = gameObject.AddComponent<TokenTranslator>();
                component.textController = textComponent;
                component.token = token;
                component.parameters = parameters;
            }
            return component;
        }
    }
}
