using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    private string merlin = "Merlin";
    private string arthur = "Arthur";
    private string arthurTutorial = "ArthurTutorial";
    private string merlinTutorial = "MerlinTutorial";

    [SerializeField] private AudioClip arthurSelect;
    [SerializeField] private AudioClip merlinSelect;
    [SerializeField] private AudioSource audioSelect;

    public GameObject mainMenu;
    public GameObject characterSelectMenu;
    public SceneFader sceneFader;

    private int sceneIndex;

    //Main menu screen

    private void Start()
    {
        sceneIndex = 0;  
    }
    public void StartGame()
    {
        //sceneFader.FadeTo(characterSelect);
        mainMenu.SetActive(false);
        characterSelectMenu.SetActive(true);
        sceneIndex = 0;
    }

    public void Tutorial()
    {
        mainMenu.SetActive(false);
        characterSelectMenu.SetActive(true);
        sceneIndex = 1;
        //sceneFader.FadeTo(tutorial);
    }

    public void Options()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }



    //Character Select
    public void Back()
    {
        mainMenu.SetActive(true);
        characterSelectMenu.SetActive(false);
    }

    public void SelectArthur()
    {
        audioSelect.PlayOneShot(arthurSelect);
        switch (sceneIndex)
        {
            case 0: sceneFader.FadeTo(arthur);
                break;
            case 1: sceneFader.FadeTo(arthurTutorial);
                break;
            case 2: 
                break;
            default: 
                break;

        }
    }

    public void SelectMerlin()
    {
        audioSelect.PlayOneShot(merlinSelect);
        switch (sceneIndex)
        {
            case 0: sceneFader.FadeTo(merlin);
                break;
            case 1: sceneFader.FadeTo(merlinTutorial);
                break;
            case 2:
                break;
            default:
                break;

        }
    }

}
