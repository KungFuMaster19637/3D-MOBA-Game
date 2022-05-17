using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] idleSounds;
    [SerializeField] private AudioClip[] normalAttackSounds;
    [SerializeField] private AudioClip[] ability1Sounds;
    [SerializeField] private AudioClip[] ability2Sounds;
    [SerializeField] private AudioClip[] ability3Sounds;
    [SerializeField] private AudioClip[] ability4Sounds;

    private AudioSource playerAudio;
    private Coroutine soundLock;


    /*
    Sound Odds:
    Idle: 100% (press button)
    Normal attack: 50% (2)
    Abilities: 100%  (1)

    Ability index:
    0 -> 5
    */
    private void Start()
    {
        playerAudio = GetComponent<AudioSource>();

    }

    #region Idle Sounds
    public void IdleSoundPlayed()
    {
        if (soundLock != null) { return; }
        soundLock = StartCoroutine(PlayIdleSounds());
    }
    public IEnumerator PlayIdleSounds()
    {

        int randomSound = Random.Range(0, idleSounds.Length);
        playerAudio.PlayOneShot(idleSounds[randomSound]);
        yield return new WaitWhile(() => playerAudio.isPlaying);
        soundLock = null;
    }
    #endregion 

    #region Attack Sounds
    public void AbilitySoundPlayed(int odds, int abilityIndex)
    {
        if (soundLock != null) { return; }
        soundLock = StartCoroutine(PlayAttackSounds(odds, abilityIndex));
    }
    public IEnumerator PlayAttackSounds(int odds, int abilityIndex)
    {
        //If the odds failed, return
        int oddsOfSound = Random.Range(0, odds);
        Debug.Log(oddsOfSound);
        if (oddsOfSound == 0)
        {
            int randomSound = 0;
            switch (abilityIndex)
            {
                case 0:
                    randomSound = Random.Range(0, normalAttackSounds.Length);
                    Debug.Log(randomSound);

                    playerAudio.PlayOneShot(normalAttackSounds[randomSound]);
                    break;
                case 1:
                    randomSound = Random.Range(0, ability1Sounds.Length);
                    playerAudio.PlayOneShot(ability1Sounds[randomSound]);
                    break;
                case 2:
                    randomSound = Random.Range(0, ability2Sounds.Length);
                    playerAudio.PlayOneShot(ability2Sounds[randomSound]);
                    break;
                case 3:
                    randomSound = Random.Range(0, ability3Sounds.Length);
                    playerAudio.PlayOneShot(ability3Sounds[randomSound]);
                    break;
                case 4:
                    Debug.Log("here");
                    randomSound = Random.Range(0, ability4Sounds.Length);
                    playerAudio.PlayOneShot(ability4Sounds[randomSound]);
                    break;
                default:
                    randomSound = 0;
                    playerAudio.Stop();
                    break;
            }
            yield return new WaitWhile(() => playerAudio.isPlaying);
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
        }
        soundLock = null;
    }
    #endregion

    public void StopSound()
    {
        playerAudio.Stop();
    }
}
