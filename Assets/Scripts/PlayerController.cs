using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    SpriteRenderer spriteRenderer;
    [SerializeField] float speed = 5f;
    Vector2 moveDelta;
    public GameObject attack, sword;
    Vector3 attackPosition = new(0.6f, 0.0f, 0.0f), attackFlippedPosition = new(-0.6f, 0.0f, 0.0f), swordPosition = new(0.3f, 0.1f, 0.0f), swordFlippedPosition = new(-0.3f, 0.1f, 0.0f);
    [SerializeField] bool startFlipped;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (startFlipped) setFlipped();
        else setUnflipped();
    }

    void Update()
    {
        calculateMove();
        verifyFlip();
    }

    void calculateMove()
    {
        moveDelta.x = Input.GetAxisRaw("Horizontal");
        moveDelta.y = Input.GetAxisRaw("Vertical");
    }

    void setFlipped()
    {
        spriteRenderer.flipX = true;
        attack.transform.localPosition = attackFlippedPosition;
        sword.transform.localPosition = swordFlippedPosition;
        sword.GetComponent<Weapon>().setFlipped(true);
    }

    void setUnflipped()
    {
        spriteRenderer.flipX = false;
        attack.transform.localPosition = attackPosition;
        sword.transform.localPosition = swordPosition;
        sword.GetComponent<Weapon>().setFlipped(false);
    }

    void verifyFlip()
    {
        if (moveDelta.x < 0) setFlipped();
        else if (moveDelta.x > 0) setUnflipped();
    }

    void FixedUpdate() => rbody.MovePosition(rbody.position + (moveDelta.normalized * speed * Time.fixedDeltaTime));
}