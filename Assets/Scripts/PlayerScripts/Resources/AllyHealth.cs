using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllyHealth : MonoBehaviour
{
    public Slider playerSlider3D;

    PlayerStats statsScript;
    void Start()
    {
        statsScript = GameObject.FindGameObjectWithTag("Ally").GetComponent<PlayerStats>();

        playerSlider3D.maxValue = statsScript.maxHealth;
        //statsScript.health = statsScript.maxHealth;
    }

    void Update()
    {
        playerSlider3D.value = statsScript.health;
    }
}
