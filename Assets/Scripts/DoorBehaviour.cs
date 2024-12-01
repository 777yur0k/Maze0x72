using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public Sprite openSprite;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && GameData.Character.Key)
        {
            GetComponent<SpriteRenderer>().sprite = openSprite;
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), other.collider, true);
            Invoke(nameof(EndGame), 0.5f);
        }
    }

    void EndGame() => GameData.Manager.NextLevel();
}