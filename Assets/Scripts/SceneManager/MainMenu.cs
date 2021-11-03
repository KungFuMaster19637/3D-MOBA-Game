using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string merlin = "Merlin";
    private string arthur = "Arthur";
    private string tutorial = "Tutorial";
    

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
        sceneFader.FadeTo(tutorial);
    }

    public void Options()
    {

    }

    public void QuitGame()
    {
        
    }



    //Character Select
    public void Back()
    {
        mainMenu.SetActive(true);
        characterSelectMenu.SetActive(false);
    }

    public void SelectArthur()
    {
        sceneFader.FadeTo(arthur);
    }

    public void SelectMerlin()
    {
        sceneFader.FadeTo(merlin);
    }

}
