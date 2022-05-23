using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1Trigger : MonoBehaviour
{
    [SerializeField] private GameObject markedArea;
    private void OnTriggerEnter(Collider other)
    {
        TutorialManager.Instance.Tutorial1();
    }

    [ContextMenu("Skip Tutorial")]
    public void SkipTutorial1()
    {
        TutorialManager.Instance.Tutorial1();
    }
}

