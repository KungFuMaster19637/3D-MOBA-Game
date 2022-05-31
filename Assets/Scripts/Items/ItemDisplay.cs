using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemDisplay : MonoBehaviour
{

    /*
    Index:
    HealthPotion
    ManaPotion
    StrengthPotion
    DefencePotion
    SpellPotion
    Wheat
    Iron
    */
    public int[] itemAmount = new int[12];
    [SerializeField] private TMP_Text[] itemAmountText;
    [SerializeField] private GameObject[] itemDisplay;

    private void Awake()
    {

        for (int i = 0; i < itemAmount.Length; i++)
        {
            itemAmount[i] = 0;
        }
        RefreshInventoryItems();
    }

    public void RefreshInventoryItems()
    {
        for (int i = 0; i < itemAmount.Length; i++)
        {
            if (itemAmount[i] == 0)
            {
                itemDisplay[i].SetActive(false);
            }
            else
            {
                itemDisplay[i].SetActive(true);
                itemAmountText[i].text = itemAmount[i].ToString();
            }
        }
    }

    public void AddItem(int index)
    {
        itemAmount[index]++;
        RefreshInventoryItems();
    }

    public void UseItem(int index)
    {
        itemAmount[index]--;
        RefreshInventoryItems();
    }
}
