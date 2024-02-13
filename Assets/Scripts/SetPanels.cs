using UnityEngine;

public class SetPanels : MonoBehaviour
{
    [Header("Debug:")]
    public bool EnableDebug;
    string ActivePanel = "MainMenu", OldPanel;
    public GameObject Panels;

    public void SetPanel(string NewPanel)
    {
        Panels.SetActive(true);

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

    int GetIDPanel(string Name)
    {
        for (var i = 0; i < Panels.transform.childCount; i++) if (Name == Panels.transform.GetChild(i).name) return i;
        Debug.LogError("Error name of Panel");
        return 0;
    }

    public GameObject GetPanel(string Name) => Panels.transform.GetChild(GetIDPanel(Name)).gameObject;

    public void BackPanel() => SetPanel(OldPanel);

    public void ClosePanel()
    {
        GetPanel(ActivePanel).SetActive(false);
        ActivePanel = "";
    }
}