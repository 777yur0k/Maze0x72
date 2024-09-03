using UnityEngine;
using UnityEngine.Events;

public class OptionsController : MonoBehaviour
{
    public DropDownScript ChangeLanguageDrop;
    UnityAction Action;

    async void OnEnable()
    {
        ChangeLanguageDrop.GenerateOptions(LangMassive.Manifest.Languages, GameData.Options.Language);
        ChangeLanguageDrop.UnityAction = SetLanguage;
        ChangeLanguageDrop.MainLable.text = GameData.Options.Language;
    }

    public void SetLanguage()
    {
        GameData.Options.Language = ChangeLanguageDrop.ItemsScripts[ChangeLanguageDrop.id].Label.text;
        ChangeLanguage.LanguageChanged();
    }
}