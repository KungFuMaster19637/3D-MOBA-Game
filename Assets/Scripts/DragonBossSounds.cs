using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBossSounds : MonoBehaviour
{

    [SerializeField] private AudioClip dragonAwake;
    [SerializeField] private AudioClip dragonAttack;
    [SerializeField] private AudioClip dragonFlame;
    [SerializeField] private AudioClip dragonDead;
    private AudioSource dragonAudio;
    //public AudioSource[] dragonAudio;
    private bool playAwakeOnce;
    private bool playDeadOnce;

    private void Start()
    {
        dragonAudio = GetComponent<AudioSource>();
        playAwakeOnce = true;
        playDeadOnce = true;
    }

    public IEnumerator PlayDragonAwake()
    {
        yield return new WaitForSeconds(0.5f);
        if (playAwakeOnce)
        {
            dragonAudio.PlayOneShot(dragonAwake);
            playAwakeOnce = false;
        }

        yield return null;
    }

    public IEnumerator PlayDragonAttack()
    {
        dragonAudio.PlayOneShot(dragonAttack);
        yield return null;
    }

    public IEnumerator PlayDragonFlame()
    {
        dragonAudio.PlayOneShot(dragonFlame);
        yield return null;
    }
    public IEnumerator PlayDragonDead()
    {
        if (playDeadOnce)
        {
            dragonAudio.PlayOneShot(dragonDead);
            playDeadOnce = false;
        }
        yield return null;
    }


}
