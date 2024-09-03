using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] Sprite openSprite;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && GameData.Character.Key)
        {
            GetComponent<SpriteRenderer>().sprite = openSprite;
            GameManager.instance.PlayerWin();
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), other.collider, true);
            Camera.main.GetComponent<SetPanels>().SetPanel("EndGame");
            Camera.main.GetComponent<SetPanels>().GetPanel("EndGame").GetComponent<EndGameController>().WinGame();
            Camera.main.GetComponent<SetPanels>().DestroyLevel();
        }
    }
}