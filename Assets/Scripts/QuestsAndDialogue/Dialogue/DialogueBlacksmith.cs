using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBlacksmith : MonoBehaviour
{
    public GameObject InteractButton;
    public GameObject ClaimRewardButton;

    public QuestGiver questGiver;

    private void OnTriggerEnter(Collider other)
    {
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
        ClaimRewardButton.SetActive(false);
        InteractButton.SetActive(false);
    }
}
