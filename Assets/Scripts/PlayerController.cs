using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    SpriteRenderer spriteRenderer;
    [SerializeField] float speed = 5f;
    Vector2 moveDelta;
    public GameObject attack, sword;
    [SerializeField] bool startFlipped;
    public GameObject Key;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() => CalculateMove();

    public void ShowKey() => Key.SetActive(true);

    void CalculateMove()
    {
        moveDelta.x = Input.GetAxisRaw("Horizontal");
        moveDelta.y = Input.GetAxisRaw("Vertical");

        if (moveDelta.x < 0) transform.localScale = new(1,1,1);
        else if (moveDelta.x > 0) transform.localScale = new(-1, 1, 1);
    }
    
    void FixedUpdate() => rbody.MovePosition(rbody.position + (moveDelta.normalized * speed * Time.fixedDeltaTime));
}