using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Sonny2017LanguageCompat.Tokenizations
{
    internal class TComic
    {
        public static void Init()
        {
            TokenizeComics();
        }

        private static void TokenizeComics()
        {
            //comic1
            //HandleComic("Comic1", Pair("", "")); no text
            HandleComic("Comic2", Pair("", "COMIC_2"));
            HandleComic("Comic3", Pair("", "COMIC_3"));
            HandleComic("Comic4", Pair("", "COMIC_4"));
            HandleComic("Comic5", Pair("", "COMIC_5"));
            //HandleComic("Comic6", Pair("", "COMIC_")); unused
            HandleComic("Comic7", Pair("", "COMIC_7A"), Pair(" 2", "COMIC_7B"));
            HandleComic("Comic8", Pair("", "COMIC_8"));
            HandleComic("Comic9", Pair("", "COMIC_9A"), Pair(" 2", "COMIC_9B"));
            HandleComic("Comic10", Pair("", "COMIC_10A"), Pair(" (1)", "COMIC_10B"), Pair(" (2)", "COMIC_10C"));
            HandleComic("Comic11", Pair("", "COMIC_11A"), Pair(" 2", "COMIC_11B"));
            //HandleComic("Comic12", Pair("", "COMIC_")); unused
            HandleComic("Comic13", Pair("", "COMIC_13A"), Pair(" (1)", "COMIC_13B"));
            //HandleComic("Comic14", Pair("", "COMIC_")); no txt
            //HandleComic("Comic15", Pair("", "COMIC_")); no txt
            HandleComic("beginzone2.1", Pair("", "COMIC_BEGINZONE2_1"));
            HandleComic("beginZone2.2", Pair("", "COMIC_BEGINZONE2_2"));
            HandleComic("BeginZone2.3", Pair("", "COMIC_BEGINZONE2")); //test
            HandleComic("beginzone2.4", Pair("", "COMIC_BEGINZONE2_0A"), Pair(" (1)", "COMIC_BEGINZONE2_0B"));
            //2_0A = But insteadâ€¦ <- elipses
            HandleComic("beginzone3", Pair("", "COMIC_BEGINZONE3A"), Pair(" (1)", "COMIC_BEGINZONE3B"), Pair(" (2)", "COMIC_BEGINZONE3C"));
            HandleComic("beginzone4.1", Pair("", "COMIC_BEGINZONE4A"), Pair(" (1)", "COMIC_BEGINZONE4B"), Pair(" (2)", "COMIC_BEGINZONE4C"));
            //4B: Itâ€™s red raw. Cracked. Thirsty.
            HandleComic("beginzone4.2", Pair("", "COMIC_BEGINZONE4_0"));
            HandleComic("beginzone5", Pair("", "COMIC_BEGINZONE5A"), Pair(" (1)", "COMIC_BEGINZONE5B"));
            HandleComic("beginzone6", Pair(" (1)", "COMIC_BEGINZONE6A"), Pair(" (2)", "COMIC_BEGINZONE6B"), Pair(" (3)", "COMIC_BEGINZONE6C"));
            HandleComic("beginzone7.1", Pair(" (3)", "COMIC_BEGINZONE7A"), Pair(" (4)", "COMIC_BEGINZONE7B"));
            //A To know that somewhereâ€¦
            //B Itâ€™s somewhat comfortingâ€¦
            HandleComic("beginzone7.2", Pair(" (1)", "COMIC_BEGINZONE7_0A"), Pair(" (5)", "COMIC_BEGINZONE7_0B"));
            //A: Someone can still enjoy the simple things in lifeâ€¦
            HandleComic("beginzone8.1", Pair(" (1)", "COMIC_BEGINZONE8_2A"), Pair(" (2)", "COMIC_BEGINZONE8_2B"), Pair(" (5)", "COMIC_BEGINZONE8_2C"));
            //A: As a warriorâ€¦
            HandleComic("beginzone8.2", Pair(" (1)", "COMIC_BEGINZONE8A"), Pair(" (2)", "COMIC_BEGINZONE8B"));
            //A:And sometimesâ€¦
            HandleComic("beginzone8.3", Pair(" (1)", "COMIC_BEGINZONE8_0"));
            HandleComic("beginzone8.4", Pair(" (1)", "COMIC_BEGINZONE8_1"));
            HandleComic("beginzone9.1", Pair(" (1)", "COMIC_BEGINZONE9A"), Pair(" (2)", "COMIC_BEGINZONE9B"), Pair(" (3)", "COMIC_BEGINZONE9C"));
            HandleComic("endzone1", Pair("", "COMIC_ENDZONE1A"), Pair(" (1)", "COMIC_ENDZONE1B"));
            HandleComic("endzone2", Pair("", "COMIC_ENDZONE2A"), Pair(" (1)", "COMIC_ENDZONE2B"));
            //A Sometimes itâ€™s just there. In front of your face.
            HandleComic("endzone3", Pair("", "COMIC_ENDZONE3A"), Pair(" (1)", "COMIC_ENDZONE3B"));
            //A: Take it by the horns. Itâ€™ll try to throw you off.
            HandleComic("endzone4", Pair("", "COMIC_ENDZONE4A"), Pair(" (1)", "COMIC_ENDZONE4B"));
            // note: A and B uses '...' instead of an ellipsis for some reason
            HandleComic("endzone5", Pair("", "COMIC_ENDZONE5"));
            //HandleComic("endzone6", Pair("", "COMIC_")); no text
        }

        private static KeyValuePair<string, string> Pair(string key, string value)
        {
            return new KeyValuePair<string, string>(key, value);
        }

        private static void HandleComic(string comicName, params KeyValuePair<string, string>[] pairs)
        {
            //Debug.Log($"Loading comic {comicName}");
            HandleComic(LoadComic(comicName), pairs);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comic"></param>
        /// <param name="kvp">GameObject suffixes to tokens: {" (1)", "Z1_NAME"}</param>
        private static void HandleComic(GameObject comic, params KeyValuePair<string, string>[] pairs)
        {
            if (!comic)
            {
                //Debug.LogError($"Comic was null!");
                return;
            }
            //Debug.Log($"Handling gameobject: {comic}");
            foreach (var pair in pairs)
            {
                //Debug.Log($"Modify: \"{pair.Key}\" - \"{pair.Value}\"");
                try
                {
                    var textComp = comic.transform.Find($"ComicDialog{pair.Key}/Text").GetComponent<Text>();
                    var newComp = textComp.gameObject.AddComponent<TokenTranslator>();
                    newComp.textController = textComp;
                    newComp.token = pair.Value;
                }
                catch (Exception e)
                {
                    Debug.LogError(e.ToString());
                }
            }
        }

        private static GameObject LoadComic(string comicName)
        {
            //Debug.Log($"Mod: Loading comic: {comicName}");
            return Resources.Load<GameObject>("Comic/"+comicName);
        }
    }
}
