using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public GameObject InteractButton;

    private void OnTriggerEnter(Collider other)
    {
        InteractButton.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        InteractButton.SetActive(false);
    }
}
