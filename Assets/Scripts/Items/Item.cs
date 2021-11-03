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
        Coin,
        Wheat,
        Iron
    }

    public ItemType itemType;
    public int amount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (this.name == "HealthPotion" && itemDisplay.itemAmount[0] < 9)
            {
                itemDisplay.AddItem(0);
                Destroy(gameObject);
            }

            if (this.name == "ManaPotion" && itemDisplay.itemAmount[1] < 9)
            {
                itemDisplay.AddItem(1);
                Destroy(gameObject);
            }

            if (this.name == "StrengthPotion" && itemDisplay.itemAmount[2] < 9)
            {
                itemDisplay.AddItem(2);
                Destroy(gameObject);
            }

            if (this.name == "DefencePotion" && itemDisplay.itemAmount[3] < 9)
            {
                itemDisplay.AddItem(3);
                Destroy(gameObject);
            }

            if (this.name == "SpellPotion" && itemDisplay.itemAmount[4] < 9)
            {
                itemDisplay.AddItem(4);
                Destroy(gameObject);
            }

            if (this.name == "Wheat" && itemDisplay.itemAmount[5] < 99)
            {
                itemDisplay.AddItem(5);
                questGiver.WheatAcquired();
                Destroy(gameObject);
            }

            if (this.name == "Iron" && itemDisplay.itemAmount[6] < 99)
            {
                itemDisplay.AddItem(6);
                questGiver.IronAcquired();
                Destroy(gameObject);
            }
        }
    }
}
