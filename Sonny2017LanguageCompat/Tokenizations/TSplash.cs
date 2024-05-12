using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using static Sonny2017LanguageCompat.Common;

namespace Sonny2017LanguageCompat.Tokenizations
{
    internal class TSplash
    {
        public static void Init()
        {
            On.KJCanary.PlayRenderedMusic_string += KJCanary_PlayRenderedMusic_string;
            On.SplashScene.Start += SplashScene_Start;
        }

        private static void SplashScene_Start(On.SplashScene.orig_Start orig, SplashScene self)
        {
            orig(self);
            self.version.gameObject.AddTokenizer(self.version, Common.VersionToken, Common.VersionCurrentLanguage);
            var buttonHolder = self.version.transform.parent.Find("Logo/Button Holder/");
            foreach (Transform button in buttonHolder.GetComponentInChildren<Transform>())
            {
                var textComp = button.Find("Text").GetComponent<UnityEngine.UI.Text>();
                var tran = textComp.gameObject.AddComponent<TokenTranslator>();
                tran.textController = textComp;
                //Debug.Log($"Splash: TextComp: {textComp.text}");
                switch (textComp.text)
                {
                    case "START GAME":
                        tran.token = "SPLASH_STARTGAME";
                        break;
                    case "VIDEO SETTINGS":
                        tran.token = "SPLASH_VIDEOSETTINGS";
                        break;
                    case "QUIT GAME":
                        tran.token = "SPLASH_QUITGAME";
                        break;
                }
            }
        }

        private static void KJCanary_PlayRenderedMusic_string(On.KJCanary.orig_PlayRenderedMusic_string orig, string trackName)
        {
            if (trackName == "Overworld2") trackName = "RadioSong";
            orig(trackName);
        }
    }
}
