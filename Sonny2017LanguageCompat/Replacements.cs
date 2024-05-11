using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using KJ;
using Sonny;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using static Sonny2017LanguageCompat.LanguageManager;

namespace Sonny2017LanguageCompat
{
    internal static class Replacements
    {
        public static void Init()
        {
            //A_ModifyPreviousCooldown: Refreshed, Silenced
            /* 
				string text = (modValue <= 0) ? "Refreshed" : "Silenced";
				text = text + ": " + Mathf.Abs(modValue).ToString();
				unit.vUnit.ShowBuffText(previousAbilityOut.name, text, previousAbilityOut.element, true);
             */
            //A_ModifySelfCooldown
            /*
			string text = (num <= 0) ? "Refreshed" : "Silenced";
			text = text + ": " + Mathf.Abs(num).ToString();
			base.caster.vUnit.ShowBuffText(this.mechanic.evolution.name, text, this.mechanic.evolution.element, true);
            */
            //AbilityPopup.Deploy <- giga file
            /// *LEVEL*
            

            //On.AbilityPopup.SetText += AbilityPopup_SetText;
            //On.KJPopUp.Deploy += KJPopUp_Deploy;

            //Ally_AbilitySet - TODO
            //CoreData ReplaceToken
            On.CoreScript.CreateCharacters += CoreScript_CreateCharacters; //this might not work
            //On.ElementPlate.Calibrate += ElementPlate_Calibrate;
            //Enemy_Ability_Zone2 2-9 + ZPCI
            //_AbilitySet
            //ItemPool
            //ItemPopup
            On.ItemScene.CalibrateGold += ItemScene_CalibrateGold;
            On.ItemScene.CalibrateProfile += ItemScene_CalibrateProfile;
            On.ItemStatPlate.Deploy_string_string_int_bool += ItemStatPlate_Deploy_string_string_int_bool;
            On.ItemStore.CalibrateItem += ItemStore_CalibrateItem;
            //skipped all KJ* stuff because its probably all just base stuff
            On.KJPopUp.Deploy += KJPopUp_Deploy;
            On.KJMenuButton.Calibrate += KJMenuButton_Calibrate;
            On.PartyPlate.EndXPGain += PartyPlate_EndXPGain;
            //RateMePopup is disabled i think so no point in translating it
            On.ScreenPopup.UpdateButtons += ScreenPopup_UpdateButtons;
            //settingspopup.deploy, updatebuttons, 
            On.ShopScene.CalibrateGold += ShopScene_CalibrateGold;
            //skilltooltips.calibrate (hovertooltips)
            //sonnyachievementcontroller
            //splashscene version text
            On.KJTween.ColorTo_KJObject_Color_float_VoidDelegate_EaseType_TimeSpaceType_bool_FloatDelegate += KJTween_ColorTo_KJObject_Color_float_VoidDelegate_EaseType_TimeSpaceType_bool_FloatDelegate; //YUCK!
            On.StoryScript.Add_StoryCharacter_string += StoryScript_Add_StoryCharacter_string;
            On.TipPool.CalibrateTipList += TipPool_CalibrateTipList; //TODO: Replace for loop with direct replacements
            //tk2d is some unity thing prob, ignoring
            //treepopup.deploy
            //Tutorial (?)
            //TutorialPool
            //Unit_Zone*
            //UnitPool
            On.UpgradePopUp.SetText += UpgradePopUp_SetText;
            On.VAbilityPanel.ObjectDelegate += VAbilityPanel_ObjectDelegate;
            //Vabilitypanel.OnTap TODO
            On.VAbilityPanelTree.ObjectDelegate += VAbilityPanelTree_ObjectDelegate;
            //VAbilityPanelTree.OnTap
            //Vabilitypaneltree.Upgrade
            On.VAbilityTree.SetLevelPanel += VAbilityTree_SetLevelPanel;
            //Vabilitree.OnTap
            //vabilitytree.Upgrade / .SetLearned .SetActive .SetTextRequire
            //VictoryScene
            //VItemPanel
            //VItemStore
            //VShopPanel
            //VUnit
            //WavePanel


            On.VictoryScene.CalibrateTip += VictoryScene_CalibrateTip;
        }

        private static void VAbilityTree_SetLevelPanel(On.VAbilityTree.orig_SetLevelPanel orig, VAbilityTree self, bool show)
        {
            orig(self, show);
            if (show)
            {
                if (self.ability.DisplayLevel == Profile.Singleton.abilityManager.maxLevelAbility || (self.ability.DisplayLevel == 1 && !self.ability.canUpgrade))
                {
                    self.levelLabel.text = LanguageManager.GetString("VABILITYTREE_MAX");
                }
                else
                {
                    self.levelLabel.text = LanguageManager.GetString("VABILITYTREE_LEVELFORMAT", self.ability.level, Profile.Singleton.abilityManager.maxLevelAbility);
                }
            }
        }

