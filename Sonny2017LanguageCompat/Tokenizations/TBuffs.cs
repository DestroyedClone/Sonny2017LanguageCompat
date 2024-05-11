using System;
using System.Collections.Generic;
using System.Text;
using static Sonny2017LanguageCompat.LanguageManager;

namespace Sonny2017LanguageCompat.Tokenizations
{
    internal static class TBuffs
    {
        public static void Init()
        {
            //On.CoreBuffSet.NewBuff += CoreBuffSet_NewBuff;
            foreach (var buffData in KJSingleton<BuffPool>.Singleton.buffDataList)
            {
                CorrectBuff(buffData);
                CorrectBuff(KJSingleton<BuffPool>.Singleton.buffDataDict[buffData.UDID]);
            }
            On.VUnit.ShowBuffText += VUnit_ShowBuffText;
            //Buff ReplaceToken thing
            On.BuffPopUp.SetText += BuffPopUp_SetText;
        }
        private static void BuffPopUp_SetText(On.BuffPopUp.orig_SetText orig, BuffPopUp self, string header, string body)
        {
            //Did code block but can be done through replacements
            string text = string.Empty;
            string text2 = string.Empty;
            self.durationField.text = string.Empty;
            self.turnField.text = string.Empty;
            self.chargeField.text = string.Empty;
            self.durationField.gameObject.SetActive(true);
            if (self.buff.currentDuration > 0 && self.buff.currentDuration < 50) //should i patch this out?
            {
                text = (self.buff.currentDuration != 1) ? LanguageManager.GetString("BUFFPOPUP_AMTTURNS", self.buff.currentDuration) : LanguageManager.GetString("BUFFPOPUP_ONETURNS");
            }
            if (self.buff.currentCharges > 0 && self.buff.currentCharges < 50)
            {
                if (self.buff.condition == BuffCondition.Type.OnTurnEnd || self.buff.condition == BuffCondition.Type.OnTurnStart)
                {
                    if (self.buff.currentCharges < self.buff.currentDuration)
                    {
                        text = (self.buff.currentCharges != 1) ? LanguageManager.GetString("BUFFPOPUP_AMTTURNS", self.buff.currentCharges) : LanguageManager.GetString("BUFFPOPUP_ONETURNS");
                    }
                }
                else
                {
                    text2 = (self.buff.currentCharges != 1) ? LanguageManager.GetString("BUFFPOPUP_AMTCHARGES", self.buff.currentCharges) : LanguageManager.GetString("BUFFPOPUP_ONECHARGES");
                }
            }
            if (self.buff.isPassive)
            {
                self.durationField.text = LanguageManager.GetString("BUFFPOPUP_PASSIVE");
            }
            else if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
            {
                self.turnField.text = text;
                self.chargeField.text = text2;
            }
            else if (!string.IsNullOrEmpty(text))
            {
                self.durationField.text = text;
            }
            else if (!string.IsNullOrEmpty(text2))
            {
                self.durationField.text = text2;
            }
            //self.buff = buff;
            self.SetText(self.buff.name, self.buff.Description);
            orig(self, header, body);
        }


        public static void CorrectBuff(BuffData buffData)
        {
            var udid = buffData.UDID;
            var baseToken = "BUFF_" + udid;
            buffData.name = baseToken + "_NAME";
            buffData.description = baseToken + "_DESC";
            buffData.shortDescription = baseToken + "_SHORTDESC";
        }

        private static void VUnit_ShowBuffText(On.VUnit.orig_ShowBuffText orig, VUnit self, string text, string details, Sonny.Element element, bool shouldMoveCamera)
        {
            text = ReplaceStringWithToken(text, "Refreshed", "BUFFSHARED_REFRESHED");
            text = ReplaceStringWithToken(text, "Silenced", "BUFFSHARED_SILENCED");
            if (text.EndsWith(" Dispelled"))
            {
                text = LanguageManager.UseStringAsInputForToken(text, "BUFFSHARED_BUFFDISPELLED_FORMAT", " Dispelled");
            }
            if (text.EndsWith(" Silenced"))
            {
                text = LanguageManager.UseStringAsInputForToken(text, "BUFFSHARED_BUFFSILENCED_FORMAT", " Silenced");
            }
            string endingText;
            string startingText;

            endingText = " Power";
            startingText = "Gained ";
            text = ReplaceStringWithToken(text, "Power Down", "BUFFSHARED_POWERDOWN");
            text = ReplaceStringWithToken(text, "Power Up", "BUFFSHARED_POWERUP");
            if (details.EndsWith(endingText))
            {
                if (details.StartsWith(startingText))
                {
                    var cutText = RemoveText(details, startingText, endingText);
                    details = LanguageManager.GetString("BUFFSHARED_POWERUPFORMAT", cutText);
                }
                else
                {
                    var cutText = details.Replace(endingText, "");
                    details = LanguageManager.GetString("BUFFSHARED_POWERDOWNFORMAT", cutText);
                }
            }

            endingText = " Defense";
            startingText = "Gained ";
            text = ReplaceStringWithToken(text, "Power Down", "BUFFSHARED_DEFENSEDOWN");
            text = ReplaceStringWithToken(text, "Power Up", "BUFFSHARED_DEFENSEUP");
            if (details.EndsWith(endingText))
            {
                if (details.StartsWith(startingText))
                {
                    var cutText = RemoveText(details, startingText, endingText);
                    details = LanguageManager.GetString("BUFFSHARED_DEFENSEUPFORMAT", cutText);
                }
                else
                {
                    var cutText = details.Replace(endingText, "");
                    details = LanguageManager.GetString("BUFFSHARED_DEFENSEDOWNFORMAT", cutText);
                }
            }

            endingText = " Speed";
            startingText = "Gained ";
            text = ReplaceStringWithToken(text, "Power Down", "BUFFSHARED_SPEEDDOWN");
            text = ReplaceStringWithToken(text, "Power Up", "BUFFSHARED_SPEEDUP");
            if (details.EndsWith(endingText))
            {
                if (details.StartsWith(startingText))
                {
                    var cutText = RemoveText(details, startingText, endingText);
                    details = LanguageManager.GetString("BUFFSHARED_SPEEDUPFORMAT", cutText);
                }
                else
                {
                    var cutText = details.Replace(endingText, "");
                    details = LanguageManager.GetString("BUFFSHARED_SPEEDDOWNFORMAT", cutText);
                }
            }
            orig(self, text, details, element, shouldMoveCamera);
        }
    }
}
