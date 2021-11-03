using UnityEngine;

public class TeleportArea : MonoBehaviour
{
    public GameObject teleportButton;

    private void OnTriggerEnter(Collider other)
    {
        teleportButton.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        teleportButton.SetActive(false);
    }
}
