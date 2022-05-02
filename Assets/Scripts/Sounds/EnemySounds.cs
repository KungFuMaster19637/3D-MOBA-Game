using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    [SerializeField] private AudioClip enemyHit1;
    [SerializeField] private AudioClip enemyHit2;
    [SerializeField] private AudioClip enemyHit3;
    [SerializeField] private AudioClip enemyDie;

    private AudioSource enemyAudio;
    private bool playDeadOnce;



    // Start is called before the first frame update
    void Start()
    {
        enemyAudio = GetComponent<AudioSource>();
        playDeadOnce = true;

    }

    public IEnumerator PlayEnemyHit()
    {
        int randomSound = Random.Range(0, 3);
        switch(randomSound)
        {
            case 0: enemyAudio.PlayOneShot(enemyHit1);
                break;
            case 1: enemyAudio.PlayOneShot(enemyHit2);
                break;
            case 2: enemyAudio.PlayOneShot(enemyHit3);
                break;
            default:
                break;
        }
        yield return null;
    }

    public IEnumerator PlayEnemyDie()
    {
        if (playDeadOnce)
        {
            enemyAudio.PlayOneShot(enemyDie);
            playDeadOnce = false;
        }
        yield return null;
    }
}
