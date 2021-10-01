using UnityEngine;

public class DialogueArea : MonoBehaviour
{
    public GameObject ButtonToShow;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entering");
        ButtonToShow.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        ButtonToShow.SetActive(false);
    }
}
