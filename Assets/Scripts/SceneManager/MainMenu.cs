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

    //Main menu screen
    public void StartGame()
    {
        //sceneFader.FadeTo(characterSelect);
        mainMenu.SetActive(false);
        characterSelectMenu.SetActive(true);
    }

    public void Tutorial()
    {
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
        sceneFader.FadeTo(arthur);
    }

    public void SelectMerlin()
    {
        audioSelect.PlayOneShot(merlinSelect);
        sceneFader.FadeTo(merlin);
    }

}
