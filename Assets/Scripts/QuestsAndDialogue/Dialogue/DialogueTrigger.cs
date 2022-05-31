using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private GameObject quest1Background;
    [SerializeField] private GameObject quest2Background;
    [SerializeField] private GameObject shopUI;

    public QuestGiver questGiver;
    public ShopManager shopManager;
    public int DialogueID;
    public Dialogue bakerDialogue1;
    public Dialogue blacksmithDialogue1;


    [SerializeField] private GameObject rewardButton;
    [SerializeField] private GameObject voiceButton;

    //Work in progress
    //public Dialogue bakerDialogue2;
    //public Dialogue blacksmithDialogue2;

    //public Dialogue bakerDialogue3;
    //public Dialogue blacksmithDialogue3;

    private void Start()
    {
        DialogueID = 0;
    }

    //public void TriggerBakerDialogue()
    //{
    //    FindObjectOfType<DialogueManager>().StartDialogue(bakerDialogue1);
    //}

    //public void TriggerBlackSmithDialogue()
    //{
    //    FindObjectOfType<DialogueManager>().StartDialogue(blacksmithDialogue1);
    //}

    public void TriggerDialogue()
    {
        switch(DialogueID)
        {
            case 0:
                Debug.Log("No dialogue");
                break;
            case 1:
                GetComponent<DialogueManager>().StartDialogue(bakerDialogue1);
                quest1Background.SetActive(true);
                questGiver.NotificationWheatQuest();
                break;
            case 2:
                GetComponent<DialogueManager>().StartDialogue(blacksmithDialogue1);
                quest2Background.SetActive(true);
                questGiver.NotificationIronQuest();
                break;
            case 3:
                shopUI.SetActive(true);
                shopManager.Shopping();
                break;
            default:
                break;
        }
    }

    public void TriggerClaimReward()
    {
        Debug.Log(DialogueID);
        switch (DialogueID)
        {
            case 0:
                Debug.Log("No reward");
                break;
            case 1:
                questGiver.ClaimWheatReward();
                break;
            case 2:
                questGiver.ClaimIronReward();
                break;
            default:
                break;
        }
        rewardButton.SetActive(false);
        voiceButton.SetActive(false);
    }
    /*
    Button funtion:
    Claim reward
    Button inactive
    Area uninteractive
    Checkmark over quest UI
    */
}
