using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBlacksmith : MonoBehaviour
{
    [SerializeField] private GameObject InteractButton;
    [SerializeField] private GameObject ClaimRewardButton;
    [SerializeField] private GameObject VoiceButton;

    public DialogueTrigger dialogueTrigger;
    public QuestGiver questGiver;
    public int BlacksmithID;

    private void Start()
    {
        BlacksmithID = 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (questGiver.ironQuestCompleted) return;
        dialogueTrigger.DialogueID = BlacksmithID;
        VoiceButton.SetActive(false);
        if (questGiver.ironQuest.completed)
        {
            ClaimRewardButton.SetActive(true);
        }
        else
        {
            InteractButton.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        VoiceButton.SetActive(true);
        ClaimRewardButton.SetActive(false);
        InteractButton.SetActive(false);
    }
}
