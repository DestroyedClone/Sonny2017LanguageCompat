using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using KJ;
using Sonny;
using UnityStandardAssets.ImageEffects;
using static Sonny2017LanguageCompat.LanguageManager;

namespace Sonny2017LanguageCompat.Tokenizations
{
    internal class TMenu
    {
        public static void Init()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += ModifyLegendScene;
            On.SaveLoadPlate.Deploy += SaveLoadPlate_Deploy;
            //On.GameSavedNotification.OnSaveProgress += GameSavedNotification_OnSaveProgress; progress elipsis ex: loading.....
            On.GameSavedNotification.OnSaveComplete += GameSavedNotification_OnSaveComplete;
            On.CinematicScene.Setbutton += CinematicScene_Setbutton;
        }

        private static void CinematicScene_Setbutton(On.CinematicScene.orig_Setbutton orig, CinematicScene self)
        {
            orig(self);
            if (Profile.Singleton.zoneMaxId < 9)
            {
                self.labelEnd.label = LanguageManager.GetString("CINEMATICSCENE_LOCKED");
            }
            if (Profile.Singleton.zoneMaxId < 8)
            {
                self.labelZone8.label = LanguageManager.GetString("CINEMATICSCENE_LOCKED");
            }
            if (Profile.Singleton.zoneMaxId < 7)
            {
                self.labelZone7.label = LanguageManager.GetString("CINEMATICSCENE_LOCKED");
            }
            if (Profile.Singleton.zoneMaxId < 6)
            {
                self.labelZone6.label = LanguageManager.GetString("CINEMATICSCENE_LOCKED");
            }
            if (Profile.Singleton.zoneMaxId < 5)
            {
                self.labelZone5.label = LanguageManager.GetString("CINEMATICSCENE_LOCKED");
            }
            if (Profile.Singleton.zoneMaxId < 4)
            {
                self.labelZone4.label = LanguageManager.GetString("CINEMATICSCENE_LOCKED");
            }
            if (Profile.Singleton.zoneMaxId < 3)
            {
                self.labelZone3.label = LanguageManager.GetString("CINEMATICSCENE_LOCKED");
                self.labelTape.label = LanguageManager.GetString("CINEMATICSCENE_LOCKED");
            }
            if (Profile.Singleton.zoneMaxId < 2)
            {
                self.labelZone2.label = LanguageManager.GetString("CINEMATICSCENE_LOCKED");
            }
            if (Profile.Singleton.zoneMaxId < 1)
            {
                self.labelZone1.label = LanguageManager.GetString("CINEMATICSCENE_LOCKED");
            }
        }

        private static void ModifyLegendScene(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.LoadSceneMode arg1)
        {
            if (arg0.name != "Legend") return;
            var canvas = GameObject.Find("Canvas/GameObject");
            canvas.transform.Find("Text").GetComponent<Text>().text = LanguageManager.GetString("MENU_LEGENDRUNHEADER");
            canvas.transform.Find("Text (1)").GetComponent<Text>().text = LanguageManager.GetString("MENU_LEGENDRUNBODY");

        }
        private static void SaveLoadPlate_Deploy(On.SaveLoadPlate.orig_Deploy orig, SaveLoadPlate self, Profile _profile, VoidDelegate onSaveLoad, VoidDelegate onReset)
        {
            orig(self, _profile, onSaveLoad, onReset);

            if (_profile.hasData == 0)
            {
                self.nameLabel.text = LanguageManager.GetString("MENU_SAVE_NEWGAME");
            }
            else
            {
                self.levelLabel.text = LanguageManager.GetString("MENU_SAVE_STAGEFORMAT", (_profile.fightId + 1));
                int num = _profile.zoneMaxId + 1;
                KJMath.Cap(ref num, 1, 9);
                self.nameLabel.text = (num != 1) ? LanguageManager.GetString("MENU_SAVE_ZONEFORMAT", num - 1) : LanguageManager.GetString("MENU_SAVE_ZONEPROLOGUE");
            }

            //will get ran 3 times but its fiiiine
            var canvas = self.transform.parent;
            var textObj = canvas.gameObject.transform.Find("Text");
            if (textObj && !textObj.GetComponent<TokenTranslator>())
            {
                var comp = textObj.gameObject.AddComponent<TokenTranslator>();
                comp.token = "MENU_SAVE_SELECTSLOT";
                comp.textController = textObj.GetComponent<Text>();
            }
        }

        private static void GameSavedNotification_OnSaveComplete(On.GameSavedNotification.orig_OnSaveComplete orig, GameSavedNotification self)
        {
            orig(self);
            self.saveText.text = LanguageManager.GetString("GAMESAVEDNOTIFICATION_GAMESAVED");
        }
    }
}
