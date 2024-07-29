using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public static bool hasWon { get; private set; } = false;
    public PlayerController PlayerController;

    void Awake()
    {
        if (instance != null && instance != this) 
        { 
            Destroy(this);
            return;
        } 
   
        instance = this;
    }

    public void GotKey()
    {
        GameData.Character.Key = true;
        PlayerController.ShowKey();
    }

    public void PlayerWin() => hasWon = true;
}