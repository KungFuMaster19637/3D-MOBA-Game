using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsHover : MonoBehaviour
{
    public TMP_Text statsDescription;
    public GameObject descriptionUI;

    private PlayerStats playerStats;

    private string attackHover;
    private string spellHover;
    private string defenceHover;

    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        descriptionUI.SetActive(false);
    }

    private void Update()
    {
        attackHover = "Attack: " + playerStats.attackDamage + " \n" + "Attack Speed: " + playerStats.attackSpeed;
        defenceHover = "Defence: " + playerStats.defence;
        spellHover = "Spell Power: " + playerStats.spellPower;
    }

    public void OnMouseOver()
    {
        descriptionUI.SetActive(true);
        if (gameObject.name == "AttackHover")
        {
            statsDescription.text = attackHover;
        }
        if (gameObject.name == "DefenceHover")
        {
            statsDescription.text = defenceHover;
        }
        if (gameObject.name == "SpellHover")
        {
            statsDescription.text = spellHover;
        }
    }

    public void OnMouseExit()
    {
        statsDescription.text = "";
        descriptionUI.SetActive(false);
    }
}
