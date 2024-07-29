using UnityEngine;

public class OpenLink : MonoBehaviour
{
    public void OpenURL(string URL) => Application.OpenURL(URL);
}