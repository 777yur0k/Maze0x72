using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Character Character = new();
    public PlayerController controller;
    public Weapon weaponController;
    public GameObject[] Hearts;
    bool damaged;

    void HeartsUpdate()
    {
        for (var i = 0; i < Hearts.Length; i++) Hearts[i].SetActive(false);
        for (var i = 0; i < Character.Health; i++) Hearts[i].SetActive(true);
    }

    public void ProcessHit()
    {
        if (damaged) return;

        damaged = true;
        GetComponent<Animator>().SetTrigger("Blink");

        if (Character.Health > 0)
        { 
            Character.Health -= 1;
            HeartsUpdate();
        }

        controller.enabled = false;
        weaponController.enabled = false;

        if (Character.Health <= 0) GetComponent<Animator>().SetTrigger("Die");

        else Invoke(nameof(ChangeColliderState), 0.5f);
    }

    void ChangeColliderState()
    {
        controller.enabled = true;
        weaponController.enabled = true;
        damaged = false;
    }

    public void DieAnimation()
    {
        Camera.main.GetComponent<SetPanels>().SetPanel("EndGame");
        Camera.main.GetComponent<SetPanels>().GetPanel("EndGame").GetComponent<EndGameController>().LoseGame();
        Camera.main.GetComponent<SetPanels>().DestroyLevel();
        damaged = false;
    }
}