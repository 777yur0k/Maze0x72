using UnityEngine;

public class CachingScript : MonoBehaviour
{
    public TextAsset GraphCache;

    public void Initialize() => GameData.Manager = GetComponent<GameManager>();
}
