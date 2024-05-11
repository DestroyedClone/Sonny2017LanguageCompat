using System;
using System.Collections.Generic;
using System.Text;

namespace Sonny2017LanguageCompat.Tokenizations
{
    internal class TUnit
    {
        public static void Init()
        {
            On.UnitPool.OnInitialize += UnitPool_OnInitialize;
        }

        private static void UnitPool_OnInitialize(On.UnitPool.orig_OnInitialize orig, UnitPool self)
        {
            orig(self);

            foreach (var unitData in KJSingleton<UnitPool>.Singleton.unitList)
            {
                CorrectData(unitData);
                CorrectData(KJSingleton<UnitPool>.Singleton.unitDic[unitData.UDID]);
            }
        }

        public static void CorrectData(UnitData unitData)
        {
            if (unitData.name == "Sonny") return; //PartyPlate.ShowAbilityList
            //UNIT_Z1_Sailor_NAME
            unitData.name = $"UNIT_{unitData.UDID}_NAME";
            for (int i = 0; i < unitData.tip.Count; i++)
            {
                unitData.tip[i] = $"UNIT_{unitData.UDID}_TIP_{i}";
            }
        }
    }
}
