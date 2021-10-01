using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue bakerDialogue;
    public Dialogue blacksmithDialogue;


    public void TriggerBakerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(bakerDialogue);
    }

    public void TriggerBlackSmithDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(blacksmithDialogue);

    }
}
