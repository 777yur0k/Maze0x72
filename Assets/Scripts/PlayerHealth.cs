using UnityEngine;
using MyLibrary;

public class PlayerHealth : MonoBehaviour
{
    Character Character = new();
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
        GetComponent<Animator>().SetBool("Blink", true);

        if (Character.Health > 0)
        { 
            Character.Health -= 1;
            HeartsUpdate();
        }

        controller.enabled = false;
        weaponController.enabled = false;

        if (Character.Health <= 0) GetComponent<Animator>().SetBool("Die", true);

        else Invoke(nameof(ChaneColliderState), 0.5f);
    }

    void ChaneColliderState()
    {
        controller.enabled = true;
        weaponController.enabled = true;
        damaged = false;
    }

    public void DieAnimation()
    {
        Camera.main.GetComponent<SetPanels>().SetPanel("Lose");
        damaged = false;
    }
}