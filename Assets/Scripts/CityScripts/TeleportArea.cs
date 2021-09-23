using UnityEngine;

public class TeleportArea : MonoBehaviour
{
    public GameObject ButtonToShow;

    private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("entered");
        ButtonToShow.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        ButtonToShow.SetActive(false);
    }
}
