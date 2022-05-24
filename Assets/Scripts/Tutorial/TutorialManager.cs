using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [Header("Dialogue input")]
    public Queue<string> sentences;
    public Button continueButton;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public Tutorial[] tutorials;
    public int tutorialCounter;

    [Header("Tutorial 1")]
    public GameObject markedArea;
    private bool finalSentence1;

    [Header("Tutorial 2")]
    private bool finalSentence2;

    [Header("Tutorial3")]
    [SerializeField] private Tutorial3Trigger tutorial3Trigger; 
    private bool finalSentence3;

    [Header("Tutorial4")]
    [SerializeField] private Tutorial4Trigger tutorial4Trigger;
    private bool finalSentence4;

    [SerializeField] private GameObject tutorialExit;
    [SerializeField] private SceneFader sceneFader;
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


    #region Tutorial Text
    /*
    Intro:
    Welcome to Camelot!
    This is the tutorial to teach you how to play the game! First we are going to start with the movement of the player. 

    Movement:
    Right-click anywhere to walk. 
    Right-clicking anywhere else again will make that the new destination. 
    Go to the marked area.

    Abilities:
    The player has access to 4 abilities and 1 passive. These are essential for fighting enemies that you encounter! 
    By hovering over the abilities, the player can read their effects. The passive is like it says, a passive, that can't be triggered by pressing keys. 
    Pressing A-Z-E-R (or Q-W-E-R), depending on your keyboard layout will trigger the abilities. Sometimes the player has to use their left-click to fully activate their ability. 
    Try out all the abilities one by one!

    Items:


    Fighting:
    Aside from abilities, the player has also access to normal attacks, which can be used when right-clicking an enemy. 
    Enemies will attack if the player enters their aggresion zone. By running out of this zone the player can avoid fighting them. 
    Try to defeat all the enemies!

    Completed:
    Congratulations, you have finished the tutorial! 
    Go to the teleport area to exit the tutorial.

    */
    #endregion

    private void Start()
    {
        sentences = new Queue<string>();

        finalSentence1 = false;
        finalSentence2 = false;
        finalSentence3 = false;
        finalSentence4 = false;

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
        if (sentences.Count == 1 && tutorialCounter != 0)
        {
            if (tutorialCounter == 3)
            {
                tutorial3Trigger.SpawnItems();
            }

            if (tutorialCounter == 4)
            {
                tutorial4Trigger.SpawnEnemies();
            }
            if (tutorialCounter == 5)
            {
                tutorialExit.SetActive(true);
            }

            continueButton.interactable = false;
            CheckLastSentence(tutorialCounter);
        }
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

    //We need this method to make tutorial past final part
    private void CheckLastSentence(int counter)
    {
        switch (counter)
        {
            case 0:
                break;
            case 1: finalSentence1 = true;
                break;
            case 2: finalSentence2 = true;
                break;
            case 3: finalSentence3 = true;
                break;
            case 4: finalSentence4 = true;
                break;
        }
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
        if (tutorialCounter == 1 && !tutorials[1].tutorialFinished && finalSentence1)
        {
            StartCoroutine(TypeSentence("Good job!"));
            markedArea.SetActive(false);
            tutorials[1].tutorialFinished = true;
            continueButton.interactable = true ;
        }
    }
    public void Tutorial2()
    {
        if (tutorialCounter == 2 && !tutorials[2].tutorialFinished && finalSentence2)
        {
            StartCoroutine(TypeSentence("Good job! And we restored most of your mana back!"));

            //Give mana back
            PlayerStats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
            playerStats.mana = playerStats.maxMana;

            tutorials[2].tutorialFinished = true;
            continueButton.interactable = true;
        }
    }
    public void Tutorial3()
    {
        if (tutorialCounter == 3 && !tutorials[3].tutorialFinished && finalSentence3)
        {
            StartCoroutine(TypeSentence("Good job!"));
            tutorials[3].tutorialFinished = true;
            continueButton.interactable = true;
        }
    }

    public void Tutorial4()
    {
        if (tutorialCounter == 4 && !tutorials[4].tutorialFinished && finalSentence4)
        {
            StartCoroutine(TypeSentence("Good job!"));
            tutorials[4].tutorialFinished = true;
            continueButton.interactable = true;
        }
    }
    #endregion

    public void ExitTutorial()
    {
        sceneFader.FadeTo("MainMenu");
    }

}
