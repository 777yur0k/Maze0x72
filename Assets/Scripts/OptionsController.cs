using UnityEngine;

public class OptionsController : MonoBehaviour
{
    public DropDownScript ChangeLanguageDrop;

    void OnEnable()
    {
        ChangeLanguageDrop.Initialize(GraphLang.Languages, GameData.Options.Language, SetLanguage);
    }

    public void SetLanguage(string NewLanguage)
    {
        GameData.Options.Language = NewLanguage;
        GraphLang.LanguageChanged();
    }
}