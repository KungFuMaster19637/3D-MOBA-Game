using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue bakerDialogue1;
    public Dialogue blacksmithDialogue1;

    //Work in progress
    public Dialogue bakerDialogue2;
    public Dialogue blacksmithDialogue2;

    public Dialogue bakerDialogue3;
    public Dialogue blacksmithDialogue3;


    public void TriggerBakerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(bakerDialogue1);
    }

    public void TriggerBlackSmithDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(blacksmithDialogue1);
    }

    /*
    Button funtion:
    Claim reward
    Button inactive
    Area uninteractive
    Checkmark over quest UI
    */
}
