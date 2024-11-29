using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class SerializationScript : MonoBehaviour
{
    public static object ClassDeser(XmlSerializer serializer, string path) => serializer.Deserialize(new FileStream(Application.streamingAssetsPath  + "/" + path, FileMode.Open, FileAccess.Read));

    public static object ClassDeser(XmlSerializer serializer, TextReader stream) => serializer.Deserialize(stream);
}