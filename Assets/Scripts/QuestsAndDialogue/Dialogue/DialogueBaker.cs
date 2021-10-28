using UnityEngine;

public class DialogueBaker : MonoBehaviour
{
    public GameObject InteractButton;
    public GameObject ClaimRewardButton;

    public QuestGiver questGiver;

    private void OnTriggerEnter(Collider other)
    {
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
        ClaimRewardButton.SetActive(false);
        InteractButton.SetActive(false);
    }
}
