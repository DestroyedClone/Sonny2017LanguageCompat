using BepInEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace Sonny2017LanguageCompat.Tokenizations
{
    internal class TScript
    {
        public static void Init()
        {
            //On.CharacterContainer.Load += CharacterContainer_Load;
            On.ScriptContainer.Load += ScriptContainer_Load;
            On.StoryPopup.ExecuteDialogue += StoryPopup_ExecuteDialogue;
            On.StoryPopup.Scan += StoryPopup_Scan;
            //On.StoryScript.Add_string += StoryScript_Add_string;

            AutoGenerateTokens(); // then go jsonlint
            AutoGenerateNewScript();
        }

        private static string StoryPopup_Scan(On.StoryPopup.orig_Scan orig, StoryPopup self, string decodedLine)
        {
            decodedLine = LanguageManager.GetString(decodedLine);
            return orig(self, decodedLine);
        }

        private static void StoryPopup_ExecuteDialogue(On.StoryPopup.orig_ExecuteDialogue orig, StoryPopup self, StoryDialogue dialogue)
        {
            orig(self, dialogue);

        }

        public static Dictionary<string, string> autoTokens = new Dictionary<string, string>();

        private static void AutoGenerateNewScript()
        {
            TextAsset textAsset = (TextAsset)Resources.Load("XML/script");
            using StringReader reader = new StringReader(textAsset.text);
            string readText = reader.ReadToEnd();
            //readText.Replace("&#x2026;", "…");
            foreach (var tokenPairs in autoTokens)
            {
                if (tokenPairs.Value.IsNullOrWhiteSpace() || tokenPairs.Key.IsNullOrWhiteSpace())
                    continue;
                readText = readText.Replace(@"<conversation>" + @tokenPairs.Value + @"</conversation>", @"<conversation>" + @tokenPairs.Key + @"</conversation>");
            }

            string path = @"C:\Users\destr\source\repos\Sonny2017LanguageCompat\Sonny2017LanguageCompat\languages\tokenScript.xml";
            File.WriteAllText(path, readText);
        }

        private static void StoryScript_Add_string(On.StoryScript.orig_Add_string orig, StoryScript self, string narrationLine)
        {
            if (narrationLine.EndsWith(" has joined the party."))
            {
                narrationLine = LanguageManager.UseStringAsInputForToken(narrationLine, "SCRIPTSHARED_UNITJOINEDPARTY", " has joined the party.");
            }
            orig(self, narrationLine);
        }

        private static void AutoGenerateTokens()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ScriptContainer));
            TextAsset textAsset = (TextAsset)Resources.Load("XML/script");
            StringReader textReader = new StringReader(textAsset.text);
            var scriptContainer = xmlSerializer.Deserialize(textReader) as ScriptContainer;
            StringBuilder sb = new StringBuilder();
            foreach (_StoryScriptData storyScriptData in scriptContainer.storyScript)
            {
                int incrementor = 0;
                foreach (ConversationData conversationData in storyScriptData.conversationData)
                {
                    string script_type = conversationData.script_type;
                    var token = $"SCRIPT_{storyScriptData.name}_{incrementor++}_{script_type.ToUpper()}";
                    var line = "";
                    switch (script_type)
                    {
                        case "Dialogue":
                            if (conversationData.character_id != "-1")
                            {
                                var character = Zone1Script.GetCharacterData(conversationData.character_id);
                                token += "_" + character.name.ToUpper();
                            }
                            else
                            {
                                token += "_NOSPEAKER";
                            }
                            line = @conversationData.conversation;
                            if (token == "SCRIPT_Z2_2_0_3_DIALOGUE_VERADUX") //newline ruining my horrible code!
                                line = "Starting to think I'm dead already. This is Grade A nuts. @VO_Veradux_01";
                            break;
                        case "Narration":
                            line = @conversationData.conversation;
                            break;
                        case "Header":
                            token += $"_HEADERNUM_{@conversationData.header_number}";
                            line = @conversationData.conversation;
                            break;
                    }
                    line = line.Replace(System.Environment.NewLine, $"\n");
                    autoTokens.Add(token, line);
                    //Debug.Log(line);
                    sb.AppendLine($"\"{token}\": \"{line}\",");
                }
            }
            //Debug.Log(sb.ToString());
            string path = @"C:\Users\destr\source\repos\Sonny2017LanguageCompat\Sonny2017LanguageCompat\languages\english\Script.json";
            File.WriteAllText(path,
                "{\n" + 
                sb.ToString() + 
                "\n}");
        }

        private static ScriptContainer ScriptContainer_Load(On.ScriptContainer.orig_Load orig, string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ScriptContainer));
            return xmlSerializer.Deserialize(FileLoader.tokenScriptFileInfo.OpenText()) as ScriptContainer;
            /*
            if (path == "XML/script")
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(ScriptContainer));
                return xmlSerializer.Deserialize(FileLoader.tokenScriptFileInfo.OpenText()) as ScriptContainer;
            }
            return orig(path);*/
        }

        private static CharacterContainer CharacterContainer_Load(On.CharacterContainer.orig_Load orig, string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CharacterContainer));
            TextAsset textAsset = (TextAsset)Resources.Load("XML/character");
            StringReader textReader = new StringReader(textAsset.text);
            return xmlSerializer.Deserialize(textReader) as CharacterContainer;
        }
    }
}
