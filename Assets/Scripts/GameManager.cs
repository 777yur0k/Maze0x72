using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public bool hasKey { get; private set; } = false;
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
        hasKey = true;
        PlayerController.ShowKey();
    }

    public void PlayerWin() => hasWon = true;
}