        private static void VAbilityPanelTree_ObjectDelegate(On.VAbilityPanelTree.orig_ObjectDelegate orig, VAbilityPanelTree self, GameObject gameObject)
        {
            orig(self, gameObject);
            Ability ability = gameObject.GetComponent<VAbilityStore>().ability;
            if (!Profile.Singleton.abilityManager.HasDuplicateOf(ability))
            {
                self.AbilityPanel.alert.text = LanguageManager.GetString("VABILITYPANEL_ABILITYREADY", ability.name);
            }
            else
            {
                self.AbilityPanel.alert.text = LanguageManager.GetString("VABILITYPANEL_ABILITYACTIVE", ability.name);
            }
        }

        private static void VAbilityPanel_ObjectDelegate(On.VAbilityPanel.orig_ObjectDelegate orig, VAbilityPanel self, GameObject gameObject)
        {
            orig(self, gameObject); 
            Ability ability = gameObject.GetComponent<VAbilityStore>().ability;
            if (!Profile.Singleton.abilityManager.HasDuplicateOf(ability))
            {
                self.AbilityPanel.text.text = LanguageManager.GetString("VABILITYPANEL_ABILITYREADY", ability.name);
            }
            else
            {
                self.AbilityPanel.text.text = LanguageManager.GetString("VABILITYPANEL_ABILITYACTIVE", ability.name);
            }
        }

        private static void UpgradePopUp_SetText(On.UpgradePopUp.orig_SetText orig, UpgradePopUp self)
        {
            /*int num = (!ability.canUpgrade) ? 1 : 5;
            @new.level.text = string.Concat(new object[]
            {
            "LEVEL ",
            ability.power.Level,
            " / ",
            num.ToString()
            });
            @new.hitField.text = string.Empty;
            KJMath.SetY(@new.cdLabel.transform, 0f);
            if (ability.cooldownMax > 0)
            {
                @new.cdLabel.text = ability.cooldownMax.ToString() + " TURN COOLDOWN";
            }
            else
            {
                @new.cdLabel.text = "NO COOLDOWN";
            }*/
            orig(self);
        }

        private static void VictoryScene_CalibrateTip(On.VictoryScene.orig_CalibrateTip orig, VictoryScene self)
        {
            orig(self);
            self.tip.text = LanguageManager.GetString(self.tip.text);
        }

        private static void TipPool_CalibrateTipList(On.TipPool.orig_CalibrateTipList orig, TipPool self)
        {
            orig(self); //todo manual replacements. doing for loop rn because bored
            self.generalTips.Clear();
            if (Profile.Singleton.zoneMaxId > 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    self.generalTips.Add("TIP_" + i);
                }
            }
            if (Profile.Singleton.CanUseEvo)
            {
                for (int i = 10; i < 14; i++)
                {
                    self.generalTips.Add("TIP_" + i);
                }
            }
        }

        private static void StoryScript_Add_StoryCharacter_string(On.StoryScript.orig_Add_StoryCharacter_string orig, StoryScript self, StoryCharacter character, string line)
        {
            if (line.EndsWith(" has joined the party."))
            {
                var cutString = line.Replace(" has joined the party.", "");
                line = LanguageManager.GetString("STORYSCRIPT_HASJOINEDPARTY", cutString);
            }
            else if (line.EndsWith(" has left the party."))
            {
                var cutString = line.Replace(" has left the party.", "");
                line = LanguageManager.GetString("STORYSCRIPT_HASLEFTPARTY", cutString);
            }
            /*else if (line.EndsWith(""))
            {
                line = LanguageManager.GetString("");
            }*/
            orig(self, character, line);
        }

        private static KJColorTween KJTween_ColorTo_KJObject_Color_float_VoidDelegate_EaseType_TimeSpaceType_bool_FloatDelegate(On.KJTween.orig_ColorTo_KJObject_Color_float_VoidDelegate_EaseType_TimeSpaceType_bool_FloatDelegate orig, KJObject kjObject, Color colorTarget, float duration, VoidDelegate onComplete, EaseType easeType, KJTime.TimeSpaceType timeSpaceType, bool isLoop, FloatDelegate onProgress)
        {
            if (kjObject is VNarrationHolder vNarrationHolder)
            {
                var chapterText = vNarrationHolder.chapterText.text;
                if (chapterText.StartsWith("CHAPTER "))
                {
                    var cutString = chapterText.Replace("CHAPTER ", "");
                    vNarrationHolder.chapterText.text = LanguageManager.GetString("STORYPOPUP_CHAPTERFORMAT", cutString);
                } else if (chapterText == "PROLOGUE")
                {
                    vNarrationHolder.chapterText.text = LanguageManager.GetString("SAVELOADPLATE_ZONEPROLOGUE");
                }
            }

            return orig(kjObject, colorTarget, duration, onComplete, easeType, timeSpaceType, isLoop, onProgress);
        }

        private static void ShopScene_CalibrateGold(On.ShopScene.orig_CalibrateGold orig, ShopScene self)
        {
            orig(self);
            self.gold.text = LanguageManager.GetString("CASHFORMATTED", Profile.Singleton.Gold);
        }

