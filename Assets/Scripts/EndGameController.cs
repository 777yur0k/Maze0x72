using TMPro;
using UnityEngine;

public class EndGameController : MonoBehaviour
{
    public TMP_Text Text;

    public void LoseGame() => Text.text = GetComponent<LanguageScript>().Language[2];

    public void WinGame() => Text.text = GetComponent<LanguageScript>().Language[3];
}