using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    Vector2 moveDelta;
    public Character Character = new();
    public Weapon weaponController;
    bool damaged;

    public void ProcessHit()
    {
        if (damaged) return;

        damaged = true;
        GetComponent<Animator>().SetTrigger("Blink");

        if (Character.Health > 0)
        {
            Character.Health -= 1;
            GameData.Manager.HeartsUpdate();
        }

        enabled = false;
        weaponController.enabled = false;

        if (Character.Health <= 0)
        {
            Camera.main.transform.SetParent(null);
            GetComponent<Animator>().SetTrigger("Die");
        }

        else Invoke(nameof(ChangeColliderState), 0.5f);
    }

    void ChangeColliderState()
    {
        enabled = true;
        weaponController.enabled = true;
        damaged = false;
    }

    public void DieAnimation()
    {
        GameData.Manager.SetPanel("EndGame");
        GameData.Manager.GetPanel("EndGame").GetComponent<EndGameController>().LoseGame();
        GameData.Manager.DestroyLevel();
        damaged = false;
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.CompareTag("Key"))
        {
            GameData.Manager.GotKey();
            trigger.gameObject.SetActive(false);
        }

        if (trigger.CompareTag("Potion"))
        {
            if (GameData.Character.Health < GameData.Character.MaxHealth)
            {
                GameData.Character.Health++;
                trigger.gameObject.SetActive(false);
                GameData.Manager.HeartsUpdate();
            }
        }
    }

    void CalculateMove()
    {
        moveDelta.x = Input.GetAxisRaw("Horizontal");
        moveDelta.y = Input.GetAxisRaw("Vertical");

        if (moveDelta.x < 0) transform.localScale = new(1,1,1);
        else if (moveDelta.x > 0) transform.localScale = new(-1, 1, 1);
    }

    void Update() => CalculateMove();

    void FixedUpdate() => GetComponent<Rigidbody2D>().MovePosition(GetComponent<Rigidbody2D>().position + (speed * Time.fixedDeltaTime * moveDelta.normalized));
}