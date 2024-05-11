using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Sonny2017LanguageCompat
{
    internal class TokenTranslator : MonoBehaviour
    {
        public UnityEngine.UI.Text textController;
        public string token = "TOKENNOTSET";
        public void Start()
        {
            //LanguageManager.onLanguageChanged += OnLanguageChanged;
            OnLanguageChanged();
        }

        public void OnLanguageChanged()
        {
            if (textController)
                textController.text = LanguageManager.GetString(token);
        }
    }
}
