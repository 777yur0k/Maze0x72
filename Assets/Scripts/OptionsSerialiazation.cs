using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using MyLibrary;
using System;

public class OptionsSerialiazation : MonoBehaviour
{
    public class Serializable
    {
        public Options Options;
    }

    Serializable Ser = new();
    string DocumentsPath;

    public void Initialize()
    {
        PlayerPrefs.DeleteAll();
        DocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Maze0x72/";
        Load();
    }

    public void Load()
    {
#if UNITY_STANDALONE
        if (File.Exists(DocumentsPath + "Options.xml"))
        {
            Ser = (Serializable)new XmlSerializer(typeof(Serializable)).Deserialize(new FileStream(DocumentsPath + "Options.xml", FileMode.Open, FileAccess.Read));
            SyncData(false);
        }
#elif UNITY_ANDROID
        if (File.Exists(Path.Combine(Application.persistentDataPath, "Options.xml")))
        {
            Ser = (Serializable)new XmlSerializer(typeof(Serializable)).Deserialize(new FileStream(Path.Combine(Application.persistentDataPath, "Options.xml"), FileMode.Open, FileAccess.Read));
            SyncData(false);
        }
#elif UNITY_WEBGL
        if (PlayerPrefs.HasKey("Options"))
        {
            using (TextReader stream = new StringReader(PlayerPrefs.GetString("Options")))
            {
                Ser = (Serializable)new XmlSerializer(typeof(Serializable)).Deserialize(stream);
            };
            SyncData(false);
        }
#endif
    }

    public void Save()
    {
        SyncData(true);
#if UNITY_STANDALONE
        if (!Directory.Exists(DocumentsPath)) Directory.CreateDirectory(DocumentsPath);

        using (FileStream stream = new(DocumentsPath + "Options.xml", FileMode.Create, FileAccess.Write))
        {
            new XmlSerializer(typeof(Serializable)).Serialize(XmlWriter.Create(stream, MyObject.Settings), Ser);
        }
#elif UNITY_ANDROID
        using (FileStream stream = new(Path.Combine(Application.persistentDataPath, "Options.xml"), FileMode.Create, FileAccess.Write))
        {
            new XmlSerializer(typeof(Serializable)).Serialize(XmlWriter.Create(stream, MyObject.Settings), Ser);
        }
#elif UNITY_WEBGL
        using (StringWriter writer = new())
        {
            new XmlSerializer(typeof(Serializable)).Serialize(writer, Ser);
            PlayerPrefs.SetString("Options", writer.ToString());
        }
        PlayerPrefs.Save();
#endif
    }

    void SyncData(bool command)
    {
        if (command) Ser.Options = GameData.Options;

        else GameData.Options = Ser.Options;
    }
}