using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExperienceEffect : MonoBehaviour
{
    [SerializeField] private TMP_Text experienceText;
    public GameObject experienceEffect;
    public void PlayExperienceEffect(float amount)
    {
        experienceText.text = "+ " + amount.ToString();
        experienceEffect.SetActive(true);
        StartCoroutine(PlayEffectForSeconds());
    }

    private IEnumerator PlayEffectForSeconds()
    {
        yield return new WaitForSeconds(1f);
        experienceEffect.SetActive(false);
    }
}
