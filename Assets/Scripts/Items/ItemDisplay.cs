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
    StregnthPotion
     
    */
    public int[] itemAmount = new int[12];
    [SerializeField] private TMP_Text[] itemAmountText;

    private void Awake()
    {

        for (int i = 0; i < itemAmount.Length; i++)
        {
            itemAmount[i] = 0;
        }
    }

    protected void RefreshInventoryItems()
    {
        for (int i = 0; i < itemAmount.Length; i++)
        {
            itemAmountText[i].text = itemAmount[i].ToString();
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
