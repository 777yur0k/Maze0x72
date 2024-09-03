using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    Dialogue dialogue = new();

    void Start() => dialogue.sentences = GetComponent<LanguageScript>().Language;

    public void TriggerDialogue() => FindFirstObjectByType<DialogueManager>().StartDialogue(dialogue);

    public void CloseDialog() => FindFirstObjectByType<DialogueManager>().CloseDialog();
}