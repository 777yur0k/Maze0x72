using UnityEngine;

public class ItemsSpawner : MonoBehaviour
{
    public Transform Key, Potion;
    public Transform[] Locations;

    void Start()
    {
        var KeyObj = Instantiate(Key, Locations[Random.Range(0, Locations.Length)]);
        if (Locations.Length > 1)
        {
            var randomPosID = Random.Range(0, Locations.Length);
            while (Locations[randomPosID].position == KeyObj.position)
                randomPosID = Random.Range(0, Locations.Length);
            Instantiate(Potion, Locations[randomPosID]);
        }
    }
}