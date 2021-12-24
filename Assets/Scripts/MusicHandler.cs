using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] private Teleporter teleporter;
    [SerializeField] private Teleporter2 teleporter2;
    [SerializeField] private AudioSource camelotMusic;
    [SerializeField] private AudioSource wildMusic;
    [SerializeField] private AudioSource caveMusic;
    [SerializeField] private GameOver gameOver;

    private bool isPlayingCamelot;
    private bool isPlayingWild;
    private bool isPlayingCave;

    private void Start()
    {
        StartCoroutine(PlayCamelotMusic());
    }
    private void Update()
    {

        //if player is dead, fade out music
        if (gameOver.gameIsOver)
        {
            if (teleporter.isInCastle)
            {
                StartCoroutine(FadeOut(camelotMusic, 2f));
            }

            if (!teleporter.isInCastle)
            {
                StartCoroutine(FadeOut(wildMusic, 2f));
            }
        }

        if (teleporter.isInCastle)
        {
            if (wildMusic.isPlaying && isPlayingWild)
            {
                StartCoroutine(FadeOut(wildMusic, 2f));
                isPlayingWild = false;
            }
            if (!isPlayingCamelot)
            {
                StartCoroutine(PlayCamelotMusic());
            }
        }

        if (!teleporter.isInCastle && !teleporter2.isInCave)
        {
            if (camelotMusic.isPlaying && isPlayingCamelot)
            {
                StartCoroutine(FadeOut(camelotMusic, 2f));
                isPlayingCamelot = false;
            }
            if (caveMusic.isPlaying && isPlayingCave)
            {
                StartCoroutine(FadeOut(caveMusic, 2f));
                isPlayingCave = false;
            }
            if (!isPlayingWild)
            {
                StartCoroutine(PlayWildMusic());
            }
        }

        if (teleporter2.isInCave)
        {
            if (wildMusic.isPlaying && isPlayingWild)
            {
                StartCoroutine(FadeOut(wildMusic, 2f));
                isPlayingWild = false;
            }
            if (!isPlayingCave)
            {
                StartCoroutine(PlayCaveMusic());
            }
        }
    }

    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    private IEnumerator PlayCamelotMusic()
    {
        yield return new WaitForSeconds(1f);

        camelotMusic.Play();
        isPlayingWild = false;
        isPlayingCamelot = true;

        yield return null;
    }
    private IEnumerator PlayWildMusic()
    {
        yield return new WaitForSeconds(1f);

        wildMusic.Play();
        isPlayingWild = true;
        isPlayingCave = false;
        isPlayingCamelot = false;

        yield return null;
    }
    private IEnumerator PlayCaveMusic()
    {
        yield return new WaitForSeconds(1f);

        caveMusic.Play();
        isPlayingWild = false;
        isPlayingCave = true;

        yield return null;
    }
}
