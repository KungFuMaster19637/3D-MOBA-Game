using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [Header("Dialogue input")]
    public Queue<string> sentences;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public Tutorial[] tutorials;
    private int tutorialCounter;

    [Header("Tutorial Checker")]
    [Header("Tutorial 1")]
    public Transform tutorial1Location;


    #region Singleton
    public static TutorialManager Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }
    #endregion

    private void Start()
    {
        sentences = new Queue<string>();
        tutorialCounter = 0;
        StartTutorial(tutorials[tutorialCounter]);
    }

    #region Tutorial Display
    public void StartTutorial(Tutorial tutorial)
    {

        nameText.text = tutorial.tutorialName;

        sentences.Clear();

        foreach (string sentence in tutorial.tutorialDescriptions)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            if (tutorialCounter == 0)
            {
                tutorialCounter++;
                StartTutorial(tutorials[tutorialCounter]);
            }
            else if (tutorials[tutorialCounter].tutorialFinished)
            {
                EndCurrentTutorial();
            }
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    public void EndCurrentTutorial()
    {
        tutorialCounter++;
        if (tutorialCounter <= tutorials.Length)
        {
            StartTutorial(tutorials[tutorialCounter]);
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        descriptionText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            descriptionText.text += letter;
            yield return null;
        }
    }
    #endregion

    #region Tutorial Checks
    public void Tutorial1()
    {
        if (tutorialCounter == 1 && !tutorials[1].tutorialFinished)
        {
            StartCoroutine(TypeSentence("Good job!"));
            tutorials[1].tutorialFinished = true;
        }
    }
    public void Tutorial2()
    {
        if (tutorialCounter == 2 && !tutorials[2].tutorialFinished)
        {
            StartCoroutine(TypeSentence("Good job!"));
            tutorials[2].tutorialFinished = true;
        }
    }
    public void Tutorial3()
    {
        if (tutorialCounter == 3 && !tutorials[3].tutorialFinished)
        {
            StartCoroutine(TypeSentence("Good job!"));
            tutorials[3].tutorialFinished = true;
        }
    }


    #endregion


}
