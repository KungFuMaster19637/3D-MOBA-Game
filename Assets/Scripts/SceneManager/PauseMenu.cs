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
    [SerializeField] private GameObject settingsMenuUI;
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
            if (!settingsMenuUI.activeInHierarchy)
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
            else
            {
                SettingsBack();
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
    public void Settings()
    {
        settingsMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void SettingsBack()
    {
        settingsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void Save()
    {
        saveManager.SaveToXML();
    }
    public void Load()
    {
        StartCoroutine(FadeOutScreen());
        Resume();
    }


    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");

    }

    private IEnumerator FadeOutScreen()
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            fader.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        yield return new WaitForSeconds(0.5f);
        saveManager.LoadFromXML();
        agent.ResetPath();
        StartCoroutine(FadeInScreen());

    }
    private IEnumerator FadeInScreen()
    {
        float b = 1f;

        while (b > 0f)
        {
            b -= Time.deltaTime;
            float a = curve.Evaluate(b);
            fader.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }

}
