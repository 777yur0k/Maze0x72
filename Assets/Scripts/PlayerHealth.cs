using UnityEngine;
using MyLibrary;

public class PlayerHealth : MonoBehaviour
{
    Character Character = new();
    public PlayerController controller;
    public Weapon weaponController;
    public GameObject[] Hearts;

    void HeartsUpdate()
    {
        for (var i = 0; i < Character.MaxHealth - Character.Health; i++) Hearts[i].SetActive(false);
    }

    public void processHit()
    {
        GetComponent<BlinkBehaviour>().Blink(0.125f);

        if (Character.Health > 0)
        { 
            Character.Health -= 1;
            HeartsUpdate();
        }

        ChaneColliderState();

        if (Character.Health <= 0)
        {
            GetComponent<SpriteRenderer>().sortingOrder = -1; // Para que os inimigos "passem por cima"
            weaponController.GetComponentInChildren<SpriteRenderer>().sortingOrder = -1; // Para que os inimigos "passem por cima"
            GetComponent<Animator>().SetBool("Die", true);
        }

        else Invoke(nameof(ChaneColliderState), 0.25f);
    }

    void ChaneColliderState()
    {
        controller.enabled = !controller.enabled;
        weaponController.enabled = !weaponController.enabled;
    }

    public void DieAnimation() => Camera.main.GetComponent<SetPanels>().SetPanel("Lose");
}