using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [SerializeField] private GameObject InteractButton;
    [SerializeField] private GameObject VoiceButton;

    public DialogueTrigger dialogueTrigger;
    public int ShopID;

    private void Start()
    {
        ShopID = 3;
    }

    private void OnTriggerEnter(Collider other)
    {
        dialogueTrigger.DialogueID = ShopID;
        InteractButton.SetActive(true);
        VoiceButton.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        InteractButton.SetActive(false);
        VoiceButton.SetActive(true);
    }
}
