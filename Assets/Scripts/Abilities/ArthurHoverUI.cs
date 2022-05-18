using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArthurHoverUI : MonoBehaviour
{

    public TMP_Text abilityDescription;
    public TMP_Text abilityName;
    public GameObject descriptionUI;

    [Header ("Arthur abilities")]
    public ArthurAbilities arthurAbilities;
    public ArthurAbilityManager abilityManager;

    private PlayerStats statsScript;

    private string passive;
    private string ability1;
    private string ability2;
    private string ability3;
    private string ability4;

    private void Start()
    {
        statsScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    void Update()
    {
        passive = "Every 2nd hit, Arthur gains " + abilityManager.healthGain + " health and " + abilityManager.manaGain + " mana";
        ability1 = "Arthur charges forward increasing his movement speed by " + abilityManager.movementSpeedBuff +
            " and his attack by " + abilityManager.attackBuff + " + " + (statsScript.attackDamage * 0.1f) + " for " + abilityManager.totalDuration1 + " seconds";
        ability2 = "Arthur heals all his allies in range by " + abilityManager.healAmount + " + " + (statsScript.spellPower * 0.5f) +
           " healthpoints, and increases his own health regeneration by " + abilityManager.regenerationBuff +
           " for " + abilityManager.totalDuration2 + " seconds";
        ability3 = "Arthur blocks all incoming damage for " + abilityManager.totalDuration3 + " seconds";
        ability4 = "Arthur calls out Excalibur for 2 seconds, and gains " + (abilityManager.attackSpeedBuff - 1) * 100 +
            "% attack speed and increases range by " + abilityManager.attackRangeBuff + " for " +
            abilityManager.totalDuration4 + " seconds";
    }
    public void OnMouseOver()
    {
        descriptionUI.SetActive(true);
        if (gameObject.name == "Passive")
        {
            abilityDescription.text = passive;
            abilityName.text = "Divine Blessing";
        }
        if (gameObject.name == "Ability1")
        {
            abilityDescription.text = ability1;
            abilityName.text = "Charge Forward";
        }
        if (gameObject.name == "Ability2")
        {
            abilityDescription.text = ability2;
            abilityName.text = "Lay On Hands";
        }
        if (gameObject.name == "Ability3")
        {
            abilityDescription.text = ability3;
            abilityName.text = "Pridwen";
        }
        if (gameObject.name == "Ability4")
        {
            abilityDescription.text = ability4;
            abilityName.text = "Excalibur";
        }
    }

    public void OnMouseExit()
    {
        abilityDescription.text = "";
        descriptionUI.SetActive(false);
    }
}
