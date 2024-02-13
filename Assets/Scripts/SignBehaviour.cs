using UnityEngine;

public class SignBehaviour : MonoBehaviour
{
    DialogueTrigger dialogueTrigger;

    void Start() => dialogueTrigger = GetComponent<DialogueTrigger>();

    void OnTriggerEnter2D(Collider2D other) => dialogueTrigger.TriggerDialogue();

    void OnTriggerExit2D(Collider2D other) => dialogueTrigger.CloseDialog();
}