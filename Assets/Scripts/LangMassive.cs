using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;

public static class LangMassive 
{
    public class Serializable
    {
        public string[] Language = { };
    }

    public class LanguagesManifest
    {
        public string[] Languages;
    }

    public static LanguagesManifest Manifest = new();
    public static Dictionary<string, Dictionary<string, string[]>> Languages = new();

    public static void Initialize()
    {
#if UNITY_STANDALONE
        Manifest = (LanguagesManifest)SerializationScript.ClassDeser(new(typeof(LanguagesManifest)), "LanguagesManifest.xml");
#else
        Manifest = (LanguagesManifest)SerializationScript.ClassDeser(new(typeof(LanguagesManifest)), await SerializationScript.ClassDeser("LanguagesManifest.xml"));
#endif

        for (var i = 0; i < Manifest.Languages.Length; i++) Languages.Add(Manifest.Languages[i] , new Dictionary<string, string[]>());
    }

    public static async Task<string[]> LoadingLanguage(string name)
    {
        if (Languages[GameData.Options.Language].ContainsKey(name)) return Languages[GameData.Options.Language][name];

        Serializable Ser = new();

        var path = "Languages/" + GameData.Options.Language + "/" + name + ".xml";

        if (!File.Exists(Application.streamingAssetsPath + "/" + path)) path = "Languages/English/" + name + ".xml";

#if UNITY_STANDALONE
        Ser = (Serializable)SerializationScript.ClassDeser(new(typeof(Serializable)), path);
#else
        Ser = (Serializable)SerializationScript.ClassDeser(new(typeof(Serializable)), await SerializationScript.ClassDeser(path));
#endif
        Languages[GameData.Options.Language].Add(name, Ser.Language);
        return Ser.Language;
    }
}
