using BepInEx;
using BepInEx.Configuration;
using JetBrains.Annotations;
using Sonny;
using Sonny2017LanguageCompat.Language;
using Sonny2017LanguageCompat.Tokenizations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;
using UnityEngine;

[module: UnverifiableCode]
#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]

#pragma warning restore CS0618 // Type or member is obsolete


namespace Sonny2017LanguageCompat
{
    [BepInPlugin("com.DestroyedClone.LanguageCompatiblity", "Language Compatiblity", "0.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        internal static BepInEx.Logging.ManualLogSource _logger;
        public static ConfigEntry<string> cfgCurrentLanguage;
        //public static ConfigEntry<bool> cfgUseScriptOverride;

        public void Start()
        {
            _logger = Logger;
            cfgCurrentLanguage = Config.Bind("Language", "Current Language", "english", "english");
            //cfgUseScriptOverride = Config.Bind("Language", "Localize Script", true, "If true, then the script will be localized. This is a config setting because this will override the call made by CharacterContainer and other mods that take action on it will not perform their usual methods.");
            FileLoader.Init();

            //TAbility.Init();
            //TBuffs.Init();
            //TCombat.Init();
            TComic.Init();
            TCredits.Init();
            //TEvolve.Init();
            //TMenu.Init();
            //TMisc.Init();
            TScript.Init();
            TSplash.Init();
            //TUnit.Init();
            //TZones.Init();
            //On.SplashScene.Start += SplashScene_Start;
            TTutorial.Init();
            On.ItemPool.OnInitialize += ItemPool_OnInitialize;
        }

        public static void LoadComic(string comicId)
        {
            Profile.Singleton.globalManager.comicPanel = comicId;
            Profile.Singleton.globalManager.replayCinematic = true;
            Profile.Singleton.globalManager.currentTrack = KJCanary.Singleton.currentRenderTrackId;
            KJSingleton<KJCore>.Singleton.FadeToScene("Comic");
        }

        private void ItemPool_OnInitialize(On.ItemPool.orig_OnInitialize orig, ItemPool self)
        {
            orig(self);
            int i = 0;
            self.Create("6 AoE", "MOD_"+i++);
            self.item.powerX = 1f;
            self.item.icon = "1 Broken Pipe";
            self.item.itemType = ItemType.Weapon;
            self.item.rarity = ItemRarity.None;
            self.item.mechanicId = 6;
            self.Create("7 Dispel", "MOD_" + i++);
            self.item.powerX = 1f;
            self.item.icon = "1 Broken Pipe";
            self.item.itemType = ItemType.Weapon;
            self.item.rarity = ItemRarity.None;
            self.item.mechanicId = 7;
            self.Create("8 TITANBLOOD", "MOD_" + i++);
            self.item.powerX = 1f;
            self.item.icon = "1 Broken Pipe";
            self.item.itemType = ItemType.Weapon;
            self.item.rarity = ItemRarity.None;
            self.item.mechanicId = 8;
            self.Create("9 PHEONIXSOUL", "MOD_" + i++);
            self.item.powerX = 1f;
            self.item.icon = "1 Broken Pipe";
            self.item.itemType = ItemType.Weapon;
            self.item.rarity = ItemRarity.None;
            self.item.mechanicId = 9;
            self.Create("10 BLEED", "MOD_" + i++);
            self.item.powerX = 1f;
            self.item.icon = "1 Broken Pipe";
            self.item.itemType = ItemType.Weapon;
            self.item.rarity = ItemRarity.None;
            self.item.mechanicId = 10;
            self.Create("11 BACKSTAB", "MOD_" + i++);
            self.item.powerX = 1f;
            self.item.icon = "1 Broken Pipe";
            self.item.itemType = ItemType.Weapon;
            self.item.rarity = ItemRarity.None;
            self.item.mechanicId = 11;
            self.Create("12 DUEL", "MOD_" + i++);
            self.item.powerX = 1f;
            self.item.icon = "1 Broken Pipe";
            self.item.itemType = ItemType.Weapon;
            self.item.rarity = ItemRarity.None;
            self.item.mechanicId = 12;
            self.Create("13 EXTRATURN", "MOD_" + i++);
            self.item.powerX = 1f;
            self.item.icon = "1 Broken Pipe";
            self.item.itemType = ItemType.Weapon;
            self.item.rarity = ItemRarity.None;
            self.item.mechanicId = 13;
            self.Create("14 NECRO", "MOD_" + i++);
            self.item.powerX = 1f;
            self.item.icon = "1 Broken Pipe";
            self.item.itemType = ItemType.Weapon;
            self.item.rarity = ItemRarity.None;
            self.item.mechanicId = 14;
            self.Create("15 SILENTSELF", "MOD_" + i++);
            self.item.powerX = 1f;
            self.item.icon = "1 Broken Pipe";
            self.item.itemType = ItemType.Weapon;
            self.item.rarity = ItemRarity.None;
            self.item.mechanicId = 15;
            self.Create("16 AoEImba", "MOD_" + i++);
            self.item.powerX = 1f;
            self.item.icon = "1 Broken Pipe";
            self.item.itemType = ItemType.Weapon;
            self.item.rarity = ItemRarity.None;
            self.item.mechanicId = 16;
        }

        private void SplashScene_Start(On.SplashScene.orig_Start orig, SplashScene self)
        {
            orig(self);
            var allItemIcons = Resources.LoadAll("item", typeof(Sprite));
            Debug.Log(allItemIcons.Length);

            List<string> unusedItems = new List<string>();
            foreach (var icon in allItemIcons)
            {
                unusedItems.Add(icon.name);
            }
            foreach (var item in KJSingleton<ItemPool>.Singleton.itemList)
            {
                unusedItems.Remove(item.icon);
            }
            foreach (var item in unusedItems)
            {
                Debug.Log(item);
            }
        }

        public void Update()
        {
            if (Input.GetKey(KeyCode.Minus))
            {
                Time.timeScale = 0f;
            }
            if (Input.GetKey(KeyCode.Equals))
            {
                Time.timeScale = 1;
            }
            if (Input.GetKeyUp(KeyCode.Numlock))
            {
                for (int i = 0; i < 12; i++)
                {
                    Profile.Singleton.itemManager.AddToInventory("MOD_"+i);
                }
            }
        }
    }
}
