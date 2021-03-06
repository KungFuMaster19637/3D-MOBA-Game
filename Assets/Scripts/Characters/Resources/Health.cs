using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health: MonoBehaviour
{
    //Reference to 3D healthbar in the game
    public Slider playerSlider3D;

    [Header ("Health text")]
    public TMP_Text currentHealthText;
    public TMP_Text maxHealthText;

    Slider playerSlider2D;

    PlayerStats statsScript;
    void Start()
    {
        statsScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

        playerSlider2D = GetComponent<Slider>();

        playerSlider2D.maxValue = statsScript.maxHealth;
        playerSlider3D.maxValue = statsScript.maxHealth;
        statsScript.health = statsScript.maxHealth;
        maxHealthText.text = statsScript.maxHealth.ToString();
    }

    void Update()
    {
        playerSlider2D.maxValue = statsScript.maxHealth;
        playerSlider3D.maxValue = statsScript.maxHealth;

        playerSlider2D.value = statsScript.health;
        playerSlider3D.value = playerSlider2D.value;

        currentHealthText.text = statsScript.health.ToString("0");
        maxHealthText.text = statsScript.maxHealth.ToString();

    }
}
