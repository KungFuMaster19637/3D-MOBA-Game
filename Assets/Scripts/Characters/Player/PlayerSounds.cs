using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] idleSounds;
    [SerializeField] private AudioClip[] shopSounds;
    [SerializeField] private AudioClip[] normalAttackSounds;
    [SerializeField] private AudioClip[] ability1Sounds;
    [SerializeField] private AudioClip[] ability2Sounds;
    [SerializeField] private AudioClip[] ability3Sounds;
    [SerializeField] private AudioClip[] ability4Sounds;
    [SerializeField] private AudioClip deathSound;


    private AudioSource playerAudio;
    private Coroutine soundLock;

    /*
    Sound Odds:
    Idle: 100% (press button)
    Normal attack: 50% (2)
    Abilities: 100%  (1)

    ******
    Arthur Voicelines:

    Idle:
    Excalibur is a greatsword. It is also a great sword! Haha!
    Even a god can not stand between a king and the safety of his kingdom!
    Hahahahahaha!
    When trying to find the right sword, I always suggest checking your nearest lake. The customer service is unrivaled.
    I recognize a legendary artifact when I see it.

    Shop:
    Impressive magics. Maybe Merlin's creations?
    Preparations makes a leader!

    Normal Attacks:
    Hwa Hiya
    Huh Ha

    Ability 1:
    Advance!
    Strike!
    To Victory!

    Ability 2:
    You're welcome. 
    They cannot win!
    May fate be on our side!

    Ability 3:
    Fate protects me!
    I will not allow it!
    Stand aside.

    Ability 4:
    Excalibur!
    For Camelot!
    Destiny Awaits!

    Death:
    Can not fail again!

    ******
    Merlin Voicelines:

    Idle: 
    Was there any doubt as to my power?
    You do realize who I am? The mastery of magic, the air of utmost confidence. Still no? How boorishly ignorant.
    Heightened attunement from more than three schools of magic is impossible. Impossible I say!
    Hahahahaha
    Am I not just magnificent?

    Shop:
    One of these, and one of those.
    Artifacts of immeasurable value.

    Normal Attacks:
    Hiyaah!
    Hueyaah!

    Ability 1:
    Savor the warmth!
    Trial by fire!
    The arcane arts inspire burning ambition.

    Ability 2:
    Expending any effort on you would be such a waste!
    I will not hold back!
    Enemies would be so bold.

    Ability 3:
    How humiliating.
    Timing is critical.
    Beware!

    Ability 4:
    Heed the fury of a master wizard!
    My power cannot be contained!
    Observe true power!

    Death:
    I just needed to cast one more spell...

    Ability index:
    0 -> 5
    */
    private void Start()
    {
        playerAudio = GetComponent<AudioSource>();

    }

    #region Death Sound
    public void DeathSoundPlayed()
    {
        if (soundLock != null) { return; }
        soundLock = StartCoroutine(PlayDeathSound());
    }
    public IEnumerator PlayDeathSound()
    {
        Debug.Log("playing");
        playerAudio.PlayOneShot(deathSound);
        yield return new WaitWhile(() => playerAudio.isPlaying);
        soundLock = null;
    }
    #endregion

    #region Shop Sound
    public void ShopSoundPlayed()
    {
        if (soundLock != null) { return; }
        soundLock = StartCoroutine(PlayShopSounds());
    }

    public IEnumerator PlayShopSounds()
    {
        int randomSound = Random.Range(0, shopSounds.Length);
        playerAudio.PlayOneShot(shopSounds[randomSound]);
        yield return new WaitWhile(() => playerAudio.isPlaying);
        soundLock = null;
    }
    #endregion

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
        soundLock = StartCoroutine(PlayAbilitySounds(odds, abilityIndex));
    }
    public IEnumerator PlayAbilitySounds(int odds, int abilityIndex)
    {
        //If the odds failed, return
        int oddsOfSound = Random.Range(0, odds);
        if (oddsOfSound == 0)
        {
            int randomSound = 0;
            switch (abilityIndex)
            {
                case 0:
                    randomSound = Random.Range(0, normalAttackSounds.Length);
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
