using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
  [SerializeField] Dialogue dialogue;

  public void TriggerDialogue()
  {
    FindFirstObjectByType<DialogueManager>().StartDialogue(dialogue);
  }

  public void CloseDialog () {
        FindFirstObjectByType<DialogueManager>().CloseDialog();
  }
}
