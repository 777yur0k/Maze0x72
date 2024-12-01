using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    public Transform[] KeyLocations;

    void Start() => transform.position = KeyLocations[Random.Range(0, KeyLocations.Length)].position;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameData.Manager.GotKey();
            gameObject.SetActive(false);
        }
    }
}