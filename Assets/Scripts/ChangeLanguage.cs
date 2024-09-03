using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class ChangeLanguage : MonoBehaviour
{
    public static List<UnityAction> ChangeLanguageActions = new();

    public static void LanguageChanged()
    {
        InitializeOnLoad.LanguageInitialize();
        for (var i = 0; i < ChangeLanguageActions.Count; i++) ChangeLanguageActions[i].Invoke();
    }
}