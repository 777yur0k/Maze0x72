using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

public class SerializationScript : MonoBehaviour
{
    public static object ClassDeser(XmlSerializer serializer, string path) => serializer.Deserialize(new FileStream(Application.streamingAssetsPath  + "/" + path, FileMode.Open, FileAccess.Read));

    public static object ClassDeser(XmlSerializer serializer, TextReader stream) => serializer.Deserialize(stream);

    public async static UniTask<TextReader> ClassDeser(string path)
    {
        TextReader stream = new StringReader(await GetWebRequest(path));
        return stream;
    }

    async public static UniTask<string> GetWebRequest(string path)
    {
        var op = await UnityWebRequest.Get(Application.streamingAssetsPath + "/" + path).SendWebRequest();
        return op.downloadHandler.text;
    }
}