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
    private string fowlText;
    private string donutText;
    private string riskyPotionText;
    private string ringOfManaText;
    private string holyOrbText;

    //Potion Buffs
    private int healthPotion = 100;
    private int manaPotion = 60;
    private int strengthPotion = 10 , defencePotion = 10, spellPotion = 5;
    private int fowlBoost = 150;
    private int donutBoost = 50;
    private int riskyPotionAttack = 60;
    private int riskyPotionDefence = 30;
    private int riskyPotionSpellPower = 100;
    private int ringOfManaBoost = 10;



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
        fowlText = "Some fowl to fill the stomach. Raises maximum health by 150";
        donutText = "A magical donut with a special effect. Raises maximum mana by 50";
        riskyPotionText = "What a weird smell, I wonder what effects it will give?";
        ringOfManaText = "An odd ring. Increase mana regeneration permanently by 10";
        holyOrbText = "An orb to open the door to the Boss room";
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
        if (gameObject.name == "Fowl")
        {
            itemDescription.text = fowlText;
        }
        if (gameObject.name == "MagicalDonut")
        {
            itemDescription.text = donutText;
        }
        if (gameObject.name == "RiskyPotion")
        {
            itemDescription.text = riskyPotionText;
        }
        if (gameObject.name == "RingOfMana")
        {
            itemDescription.text = ringOfManaText;
        }
        if (gameObject.name == "HolyOrb")
        {
            itemDescription.text = holyOrbText;
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
            playerStats.attackDamage += strengthPotion;
            Debug.Log("Drank Strength potion");

            itemDisplayScript.UseItem(2);
        }

        if (gameObject.name == "DefencePotion" && itemDisplayScript.itemAmount[3] > 0)
        {
            playerStats.defence += defencePotion;

            itemDisplayScript.UseItem(3);
        }

        if (gameObject.name == "SpellPotion" && itemDisplayScript.itemAmount[4] > 0)
        {
            playerStats.spellPower += spellPotion;

            itemDisplayScript.UseItem(4);
        }
        if (gameObject.name == "Fowl" && itemDisplayScript.itemAmount[7] > 0)
        {
            playerStats.maxHealth += fowlBoost;
            playerStats.health += fowlBoost;

            itemDisplayScript.UseItem(7);
        }
        if (gameObject.name == "MagicalDonut" && itemDisplayScript.itemAmount[8] > 0)
        {
            playerStats.maxMana += donutBoost;
            playerStats.mana += donutBoost;

            itemDisplayScript.UseItem(8);
        }
        if (gameObject.name == "RiskyPotion" && itemDisplayScript.itemAmount[9] > 0)
        {
            playerStats.attackDamage += riskyPotionAttack;
            playerStats.defence -= riskyPotionDefence;
            playerStats.spellPower += riskyPotionSpellPower;

            itemDisplayScript.UseItem(9);
        }
        if (gameObject.name == "RingOfMana" && itemDisplayScript.itemAmount[10] > 0)
        {
            playerStats.manaRegeneration += ringOfManaBoost;

            itemDisplayScript.UseItem(10);
        }

    }
}
