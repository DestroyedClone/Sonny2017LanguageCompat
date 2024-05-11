using Sonny;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine.UI;

namespace Sonny2017LanguageCompat.Tokenizations
{
    internal class TCombat
    {
        public static void Init()
        {
            On.KJNumberPopper.Deploy_int_string_Vector2_VoidDelegate += KJNumberPopper_Deploy_int_string_Vector2_VoidDelegate;

            On.UnitPanel.Show += UnitPanel_Show;
            On.UnitAbilityBox.Deploy += UnitAbilityBox_Deploy;
            On.UnitPanelStat.SetLabel += UnitPanelStat_SetLabel;
            //UnitPanelBar has a (this.labelText.text == "HP") so this is kinda wack....
            On.UnitPanelElement.Calibrate += UnitPanelElement_Calibrate;
            On.CombatScene.Broadcast += CombatScene_Broadcast;
            On.CombatScene.SetSkipToExit += CombatScene_SetSkipToExit;
        }

        private static void CombatScene_SetSkipToExit(On.CombatScene.orig_SetSkipToExit orig, CombatScene self)
        {
            orig(self);
            self.skipButton.GetComponentInChildren<Text>().text = LanguageManager.GetString("EXIT");
        }

        private static void CombatScene_Broadcast(On.CombatScene.orig_Broadcast orig, CombatScene self, string text, BroadcastBar.BroadcastType type)
        {
            if (text.EndsWith("'s turn. Ready to evolve!"))
            {
                var cutText = text.Replace("'s turn. Ready to evolve!", "");
                text = LanguageManager.GetString("COMBATCONTROLLER_READYEVOLVE", cutText);
            }
            else if (text.EndsWith("'s turn."))
            {
                var cutText = text.Replace("'s turn.", "");
                text = LanguageManager.GetString("COMBATCONTROLLER_READYTURN", cutText);
            }
            else if (text.Contains(" uses ") && text.Contains(" on ") && text.Contains("."))
            {
                //chatgpt code
                //https://chat.openai.com/share/195d8078-fe5b-47fb-881c-225630720f46
                string pattern = @"(\w+) uses (\w+) on (\w+)\.";
                Match match = Regex.Match(text, pattern);

                string attacker = match.Groups[1].Value;
                string ability = match.Groups[2].Value;
                string enemy = match.Groups[3].Value;

                text = LanguageManager.GetString("COMBATCONTROLLER_AUSESBONC", attacker, ability, enemy);
            }
            else if (text.Contains("Evolve complete! ") && text.Contains(" is evolved with ") && text.Contains("."))
            {
                //chatgpt code
                //https://chat.openai.com/share/195d8078-fe5b-47fb-881c-225630720f46
                string pattern = @"Evolve complete! (\w+) is evolved with (\w+).";
                Match match = Regex.Match(text, pattern);

                string ability = match.Groups[1].Value;
                string evolution = match.Groups[2].Value;

                text = LanguageManager.GetString("EVOLVEPOPUP_EVOLVECOMPLETE", ability, ability);
            }
            else if (text == "You need 100 EP to do that!")
            {
                LanguageManager.GetString("VABILITY_NOTENOUGHEVOLVEPOINTS");
            }
            orig(self, text, type);
        }

        private static void KJNumberPopper_Deploy_int_string_Vector2_VoidDelegate(On.KJNumberPopper.orig_Deploy_int_string_Vector2_VoidDelegate orig, KJNumberPopper self, int numberValue, string textValue, UnityEngine.Vector2 position, KJ.VoidDelegate onComplete)
        {
            textValue = LanguageManager.GetString(textValue);
            orig(self, numberValue, textValue, position, onComplete);
        }

        private static void UnitPanel_Show(On.UnitPanel.orig_Show orig, UnitPanel self, Unit unit)
        {
            orig(self, unit);
            self.subText.text = LanguageManager.GetString("UNITPANEL_LEVELFORMAT", unit.level);

        }

        private static void UnitPanelElement_Calibrate(On.UnitPanelElement.orig_Calibrate orig, UnitPanelElement self, Unit unit, StatType statType)
        {
            var statString = GetLocalizedStatType(statType).ToUpper();
            self.panelLabel.text = LanguageManager.GetString("UNITPANEL_STATBONUS", statString);
            orig(self, unit, statType);
        }

        public static string GetLocalizedStatType(StatType statType)
        {
            return statType switch
            {
                StatType.Power => LanguageManager.GetString("UNITPANEL_POWER"),
                StatType.Defense => LanguageManager.GetString("UNITPANEL_DEFENSE"),
                StatType.Speed => LanguageManager.GetString("UNITPANEL_SPEED"),
                _ => "UNKNOWNSTATTYPE",
            };
        }

        private static void UnitPanelStat_SetLabel(On.UnitPanelStat.orig_SetLabel orig, UnitPanelStat self, string text)
        {
            switch (text)
            {
                case "Power":
                    text = LanguageManager.GetString("UNITPANEL_POWER");
                    break;
                case "Defense":
                    text = LanguageManager.GetString("UNITPANEL_DEFENSE");
                    break;
                case "Speed":
                    text = LanguageManager.GetString("UNITPANEL_SPEED");
                    break;
            }
            orig(self, text);
        }

        private static void UnitAbilityBox_Deploy(On.UnitAbilityBox.orig_Deploy orig, UnitAbilityBox self, Ability ability, float externalValue)
        {
            orig(self, ability, externalValue);
            string text = string.Empty;
            if (ability.cooldownMax > 0)
            {
                var token = ability.cooldownMax > 1 ? "UNITABILITYBOX_COOLDOWNFORMAT" : "UNITABILITYBOX_COOLDOWNSINGLEFORMAT";
                text = LanguageManager.GetString(token, ability.cooldownMax);
            }
            self.abilityCooldown.text = text;
        }
    }
}
