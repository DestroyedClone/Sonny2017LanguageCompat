using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Sonny2017LanguageCompat.Tokenizations
{
    internal class TSettings
    {
        public static void Init()
        {
            On.KJPopUp.Deploy += KJPopUp_Deploy; //SettingsPopup.Deploy -> 
            On.SettingsPopup.UpdateButtons += SettingsPopup_UpdateButtons;
            On.StandardPopup.Deploy += StandardPopup_Deploy;
            On.ScreenPopup.UpdateButtons += ScreenPopup_UpdateButtons;
            //On.SplashScene.Start += SplashScene_Start; //missing method exception system.array.empty
        }

        private static void SplashScene_Start(On.SplashScene.orig_Start orig, SplashScene self)
        {
            orig(self);
            var a = Resources.LoadAll<GameObject>("Popup/");
            foreach (var t in a)
            {
                Debug.Log(t.name);
            }


            var prefab = Resources.Load<GameObject>("popup/screenpopup");



            var prefabHeader = prefab.transform.Find("Header");
            Text textComponent = prefabHeader.GetComponent<Text>();
            prefabHeader.gameObject.AddTokenizer(textComponent, "SETTINGS_SCREENOPTIONS");
            textComponent.verticalOverflow = VerticalWrapMode.Overflow;
        }

        private static void ScreenPopup_UpdateButtons(On.ScreenPopup.orig_UpdateButtons orig, ScreenPopup self)
        {
            orig(self);
            Text textComponent = self.transform.Find("Header").gameObject.GetComponent<Text>();
            try
            {
                //textComponent.gameObject.AddTokenizer(textComponent, "SETTINGS_SCREENOPTIONS"); //throws for some reason
                //array empty exception
                var comp = textComponent.gameObject.AddComponent<TokenTranslator>();
                comp.token = "SETTINGS_SCREENOPTIONS";
                comp.textController = textComponent;
                textComponent.horizontalOverflow = HorizontalWrapMode.Overflow;
            }
            catch (Exception e)
            {
                Debug.LogWarning("Failed to add tokenizer!");
                Debug.LogError(e);
            }
            string windowTypeToken = KJSingleton<AchievementManager>.Singleton.fullScreen ? "SETTINGS_FULLSCREEN" : "SETTINGS_WINDOWED";
            string processedWindowTypeToken = LanguageManager.GetString(windowTypeToken);
            self.resuButton.label = LanguageManager.GetString("SETTINGS_DISPLAYMODEFORMAT", processedWindowTypeToken);
            self.resuButton.bodyText.horizontalOverflow = HorizontalWrapMode.Overflow;

            string camShakeToken = KJSingleton<AchievementManager>.Singleton.enableShaking ? "SETTINGS_ON" : "SETTINGS_OFF";
            string processedCamShakeToken = LanguageManager.GetString(camShakeToken);
            self.shakeButton.label = LanguageManager.GetString("SETTINGS_CAMSHAKEFORMAT", processedCamShakeToken);
            self.shakeButton.bodyText.horizontalOverflow = HorizontalWrapMode.Overflow;
            self.shakeButton.Calibrate();
        }

        private static StandardPopup StandardPopup_Deploy(On.StandardPopup.orig_Deploy orig, string header, string body, string button1, KJ.VoidDelegate onPress1, string button2, KJ.VoidDelegate onPress2, StandardPopup.ButtonColor button1Color, StandardPopup.ButtonColor button2Color)
        {
            //SettingsPopup.OnTapStar
            return orig(header, body, button1, onPress1, button2, onPress2, button1Color, button2Color);
        }

        private static void SettingsPopup_UpdateButtons(On.SettingsPopup.orig_UpdateButtons orig, SettingsPopup self)
        {
            orig(self);
            self.fpsButton.label = "FPS: " + Profile.Singleton.fps; //unchanged
        }

        private static void KJPopUp_Deploy(On.KJPopUp.orig_Deploy orig, KJPopUpContent content)
        {
            if (content is SettingsPopup settings)
            {
                settings.versionField.gameObject.AddTokenizer(settings.versionField, Common.VersionToken, Common.VersionNumber);
            }
            orig(content);
        }
    }
}
