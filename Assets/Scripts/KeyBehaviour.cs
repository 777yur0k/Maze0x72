using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    Transform[] locations;

    void Start()
    {
        locations = GetComponentsInChildren<Transform>();
        var index = Random.Range(0, locations.Length);
        transform.position = locations[index].position;  
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.GotKey();
            gameObject.SetActive(false);
        }
    }
}