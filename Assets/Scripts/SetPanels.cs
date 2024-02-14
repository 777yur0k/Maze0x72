using System.Collections.Generic;
using UnityEngine;

public class SetPanels : MonoBehaviour
{
    [Header("Debug:")]
    public bool EnableDebug;
    string ActivePanel = "MainMenu", OldPanel;
    public GameObject Panel;
    public List<GameObject> Panels;

    public void SetPanel(string NewPanel)
    {
        Panel.SetActive(true);

        OldPanel = ActivePanel;
        ActivePanel = NewPanel;

        if (OldPanel != "")
            if (GetPanel(OldPanel).activeSelf)
            {
                GetPanel(OldPanel).SetActive(false);
                if (EnableDebug) Debug.Log("Closed Panel: " + OldPanel);
            }

        GetPanel(NewPanel).SetActive(true);
        if (EnableDebug) Debug.Log("Loaded Panel: " + NewPanel);
    }

    public void ChangeActivePanel(string NamePanel)
    {
        if (!GetPanel(NamePanel).activeSelf) SetPanel(NamePanel);

        else ClosePanel();
    }

    public GameObject GetPanel(string Name) => Panels[GetIDPanel(Name)];

    int GetIDPanel(string Name)
    {
        for (var i = 0; i < Panels.Count; i++) if (Name == Panels[i].name) return i;

        Debug.LogError("Error name of Panel");
        return -1;
    }

    public void BackPanel() => SetPanel(OldPanel);

    public void ClosePanel()
    {
        GetPanel(ActivePanel).SetActive(false);
        ActivePanel = "";
    }
}