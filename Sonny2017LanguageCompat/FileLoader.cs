using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using JetBrains.Annotations;
using MiniJSON;
using UnityEngine;

namespace Sonny2017LanguageCompat
{
    internal static class FileLoader
    {
        readonly static string languageFolderName = "languages";
        public static FileInfo tokenScriptFileInfo = null;

        public static void Init()
        {

            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Debug.Log(Assembly.GetExecutingAssembly().Location);
            //C:\Program Files (x86)\Steam\steamapps\common\Sonny\BepInEx\plugins\SonnyLocalizationMod\Sonny2017LanguageCompat.dll
            Debug.Log(assemblyFolder);
            //C:\Program Files (x86)\Steam\steamapps\common\Sonny\BepInEx\plugins\SonnyLocalizationMod
            var subFolders = Directory.GetDirectories(assemblyFolder);
            //Debug.Log($"directory count {subFolders.Length}");

            DirectoryInfo languageFolderDirectory = null;
            foreach ( var subFolder in subFolders )
            {
                if (subFolder.EndsWith(languageFolderName))
                {
                    //Debug.Log(subFolder);
                    //C:\Program Files (x86)\Steam\steamapps\common\Sonny\BepInEx\plugins\SonnyLocalizationMod\languages
                    languageFolderDirectory = new DirectoryInfo(subFolder);
                    break;
                }
            }

            if (languageFolderDirectory == null)
            {
                Debug.LogError($"Couldn't find language folder! Mod will break!!!");
                return;
            }
            var rootFiles = languageFolderDirectory.GetFiles();
            Debug.Log($"Checking root files");
            foreach (var rootFile in rootFiles )
            {
                Debug.Log($"Root File: {rootFile.Name}");
                if (rootFile.Name.StartsWith("tokenScript"))
                {
                    tokenScriptFileInfo = rootFile;
                    break;
                }
            }
            if (tokenScriptFileInfo == null)
            {
                Debug.LogError($"Couldn't find XML for story script! MOD");
            }


            Debug.Log("Getting directories!");

            foreach (var languageFolder in languageFolderDirectory.GetDirectories())
            {
                var languageName = languageFolder.Name;
                Debug.Log(languageName);
                Dictionary<string, string> tokens = new Dictionary<string, string>();
                foreach (var languageFile in languageFolder.GetFiles())
                {
                    Debug.Log(languageFile.Name);
                    if (!languageFile.Extension.EndsWith("json"))
                    {
                        Debug.Log($"Non-json file {languageFile} found in language folder");
                        continue;
                    }
                    var content = File.ReadAllText(languageFile.FullName);
                    //var tempTokens = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);
                    var tempTokens = MiniJSON.Json.Deserialize(content) as Dictionary<string, object>;

                    foreach (var token in tempTokens)
                    {
                        tokens.Add(token.Key, (string)token.Value);
                    }
                }
                LanguageManager.languages.Add(languageName, tokens);
                Debug.Log($"Languages: Added {languageName} with {tokens.Count} tokens!");
                LanguageManager.SetLanguage(Plugin.cfgCurrentLanguage.Value);
            }
        }
    }
}
