using System.Collections.Generic;
using UnityEngine;

public class SetPanels : MonoBehaviour
{
    [Header("Debug:")]
    public bool EnableDebug;
    string ActivePanel = "MainMenu", OldPanel;
    public GameObject Panel;
    public List<GameObject> Panels;
    public List<GameObject> Levels;
    public GameObject Player;
    string CurrentLevel;
    GameObject CurrentLevelObject;


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

    public GameObject GetPanel(string Name) => GetObject(Name, Panels);

    public GameObject GetObject(string Name, List<GameObject> list) => list[GetID(Name, list)];

    int GetID(string Name, List<GameObject> list)
    {
        for (var i = 0; i < list.Count; i++) if (Name == list[i].name) return i;

        Debug.LogError("Error name of Panel");
        return -1;
    }

    public void BackPanel() => SetPanel(OldPanel);

    public void ClosePanel()
    {
        GetPanel(ActivePanel).SetActive(false);
        ActivePanel = "";
    }

    public void PlayerSpawn()
    {
        var player = Instantiate(Player, CurrentLevelObject.transform.Find("PlayerSpawn"));
        GameData.Character = player.GetComponent<PlayerHealth>().Character;
        Camera.main.GetComponent<CameraMotor>().lookAt = player.transform;
        Camera.main.GetComponent<GameManager>().PlayerController = player.GetComponent<PlayerController>();
    }

    public void LoadLevel(string Name)
    {
        CurrentLevel = Name;
        CurrentLevelObject = Instantiate(GetObject(Name, Levels));
        PlayerSpawn();
    }

    public void DestroyLevel() => Destroy(CurrentLevelObject);

    public void ReLoadLevel() => LoadLevel(CurrentLevel);
}