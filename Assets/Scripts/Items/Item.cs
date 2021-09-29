using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item : MonoBehaviour
{
    [SerializeField] private ItemDisplay itemDisplay;
    public enum ItemType
    {
        Sword,
        HealthPotion,
        ManaPotion,
        StrengthPotion,
        Coin,
        Medkit,
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
        }
    }
}
