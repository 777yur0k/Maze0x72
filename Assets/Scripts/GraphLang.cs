using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;

public static class GraphLang 
{
    public static List<string> Languages;
    public static GraphCompilation Graph = new();
    public static List<UnityAction> ChangeLanguageActions = new();

    public static void LanguageChanged()
    {
        for (var i = 0; i < ChangeLanguageActions.Count; i++) ChangeLanguageActions[i].Invoke();
    }

    public static void Initialize()
    {
            try
            {
#if UNITY_STANDALONE
                if (!File.Exists(Application.streamingAssetsPath + "/Graph.xml"))
                {
                    GraphGeneration.Initialize();
                    Graph = GraphGeneration.graph;
                }
                else
                    Graph = (GraphCompilation)SerializationScript.ClassDeser(new(typeof(GraphCompilation)), "Graph.xml");
#endif
            }

            catch
            {
                TextReader stream = new StringReader(Camera.main.GetComponent<CachingScript>().GraphCache.text);
                XmlSerializer serializer = new(typeof(GraphCompilation));
                Graph = (GraphCompilation)serializer.Deserialize(stream);
            }
        Languages = Graph.GetManifest();
    }

    public static string[] GetLanguage(string name) => Graph.GetGraph(GameData.Options.Language).GetText(name);
}
