using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    public ItemDisplay itemDisplay;
    [SerializeField] private QuestGiver questGiver;
    public enum ItemType
    {
        HealthPotion,
        ManaPotion,
        StrengthPotion,
        DefencePotion,
        SpellPotion,
        Wheat,
        Iron,
        Fowl,
        MagicalDonut,
        RiskyPotion,
        RingOfMana,
        HolyOrb
    }

    public ItemType itemType;
    public int amount;
    public bool isPickedUp = false;

    private void ItemPickedUp()
    {
        isPickedUp = true;
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.transform.GetChild(0).transform.gameObject.SetActive(false);
    }

    private void ItemExperience(float experience)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<LevelUpStats>().SetExperience(experience);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (itemType == ItemType.HealthPotion && itemDisplay.itemAmount[0] < 9)
            {
                itemDisplay.AddItem(0);
                ItemExperience(2);
                ItemPickedUp();
            }

            if (itemType == ItemType.ManaPotion && itemDisplay.itemAmount[1] < 9)
            {
                itemDisplay.AddItem(1);
                ItemExperience(2);
                ItemPickedUp();
            }

            if (itemType == ItemType.StrengthPotion && itemDisplay.itemAmount[2] < 9)
            {
                itemDisplay.AddItem(2);
                ItemExperience(2);
                ItemPickedUp();
            }

            if (itemType == ItemType.DefencePotion && itemDisplay.itemAmount[3] < 9)
            {
                itemDisplay.AddItem(3);
                ItemExperience(2);
                ItemPickedUp();
            }

            if (itemType == ItemType.SpellPotion && itemDisplay.itemAmount[4] < 9)
            {
                itemDisplay.AddItem(4);
                ItemExperience(2);
                ItemPickedUp();
            }

            if (itemType == ItemType.Wheat && itemDisplay.itemAmount[5] < 99)
            {
                itemDisplay.AddItem(5);
                ItemExperience(2);
                questGiver.WheatAcquired();
                ItemPickedUp();
            }

            if (itemType == ItemType.Iron && itemDisplay.itemAmount[6] < 99)
            {
                itemDisplay.AddItem(6);
                ItemExperience(2);
                questGiver.IronAcquired();
                ItemPickedUp();
            }

            if (itemType == ItemType.Fowl && itemDisplay.itemAmount[7] < 9)
            {
                itemDisplay.AddItem(7);
                ItemExperience(2);
                ItemPickedUp();
            }

            if (itemType == ItemType.MagicalDonut && itemDisplay.itemAmount[8] < 9)
            {
                itemDisplay.AddItem(8);
                ItemExperience(5);
                ItemPickedUp();
            }

            if (itemType == ItemType.RiskyPotion && itemDisplay.itemAmount[9] < 9)
            {
                itemDisplay.AddItem(9);
                ItemExperience(5);
                ItemPickedUp();
            }

            if (itemType == ItemType.RingOfMana && itemDisplay.itemAmount[10] < 99)
            {
                itemDisplay.AddItem(10);
                ItemExperience(5);
                ItemPickedUp();
            }

            if (itemType == ItemType.HolyOrb && itemDisplay.itemAmount[11] < 99)
            {
                itemDisplay.AddItem(11);
                ItemExperience(10);
                ItemPickedUp();
            }

        }
    }
}
