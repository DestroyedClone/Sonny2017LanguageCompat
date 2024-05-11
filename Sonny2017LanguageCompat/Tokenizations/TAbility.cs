using System;
using System.Collections.Generic;
using System.Text;
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

namespace Sonny2017LanguageCompat.Tokenizations
{
    internal static class TAbility
    {
        public static void Init()
        {
            On.AbilityPopup.SetResourcePanel += AbilityPopup_SetResourcePanel; //only called once, preserves code
            On.AbilityPopup.BoldTag += AbilityPopup_BoldTag;
            On.AbilityPopup.ColorTag += AbilityPopup_ColorTag;
            //On.AbilityPopupScene.SetText += AbilityPopupScene_SetText; is deploy even called?
            On.AbilityTree.Start += AbilityTree_Start;
            On.AbilityTree.CalibrateAll += AbilityTree_CalibrateAll;
            On.AbilityTree.SetLearned += AbilityTree_SetLearned;
            On.AbilityTree.ResetAbility += AbilityTree_ResetAbility;
            //AbilityTreePool
        }

        private static void AbilityTree_ResetAbility(On.AbilityTree.orig_ResetAbility orig, AbilityTree self)
        {
            orig(self);
            self.abilityPoint.text = LanguageManager.GetString("ABILITYTREE_SKILLPOINTS", Profile.AbilityPoints);
            self.gold.text = LanguageManager.GetString("CASHFORMATTED", Profile.Singleton.Gold.ToString());
        }
        public static void AbilityTree_SetLearned(On.AbilityTree.orig_SetLearned orig, AbilityTree self, int index, Ability ability)
        {
            orig(self, index, ability);
            self.abilityPoint.text = LanguageManager.GetString("ABILITYTREE_SKILLPOINTS", Profile.AbilityPoints);
        }

        private static void AbilityTree_CalibrateAll(On.AbilityTree.orig_CalibrateAll orig, AbilityTree self)
        {
            orig(self);
            self.respecButton.label = LanguageManager.GetString("ABILITYTREE_RESPEC", self.respecPrice.ToString());
            self.abilityPoint.text = LanguageManager.GetString("ABILITYTREE_SKILLPOINTS", Profile.AbilityPoints);
        }

        private static void AbilityTree_Start(On.AbilityTree.orig_Start orig, AbilityTree self)
        {
            orig(self);
            self.gold.text = LanguageManager.GetString("ABILITYTREE_CASH", Profile.Singleton.Gold.ToString());
        }

        private static string AbilityPopup_ColorTag(On.AbilityPopup.orig_ColorTag orig, string inputString, string colorHex)
        {
            var newString = inputString.Replace("Next Level: +", "");
            inputString = LanguageManager.GetString("ABILITYPOPUP_NEXTLEVELPLUS", newString);
            return orig(inputString, colorHex);
        }

        private static string AbilityPopup_BoldTag(On.AbilityPopup.orig_BoldTag orig, string inputString)
        {
            void Replace(string endsWithText, string formatToken)
            {
                if (inputString.EndsWith(endsWithText))
                {
                    var cutString = inputString.Replace(endsWithText, "");
                    inputString = LanguageManager.GetString(formatToken, cutString);
                }
            }
            Replace(" Damage", "ABILITYPOPUP_DAMAGE");
            Replace(" Healing", "ABILITYPOPUP_HEALING");
            Replace(" Power", "ABILITYPOPUP_POWER");
            Replace("% Proc Chance", "ABILITYPOPUP_PCTPROCCHANCE");
            Replace("% Crit Chance", "ABILITYPOPUP_PCTCRITCHANCE");
            Replace("% Crit Damage", "ABILITYPOPUP_PCTCRITDAMAGE");
            Replace("% Defense To Power", "ABILITYPOPUP_PCTDEFENSETOPOWER");
            Replace("% Effect Power", "ABILITYPOPUP_PCTEFFECTPOWER");
            Replace(" Focus", "ABILITYPOPUP_FOCUS");
            Replace("% Speed To Power", "ABILITYPOPUP_PCTSPEEDTOPOWER");
            Replace(" Less Damage Taken", "ABILITYPOPUP_LESSDAMAGETAKEN");
            return orig(inputString);
        }

        private static void AbilityPopup_SetResourcePanel(On.AbilityPopup.orig_SetResourcePanel orig, AbilityPopup self, bool isCombat)
        {
            orig(self, isCombat);
            var maxLevel = (!self.ability.canUpgrade) ? 1 : 5;
            self.level.text = LanguageManager.GetString("LEVELFORMAT", self.ability.power.Level, maxLevel);
            if (self.hitField.text.StartsWith("HIT CHANCE"))
            {
                self.hitField.text = LanguageManager.GetString("HITCHANCEFORMAT", (self.ability.HitChance * 100f).ToString("F0"));
                KJMath.SetY(self.cdLabel.transform, self.cooldownOffset);
            }
            if (self.ability.cooldownMax > 0)
            {
                self.cdLabel.text = LanguageManager.GetString("TURNCOOLDOWNFORMAT", self.ability.cooldownMax.ToString());
            }
            else
            {
                self.cdLabel.text = LanguageManager.GetString("NOCOOLDOWN");
            }
            self.evoText.text = ReplaceStringWithToken(self.evoText.text, "Basic Attack", "BASICATTACK");
            self.evoText.text = ReplaceStringWithToken(self.evoText.text, "Passive Ability", "PASSIVEABILITY");
            //could go with a copy code route if this doesnt work right?
            var cutString = self.evoText.text.Replace("Evolved with ", "EVOLVEDWITH");
            self.evoText.text = ReplaceStringWithToken(self.evoText.text, "Evolved with ", "EVOLVEDWITH");
            self.evoText.text = LanguageManager.GetString("EVOLVEDWITH", cutString);

            //VAbility.OnHold (TODO)

            /*if (self.button1.label == "Evolve")
            {
                if (ability.unit.EvolvePoints.Value < 100f)
                {
                    self.button2.transform.localPosition = new Vector2(0f, @new.button2.transform.localPosition.y);
                    self.button1.gameObject.SetActive(false);
                }
                else
                {
                    @new.SetYellowButton(@new.button1);
                }
            }
            if (button1 == "Learn")
            {
                @new.SetYellowButton(@new.button1);
                KJSingleton<TutorialPool>.Singleton.SetInteractLink("T4_TapLearn", @new.button1.ButtonCore, null, Tutorial.InteractMode.Press);
            }
            if (button2 == "Upgrade")
            {
                @new.SetYellowButton(@new.button2);
            }*/
        }

        private static string GetIsolatedString(string baseText, string textToRemove)
        {
            return baseText.Replace(textToRemove, "");
        }
    }
}
