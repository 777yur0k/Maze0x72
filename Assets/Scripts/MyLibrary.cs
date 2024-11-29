using System.Collections.Generic;
using System.IO;
using System;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using System.Linq;

public class MyObject
{
    static XmlWriterSettings settings = new();
    public static XmlWriterSettings Settings
    {
        get
        {
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            settings.NewLineOnAttributes = true;

            return settings;
        }

        set => settings = value;
    }

    public static void ChangeActive(GameObject obj) => obj.SetActive(!obj.activeSelf);

    public static void ChangeActive(GameObject[] Mobj)
    {
        for (var i = 0; i < Mobj.Length; i++) Mobj[i].SetActive(!Mobj[i].activeSelf);
    }
}

public class Character
{
    public int Health = 3, MaxHealth = 3;
    public bool Key;
}

public class Options
{
    public string Language = "English";
    public bool FirstStartGame = true;
}

[Serializable]
public class Dialogue
{
    [TextArea(3, 10)]
    public string[] sentences;
}

public class Graph
{
    public class Serializable
    {
        public UIStrings[] Strings;
    }

    public class UIStrings
    {
        public string Name;
        public string[] Language;
    }

    public string Language;
    public Serializable SerUIStrings = new();

    public void Initialize(string language)
    {
        Language = language;
        SerUIStrings = (Serializable)SerializationScript.ClassDeser(new(typeof(Serializable)), "Languages/" + Language + "/UIStrings.xml");
    }

    public string[] GetText(string Name)
    {
        foreach (UIStrings text in SerUIStrings.Strings)
            if (text.Name == Name) return text.Language;
        Debug.LogException(new Exception());
        return null;
    }
}

public class GraphCompilation
{
    public List<Graph> GraphsLanguages = new();

    public void Initialize(string[] Languages)
    {
        for (int i = 0; i < Languages.Length; i++)
        {
            GraphsLanguages.Add(new Graph());
            GraphsLanguages[i].Initialize(Path.GetFileName(Languages[i]));
        }
    }

    public Graph GetGraph(string Language)
    {
        foreach (Graph graph in GraphsLanguages)
            if (graph.Language == Language) return graph;
        Debug.LogException(new Exception());
        return null;
    }

    public List<string> GetManifest()
    {
        var list = new List<string>();

        foreach (Graph graph in GraphsLanguages)
            list.Add(graph.Language);

        return list;
    }
}

public static class GraphGeneration
{
    public static GraphCompilation graph = new();
    public static string ProjectPath = Application.streamingAssetsPath;

    public static void Initialize()
    {
        graph.Initialize(Directory.GetDirectories(ProjectPath + "\\Languages\\"));
        Serialization(ProjectPath + "/Graph.xml");
#if UNITY_EDITOR
        Serialization(Directory.GetCurrentDirectory() + "/Assets/Graph.xml");
#endif
    }

    static public string GeneratePath(List<int> list, string Language, int CharacterID)
    {
        var path = "Languages/" + Language + "/Characters/" + CharacterID + "/";
        for (var i = 0; i < list.Count; i++) path += list[i] + "/";
        return path;
    }
    static void Serialization(string path)
    {
        using (FileStream stream = new(path, FileMode.Create, FileAccess.Write))
        {
            new XmlSerializer(typeof(GraphCompilation)).Serialize(XmlWriter.Create(stream, MyObject.Settings), graph);
        }
    }
}