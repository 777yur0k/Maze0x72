using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    Vector2 moveDelta;

    void Update() => CalculateMove();

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
                GetComponent<PlayerHealth>().HeartsUpdate();
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
    
    void FixedUpdate() => GetComponent<Rigidbody2D>().MovePosition(GetComponent<Rigidbody2D>().position + (speed * Time.fixedDeltaTime * moveDelta.normalized));
}