        private static void ScreenPopup_UpdateButtons(On.ScreenPopup.orig_UpdateButtons orig, ScreenPopup self)
        {
            orig(self);
            string arg = (!KJSingleton<AchievementManager>.Singleton.fullScreen) ? LanguageManager.GetString("SCREENPOPUP_WINDOWED") : LanguageManager.GetString("SCREENPOPUP_FULLSCREEN");
            self.resuButton.label = LanguageManager.GetString("SCREENPOPUP_DISPLAYMODEFORMAT", arg);
            string str = ((!KJSingleton<AchievementManager>.Singleton.enableShaking) ? LanguageManager.GetString("SCREENPOPUP_OFF") : LanguageManager.GetString("SCREENPOPUP_ON"));
            self.shakeButton.label = LanguageManager.GetString("SCREENPOPUP_CAMSHAKEFORMAT", str);
        }

        

        private static void PartyPlate_EndXPGain(On.PartyPlate.orig_EndXPGain orig, PartyPlate self)
        {
            self.statusText.text = LanguageManager.GetString("PARTYPLATE_LEVELUP");
            orig(self);
        }

        private static void KJMenuButton_Calibrate(On.KJMenuButton.orig_Calibrate orig, KJMenuButton self)
        {
            if (self.label.StartsWith("SHAKE:"))
            {
                string str = (!KJSingleton<AchievementManager>.Singleton.enableShaking) ? LanguageManager.GetString("DISABLE") : LanguageManager.GetString("ENABLE");
                self.label = LanguageManager.GetString("MENUPOPUP_SHAKE", str);
            }
            orig(self);
        }

        private static void KJPopUp_Deploy(On.KJPopUp.orig_Deploy orig, KJPopUpContent content)
        {
            if (content is MenuPopup menuPopup)
            {
                if (menuPopup.versionField.text.StartsWith("Version "))
                {
                    var cutText = menuPopup.versionField.text.Replace("Version ", "");
                    menuPopup.versionField.text = LanguageManager.GetString("VERSIONFORMAT", cutText);
                }
            }
            orig(content);
        }

        private static void ItemStore_CalibrateItem(On.ItemStore.orig_CalibrateItem orig, ItemStore self)
        {
            orig(self);

            int num2 = Mathf.CeilToInt((float)Profile.Inventory.itemList.Count / 16f);
            if (num2 < self.page)
            {
                num2 = self.page;
            }
            self.pageLabel.text = LanguageManager.GetString("ITEMSTORE_PAGE", self.page.ToString(), num2.ToString());
        }

        private static void ItemScene_CalibrateProfile(On.ItemScene.orig_CalibrateProfile orig, ItemScene self)
        {
            orig(self);
            UnitData[] unlockedUnits = Profile.Singleton.UnlockedUnits;
            UnitData unitData = unlockedUnits[self.index];
            self.level.text = LanguageManager.GetString("ITEMSCENE_LEVELFORMAT", unitData.level);
        }

        private static void ItemStatPlate_Deploy_string_string_int_bool(On.ItemStatPlate.orig_Deploy_string_string_int_bool orig, ItemStatPlate self, string label, string value, int colorId, bool specialStatColor)
        {
            switch (label)
            {
                case "Power":
                    label = LanguageManager.GetString("ITEMSTATPLATE_POWER");
                    break;
                case "Defense":
                    label = LanguageManager.GetString("ITEMSTATPLATE_DEFENSE");
                    break;
                case "Speed":
                    label = LanguageManager.GetString("ITEMSTATPLATE_SPEED");
                    break;
                case "Vitality":
                    label = LanguageManager.GetString("ITEMSTATPLATE_VITALITY");
                    break;
                case "Crit. Chance":
                    label = LanguageManager.GetString("ITEMSTATPLATE_CRITCHANCE");
                    break;
                case "Hit. Bonus":
                    label = LanguageManager.GetString("ITEMSTATPLATE_HITBONUS");
                    break;
            }
            orig(self, label, value, colorId, specialStatColor);
        }

        private static void ItemScene_CalibrateGold(On.ItemScene.orig_CalibrateGold orig, ItemScene self)
        {
            orig(self);
            self.gold.text = LanguageManager.GetString("CASHFORMATTED", Profile.Singleton.Gold);
        }

        private static void CoreScript_CreateCharacters(On.CoreScript.orig_CreateCharacters orig)
        {
            orig();
            CoreScript.Sonny.name = LanguageManager.GetString("CORESCRIPT_SONNY");
            CoreScript.Darkara.name = LanguageManager.GetString("CORESCRIPT_DARKARA");
            CoreScript.Zarion.name = LanguageManager.GetString("CORESCRIPT_ZARION");
            CoreScript.ZPCI.name = LanguageManager.GetString("CORESCRIPT_ZPCI");
        }

        //https://stackoverflow.com/a/1857525
        public static string GetUntilOrEmpty(this string text, string stopAt = "-")
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return String.Empty;
        }

    }
}
