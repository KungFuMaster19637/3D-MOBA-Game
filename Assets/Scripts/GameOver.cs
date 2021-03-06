using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public GameObject gameOverUI;
    public Image fader;
    public AnimationCurve curve;
    [SerializeField] private AudioClip playerDeath;
    [SerializeField] private AudioSource deathPlayer;

    public bool gameIsOver;

    private void Start()
    {
        gameIsOver = false;
    }
    public IEnumerator PlayerDied()
    {
        if (!gameObject.GetComponent<AudioListener>())
        {
            gameObject.AddComponent<AudioListener>();
        }
        deathPlayer.PlayOneShot(playerDeath);
        gameIsOver = true;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            fader.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
        gameOverUI.SetActive(true);
        yield return null;
    }

    public void MainMenu()
    {

        SceneManager.LoadScene("MainMenu");
    }
}
