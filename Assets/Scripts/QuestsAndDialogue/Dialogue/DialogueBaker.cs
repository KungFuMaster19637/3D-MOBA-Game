using UnityEngine;

public class DialogueBaker : MonoBehaviour
{
    [SerializeField] private GameObject InteractButton;
    [SerializeField] private GameObject ClaimRewardButton;
    [SerializeField] private GameObject VoiceButton;

    public DialogueTrigger dialogueTrigger;
    public QuestGiver questGiver;
    public int BakerID;

    private void Start()
    {
        BakerID = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (questGiver.wheatQuestCompleted) return;
        dialogueTrigger.DialogueID = BakerID;
        VoiceButton.SetActive(false);
        if (questGiver.wheatQuest.completed)
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
