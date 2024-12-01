using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Debug:")]
    public bool EnableDebug;
    string ActivePanel = "MainMenu", OldPanel, CurrentLevelName;
    public GameObject Panel, Key, Player;
    public List<GameObject> Panels, Levels;
    int CurrentLevelID;
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
        Camera.main.transform.SetParent(player.transform);
        Camera.main.transform.localPosition = new(0,0,-1);
        GameData.Character = player.GetComponent<PlayerHealth>().Character;
    }

    public void GotKey()
    {
        GameData.Character.Key = true;
        Key.SetActive(true);
    }

    public void LoadLevel(string Name)
    {
        DestroyLevel();
        CurrentLevelID = FindLevelByName(Name);
        CurrentLevelName = Name;
        CurrentLevelObject = Instantiate(GetObject(Name, Levels));
        InitializeOnLoad.LanguageInitialize();
        PlayerSpawn();
    }

    public void DestroyLevel()
    {
        Camera.main.transform.SetParent(null);
        Key.SetActive(false);
        Destroy(CurrentLevelObject);
    }

    public void ReLoadLevel() => LoadLevel(CurrentLevelName);

    public void NextLevel()
    {
        if (CurrentLevelName == "Tutorial") LoadLevel("Level1");
        else if (CheckNextLevel()) LoadLevel("Level" + (CurrentLevelID + 1).ToString());
        else
        {
            SetPanel("EndGame");
            GetPanel("EndGame").GetComponent<EndGameController>().WinGame();
            DestroyLevel();
        }
    }

    bool CheckNextLevel()
    {
        foreach (var level in Levels)
            if (level.name == "Level" + (CurrentLevelID + 1).ToString())
                return true;
        return false;
    }

    int FindLevelByName(string Name)
    {
        for(var i = 0; i < Levels.Count; i++)
            if (Levels[i].name == Name) return i;

        return -1;
    }
}