using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialExit : MonoBehaviour
{
    [SerializeField] private GameObject tutorialExitButton;
    private void OnTriggerEnter(Collider other)
    {
        tutorialExitButton.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        tutorialExitButton.SetActive(false);
    }
}
