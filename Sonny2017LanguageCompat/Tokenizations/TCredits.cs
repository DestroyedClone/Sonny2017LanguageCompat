using System;
using System.Collections.Generic;
using System.Text;

namespace Sonny2017LanguageCompat.Tokenizations
{
    internal class TCredits
    {
        public static void Init()
        {
            On.CreditScene.AddCreditName += CreditScene_AddCreditName;
            On.CreditScene.CreateCreditRole += CreditScene_CreateCreditRole;
        }

        private static Credit CreditScene_CreateCreditRole(On.CreditScene.orig_CreateCreditRole orig, CreditScene self, string roleName)
        {
            switch (roleName)
            {
                case "Sonny":
                    roleName = LanguageManager.GetString("CREDIT_ROLE_SONNY");
                    break;
                case "by ArmorGames Studio":
                    roleName = LanguageManager.GetString("CREDIT_ARMORGAMES");
                    break;
                case "Executive Producer":
                    roleName = LanguageManager.GetString("CREDIT_ROLE_EXECUTIVEPRODUCER");
                    break;
                case "Producer and Lead Developer":
                    roleName = LanguageManager.GetString("CREDIT_ROLE_PRODUCERANDLEADDEVELOPER");
                    break;
                case "Engineering":
                    roleName = LanguageManager.GetString("CREDIT_ROLE_ENGINEERING");
                    break;
                case "Lead Artist":
                    roleName = LanguageManager.GetString("CREDIT_ROLE_LEADARTIST");
                    break;
                case "Environmental Design":
                    roleName = LanguageManager.GetString("CREDIT_ROLE_ENVIRONMENTALDESIGN");
                    break;
                case "Environmental Texture":
                    roleName = LanguageManager.GetString("CREDIT_ROLE_ENVIRONMENTALTEXTURE");
                    break;
                case "UI Art and Design":
                    roleName = LanguageManager.GetString("CREDIT_ROLE_UIARTANDDESIGN");
                    break;
                case "Sound and Music":
                    roleName = LanguageManager.GetString("CREDIT_ROLE_SOUNDANDMUSIC");
                    break;
                case "Story":
                    roleName = LanguageManager.GetString("CREDIT_ROLE_STORY");
                    break;
                case "Voice Acting":
                    roleName = LanguageManager.GetString("CREDIT_ROLE_VOICEACTING");
                    break;
                case "Original Song":
                    roleName = LanguageManager.GetString("CREDIT_ROLE_ORIGINALSONG");
                    break;
                case "Project Manager":
                    roleName = LanguageManager.GetString("CREDIT_ROLE_PROJECTMANAGER");
                    break;
                case "Production Support":
                    roleName = LanguageManager.GetString("CREDIT_ROLE_PRODUCTIONSUPPORT");
                    break;
                case "DDF Test Team":
                    roleName = LanguageManager.GetString("CREDIT_ROLE_DDFTESTTEAM");
                    break;
                case "QA & Test Engineering":
                    roleName = LanguageManager.GetString("CREDIT_ROLE_QAANDTESTENGINEERING");
                    break;
                case "Gameplay Testing":
                    roleName = LanguageManager.GetString("CREDIT_ROLE_GAMEPLAYTESTING");
                    break;
            }
            return orig(self, roleName);
        }

        private static void CreditScene_AddCreditName(On.CreditScene.orig_AddCreditName orig, CreditScene self, string name, Credit credit)
        {
            if (name == "by ArmorGames Studio")
                name = LanguageManager.GetString("CREDIT_ARMORGAMES");
            orig(self, name, credit);
        }
    }
}
