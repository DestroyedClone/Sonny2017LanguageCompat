using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Sonny2017LanguageCompat
{
    internal static class Common
    {
        public const string VersionToken = "COMMON_VERSION";
        public static string Version
        {
            get
            {
                return LanguageManager.GetString("COMMON_VERSION", VersionNumber);
            }
        }

        public static string VersionNumber = "1.6.7";

        public static bool TryGetComponent<TComponent>(this GameObject gameObject, out TComponent component)
        {
            component = gameObject.GetComponent<TComponent>();
            return component != null;
        }
    }
}
