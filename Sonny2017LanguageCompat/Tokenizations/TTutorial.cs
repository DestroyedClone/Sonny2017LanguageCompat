using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking.NetworkSystem;

namespace Sonny2017LanguageCompat.Tokenizations
{
    internal static class TTutorial
    {
        public static void Init()
        {
            On.Tutorial.ctor += Tutorial_ctor;
            On.TutorialPointer.Show_GameObject_Tutorial += TutorialPointer_Show_GameObject_Tutorial;
        }

        private static void TutorialPointer_Show_GameObject_Tutorial(On.TutorialPointer.orig_Show_GameObject_Tutorial orig, TutorialPointer self, UnityEngine.GameObject target, Tutorial tutorial)
        {
            orig(self, target, tutorial);
            self.tutorialLabel.text = LanguageManager.GetString(tutorial.toolTip);
        }

        private static void Tutorial_ctor(On.Tutorial.orig_ctor orig, Tutorial self, string name, string toolTip, float timeDelay, Tutorial.Align align)
        {
            toolTip = "TUTORIAL_" + name;
            orig(self, name, toolTip, timeDelay, align);
        }
    }
}
