using UnityEngine;
using System.Linq;

public class InitializeOnLoad
{
    [RuntimeInitializeOnLoadMethod]
    static async void Initialize()
    {
        Camera.main.GetComponent<OptionsSerialiazation>().Initialize();
#if UNITY_ANDROID
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
#endif

        if (GameData.Options.FirstStartGame == true)
        {
            GameData.Options.FirstStartGame = false;
            GameData.Options.Language = Application.systemLanguage.ToString();
            Camera.main.GetComponent<OptionsSerialiazation>().Save();
        }

        var lang = Resources.FindObjectsOfTypeAll(typeof(LanguageScript)).Cast<LanguageScript>();
        foreach (LanguageScript script in lang) script.Initialize();
    }
}