using UnityEngine;
using System.Linq;

public class InitializeOnLoad
{
    [RuntimeInitializeOnLoadMethod]
    static void Initialize()
    {
        GraphLang.Initialize();
        Camera.main.GetComponent<CachingScript>().Initialize();
        OptionsSerialiazation.Initialize();
        Camera.main.GetComponent<AudioSource>().volume = GameData.Options.MusicVolume / 100;
#if UNITY_ANDROID
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
#endif

        if (GameData.Options.FirstStartGame == true)
        {
            GameData.Options.FirstStartGame = false;
            GameData.Options.Language = Application.systemLanguage.ToString();
            OptionsSerialiazation.Save();
        }

        LanguageInitialize();
    }

    public static void LanguageInitialize()
    {
        var lang = Resources.FindObjectsOfTypeAll(typeof(LanguageScript)).Cast<LanguageScript>();
        foreach (LanguageScript script in lang) script.Initialize();
    }
}