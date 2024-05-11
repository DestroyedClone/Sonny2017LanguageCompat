using System;
using System.Collections.Generic;
using System.Text;

namespace Sonny2017LanguageCompat.Tokenizations
{
    public static class TEvolve
    {
        public static void Init()
        {
            On.EvolutionPool.OnInitialize += EvolutionPool_OnInitialize;
            On.EvolvePlate.Awake += EvolvePlate_Awake;
        }

        private static void EvolutionPool_OnInitialize(On.EvolutionPool.orig_OnInitialize orig, EvolutionPool self)
        {
            orig(self);
            foreach (var evol in self.evolutionDataList)
            {
                //EVOLVE_AMP1_NAME
                evol.name = "EVOLVE_" + evol.UDID + "_NAME";
                evol.description = "EVOLVE_" + evol.UDID + "_DESC";
            }
        }

        private static void EvolvePlate_Awake(On.EvolvePlate.orig_Awake orig, EvolvePlate self)
        {
            orig(self);
            self.button.label = LanguageManager.GetString("EVOLVEPLATE_EVOLVE");
        }
    }
}
