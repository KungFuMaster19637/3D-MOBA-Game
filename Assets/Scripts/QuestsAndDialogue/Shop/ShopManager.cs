using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShopManager : MonoBehaviour
{

    public ItemDisplay itemDisplay;
    [SerializeField] private GameObject blockInventory;

    private int healthPotionPrice;
    private int manaPotionPrice;

    private NavMeshAgent agent;
    void Start()
    {
        healthPotionPrice = 50;
        manaPotionPrice = 100;

        agent = GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        
    }

    public void Shopping()
    {
        agent.enabled = false;
        blockInventory.SetActive(true);
    }

    public void NotShopping()
    {
        agent.enabled = true;
        blockInventory.SetActive(false);
    }

    public void BuyHealthPotion()
    {
        if (MoneyDisplay.moneyAmount >= healthPotionPrice)
        {
            itemDisplay.AddItem(0);
            MoneyDisplay.UseMoney(healthPotionPrice);
        }

    }
    public void BuyManaPotion()
    {
        if (MoneyDisplay.moneyAmount >= manaPotionPrice)
        {
            itemDisplay.AddItem(1);
            MoneyDisplay.UseMoney(manaPotionPrice);
        }
    }
    //public void BuyHealthPotion()
    //{

    //}
    //public void BuyHealthPotion()
    //{

    //}

}
