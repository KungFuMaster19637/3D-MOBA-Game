using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUsage : MonoBehaviour
{
    [Header ("Display components")]
    public TMP_Text itemDescription;
    public GameObject descriptionUI;
    public GameObject itemDisplay;

    private PlayerStats playerStats;
    private ItemDisplay itemDisplayScript;

    private string healthPotionText;
    private string manaPotionText;
    private string strengthPotionText;
    private string defencePotionText;
    private string spellPotionText;
    private string wheatText;
    private string ironText;

    private int healthPotion = 50;
    private int manaPotion = 20;
    private int strengthPotion, defencePotion, spellPotion = 5;


    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        itemDisplayScript = itemDisplay.GetComponent<ItemDisplay>();

        healthPotionText = "Restores 50 health (Max 9 storage)";
        manaPotionText = "Restores 20 mana (Max 9 storage)";
        strengthPotionText = "Increase attack permanently by 5 (Max 9 storage)";
        defencePotionText = "Increase defence permanently by 5 (Max 9 storage)";
        spellPotionText = "Increase spell power permanently by 5 (Max 9 storage)";
        wheatText = "Some wheat";
        ironText = "Piece of iron chunk";
    }
    public void OnMouseOver()
    {
        descriptionUI.SetActive(true);
        if (gameObject.name == "HealthPotion")
        {
            itemDescription.text = healthPotionText;
        }
        if (gameObject.name == "ManaPotion")
        {
            itemDescription.text = manaPotionText;
        }
        if (gameObject.name == "StrengthPotion")
        {
            itemDescription.text = strengthPotionText;
        }
        if (gameObject.name == "DefencePotion")
        {
            itemDescription.text = defencePotionText;
        }
        if (gameObject.name == "SpellPotion")
        {
            itemDescription.text = spellPotionText;
        }
        if (gameObject.name == "Wheat")
        {
            itemDescription.text = wheatText;
        }
        if (gameObject.name == "Iron")
        {
            itemDescription.text = ironText;
        }
        //Work in progress
    }

    public void OnMouseExit()
    {
        itemDescription.text = "";
        descriptionUI.SetActive(false);
    }

    public void UseItemSlot()
    {
        if (gameObject.name == "HealthPotion" && itemDisplayScript.itemAmount[0] > 0)
        {
            if (playerStats.health < playerStats.maxHealth)
            {
                if (playerStats.health + healthPotion < playerStats.maxHealth)
                {
                    playerStats.health += healthPotion;
                }
                else if (playerStats.health + healthPotion > playerStats.maxHealth)
                {
                    playerStats.health = playerStats.maxHealth;
                }
                itemDisplayScript.UseItem(0);
            }
            else
            {
                //Fill in later with warning on screen
                Debug.Log("your hp bar is full");
            }

        }

        if (gameObject.name == "ManaPotion" && itemDisplayScript.itemAmount[1] > 0)
        {
            if (playerStats.mana < playerStats.maxMana)
            {
                if (playerStats.mana + manaPotion < playerStats.maxMana)
                {
                    playerStats.mana += manaPotion;
                }
                else if (playerStats.mana + manaPotion > playerStats.maxMana)
                {
                    playerStats.mana = playerStats.maxMana;
                }
                itemDisplayScript.UseItem(1);
            }
            else
            {
                Debug.Log("your mana bar is full");
            }
        }

        if (gameObject.name == "StrengthPotion" && itemDisplayScript.itemAmount[2] > 0)
        {
            Debug.Log("Drank Strength potion");
            playerStats.attackDamage += strengthPotion;

            itemDisplayScript.UseItem(2);
        }

        if (gameObject.name == "DefencePotion" && itemDisplayScript.itemAmount[3] > 0)
        {
            playerStats.defence += defencePotion;

            itemDisplayScript.UseItem(3);
        }

        if (gameObject.name == "SpellPotion" && itemDisplayScript.itemAmount[4] > 0)
        {
            playerStats.defence += spellPotion;

            itemDisplayScript.UseItem(4);
        }

    }
}
