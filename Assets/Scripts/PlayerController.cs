using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    Vector2 moveDelta;

    void Update() => CalculateMove();

    void CalculateMove()
    {
        moveDelta.x = Input.GetAxisRaw("Horizontal");
        moveDelta.y = Input.GetAxisRaw("Vertical");

        if (moveDelta.x < 0) transform.localScale = new(1,1,1);
        else if (moveDelta.x > 0) transform.localScale = new(-1, 1, 1);
    }
    
    void FixedUpdate() => GetComponent<Rigidbody2D>().MovePosition(GetComponent<Rigidbody2D>().position + (speed * Time.fixedDeltaTime * moveDelta.normalized));
}