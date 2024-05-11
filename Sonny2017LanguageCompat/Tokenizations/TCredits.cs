using System;
using System.Collections.Generic;
using System.Text;
using static Mono.Security.X509.X520;

namespace Sonny2017LanguageCompat.Tokenizations
{
    internal class TCredits
    {
        public static void Init()
        {
            On.CreditScene.AddCreditName += CreditScene_AddCreditName;
            On.CreditScene.CreateCreditRole += CreditScene_CreateCreditRole;
            On.CreditName.Deploy += CreditName_Deploy;
            On.Credit.Deploy += Credit_Deploy;
        }

        private static void Credit_Deploy(On.Credit.orig_Deploy orig, Credit self)
        {
            orig(self);
            var comp = self.header.gameObject.AddComponent<TokenTranslator>();
            comp.textController = self.header;
            comp.token = self.name;
        }

        private static void CreditName_Deploy(On.CreditName.orig_Deploy orig, CreditName self, string name, UnityEngine.Vector2 position, UnityEngine.GameObject parent)
        {
            orig(self, name, position, parent);
            var comp = self.text.gameObject.AddComponent<TokenTranslator>();
            comp.textController = self.text;
            comp.token = name;
        }

        private static Credit CreditScene_CreateCreditRole(On.CreditScene.orig_CreateCreditRole orig, CreditScene self, string roleName)
        {
            switch (roleName)
            {
                case "Sonny":
                    roleName = "CREDIT_ROLE_SONNY";
                    break;

                case "by ArmorGames Studio":
                    roleName = "CREDIT_ARMORGAMES";
                    break;

                case "Executive Producer":
                    roleName = "CREDIT_ROLE_EXECUTIVEPRODUCER";
                    break;

                case "Producer and Lead Developer":
                    roleName = "CREDIT_ROLE_PRODUCERANDLEADDEVELOPER";
                    break;

                case "Engineering":
                    roleName = "CREDIT_ROLE_ENGINEERING";
                    break;

                case "Lead Artist":
                    roleName = "CREDIT_ROLE_LEADARTIST";
                    break;

                case "Environmental Design":
                    roleName = "CREDIT_ROLE_ENVIRONMENTALDESIGN";
                    break;

                case "Environmental Texture":
                    roleName = "CREDIT_ROLE_ENVIRONMENTALTEXTURE";
                    break;

                case "UI Art and Design":
                    roleName = "CREDIT_ROLE_UIARTANDDESIGN";
                    break;

                case "Sound and Music":
                    roleName = "CREDIT_ROLE_SOUNDANDMUSIC";
                    break;

                case "Story":
                    roleName = "CREDIT_ROLE_STORY";
                    break;

                case "Voice Acting":
                    roleName = "CREDIT_ROLE_VOICEACTING";
                    break;

                case "Original Song":
                    roleName = "CREDIT_ROLE_ORIGINALSONG";
                    break;

                case "Project Manager":
                    roleName = "CREDIT_ROLE_PROJECTMANAGER";
                    break;

                case "Production Support":
                    roleName = "CREDIT_ROLE_PRODUCTIONSUPPORT";
                    break;

                case "DDF Test Team":
                    roleName = "CREDIT_ROLE_DDFTESTTEAM";
                    break;

                case "QA & Test Engineering":
                    roleName = "CREDIT_ROLE_QAANDTESTENGINEERING";
                    break;

                case "Gameplay Testing":
                    roleName = "CREDIT_ROLE_GAMEPLAYTESTING";
                    break;
            }
            return orig(self, roleName);
        }

        private static void CreditScene_AddCreditName(On.CreditScene.orig_AddCreditName orig, CreditScene self, string name, Credit credit)
        {
            if (name == "by ArmorGames Studio")
                name = "CREDIT_ARMORGAMES";
            orig(self, name, credit);
        }
    }
}