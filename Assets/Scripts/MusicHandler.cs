using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] private Teleporter teleporter;
    [SerializeField] private AudioSource camelotMusic;
    [SerializeField] private AudioSource wildMusic;

    private bool isPlayingCamelot;
    private bool isPlayingWild;
    private void Update()
    {
        Debug.Log(teleporter.isInCity);
        if (teleporter.isInCity)
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

        if (!teleporter.isInCity)
        {
            if (camelotMusic.isPlaying && isPlayingCamelot)
            {
                StartCoroutine(FadeOut(camelotMusic, 2f));
                isPlayingCamelot = false;
            }
            if (!isPlayingWild)
            {
                StartCoroutine(PlayWildMusic());
            }
        }


    }

    private IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
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
        isPlayingCamelot = false;

        yield return null;
    }
}
