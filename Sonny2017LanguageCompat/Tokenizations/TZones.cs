using System;
using System.Collections.Generic;
using System.Text;

namespace Sonny2017LanguageCompat.Tokenizations
{
    internal static class TZones
    {
        public static void Init()
        {
            On.ZonePool.OnInitialize += ZonePool_OnInitialize;
        }

        private static void ZonePool_OnInitialize(On.ZonePool.orig_OnInitialize orig, ZonePool self)
        {
            orig(self);
            //ew
            foreach (var zone in self.zoneList)
            {
                string token = "";
                token = zone.name switch
                {
                    "Patient Zero" => "ZONE_PATIENTZERO",
                    "Zone 10: Zone For test" => "ZONE_TEST",
                    "The Silver Strand" => "ZONE_SILVERSTRAND",
                    "Tera Jungle" => "ZONE_TERAJUNGLE",
                    "Firewell Factory" => "ZONE_FIREWELLFACTORY",
                    "The Hidden Forest" => "ZONE_HIDDENFOREST",
                    "Thunder Labs" => "ZONE_THUNDERLABS",
                    "Blackhall Keep" => "ZONE_BLACKHALLKEEP",
                    "ZPCI Stronghold" => "ZONE_ZPCISTRONGHOLD",
                    "The Red Pillars" => "ZONE_REDPILLARS",
                    _ => zone.name,
                };
                zone.name = LanguageManager.GetString(token);
            }
        }
    }
}
