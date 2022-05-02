using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MerlinHoverUI : MonoBehaviour
{

    public TMP_Text abilityDescription;
    public GameObject descriptionUI;

    [Header("Merlin abilities")]
    public MerlinAbilities merlinAbilities;
    public MerlinAbilityManager abilityManager;

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
        passive = "Every 3rd ability, Merlin restores " + abilityManager.passiveHealAmount + " + " + statsScript.spellPower * abilityManager.passiveHealMultiplier + " health";
        ability1 = "Merlin gains " + abilityManager.attackBuff + " attack and makes his basic attacks bigger and faster for " + 
            abilityManager.totalDuration1 + " seconds";
        ability2 = "Merlin summons an energy field that deals " + abilityManager.energyDrainDamage + " + " + statsScript.spellPower * abilityManager.energyDrainDamageMultiplier + 
            " damage to all enemies in the area and healing himself for " + abilityManager.energyDrainHeal + " + " + statsScript.spellPower * abilityManager.energyDrainHealMultiplier;
        ability3 = "Merlin grows a cone of spike terrain dealing " + abilityManager.spikeTerrainDamage + " + " + 
            statsScript.spellPower * abilityManager.spikeTerrainMultiplier + " damage and stunning enemies in the cone for " + 
            abilityManager.spikeTerrainStunDuration + " seconds";
        ability4 = "Merlin casts the holy spell of Camelot, firing magic missiles from the skies dealing " + abilityManager.meteorShowerDamage + " + " +
            statsScript.spellPower * abilityManager.meteorShowerMultiplier + " damage per hit for 2 seconds around him";
    }
    public void OnMouseOver()
    {
        Debug.Log("hovering over");
        descriptionUI.SetActive(true);
        if (gameObject.name == "Passive")
        {
            abilityDescription.text = passive;
        }
        if (gameObject.name == "Ability1")
        {
            abilityDescription.text = ability1;
        }
        if (gameObject.name == "Ability2")
        {
            abilityDescription.text = ability2;
        }
        if (gameObject.name == "Ability3")
        {
            abilityDescription.text = ability3;
        }
        if (gameObject.name == "Ability4")
        {
            abilityDescription.text = ability4;
        }
    }

    public void OnMouseExit()
    {
        abilityDescription.text = "";
        descriptionUI.SetActive(false);
    }
}
