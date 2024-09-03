using UnityEngine;

public class SignBehaviour : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) => GetComponent<DialogueTrigger>().TriggerDialogue();

    void OnTriggerExit2D(Collider2D other) => GetComponent<DialogueTrigger>().CloseDialog();
}