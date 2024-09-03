using System.Xml;
using UnityEngine;

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

[System.Serializable]
public class Dialogue
{
    [TextArea(3, 10)]
    public string[] sentences;
}