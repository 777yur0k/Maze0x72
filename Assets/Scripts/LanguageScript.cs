using UnityEngine;
using TMPro;

public class LanguageScript : MonoBehaviour
{
    public TMP_Text[] Texts;
    public string[] Language = { };

    public async void LoadLanguage() => UpdateLanguage(await LangMassive.LoadingLanguage(name));

    public async void Initialize()
    {
        LoadLanguage();
        ChangeLanguage.ChangeLanguageActions.Add(LoadLanguage);
    }

    public void UpdateLanguage(string[] lang)
    {
        Language = lang;
        if (Texts.Length == 0) return;

        for (var i = 0; i < Texts.Length; i++)
            try { Texts[i].text = lang[i]; }

            catch { Debug.LogError("Error setting UI string: " + name); }
    }
}