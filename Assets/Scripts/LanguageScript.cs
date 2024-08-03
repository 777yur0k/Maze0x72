using UnityEngine;
using System.IO;
using TMPro;

public class LanguageScript : MonoBehaviour
{
    public class Serializable
    {
        public string[] Language = { };
    }

    public TMP_Text[] Texts;
    public Serializable Ser = new();

    public void Initialize()
    {
        LoadingLanguage();
        ChangeLanguage.ChangeLanguageActions.Add(LoadingLanguage);
    }

    public async void LoadingLanguage()
    {
        var path = "";

        if (File.Exists(Application.streamingAssetsPath  + "/" + "Languages/" + GameData.Options.Language + "/" + name + ".xml")) path = "Languages/" + GameData.Options.Language + "/" + name + ".xml";

        else path = "Languages/English/" + name + ".xml";
#if UNITY_STANDALONE
        Ser = (Serializable)SerializationScript.ClassDeser(new(typeof(Serializable)), path);
#else
        Ser = (Serializable)SerializationScript.ClassDeser(new(typeof(Serializable)), await SerializationScript.ClassDeser(path));
#endif
        UpdateLanguage();
    }

    public void UpdateLanguage()
    {
        if (Texts.Length == 0) return;

        for (var i = 0; i < Texts.Length; i++)
            try { Texts[i].text = Ser.Language[i]; }

            catch { Debug.LogError("Error setting UI string: " + name); }
    }
}