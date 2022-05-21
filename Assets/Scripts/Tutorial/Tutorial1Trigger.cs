using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1Trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        TutorialManager.Instance.Tutorial1();
    }
    private void OnTriggerExit(Collider other)
    {

    }
}

