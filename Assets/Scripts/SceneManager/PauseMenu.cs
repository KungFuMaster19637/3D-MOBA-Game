using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    private SaveManager saveManager;
    private NavMeshAgent agent;

    [Header("Screen fader")]
    public Image fader;
    public AnimationCurve curve;

    private void Start()
    {
        saveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                if (Teleporter.isTeleporting || Teleporter2.isTeleporting)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Options()
    {
        //To be made
    }

    public void Save()
    {
        saveManager.SaveToXML();
    }
    public void Load()
    {
        saveManager.LoadFromXML();
        agent.ResetPath();
        Resume();

    }


    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");

    }
}
