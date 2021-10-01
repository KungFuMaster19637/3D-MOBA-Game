using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Mana : MonoBehaviour
{
    public TMP_Text currentManaText;
    public TMP_Text maxManaText;

    Slider playerSlider2D;

    PlayerStats statsScript;
    void Start()
    {
        statsScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        playerSlider2D = GetComponent<Slider>();

        playerSlider2D.maxValue = statsScript.maxMana;
        statsScript.health = statsScript.maxMana;
        maxManaText.text = statsScript.maxMana.ToString();
    }

    void Update()
    {
        playerSlider2D.value = statsScript.mana;

        currentManaText.text = statsScript.mana.ToString("0");
    }
}