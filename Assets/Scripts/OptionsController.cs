using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public DropDownScript ChangeLanguageDrop;
    public Slider SliderMusicVolume;

    void OnEnable()
    {
        ChangeLanguageDrop.Initialize(GraphLang.Languages, GameData.Options.Language, SetLanguage);
        SliderMusicVolume.value = GameData.Options.MusicVolume;
    }

    public void SetLanguage(string NewLanguage)
    {
        GameData.Options.Language = NewLanguage;
        GraphLang.LanguageChanged();
    }

    public void SetMusicVolume()
    {
        GameData.Options.MusicVolume = SliderMusicVolume.value;
        Camera.main.GetComponent<AudioSource>().volume = GameData.Options.MusicVolume / 100;
    }

    public void SaveOptions() => OptionsSerialiazation.Save();
}