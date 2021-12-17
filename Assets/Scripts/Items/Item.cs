using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemDisplay itemDisplay;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (this.name == "HealthPotion" && itemDisplay.itemAmount[0] < 9)
            {
                itemDisplay.AddItem(0);
                ItemPickedUp();
            }

            if (this.name == "ManaPotion" && itemDisplay.itemAmount[1] < 9)
            {
                itemDisplay.AddItem(1);
                ItemPickedUp();
            }

            if (this.name == "StrengthPotion" && itemDisplay.itemAmount[2] < 9)
            {
                itemDisplay.AddItem(2);
                ItemPickedUp();
            }

            if (this.name == "DefencePotion" && itemDisplay.itemAmount[3] < 9)
            {
                itemDisplay.AddItem(3);
                ItemPickedUp();
            }

            if (this.name == "SpellPotion" && itemDisplay.itemAmount[4] < 9)
            {
                itemDisplay.AddItem(4);
                ItemPickedUp();
            }

            if (this.name == "Wheat" && itemDisplay.itemAmount[5] < 99)
            {
                itemDisplay.AddItem(5);
                questGiver.WheatAcquired();
                ItemPickedUp();
            }

            if (this.name == "Iron" && itemDisplay.itemAmount[6] < 99)
            {
                itemDisplay.AddItem(6);
                questGiver.IronAcquired();
                ItemPickedUp();
            }

            if (this.name == "Fowl" && itemDisplay.itemAmount[7] < 9)
            {
                itemDisplay.AddItem(7);
                ItemPickedUp();
            }

            if (this.name == "MagicalDonut" && itemDisplay.itemAmount[8] < 9)
            {
                itemDisplay.AddItem(8);
                ItemPickedUp();
            }

            if (this.name == "RiskyPotion" && itemDisplay.itemAmount[9] < 9)
            {
                itemDisplay.AddItem(9);
                ItemPickedUp();
            }

            if (this.name == "RingOfMana" && itemDisplay.itemAmount[10] < 99)
            {
                itemDisplay.AddItem(10);
                ItemPickedUp();
            }

            if (this.name == "HolyOrb" && itemDisplay.itemAmount[11] < 99)
            {
                itemDisplay.AddItem(11);
                ItemPickedUp();
            }

        }
    }
}
