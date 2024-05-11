using System;
using System.Collections.Generic;
using System.Text;

namespace Sonny2017LanguageCompat.Tokenizations
{
    internal class TMisc
    {
        public static void Init()
        {
            On.StandardPopup.Deploy += StandardPopup_Deploy;
            On.AchievementPlate.Deploy += AchievementPlate_Deploy;
        }
        private static void AchievementPlate_Deploy(On.AchievementPlate.orig_Deploy orig, AchievementPlate self, AchievementData achievement)
        {
            orig(self, achievement);
            self.title.text = LanguageManager.GetString("ACHIEVEMENTPLATE_UNLOCKED", achievement.name);
        }


        private static StandardPopup StandardPopup_Deploy(On.StandardPopup.orig_Deploy orig, string header, string body, string button1, KJ.VoidDelegate onPress1, string button2, KJ.VoidDelegate onPress2, StandardPopup.ButtonColor button1Color, StandardPopup.ButtonColor button2Color)
        {
            switch (header)
            {
                case "ERROR":
                    header = LanguageManager.GetString("ERROR");
                    break;
                case "RESET SKILL POINTS":
                    header = LanguageManager.GetString("ABILITYTREE_RESETSKILLPOINTS");
                    break;
                case "Class Select":
                    header = LanguageManager.GetString("CLASSHOLDER_CLASSSELECT");
                    break;
                case "TRAINING":
                    header = LanguageManager.GetString("MENUSCENE_TRAINING");
                    break;
                case "Locked":
                    header = LanguageManager.GetString("MENUSCENE_LOCKED");
                    break;
                case "DELETE PROFILE":
                    header = LanguageManager.GetString("SAVELOADPLATE_DELETEPROFILE");
                    break;
                case "LEGEND STAR":
                    header = LanguageManager.GetString("SETTINGSPOPUP_LEGENDSTAR");
                    break;
                case "EVOLVE":
                    header = LanguageManager.GetString("VABILITY_EVOLVEUPPER");
                    break;
                case "UPGRADE":
                    header = LanguageManager.GetString("VABILITYPANELTREE_UPGRADEUPPER");
                    break;
            }
            switch (body)
            {
                case "You don't have any abilities to Respec.":
                    body = LanguageManager.GetString("ABILITYTREE_NOABILITIESTORESPEC");
                    break;
                case "This class has already been learnt.":
                    body = LanguageManager.GetString("CLASSHOLDER_CLASSLEARNT");
                    break;
                case "Training can help if you're having difficulty with a battle, or just want more loot and power, by letting you fight weaker enemies.":
                    body = LanguageManager.GetString("MENUSCENE_TRAININGBODY");
                    break;
                case "This section is currently locked.":
                    body = LanguageManager.GetString("MENUSCENE_LOCKEDBODY");
                    break;
                case "Are you sure? Doing this will permanently erase this character.":
                    body = LanguageManager.GetString("SAVELOADPLATE_CONFIRMDELETION");
                    break;
                case "Sonny is meant to be won with strategy and planning. The Legend star marks playthroughs that haven't yet used any Training.":
                    body = LanguageManager.GetString("SETTINGSPOPUP_LEGENDSTARBODY");
                    break;
                case "An ability can only have one evolution at a time. If you evolve this ability, the previous evolution will be overriden. Proceed with evolution?":
                    body = LanguageManager.GetString("VABILITY_EVOLVEREPLACEMENTDESCRIPTION");
                    break;
                case "That ability cannot be used on that target.":
                    body = LanguageManager.GetString("VABILITY_BADTARGET");
                    break;
                case "That ability is passive. You cannot use it directly.":
                    body = LanguageManager.GetString("VABILITY_PASSIVE");
                    break;
                case "You cannot use this ability right now.":
                    body = LanguageManager.GetString("VABILITY_UNAVAILABLE");
                    break;
                case "Are you sure you want to upgrade this ability? It is generally a better idea to spend your points first unlocking new abilities before upgrading an existing ability. This will ensure that you have more options available in combat.":
                    body = LanguageManager.GetString("VABILITYPANELTREE_CONFIRMUPGRADEIFLESSLEARNED");
                    break;

                default:
                    if (body.StartsWith("Pay $") && body.EndsWith(" to reset all skill points?"))
                    {
                        var cutString = body.Replace("Pay $", "");
                        cutString = cutString.Replace(" to reset all skill points?", "");
                        body = LanguageManager.GetString("ABILITYTREE_PAYTORESETSKILLPOINTS", cutString);
                    }
                    else if (body.StartsWith("You need $") && body.EndsWith(" to use this option."))
                    {
                        var cutString = body.Replace("You need $", "");
                        cutString = cutString.Replace(" to use this option.", "");
                        body = LanguageManager.GetString("ABILITYTREE_POOR", cutString);
                    }
                    break;
            }
            switch (button1)
            {
                //Close -> CLOSE
                //NO -> NO
                case "CLOSE":
                    button1 = LanguageManager.GetString("CLOSE");
                    break;
                case "NO":
                    button1 = LanguageManager.GetString("NO");
                    break;
                case "BACK":
                    button1 = LanguageManager.GetString("BACK");
                    break;
                case "DELETE":
                    button1 = LanguageManager.GetString("SAVELOADPLATE_DELETE");
                    break;
                case "OK":
                    button1 = LanguageManager.GetString("OK");
                    break;
                case "EVOLVE":
                    button1 = LanguageManager.GetString("VABILITY_EVOLVEUPPER");
                    break;
            }
            switch (button2)
            {
                case "TRAIN":
                    button2 = LanguageManager.GetString("TRAIN");
                    break;
                case "CANCEL":
                    button2 = LanguageManager.GetString("SAVELOADPLATE_CANCEL");
                    break;
                case "OK":
                    button1 = LanguageManager.GetString("OK");
                    break;
            }
            return orig(header, body, button1, onPress1, button2, onPress2, button1Color, button2Color);
        }
    }
}
